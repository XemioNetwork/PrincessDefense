using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Math;
using PrincessDefense.Entities.Environment;
using PrincessDefense.Entities;
using Xemio.GameLibrary.Entities;
using PrincessDefense.Entities.Characters;

namespace PrincessDefense.Scenes
{
    public class MiniMap : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MiniMap"/> class.
        /// </summary>
        /// <param name="world">The world.</param>
        public MiniMap(World world)
        {
            this._world = world;
        }
        #endregion

        #region Fields
        private World _world;

        private IBrush _backgroundColor;
        private IBrush _playerColor;
        private IBrush _princessColor;
        private IBrush _enemyColor;
        private IBrush _neutralColor;
        private IBrush _transparentColor;

        private ITexture _top;
        private ITexture _bottom;
        private ITexture _scroll;
        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            IGeometryFactory geometryFactory = this.GraphicsDevice.Geometry.Factory;
            ITextureFactory factory = this.GraphicsDevice.TextureFactory;

            this._backgroundColor = geometryFactory.CreateSolid(new Color(0, 0, 0, 0.5f));
            this._playerColor = geometryFactory.CreateSolid(new Color(0.2f, 0.8f, 0.1f, 0.9f));
            this._princessColor = geometryFactory.CreateSolid(new Color(0.1f, 0.5f, 0.8f, 0.9f));
            this._enemyColor = geometryFactory.CreateSolid(new Color(0.8f, 0.2f, 0.1f, 0.9f));
            this._neutralColor = geometryFactory.CreateSolid(new Color(0.5f, 0.5f, 0.5f, 0.9f));
            this._transparentColor = geometryFactory.CreateSolid(Color.Transparent);

            this._top = factory.CreateTexture(@"Resources\ui\scrollTop.png");
            this._bottom = factory.CreateTexture(@"Resources\ui\scrollBottom.png");
            this._scroll = factory.CreateTexture(@"Resources\ui\scroll.png");
        }
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            IGeometryProvider geometry = this.GraphicsDevice.Geometry;
            IRenderManager renderManager = this.GraphicsDevice.RenderManager;

            Vector2 offset = new Vector2(400 - 80 - 20, 220);
            Rectangle destination = new Rectangle(0, 0, 80, 1) + offset;
            Rectangle bounds = new Rectangle(0, 0, 640, 640);

            renderManager.Offset(Vector2.Zero);

            for (int i = 0; i < 80; i++)
            {
                renderManager.Render(this._scroll, new Rectangle(-5, 0, 79 + 10, 1) + offset + new Vector2(0, i));
            }

            renderManager.Render(this._top, new Rectangle(-8, -25, this._top.Width + 11, this._top.Height) + offset);
            
            foreach (Entity entity in this._world)
            {
                BaseEntity baseEntity = entity as BaseEntity;

                if (baseEntity == null) continue;
                if (!bounds.Contains(baseEntity.Position)) continue;

                int x = (int)baseEntity.Position.X / 8;
                int y = (int)baseEntity.Position.Y / 8;

                Vector2 position = new Vector2(x, y) + offset;
                IBrush color = this._transparentColor;

                if (baseEntity is Player) color = this._playerColor;
                if (baseEntity is Princess) color = this._princessColor;
                if (baseEntity.Team == Team.Skeletons) color = this._enemyColor;
                if (baseEntity.Team == Team.Neutral) color = this._neutralColor;

                geometry.FillRectangle(color, new Rectangle(-1, -1, 3, 3) + position);
                if (color != this._transparentColor)
                {
                    geometry.DrawLine(new Color(0, 0, 0, 0.7f), position + new Vector2(-1, 2), position + new Vector2(1, 2));
                }
            }
        }
        #endregion
    }
}
