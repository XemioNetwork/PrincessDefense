using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Math;

namespace PrincessDefense.Scenes
{
    public class SpellButtons : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SpellButtons"/> class.
        /// </summary>
        public SpellButtons()
        {
        }
        #endregion

        #region Fields
        private ITexture _buttons;
        #endregion

        #region Properties

        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            this._buttons = this.GraphicsDevice.TextureFactory.CreateTexture(@"Resources\ui\buttons.png");
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {

        }
        /// <summary>
        /// Renders the scene.
        /// </summary>
        public override void Render()
        {
            this.GraphicsDevice.RenderManager.Render(this._buttons, new Vector2(310, 10));
        }
        #endregion
    }
}
