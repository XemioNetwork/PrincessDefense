using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Sprites;

namespace PrincessDefense.Resources
{
    public static class SpriteRegistry
    {
        #region Constructors
        /// <summary>
        /// Initializes the <see cref="SpriteRegistry"/> class.
        /// </summary>
        static SpriteRegistry()
        {
            SpriteMappings = new Dictionary<string, SpriteSheet>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sprite mappings.
        /// </summary>
        private static Dictionary<string, SpriteSheet> SpriteMappings { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public static SpriteSheet Load(string id, string fileName, int frameWidth, int frameHeight, int startIndex, int frameCount)
        {
            if (!SpriteMappings.ContainsKey(id))
            {
                SpriteMappings.Add(id, new SpriteSheet(fileName, frameWidth, frameHeight, startIndex, frameCount));
            }

            return SpriteMappings[id];
        }
        #endregion
    }
}
