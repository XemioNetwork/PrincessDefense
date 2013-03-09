using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Math;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Levels;

namespace Xemio.PrincessDefense.Entities.Environment
{
    public class World : EntityEnvironment
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="World"/> class.
        /// </summary>
        public World(ILevel level)
        {
            this.Level = level;

            this.Camera = new Camera();
            this.Camera.Max = new Vector2(Art.Map.Width, Art.Map.Height);

            this.Collision = new WorldCollision(this);
            this.Generator = new WorldGenerator(this);

            this.WaveManager = new WaveManager(this);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the camera.
        /// </summary>
        public Camera Camera { get; private set; }
        /// <summary>
        /// Gets the collision.
        /// </summary>
        public WorldCollision Collision { get; private set; }
        /// <summary>
        /// Gets the generator.
        /// </summary>
        public WorldGenerator Generator { get; private set; }
        /// <summary>
        /// Gets the wave manager.
        /// </summary>
        public WaveManager WaveManager { get; private set; }
        /// <summary>
        /// Gets the level.
        /// </summary>
        public ILevel Level { get; private set; }
        /// <summary>
        /// Gets a value indicating whether the current level is completed.
        /// </summary>
        public bool LevelCompleted
        {
            get { return this.WaveManager.LevelCompleted; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <returns></returns>
        public Player GetPlayer()
        {
            return (Player)this.Entities.FirstOrDefault(e => e is Player);
        }
        /// <summary>
        /// Generates the world.
        /// </summary>
        public void GenerateWorld()
        {
            this.Generator.Generate(this.Level.Seed);
        }
        /// <summary>
        /// Sorteds the entity collection.
        /// </summary>
        protected override IEnumerable<Entity> SortedEntityCollection()
        {
            return this.Entities.OrderBy(entity => entity.Position.Y);
        }
        /// <summary>
        /// Handles a game tick for all entities.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this.Camera.Tick(elapsed);
            this.Collision.Tick(elapsed);
            this.WaveManager.Tick(elapsed);

            base.Tick(elapsed);
        }
        /// <summary>
        /// Renders all entities.
        /// </summary>
        public override void Render()
        {
            IRenderManager renderManager = XGL.GetComponent<IRenderManager>();
            GraphicsDevice graphicsDevice = XGL.GetComponent<GraphicsDevice>();

            Vector2 center = graphicsDevice.DisplayMode.Center;
            Vector2 offset = -this.Camera.DisplayOffset + center;

            renderManager.Offset(offset);
            renderManager.Render(Art.Map, Vector2.Zero);

            base.Render();
        }
        #endregion
    }
}
