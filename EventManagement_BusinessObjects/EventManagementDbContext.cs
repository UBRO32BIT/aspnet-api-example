using EventManagement_BusinessObjects.Common;
using EventManagement_BusinessObjects.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


namespace EventManagement_BusinessObjects
{
    public partial class EventManagementDbContext : IdentityDbContext<ApplicationUser>
    {
        public EventManagementDbContext(DbContextOptions<EventManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public DbSet<Event> Events { get; set; }
        public DbSet<TicketGroup> TicketGroups { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<EventInvitation> EventInvitations { get; set; }
        public DbSet<AuditEntry> AuditEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seeding a  'Administrator' role to AspNetRoles table
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Administrator", NormalizedName = "ADMINISTRATOR".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "dd779af5-4c29-40de-a284-4e02b54757f0", Name = "User", NormalizedName = "USER".ToUpper() });


            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<ApplicationUser>();


            //Seeding the User to AspNetUsers table
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.AuthenticationType)
                      .HasConversion<int>()
                      .IsRequired();
                entity.HasData(
                    new ApplicationUser
                    {
                        Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                        UserName = "admin",
                        NormalizedUserName = "SUPERADMIN",
                        Email = "admin@32mine.net",
                        PasswordHash = hasher.HashPassword(null, "Pa$$w0rd"),
                        AuthenticationType = Enum.AuthenticationType.Local
                    }
                );
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                }
            );

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Owner)
                    .WithMany()
                    .HasForeignKey(e => e.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.OwnerId)
                      .HasMaxLength(450);
                entity.Property(e => e.Description)
                      .IsRequired();
                entity.Property(e => e.isPublic)
                      .HasDefaultValue(false);
            });
            modelBuilder.Entity<EventInvitation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(i => i.Invitor).WithMany().HasForeignKey(i => i.InvitorId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(i => i.Event).WithMany().HasForeignKey(i => i.EventId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<TicketGroup>(entity =>
            {
                entity.HasKey(g => g.Id);
            });
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(t =>  t.Id);
                entity.HasOne(t => t.Owner).WithMany().HasForeignKey(t => t.OwnerId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(t => t.TicketGroup).WithMany().HasForeignKey(t => t.TicketGroupId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(t => t.Event).WithMany().HasForeignKey(t => t.EventId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<AuditEntry>().Property(ae => ae.Changes).HasConversion(
        value => JsonSerializer.Serialize(value, (JsonSerializerOptions)null),
        serializedValue => JsonSerializer.Deserialize<Dictionary<string, object>>(serializedValue, (JsonSerializerOptions)null));
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.IsDeleted = false;
                }

                entity.UpdatedAt = DateTime.UtcNow;
            }

            var auditEntries = OnBeforeSaveChanges();
            var result = base.SaveChanges();
            OnAfterSaveChanges(auditEntries);
            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.IsDeleted = false;
                }

                entity.UpdatedAt = DateTime.UtcNow;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var entries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                // Dot not audit entities that are not tracked, not changed, or not of type IAuditable
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged || !(entry.Entity is IAuditable))
                    continue;

                var auditEntry = new AuditEntry
                {
                    ActionType = entry.State == EntityState.Added ? "INSERT" : entry.State == EntityState.Deleted ? "DELETE" : "UPDATE",
                    EntityId = entry.Properties.Single(p => p.Metadata.IsPrimaryKey()).CurrentValue.ToString(),
                    EntityName = entry.Metadata.ClrType.Name,
                    Username = "",
                    TimeStamp = DateTime.UtcNow,
                    Changes = entry.Properties.Select(p => new { p.Metadata.Name, p.CurrentValue }).ToDictionary(i => i.Name, i => i.CurrentValue),

                    // TempProperties are properties that are only generated on save, e.g. ID's
                    // These properties will be set correctly after the audited entity has been saved
                    TempProperties = entry.Properties.Where(p => p.IsTemporary).ToList(),
                };

                entries.Add(auditEntry);
            }

            return entries;
        }

        private void OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return;

            // For each temporary property in each audit entry - update the value in the audit entry to the actual (generated) value
            foreach (var entry in auditEntries)
            {
                foreach (var prop in entry.TempProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        entry.EntityId = prop.CurrentValue.ToString();
                        entry.Changes[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        entry.Changes[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }
            }

            AuditEntries.AddRange(auditEntries);
            SaveChanges();
        }
    }
}
