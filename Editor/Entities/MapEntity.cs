using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Rendering;

namespace Xemio.PrincessDefense.Editor.Entities
{
    public class MapEntity : Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MapEntity"/> class.
        /// </summary>
        public MapEntity()
        {
            this.Renderer = new MapEntityRenderer(this);
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
    }
}
