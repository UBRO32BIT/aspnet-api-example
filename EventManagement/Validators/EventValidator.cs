using EventManagement_Services.DTOs.Event;
using FluentValidation;

namespace EventManagementAPI.Validators
{
    public class EventValidator : AbstractValidator<CreateEventRequestDTO>
    {
        public EventValidator() {
            RuleFor(model => model.HostedAt)
                .NotNull().WithMessage("Event name cannot be empty");
            RuleFor(model => model.Name)
                .NotEmpty().WithMessage("Name cannot be null")
                .MaximumLength(100).WithMessage("Event name cannot exceed 100 characters");
            RuleFor(e => e.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(3000).WithMessage("Event description cannot exceed 3000 characters");
            RuleFor(e => e.HostedAt)
                .GreaterThan(DateTime.Now).WithMessage("Hosted date must be in the future");
            RuleFor(e => e.EndDate)
                .GreaterThan(e => e.HostedAt).When(e => e.EndDate.HasValue)
                .WithMessage("End date must be later than the hosted date");
            RuleFor(e => e.Slots)
                .GreaterThan(0).WithMessage("Slots must be greater than 0");
        }
    }
}
