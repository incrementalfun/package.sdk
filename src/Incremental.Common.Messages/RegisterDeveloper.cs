using System;
using System.Collections.Generic;
using Incremental.Common.Queue.Message.Contract;

namespace Incremental.Common.Messages
{
    /// <summary>
    /// Message for registering a new developer.
    /// </summary>
    public class RegisterDeveloper : IMessage
    {
        /// <summary>
        /// Id of the developer.
        /// </summary>
        public Guid DeveloperId { get; set; }
        
        /// <summary>
        /// Name of the developer.
        /// </summary>
        public string Name { get; init; }
        
        /// <summary>
        /// Related links of the developer.
        /// </summary>
        public Dictionary<string, string> Links { get; init; }
        
        /// <summary>
        /// Id of the profile responsible for the action.
        /// </summary>
        public Guid ProfileId { get; init; }

        /// <inheritdoc />
        public (string queue, string id) Receipt { get; set; }
    }
}