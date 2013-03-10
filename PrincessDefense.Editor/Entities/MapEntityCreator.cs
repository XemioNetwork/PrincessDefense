using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;

namespace Xemio.PrincessDefense.Editor.Entities
{
    public class MapEntityCreator
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MapEntityCreator"/> class.
        /// </summary>
        public MapEntityCreator()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MapEntityCreator"/> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="command">The command.</param>
        public MapEntityCreator(ITexture texture, string command)
        {
            this.Texture = texture;
            this.Command = command;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the texture.
        /// </summary>
        public ITexture Texture { get; set; }
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        public string Command { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the entity.
        /// </summary>
        public MapEntity CreateEntity()
        {
            return new MapEntity { Texture = this.Texture, Command = this.Command };
        }
        #endregion
    }
}
