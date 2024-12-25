using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_BusinessObjects.Common.Exceptions
{
    public class ValidationException : Exception
    {
        private const string DefaultMessage = "One or more validation failures have occured";
        private IDictionary<string, string[]> Errors { get; }

        public ValidationException() : base(DefaultMessage)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
    }
}
