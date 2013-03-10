using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Entities.Upgrades;
using Xemio.PrincessDefense.Extensions;

namespace Xemio.PrincessDefense.Scenes.Menues
{
    public class CookbookMenu : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CookbookMenu"/> class.
        /// </summary>
        public CookbookMenu()
        {
            this._angleOffset = MathHelper.Pi;
        }
        #endregion

        #region Fields
        private IBrush _overlay;
        private IBrush _circleBrush;
        private IPen _circlePen;

        private IBrush _shadowBrush;
        private IPen _shadowPen;

        private float _rotation;
        private float _targetRotation;
        private float _angleOffset;

        private int _selectedIndex;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the upgrades.
        /// </summary>
        public List<IUpgrade> Upgrades
        {
            get
            {
                List<IUpgrade> upgrades = UpgradeManager.GetAvailableUpgrades();
                foreach (IUpgrade playerUpgrade in Player.Instance.Upgrades)
                {
                    upgrades.Remove(playerUpgrade);
                }

                return upgrades;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            IGeometryFactory factory = this.GraphicsDevice.Geometry.Factory;

            this._overlay = factory.CreateSolid(new Color(0, 0, 0, 0.85f));
            this._circleBrush = factory.CreateSolid(new Color(0.8f, 0.8f, 0.8f));
            this._circlePen = factory.CreatePen(new Color(0.8f, 0.8f, 0.8f), 5);

            this._shadowBrush = factory.CreateSolid(new Color(0, 0, 0));
            this._shadowPen = factory.CreatePen(new Color(0, 0, 0), 5);
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this._rotation += (this._targetRotation - this._rotation) * 0.2f;

            if (this.Keyboard.IsKeyPressed(Keys.Left))
            {
                this._selectedIndex++;
                this._targetRotation -= MathHelper.TwoPi / this.Upgrades.Count;

                Sounds.Play(Sounds.Select);
            }
            if (this.Keyboard.IsKeyPressed(Keys.Right))
            {
                this._selectedIndex--;
                this._targetRotation += MathHelper.TwoPi / this.Upgrades.Count;

                Sounds.Play(Sounds.Select);
            }

            if (this._selectedIndex >= this.Upgrades.Count) this._selectedIndex = 0;
            if (this._selectedIndex < 0) this._selectedIndex = this.Upgrades.Count - 1;

            if (this.Keyboard.IsKeyPressed(Keys.Escape))
            {
                this.Remove();
            }
        }
        /// <summary>
        /// Renders the upgrade circle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="circles">The circles.</param>
        /// <param name="offset">The offset.</param>
        private void RenderUpgradeCircle(IPen pen, IBrush brush, Vector2 offset)
        {
            IGeometryProvider geometry = this.GraphicsDevice.Geometry;
            IRenderManager renderManager = this.GraphicsDevice.RenderManager;

            List<IUpgrade> upgrades = this.Upgrades;
            float angle = MathHelper.TwoPi / (float)upgrades.Count;

            geometry.DrawCircle(pen, offset, 100);

            for (int i = 0; i < upgrades.Count; i++)
            {
                Vector2 angleVector = new Vector2(
                    MathHelper.Sin(i * angle + this._rotation + this._angleOffset) * 100,
                    MathHelper.Cos(i * angle + this._rotation + this._angleOffset) * 100);

                geometry.FillCircle(brush, offset + angleVector, 25);

                if (pen != this._shadowPen)
                {
                    if (this._selectedIndex == i)
                    {
                        geometry.DrawCircle(this._shadowPen, offset + angleVector, 25);
                    }

                    renderManager.Render(
                        upgrades[i].Icon,
                        offset + angleVector + new Vector2(-17, -17));
                }
            }
        }
        /// <summary>
        /// Renders this scene.
        /// </summary>
        public override void Render()
        {
            IGeometryProvider geometry = this.GraphicsDevice.Geometry;
            IRenderManager renderManager = this.GraphicsDevice.RenderManager;
            
            LevelSelection levelSelection = this.Parent as LevelSelection;
            Vector2 distance = levelSelection == null ? Vector2.Zero : levelSelection.Camera.Distance;

            Vector2 center = this.GraphicsDevice.DisplayMode.Center - distance;
            Vector2 shadowOffset = new Vector2(0, 2);

            geometry.FillRectangle(this._overlay, this.GraphicsDevice.DisplayMode.Bounds);

            string name = this.Upgrades[this._selectedIndex].Name;
            Vector2 fontOffset = Art.Font.MeasureString(name)* 0.5f;

            Art.Font.RenderShadowed(name, center - fontOffset - new Vector2(0, 50), 0.7f);

            this.RenderUpgradeCircle(this._shadowPen, this._shadowBrush, center + shadowOffset);
            this.RenderUpgradeCircle(this._circlePen, this._circleBrush, center);

            if (levelSelection != null)
            {
                renderManager.Render(
                    levelSelection.SelectedLevel.Icon, 
                    center - new Vector2(17, 28) + new Vector2(0, levelSelection.Offset));
            }
        }
        #endregion
    }
}
