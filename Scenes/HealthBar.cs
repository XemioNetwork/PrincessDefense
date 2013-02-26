using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Math;
using PrincessDefense.Entities.Characters;
using PrincessDefense.Entities.Components;
using Xemio.GameLibrary.Rendering.Fonts;

namespace PrincessDefense.Scenes
{
    public class HealthBar : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HealthBar"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        public HealthBar(Player player)
        {
            this._player = player;
        }
        #endregion

        #region Fields
        private Player _player;
        private SpriteFont _font;
        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            this._font = this.GraphicsDevice.TextureFactory.CreateSpriteFont(@"Resources\fonts\kenPixel.sf");
        }
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            HealthComponent health = this._player.GetComponent<HealthComponent>();
            IGeometryProvider geometry = this.GraphicsDevice.Geometry;
            IRenderManager renderManager = this.GraphicsDevice.RenderManager;

            float maxWidth = 275;
            float width = maxWidth * health.Percentage;
            float height = 14;

            Vector2 position = new Vector2(10, 300 - height - 10);

            IBrush black = geometry.Factory.CreateSolid(new Color(0.2f, 0.1f, 0.05f, 1.0f));

            renderManager.Offset(Vector2.Zero);
            geometry.FillRectangle(black, new Rectangle(position.X, position.Y, maxWidth, height));

            if (health.Percentage > 0)
            {
                IBrush red = geometry.Factory.CreateGradient(new Color(1, 0.15f, 0, 0.6f), new Color(0.9f, 0.15f, 0, 0.6f), (int)width, (int)height * 2, MathHelper.ToRadians(-90));

                geometry.FillRectangle(red, new Rectangle(position.X + 1, position.Y + 1, width - 2, height - 2));
                geometry.DrawRectangle(new Color(1, 0.9f, 0.8f, 0.1f), new Rectangle(position.X + 1, position.Y + 1, width - 3, height - 3));
            }

            renderManager.Tint(new Color(0, 0, 0, 0.6f));
            this._font.Render("Health:", position - new Vector2(0, height + 4));

            renderManager.Tint(Color.White);
            this._font.Render("Health:", position - new Vector2(0, height + 5));
        }
        #endregion
    }
}
