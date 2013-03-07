using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Rendering;

namespace Xemio.PrincessDefense.Scenes.Components
{
    public class FPSCounter : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FPSCounter"/> class.
        /// </summary>
        public FPSCounter()
        {
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the game loop.
        /// </summary>
        public GameLoop GameLoop
        {
            get { return XGL.GetComponent<GameLoop>(); }
        }
        /// <summary>
        /// Gets the texture factory.
        /// </summary>
        public ITextureFactory TextureFactory
        {
            get { return this.GraphicsDevice.TextureFactory; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            string display = string.Format(
                "FPS: {0:#}",
                1000.0 / this.GameLoop.FrameTime);

            this.GraphicsDevice.RenderManager.Tint(new Color(0, 0, 0, 0.7f));
            Art.Font.Render(display, new Vector2(5, 6));

            this.GraphicsDevice.RenderManager.Tint(Color.White);
            Art.Font.Render(display, new Vector2(5, 5));
        }
        #endregion
    }
}
