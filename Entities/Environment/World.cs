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

namespace Xemio.PrincessDefense.Entities.Environment
{
    public class World : EntityEnvironment
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="World"/> class.
        /// </summary>
        public World()
        {
            ITextureFactory factory = XGL.GetComponent<GraphicsDevice>().TextureFactory;

            this.Background = factory.CreateTexture(@"Resources\map.png");

            this.Camera = new Camera();
            this.Camera.Max = new Vector2(this.Background.Width, this.Background.Height);

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
        /// Gets the background.
        /// </summary>
        public ITexture Background { get; private set; }
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
        /// <param name="seed">The seed.</param>
        public void GenerateWorld(string seed)
        {
            this.Generator.Generate(seed);
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
            renderManager.Render(this.Background, Vector2.Zero);

            base.Render();
        }
        #endregion
    }
}
