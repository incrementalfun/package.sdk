using System;
using Incremental.Common.Queue.Message.Contract;

namespace Incremental.Common.SDK
{
    /// <summary>
    /// Message for registering a developer for a game.
    /// </summary>
    public class RegisterGameDeveloper : IMessage
    {
        /// <summary>
        /// Id of the game.
        /// </summary>
        public Guid GameId { get; init; }
        
        /// <summary>
        /// Id of the developer.
        /// </summary>
        public Guid DeveloperId { get; init; }
        
        /// <summary>
        /// Id of the profile responsible for the action.
        /// </summary>
        public Guid ProfileId { get; init; }
        
        /// <inheritdoc />
        public (string queue, string id) Receipt { get; set; }
    }
}