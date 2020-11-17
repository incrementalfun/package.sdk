using System;
using System.Collections.Generic;
using Incremental.Common.Queue.Message.Contract;

namespace Incremental.Common.Messages
{
    /// <summary>
    /// Message for registering a new game.
    /// </summary>
    public class RegisterGame : IMessage
    {
        /// <summary>
        /// Id of the game.
        /// </summary>
        public Guid GameId { get; init; }
    
        /// <summary>
        /// Title of the game.
        /// </summary>
        public string Title { get; init; }
        
        /// <summary>
        /// Description of the game.
        /// </summary>
        public string Description { get; init; }
        
        /// <summary>
        /// Developers of the game.
        /// </summary>
        public List<Guid> Developers { get; init; }
        
        /// <summary>
        /// Related links of the game.
        /// </summary>
        public Dictionary<string, string> Links { get; init; }
        
        /// <summary>
        /// Related tags of the game.
        /// </summary>
        public List<string> Tags { get; init; }

        /// <summary>
        /// Id of the profile responsible for the action.
        /// </summary>
        public Guid ProfileId { get; init; }
        
        /// <inheritdoc />
        public (string queue, string id) Receipt { get; set; }
    }
}