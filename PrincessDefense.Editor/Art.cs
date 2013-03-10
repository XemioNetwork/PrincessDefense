using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary;

namespace Xemio.PrincessDefense.Editor
{
    public static class Art
    {
        #region Fields
        private static Dictionary<string, ITexture> _textureMappings;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the map.
        /// </summary>
        public static ITexture Map { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public static void LoadContent()
        {
            ITextureFactory factory = XGL.GetComponent<ITextureFactory>();
            Art.Map = factory.CreateTexture(@"Resources\map.png");

            _textureMappings = new Dictionary<string, ITexture>();
        }
        /// <summary>
        /// Gets the specified texture.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public static ITexture Get(string fileName)
        {
            ITextureFactory factory = XGL.GetComponent<ITextureFactory>();
            string fullPath = Path.GetFullPath(fileName);

            if (!_textureMappings.ContainsKey(fileName))
            {
                _textureMappings.Add(fileName, factory.CreateTexture(fullPath));
            }

            return _textureMappings[fileName];
        }
        #endregion
    }
}
