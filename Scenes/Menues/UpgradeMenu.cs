using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Math;
using PrincessDefense.Entities.Characters;
using PrincessDefense.Entities.Upgrades;
using System.Windows.Forms;
using PrincessDefense.Entities.Components;

namespace PrincessDefense.Scenes.Menues
{
    public class UpgradeMenu : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeMenu"/> class.
        /// </summary>
        public UpgradeMenu(Player player)
        {
            this._player = player;
            this._selectedIndex = 0;
        }
        #endregion

        #region Fields
        private Player _player;

        private IBrush _overlayBrush;
        private IBrush _selectedBrush;

        private SpriteFont _font;

        private int _selectedIndex;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UpgradeMenu"/> is visible.
        /// </summary>
        public bool Visible { get; set; }
        /// <summary>
        /// Gets the selected upgrade.
        /// </summary>
        public IUpgrade SelectedUpgrade
        {
            get { return this._player.Upgrades[this._selectedIndex]; }
        }
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        protected IRenderManager RenderManager
        {
            get { return this.GraphicsDevice.RenderManager; }
        }
        /// <summary>
        /// Gets the geometry.
        /// </summary>
        protected IGeometryProvider Geometry
        {
            get { return this.GraphicsDevice.Geometry; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Toggles the visibility.
        /// </summary>
        public void ToggleVisibility()
        {
            this.Visible = !this.Visible;
        }
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            this._overlayBrush = this.Geometry.Factory.CreateSolid(new Color(0, 0, 0, 0.6f));
            this._selectedBrush = this.Geometry.Factory.CreateSolid(new Color(0.8f, 0.3f, 0.1f, 0.6f));
            this._font = this.GraphicsDevice.TextureFactory.CreateSpriteFont(@"Resources\fonts\kenPixel.sf");
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this.Keyboard.IsKeyPressed(Keys.Down))
            {
                this._selectedIndex++;
                if (this._selectedIndex >= this._player.Upgrades.Count)
                {
                    this._selectedIndex = 0;
                }
            }
            if (this.Keyboard.IsKeyPressed(Keys.Up))
            {
                this._selectedIndex--;
                if (this._selectedIndex < 0)
                {
                    this._selectedIndex = this._player.Upgrades.Count - 1;
                }
            }

            if (this.Keyboard.IsKeyPressed(Keys.Enter))
            {
                UpgradeManager.Upgrade(this._player, this.SelectedUpgrade);
            }
        }
        /// <summary>
        /// Renders this menu.
        /// </summary>
        public override void Render()
        {
            if (this.Visible)
            {
                this.Geometry.FillRectangle(this._overlayBrush, this.GraphicsDevice.DisplayMode.Bounds);

                ExperienceComponent experience = this._player.GetComponent<ExperienceComponent>();
                string title = "Upgrades: (" + experience.SkillPoints.ToString() + "P)";

                this.RenderManager.Tint(new Color(0, 0, 0, 0.5f));
                this._font.Render(title, new Vector2(40, 50 + 1));

                this.RenderManager.Tint(Color.White);
                this._font.Render(title, new Vector2(40, 50));

                for (int i = 0; i < this._player.Upgrades.Count; i++)
                {
                    IUpgrade upgrade = this._player.Upgrades[i];

                    string message = string.Format("{0}\n{1} / {2}",
                        upgrade.Name, upgrade.Level, upgrade.MaximumLevel);

                    this.Geometry.FillRectangle(this._overlayBrush, new Rectangle(42, i * 44 + 68, 220, 40));
                    if (this._selectedIndex == i)
                    {
                        this.Geometry.FillRectangle(this._selectedBrush, new Rectangle(42, i * 44 + 68, 220, 40));
                    }

                    this.Geometry.DrawLine(new Color(0, 0, 0, 0.8f), new Vector2(42, i * 44 + 108), new Vector2(261, i * 44 + 108));

                    if (!UpgradeManager.CanUpgrade(this._player, upgrade))
                    {
                        this.RenderManager.Tint(new Color(1, 1, 1, 0.3f));
                    }
                    this.RenderManager.Render(upgrade.Icon, new Vector2(44, i * 44 + 72));

                    this.RenderManager.Tint(new Color(0, 0, 0, 0.7f));
                    this._font.Render(message, new Vector2(80, i * 44 + 74 + 1));

                    this.RenderManager.Tint(Color.White);
                    this._font.Render(message, new Vector2(80, i * 44 + 74));

                    string costs = upgrade.Costs.ToString() + "P";
                    if (upgrade.Level == upgrade.MaximumLevel)
                    {
                        costs = "max";
                    }

                    this.RenderManager.Tint(new Color(0, 0, 0, 0.7f));
                    this._font.Render(costs, new Vector2(220, i * 44 + 81 + 1));

                    this.RenderManager.Tint(Color.White);
                    this._font.Render(costs, new Vector2(220, i * 44 + 81));
                }
            }
        }
        #endregion
    }
}
