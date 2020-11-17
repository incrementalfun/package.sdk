using System;
using Incremental.Common.Queue.Message.Contract;

namespace Incremental.Common.Messages
{
    /// <summary>
    /// Message for registering a link for a game.
    /// </summary>
    public class RegisterGameLink : IMessage
    {
        /// <summary>
        /// Id of the game.
        /// </summary>
        public Guid GameId { get; init; }
        
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