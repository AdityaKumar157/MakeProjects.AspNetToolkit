using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeProjects.AspNetToolkit.Abstractions.Exceptions
{
    /// <summary>
    /// Class containing custom exceptions for the application.
    /// </summary>
    public class Exceptions
    {
        /// <summary>
        /// Represents an error when an entity is not found.
        /// </summary>
        public class NotFoundException : Exception
        {
            /// <summary>
            /// Constructs a new instance of the <see cref="NotFoundException"/> class with a specified error message.
            /// </summary>
            /// <param name="message"></param>
            public NotFoundException(string message) : base(message) { }

            /// <summary>
            /// Constructs a new instance of the <see cref="NotFoundException"/> class with a specified entity name and ID.
            /// </summary>
            /// <param name="entityName"></param>
            /// <param name="id"></param>
            public NotFoundException(string entityName, Guid id)
            : base($"{entityName} with Id {id} was not found.") { }
        }

        /// <summary>
        /// Represents an error when a request is invalid or cannot be processed.
        /// </summary>
        public class BadRequestException : Exception
        {
            /// <summary>
            /// Constructs a new instance of the <see cref="BadRequestException"/> class with a specified error message.
            /// </summary>
            /// <param name="message"></param>
            public BadRequestException(string message) : base(message) { }
        }

        /// <summary>
        /// Represents an error when a conflict occurs, such as when trying to create a resource that already exists.
        /// </summary>
        public class ConflictException : Exception
        {
            /// <summary>
            /// Constructs a new instance of the <see cref="ConflictException"/> class with a specified error message.
            /// </summary>
            /// <param name="message"></param>
            public ConflictException(string message) : base(message) { }
        }

        /// <summary>
        /// Represents errors that occur in the domain/business logic layer.
        /// </summary>
        public class DomainException : Exception
        {
            /// <summary>
            /// Constructs a new instance of the <see cref="DomainException"/> class.
            /// </summary>
            public DomainException() { }

            /// <summary>
            /// Constructs a new instance of the <see cref="DomainException"/> class with a specified error message.
            /// </summary>
            /// <param name="message"></param>
            public DomainException(string message) : base(message) { }

            /// <summary>
            /// Constructs a new instance of the <see cref="DomainException"/> class with a specified error message and an inner exception.
            /// </summary>
            /// <param name="message"></param>
            /// <param name="innerException"></param>
            public DomainException(string message, Exception innerException)
                : base(message, innerException) { }
        }

        /// <summary>
        /// Represents errors due to invalid input or data validation failures.
        /// </summary>
        public class ValidationException : Exception
        {
            /// <summary>
            /// Collection of validation error messages.
            /// </summary>
            public IEnumerable<string> Errors { get; }

            /// <summary>
            /// Constructs a new instance of the <see cref="ValidationException"/> class with a specified error message.
            /// </summary>
            /// <param name="errors"></param>
            public ValidationException(IEnumerable<string> errors)
                : base("One or more validation errors occurred.")
            {
                Errors = errors;
            }
        }
    }
}
