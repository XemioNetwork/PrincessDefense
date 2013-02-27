using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Extensions;

namespace Xemio.PrincessDefense.Scenes
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

        private IBrush _background;
        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            this._font = this.GraphicsDevice.TextureFactory.CreateSpriteFont(@"Resources\fonts\kenPixel.sf");
            this._background = this.GraphicsDevice.Geometry.Factory.CreateSolid(new Color(0.2f, 0.1f, 0.05f, 1.0f));
        }
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            ExperienceComponent experience = this._player.GetComponent<ExperienceComponent>();
            HealthComponent health = this._player.GetComponent<HealthComponent>();

            IGeometryProvider geometry = this.GraphicsDevice.Geometry;
            IRenderManager renderManager = this.GraphicsDevice.RenderManager;

            float maxWidth = 275;
            float width = maxWidth * health.Percentage;
            float height = 14;

            Vector2 position = new Vector2(10, 300 - height - 10);

            renderManager.Offset(Vector2.Zero);
            geometry.FillRectangle(this._background, new Rectangle(position.X, position.Y, maxWidth, height));

            if (health.Percentage > 0)
            {
                IBrush gradient = geometry.Factory.CreateGradient(
                    new Color(1, 0.15f, 0, 0.6f),
                    new Color(0.9f, 0.15f, 0, 0.6f),
                    (int)width, (int)height * 2, 
                    MathHelper.ToRadians(-90));

                geometry.FillRectangle(
                    gradient,
                    new Rectangle(position.X + 1, position.Y + 1, width - 2, height - 2));

                geometry.DrawRectangle(
                    new Color(1, 0.9f, 0.8f, 0.1f),
                    new Rectangle(position.X + 1, position.Y + 1, width - 3, height - 3));
            }

            string healthMessage = string.Format("Health: {0}/{1}", health.Health, health.MaxHealth);

            this._font.RenderShadowed(healthMessage, position - new Vector2(1, height + 3), 0.7f);
            this._font.RenderShadowed("Level " + experience.GetLevel(), position - new Vector2(-225, height + 3), 0.7f);
        }
        #endregion
    }
}
