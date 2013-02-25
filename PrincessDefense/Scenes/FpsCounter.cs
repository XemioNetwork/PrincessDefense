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

namespace PrincessDefense.Scenes
{
    public class FpsCounter : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FpsCounter"/> class.
        /// </summary>
        public FpsCounter()
        {
        }
        #endregion

        #region Fields
        private SpriteFont _font;
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
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            this._font = this.TextureFactory.CreateSpriteFont(@"Resources\fonts\kenPixel.sf");
            base.LoadContent();
        }
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            string display = string.Format(
                "FPS: {0:#}",
                1000.0 / this.GameLoop.FrameTime);

            this._font.Render(display, new Vector2(5, 5));
        }
        #endregion
    }
}
