using System;
using Incremental.Common.Queue.Message.Contract;

namespace Incremental.Common.Messages
{
    /// <summary>
    /// Message representing a new user account.
    /// </summary>
    public class UserCreated : IMessage
    {
        /// <summary>
        /// Id of the new user account.
        /// </summary>
        public Guid UserId { get; set; }
        
        /// <summary>
        /// Username of the new user account.
        /// </summary>
        public string Username { get; set; }

        /// <inheritdoc />
        public (string queue, string id) Receipt { get; set; }
    }
    
        
}