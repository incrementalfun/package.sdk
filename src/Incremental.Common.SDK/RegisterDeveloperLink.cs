using System;
using Incremental.Common.Queue.Message.Contract;

namespace Incremental.Common.SDK
{
    /// <summary>
    /// Message for registering a link for a developer.
    /// </summary>
    public class RegisterDeveloperLink : IMessage
    {
        /// <summary>
        /// Id of the developer.
        /// </summary>
        public Guid DeveloperId { get; init; }
        
        /// <summary>
        /// Type of the link.
        /// </summary>
        public string Type { get; init; }
        
        /// <summary>
        /// Value of the link.
        /// </summary>
        public string Value { get; init; }
        
        /// <summary>
        /// Id of the profile responsible for the action.
        /// </summary>
        public Guid ProfileId { get; init; }

        /// <inheritdoc />
        public (string queue, string id) Receipt { get; set; }
    }
}