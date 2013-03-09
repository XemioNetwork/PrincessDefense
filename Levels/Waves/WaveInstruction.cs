using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.PrincessDefense.Levels.Waves
{
    public class WaveInstruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WaveInstruction"/> class.
        /// </summary>
        public WaveInstruction()
        {
            this.Spawns = new List<SpawnInstruction>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the enemy count.
        /// </summary>
        public int EnemyCount
        {
            get { return this.Spawns.Sum(spawn => spawn.Count); }
        }
        /// <summary>
        /// Gets the spawns.
        /// </summary>
        public List<SpawnInstruction> Spawns { get; set; }
        #endregion
    }
}
