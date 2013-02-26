using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary;
using Xemio.PrincessDefense.Scenes;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.GameLibrary.Math;

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
            this.World = world;
            this.WaveDelay = 3000;
        }
        #endregion

        #region Fields
        private float _elapsed = 2000;
        private float _spawnRate = 1;

        private int _waveIndex = 1;
        private int _lastCount = -1;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the world.
        /// </summary>
        public World World { get; private set; }
        /// <summary>
        /// Gets or sets the wave delay.
        /// </summary>
        public float WaveDelay { get; set; }
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
        /// Announces a wave.
        /// </summary>
        /// <param name="announcer">The announcer.</param>
        /// <param name="waveIndex">Index of the wave.</param>
        private void AnnounceWave(Announcer announcer, int waveIndex)
        {
            announcer.Announce("Wave " + waveIndex + " incoming");
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            Announcer announcer = (Announcer)this.SceneManager.GetScene(a => a is Announcer);

            int count = this.World.Count(e => e is Skeleton);
            if (count == 0 && this._lastCount != 0 && this._waveIndex > 1)
            {
                announcer.Announce("Wave completed.");
            }

            this._lastCount = count;

            if (count == 0)
            {
                this._elapsed += elapsed;
                if (this._elapsed >= this.WaveDelay)
                {
                    for (int i = 0; i < this._spawnRate; i++)
                    {
                        this.World.Add(new Skeleton { Position = new Vector2(320 + i, -200) });
                        this.World.Add(new Skeleton { Position = new Vector2(-200, 320 + i) });
                        this.World.Add(new Skeleton { Position = new Vector2(320 + i, 840) });
                        this.World.Add(new Skeleton { Position = new Vector2(840, 320 + i) });
                    }

                    this.AnnounceWave(announcer, this._waveIndex);

                    this._waveIndex++;
                    this._spawnRate += 0.5f;
                    this._elapsed = 0;
                }
            }
        }
        #endregion
    }
}
