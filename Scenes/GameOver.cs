using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;

namespace Xemio.PrincessDefense.Scenes
{
    public class GameOver : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GameOver"/> class.
        /// </summary>
        public GameOver()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the opacity.
        /// </summary>
        public float Opacity { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Ticks the specified elapsed.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this.Opacity += 0.05f;
            if (this.Opacity > 1.0f)
            {
                this.Opacity = 1.0f;
            }
        }
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            Vector2 position = this.GraphicsDevice.DisplayMode.Center;
            position -= new Vector2(Art.GameOver.Width * 0.5f, Art.GameOver.Height * 0.5f);

            this.GraphicsDevice.RenderManager.Tint(new Color(1, 1, 1, this.Opacity));
            this.GraphicsDevice.RenderManager.Render(Art.GameOver, position);
        }
        #endregion
    }
}
