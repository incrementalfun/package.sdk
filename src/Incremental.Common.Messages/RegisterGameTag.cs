using System;
using Incremental.Common.Queue.Message.Contract;

namespace Incremental.Common.Messages
{
    /// <summary>
    /// Message for registering a tag for a game.
    /// </summary>
    public class RegisterGameTag : IMessage
    {
        /// <summary>
        /// Id of the game.
        /// </summary>
        public Guid GameId { get; init; }
        
        /// <summary>
        /// Name of the tag.
        /// </summary>
        public string Tag { get; init; }
        
        /// <summary>
        /// Id of the profile responsible for the action.
        /// </summary>
        public Guid ProfileId { get; init; }
        
        /// <inheritdoc />
        public (string queue, string id) Receipt { get; set; }
    }
}