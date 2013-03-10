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
                        new SpawnInstruction { Top = 2, Spawner = new SlimeSpawner(), NextSpawn = 3000 },
                        new SpawnInstruction { Top = 1, Left = 1, Spawner = new SlimeSpawner(), NextSpawn = 0 }
                    }
                };
                case 1: return new WaveInstruction
                {
                    Spawns = new List<SpawnInstruction>()
                    {
                        new SpawnInstruction { Top = 2, Spawner = new SlimeSpawner(), NextSpawn = 4000 },
                        new SpawnInstruction { Bottom = 2, Spawner = new BatSpawner(), NextSpawn = 4000 },
                        new SpawnInstruction { Top = 2, Spawner = new BatSpawner(), NextSpawn = 4000 },
                        new SpawnInstruction { Bottom = 2, Spawner = new SlimeSpawner(), NextSpawn = 0 },
                    }
                };
                case 2: return new WaveInstruction
                {
                    Spawns = new List<SpawnInstruction>()
                    {
                        new SpawnInstruction { Bottom = 1, Right = 1, Spawner = new SlimeSpawner(), NextSpawn = 3000 },
                        new SpawnInstruction { Top = 1, Left = 1, Spawner = new BatSpawner(), NextSpawn = 3000 },
                        new SpawnInstruction { Bottom = 2, Left = 2, Spawner = new SlimeSpawner(), NextSpawn = 4000 },
                        new SpawnInstruction { Top = 2, Right = 2, Spawner = new SlimeSpawner(), NextSpawn = 0 },
                    }
                };
            }

            return null;
        }
        #endregion
    }
}
