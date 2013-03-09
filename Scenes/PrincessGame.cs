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
using Xemio.PrincessDefense.Entities.Spells;
using Xemio.PrincessDefense.Scenes.Components;
using Xemio.PrincessDefense.Levels;
using Xemio.PrincessDefense.Entities.Particles;

namespace Xemio.PrincessDefense.Scenes
{
    public class PrincessGame : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PrincessGame"/> class.
        /// </summary>
        public PrincessGame(ILevel level)
        {
            this._world = new World(level);
        }
        #endregion

        #region Fields
        private World _world;
        private float _counter;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the upgrade menu.
        /// </summary>
        public UpgradeMenu UpgradeMenu
        {
            get { return this.GetScene<UpgradeMenu>(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            Player player = Player.Instance;
            player.Position = new Vector2(180, 100);

            this.Scenes.Add(new UpgradeMenu(player));
            this.Scenes.Add(new MiniMap(this._world));
            this.Scenes.Add(new HealthBar(player));
            this.Scenes.Add(new SpellButtons());
            this.Scenes.Add(new Announcer());

            Princess princess = new Princess();
            princess.Position = new Vector2(300, 300);
            
            this._world.Camera.Target = player;

            this._world.Add(player);
            this._world.Add(princess);

            this._world.GenerateWorld();
        }
        /// <summary>
        /// Ticks the specified elapsed.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this._world.LevelCompleted)
            {
                ILevel level = this._world.Level;
                level.Unlock();

                this.SceneManager.Remove(this);
                this.SceneManager.Add(new LevelSelection(level.Root, level));
            }
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
