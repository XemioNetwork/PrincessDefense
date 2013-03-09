using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Entities;
using Xemio.PrincessDefense.Scenes;
using Xemio.PrincessDefense.Scenes.Components;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Levels;
using Xemio.PrincessDefense.Levels.Waves;

namespace Xemio.PrincessDefense.Entities.Environment
{
    public class WaveManager
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WaveManager"/> class.
        /// </summary>
        /// <param name="world">The world.</param>
        public WaveManager(World world)
        {
            this._spawnDelay = 0;
            this._waveIndex = 0;

            this._firstWave = true;
            this._entities = new List<Entity>();

            this.World = world;

            int waveIndex = 0;
            WaveInstruction wave;

            this._enemies = new List<int>();
            if (this.WaveProvider != null)
            {
                while ((wave = this.WaveProvider.CreateWave(waveIndex)) != null)
                {
                    this._enemies.Add(wave.EnemyCount);
                    waveIndex++;
                }
            }
        }
        #endregion

        #region Fields
        private int _waveIndex;
        private List<int> _enemies;

        private int _spawnIndex;
        private float _spawnDelay;
        private float _spawnElapsed;

        private float _nextWaveDelay;
        private float _nextWaveElapsed;

        private bool _nextWave;
        private bool _firstWave;

        private List<Entity> _entities;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the world.
        /// </summary>
        public World World { get; private set; }
        /// <summary>
        /// Gets the current wave.
        /// </summary>
        public WaveInstruction CurrentWave { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="WaveManager"/> is completed.
        /// </summary>
        public bool WaveCompleted
        {
            get 
            {
                if (this.WaveProvider == null) return true;
                if (this._waveIndex >= this._enemies.Count) return true;

                int enemyCount = this._enemies[this._waveIndex];
                bool allEnemiesSpawned = this._entities.Count == enemyCount;
                bool allEnemiesDestroyed = this._entities.All(e => e.IsDestroyed);

                return allEnemiesSpawned && allEnemiesDestroyed;
            }
        }
        /// <summary>
        /// Gets a value indicating whether the current level is completed.
        /// </summary>
        public bool LevelCompleted
        {
            get 
            { 
                bool hasNoWaves = this.WaveProvider == null;
                bool allWavesCompleted = this.CurrentWave == null && this.WaveCompleted && !this._firstWave;

                return hasNoWaves || allWavesCompleted;
            }
        }
        /// <summary>
        /// Gets the wave provider.
        /// </summary>
        public IWaveProvider WaveProvider
        {
            get { return this.World.Level.WaveProvider; }
        }
        /// <summary>
        /// Gets the scene manager.
        /// </summary>
        protected SceneManager SceneManager
        {
            get { return XGL.GetComponent<SceneManager>(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Announces the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void Announce(string message)
        {
            PrincessGame game = this.SceneManager.GetScene<PrincessGame>();

            if (game != null)
            {
                Announcer announcer = game.GetScene<Announcer>();
                announcer.Announce(message);
            }
        }
        /// <summary>
        /// Announces a wave.
        /// </summary>
        /// <param name="waveIndex">Index of the wave.</param>
        private void AnnounceWave(int waveIndex)
        {
            this.Announce("Wave " + (waveIndex + 1) + " incoming");
        }
        /// <summary>
        /// Announces a wave completion.
        /// </summary>
        private void AnnounceCompleted()
        {
            if (this._waveIndex + 1 == this._enemies.Count)
            {
                this.Announce("Congratulations! Level completed.");
            }
            else
            {
                this.Announce("Wave completed.");
            }
        }
        /// <summary>
        /// Processes the instruction.
        /// </summary>
        /// <param name="instruction">The instruction.</param>
        private void ProcessInstruction(SpawnInstruction instruction)
        {
            Vector2 top = new Vector2(Art.Map.Width * 0.5f, -100);
            Vector2 right = new Vector2(Art.Map.Width + 100, Art.Map.Height * 0.5f);
            Vector2 bottom = new Vector2(Art.Map.Width * 0.5f, Art.Map.Height + 100);
            Vector2 left = new Vector2(-100, Art.Map.Height * 0.5f);

            for (int t = 0; t < instruction.Top; t++)
                this.Add(instruction.Spawner.Spawn(top + new Vector2(0, -0.1f * t)));

            for (int r = 0; r < instruction.Right; r++)
                this.Add(instruction.Spawner.Spawn(right + new Vector2(0.1f * r, 0)));

            for (int b = 0; b < instruction.Bottom; b++)
                this.Add(instruction.Spawner.Spawn(bottom + new Vector2(0, 0.1f * b)));

            for (int l = 0; l < instruction.Left; l++)
                this.Add(instruction.Spawner.Spawn(left + new Vector2(-0.1f * l, 0)));
        }
        /// <summary>
        /// Spawns the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        private void Add(Entity entity)
        {
            this._entities.Add(entity);
            this.World.Add(entity);
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            if (this.WaveCompleted && !this._nextWave)
            {
                if (!this._firstWave)
                {
                    this.AnnounceCompleted();
                }

                if (this.CurrentWave != null)
                {
                    this._entities.Clear();

                    this._nextWaveDelay = 3000;
                    this._nextWaveElapsed = 0;
                    this._nextWave = true;
                }
            }

            if (this._nextWave || this._firstWave)
            {
                this._nextWaveElapsed += elapsed;

                if (this._nextWaveElapsed >= this._nextWaveDelay)
                {
                    if (!this._firstWave)
                    {
                        this._spawnIndex = 0;
                        this._waveIndex++;
                    }

                    if (this.WaveProvider != null)
                    {
                        this.CurrentWave = this.WaveProvider.CreateWave(this._waveIndex);
                        this.AnnounceWave(this._waveIndex);
                    }

                    this._spawnElapsed = 0;
                    this._spawnDelay = 0;

                    this._nextWave = false;
                    this._firstWave = false;
                }
            }
            
            if (this.CurrentWave != null)
            {
                this._spawnElapsed += elapsed;

                if (this._spawnElapsed >= this._spawnDelay &&
                    this._spawnIndex < this.CurrentWave.Spawns.Count)
                {
                    SpawnInstruction instruction = this.CurrentWave.Spawns[this._spawnIndex];
                    this.ProcessInstruction(instruction);

                    this._spawnDelay = instruction.NextSpawn;

                    this._spawnElapsed = 0;
                    this._spawnIndex++;
                }
            }
        }
        #endregion
    }
}
