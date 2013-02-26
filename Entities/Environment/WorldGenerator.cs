using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Randomization;
using Xemio.GameLibrary.Math;

namespace Xemio.PrincessDefense.Entities.Environment
{
    public class WorldGenerator
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldGenerator"/> class.
        /// </summary>
        /// <param name="world">The world.</param>
        public WorldGenerator(World world)
        {
            this._world = world;
        }
        #endregion

        #region Fields
        private World _world;
        #endregion

        #region Methods
        /// <summary>
        /// Generates the world.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public void Generate(string seed)
        {
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
        #endregion
    }
}
