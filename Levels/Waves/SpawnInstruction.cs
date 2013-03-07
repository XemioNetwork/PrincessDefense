using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.PrincessDefense.Levels.Waves.Spawners;

namespace Xemio.PrincessDefense.Levels.Waves
{
    public class SpawnInstruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SpawnInstruction"/> class.
        /// </summary>
        public SpawnInstruction()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SpawnInstruction"/> class.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="right">The right.</param>
        /// <param name="bottom">The bottom.</param>
        /// <param name="left">The left.</param>
        public SpawnInstruction(int top, int right, int bottom, int left)
        {
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
            this.Left = left;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the spawn delay.
        /// </summary>
        public float NextSpawn { get; set; }
        /// <summary>
        /// Gets or sets the spawner.
        /// </summary>
        public IMobSpawner Spawner { get; set; }
        /// <summary>
        /// Gets the top skeleton count.
        /// </summary>
        public int Top { get; set; }
        /// <summary>
        /// Gets the right skeleton count.
        /// </summary>
        public int Right { get; set; }
        /// <summary>
        /// Gets the bottom skeleton count.
        /// </summary>
        public int Bottom { get; set; }
        /// <summary>
        /// Gets the left skeleton count.
        /// </summary>
        public int Left { get; set; }
        #endregion
    }
}
