using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.PrincessDefense.Levels.Waves;
using Xemio.PrincessDefense.Levels.Waves.Spawners;

namespace Xemio.PrincessDefense.Levels
{
    public class BreadWaveProvider : IWaveProvider
    {
        #region IWaveProvider Member
        /// <summary>
        /// Gets the enemy count.
        /// </summary>
        /// <param name="waveIndex">Index of the wave.</param>
        /// <returns></returns>
        public int GetEnemyCount(int waveIndex)
        {
            switch (waveIndex)
            {
                case 0: return 4;
                case 1: return 14;
                case 2: return 30;
            }

            return 0;
        }
        /// <summary>
        /// Creates wave instructions for the current wave.
        /// </summary>
        public WaveInstruction CreateWave(int waveIndex)
        {
            switch (waveIndex)
            {
                case 0: return new WaveInstruction
                {
                    Spawns = new List<SpawnInstruction>()
                    {
                        new SpawnInstruction { Top = 2, Spawner = new SlimeSpawner(), NextSpawn = 2000 },
                        new SpawnInstruction { Top = 1, Left = 1, Spawner = new SlimeSpawner(), NextSpawn = 0 }
                    }
                };
                case 1: return new WaveInstruction
                {
                    Spawns = new List<SpawnInstruction>()
                    {
                        new SpawnInstruction { Top = 2, Spawner = new SlimeSpawner(), NextSpawn = 1000 },
                        new SpawnInstruction { Top = 3, Spawner = new SlimeSpawner(), NextSpawn = 1500 },
                        new SpawnInstruction { Top = 4, Spawner = new SlimeSpawner(), NextSpawn = 2000 },
                        new SpawnInstruction { Top = 5, Spawner = new SlimeSpawner(), NextSpawn = 0 },
                    }
                };
                case 2: return new WaveInstruction
                {
                    Spawns = new List<SpawnInstruction>()
                    {
                        new SpawnInstruction { Top = 3, Left = 3, Spawner = new SlimeSpawner(), NextSpawn = 3000 },
                        new SpawnInstruction { Top = 3, Left = 3, Spawner = new SlimeSpawner(), NextSpawn = 3000 },
                        new SpawnInstruction { Bottom = 4, Left = 4, Spawner = new SlimeSpawner(), NextSpawn = 4000 },
                        new SpawnInstruction { Top = 5, Right = 5, Spawner = new SlimeSpawner(), NextSpawn = 0 },
                    }
                };
            }

            return null;
        }
        #endregion
    }
}
