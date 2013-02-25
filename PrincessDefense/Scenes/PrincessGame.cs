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
using PrincessDefense;
using PrincessDefense.Entities;
using PrincessDefense.Entities.Components;
using PrincessDefense.Entities.Environment;
using PrincessDefense.Entities.Characters;

namespace PrincessDefense.Scenes
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

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            this._world = new World();

            Player player = new Player();
            player.Position = new Vector2(180, 100);

            this.SceneManager.Add(new MiniMap(this._world));
            this.SceneManager.Add(new HealthBar(player));
            this.SceneManager.Add(new Anouncer());

            Princess princess = new Princess();
            princess.Position = new Vector2(300, 300);
            
            this._world.Camera.Target = player;

            this._world.Add(player);
            this._world.Add(princess);

            PerlinNoise perlin = new PerlinNoise("Forest");
            RandomProxy random = new RandomProxy("Forest");

            float[,] baseNoise = perlin.GenerateBaseNoise(72, 72, 1.5f);
            float[,] noise = perlin.GeneratePerlinNoise(baseNoise, 0.6f, 1, 3);

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (noise[x * 6, y * 6] > 0.7f && random.NextBoolean(0.4f) ||
                        (x <= 0 || x >= 9 || y <= 0 || y >= 9) && random.NextBoolean(0.1f))
                    {
                        Vector2 position = new Vector2(x * 64, y * 64);
                        position += new Vector2(random.Next(-15, 15), random.Next(-15, 15));

                        this._world.Add(new Tree { Position = position });
                    }
                }
            }
        }
        private float _testElapsed = 14000;
        private int _waveIndex = 1;
        private int _spawnRate = 2;
        private int _lastCount = 1;
        /// <summary>
        /// Ticks the specified elapsed.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            Anouncer anouner = this.SceneManager.GetScene(a => a is Anouncer) as Anouncer;
            int count = this._world.Count(e => e is Skeleton);

            if (count == 0 && count != this._lastCount && this._waveIndex > 1)
            {
                anouner.Announce("Wave completed.");
            }

            this._lastCount = count;

            if (count == 0)
            {
                this._testElapsed += elapsed;
                if (this._testElapsed >= 3000)
                {
                    for (int i = 0; i < this._spawnRate; i++)
                    {
                        this._world.Add(new Skeleton { Position = new Vector2(320 + i, -200) });
                    }
                    for (int i = 0; i < this._spawnRate; i++)
                    {
                        this._world.Add(new Skeleton { Position = new Vector2(-200, 320 + i) });
                    }

                    anouner.Announce("Wave " + this._waveIndex.ToString() + " incoming");

                    this._waveIndex++;
                    this._spawnRate++;
                    this._testElapsed = 0;
                }
            }

            this._world.Tick(elapsed);
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
