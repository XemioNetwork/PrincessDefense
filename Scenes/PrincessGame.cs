using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Rendering.Sprites;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Common.Randomization;
using Xemio.PrincessDefense;
using Xemio.PrincessDefense.Entities;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Entities.Environment;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Scenes.Menues;

namespace Xemio.PrincessDefense.Scenes
{
    public class PrincessGame : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PrincessGame"/> class.
        /// </summary>
        public PrincessGame()
        {
        }
        #endregion

        #region Fields
        private World _world;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the upgrade menu.
        /// </summary>
        public UpgradeMenu UpgradeMenu
        {
            get { return (UpgradeMenu)this.SceneManager.GetScene(scene => scene is UpgradeMenu); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            this._world = new World();

            Player player = new Player();
            player.Position = new Vector2(180, 100);

            this.SceneManager.Add(new UpgradeMenu(player));
            this.SceneManager.Add(new MiniMap(this._world));
            this.SceneManager.Add(new HealthBar(player));
            this.SceneManager.Add(new SpellButtons());
            this.SceneManager.Add(new Announcer());

            Princess princess = new Princess();
            princess.Position = new Vector2(300, 300);
            
            this._world.Camera.Target = player;

            this._world.Add(player);
            this._world.Add(princess);

            this._world.GenerateWorld("Forest");
        }
        /// <summary>
        /// Ticks the specified elapsed.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this.Keyboard.IsKeyPressed(Keys.E))
            {
                this.UpgradeMenu.ToggleVisibility();
            }
            if (!this.UpgradeMenu.Visible)
            {
                this._world.Tick(elapsed);
            }
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        public override void Render()
        {
            this._world.Render();
        }
        #endregion
    }
}
