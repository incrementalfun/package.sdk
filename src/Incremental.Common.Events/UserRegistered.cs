using System;

namespace Incremental.Common.Events
{
    /// <summary>
    /// Represents a new user account created.
    /// </summary>
    public class UserCreated
    {
        /// <summary>
        /// Id of the new user account.
        /// </summary>
        public Guid Id { get; set; }
    }
}