using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeProjects.Abstractions.Services
{
    /// <summary>
    /// Represents a generic service with a description.
    /// </summary>
    /// <remarks>This interface defines a contract for services that provide a textual description.
    /// Implementing types should ensure the <see cref="Description"/> property is properly initialized and provides
    /// meaningful information about the service.</remarks>
    public interface IService
    {
        /// <summary>
        /// Gets the description of the service.
        /// </summary>
        string Description { get; }
    }
}
