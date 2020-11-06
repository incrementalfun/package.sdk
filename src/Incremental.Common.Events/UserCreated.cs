using System;
using Incremental.Common.Sourcing.Events.Contract;

namespace Incremental.Common.Events
{
    /// <summary>
    /// Represents a new user account created.
    /// </summary>
    public class UserCreated : IExternalEvent
    {
        /// <summary>
        /// Id of the new user account.
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Username of the new user account.
        /// </summary>
        public string Username { get; set; }
    }
}