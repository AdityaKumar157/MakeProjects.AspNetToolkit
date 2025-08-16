using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeProjects.Abstractions.Exceptions
{
    public class Exceptions
    {
        /// <summary>
        /// Represents an error when an entity is not found.
        /// </summary>
        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message) { }

            public NotFoundException(string entityName, Guid id)
            : base($"{entityName} with Id {id} was not found.") { }
        }

        public class BadRequestException : Exception
        {
            public BadRequestException(string message) : base(message) { }
        }

        public class ConflictException : Exception
        {
            public ConflictException(string message) : base(message) { }
        }

        /// <summary>
        /// Represents errors that occur in the domain/business logic layer.
        /// </summary>
        public class DomainException : Exception
        {
            public DomainException() { }

            public DomainException(string message) : base(message) { }

            public DomainException(string message, Exception innerException)
                : base(message, innerException) { }
        }

        /// <summary>
        /// Represents errors due to invalid input or data validation failures.
        /// </summary>
        public class ValidationException : Exception
        {
            public IEnumerable<string> Errors { get; }

            public ValidationException(IEnumerable<string> errors)
                : base("One or more validation errors occurred.")
            {
                Errors = errors;
            }
        }
    }
}
