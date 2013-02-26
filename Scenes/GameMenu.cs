using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Rendering.Fonts.Utility;
using Xemio.GameLibrary.Game;

namespace PrincessDefense.Scenes
{
    public class GameMenu : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GameMenu"/> class.
        /// </summary>
        public GameMenu()
        {
        }
        #endregion

        #region Fields
        private ITexture _background;
        private ITexture _title;
        private SpriteFont _font;

        private float _remainingInvertTime;
        private float _maxRemainingTime;
        private bool _render;

        private bool _isRemoved;
        private float _elapsedRemoveTime;
        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            ITextureFactory factory = this.GraphicsDevice.TextureFactory;

            this._background = factory.CreateTexture(@"Resources\misc\menu.png");
            this._title = factory.CreateTexture(@"Resources\misc\title.png");
            this._font = factory.CreateSpriteFont(@"Resources\fonts\kenPixel.sf");

            this._maxRemainingTime = 500;
            this._remainingInvertTime = this._maxRemainingTime;
            this._render = true;

            base.LoadContent();
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this._remainingInvertTime -= elapsed;            
            if (this._remainingInvertTime <= 0)
            {
                this._render = !this._render;
                this._remainingInvertTime = this._maxRemainingTime;
            }

            if (this.Keyboard.IsKeyPressed(Keys.Enter))
            {
                this._isRemoved = true;
            }

            if (this._isRemoved)
            {
                this._elapsedRemoveTime += elapsed;
                if (this._elapsedRemoveTime >= 1000.0f)
                {
                    this.SceneManager.Remove(this);

                    this.SceneManager.Add(new PrincessGame());
                    this.SceneManager.GetScene(scene => scene is FpsCounter)
                        .BringToFront();
                }
            }
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        public override void Render()
        {
            this.GraphicsDevice.Clear(Color.Black);

            float alpha = Math.Max(1.0f - this._elapsedRemoveTime / 1000.0f, 0);

            this.GraphicsDevice.RenderManager.Tint(new Color(1, 1, 1, alpha));
            this.GraphicsDevice.RenderManager.Render(this._background, Vector2.Zero);
            this.GraphicsDevice.RenderManager.Render(this._title, Vector2.Zero);

            if (this._render)
            {
                string message = "Press Enter to start the game";
                Vector2 fontCenter = this._font.MeasureString(message) * .5f - new Vector2(0, 120);
                Vector2 shadowOffset = new Vector2(1, 1);

                this._font.Render(
                    message,
                    this.GraphicsDevice.DisplayMode.Center - fontCenter + shadowOffset,
                    new Color(0.0f, 0.0f, 0.0f, 0.8f));

                this._font.Render(
                    message,
                    this.GraphicsDevice.DisplayMode.Center - fontCenter);
            }
        }
        #endregion
    }
}
