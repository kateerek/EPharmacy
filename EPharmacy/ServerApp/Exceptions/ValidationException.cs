using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EPharmacy.ServerApp.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Failures { get; } = new Dictionary<string, string[]>();
        public ValidationException(Exception innerException = null)
            : base("One or more validation failures have occurred.", innerException)
        { }

        public ValidationException(List<ValidationFailure> failures, Exception innerException = null)
            : this(innerException)
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }
    }
}
