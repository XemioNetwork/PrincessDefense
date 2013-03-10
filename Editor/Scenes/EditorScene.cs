using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Entities;
using System.Windows.Forms;
using Xemio.PrincessDefense.Editor.Entities;
using Xemio.PrincessDefense.Editor.Commands;

namespace Xemio.PrincessDefense.Editor.Scenes
{
    public class EditorScene : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorScene"/> class.
        /// </summary>
        public EditorScene()
        {
            this.Environment = new MapEntityEnvironment();
            this.GridSize = 32;
        }
        #endregion

        #region Fields
        private MapEntityCreator _creator;
        private bool _lastPressed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the creator.
        /// </summary>
        public MapEntityCreator Creator
        {
            get { return this._creator; }
            set
            {
                if (this.SelectedEntity != null)
                {
                    this.Environment.Remove(this.SelectedEntity);
                }

                this._creator = value;
                this.CreateEntity();
            }
        }
        /// <summary>
        /// Gets or sets the selected entity.
        /// </summary>
        public MapEntity SelectedEntity { get; set; }
        /// <summary>
        /// Gets the environment.
        /// </summary>
        public EntityEnvironment Environment { get; private set; }
        /// <summary>
        /// Gets or sets the size of the grid.
        /// </summary>
        public int GridSize { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the entity.
        /// </summary>
        private void CreateEntity()
        {
            this.SelectedEntity = this.Creator.CreateEntity();
            this.Environment.Add(this.SelectedEntity);
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this.SelectedEntity != null)
            {
                Vector2 position = new Vector2(
                    (int)this.Mouse.Position.X / this.GridSize,
                    (int)this.Mouse.Position.Y / this.GridSize);

                this.SelectedEntity.Position = position * this.GridSize;
                if (this.Mouse.IsButtonPressed(MouseButtons.Left) && !this._lastPressed)
                {
                    CommandManager.Instance.Add(new CreateEntityCommand(this.Environment, this.SelectedEntity));
                    this.CreateEntity();
                }
            }

            this._lastPressed = this.Mouse.IsButtonPressed(MouseButtons.Left);
            base.Tick(elapsed);
        }
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            this.GraphicsDevice.RenderManager.Render(Art.Map, Vector2.Zero);
            foreach (Entity entity in this.Environment)
            {
                entity.Renderer.Render();
            }

            base.Render();
        }
        #endregion
    }
}
