using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.GameLibrary.Common.Randomization;

namespace Xemio.PrincessDefense.Entities.Spells
{
    public class LightningclawSpawner : BaseEntity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LightningclawSpawner"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public LightningclawSpawner(Player owner)
        {
            this.ThunderclawCount = 10;
            this.Delay = 500;

            this._owner = owner;
            this._elapsed = this.Delay;
            this._random = new RandomProxy();
        }
        #endregion

        #region Fields
        private Player _owner;
        private IRandom _random;

        private int _currentIndex;
        private float _elapsed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the thunderclaw count.
        /// </summary>
        public int ThunderclawCount { get; set; }
        /// <summary>
        /// Gets the angle.
        /// </summary>
        public float Angle
        {
            get { return this._currentIndex * this.Increment; }
        }
        /// <summary>
        /// Gets the increment.
        /// </summary>
        public float Increment
        {
            get { return MathHelper.TwoPi / this.ThunderclawCount; }
        }
        /// <summary>
        /// Gets or sets the delay.
        /// </summary>
        public float Delay { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this._elapsed += elapsed;
            if (this._elapsed >= this.Delay)
            {
                this._elapsed = 0;
                this._currentIndex = this._random.Next(0, this.ThunderclawCount);

                if (this._random.NextBoolean(1.0f - 1 / 30.0f))
                {
                    LightningclawSpell spell = new LightningclawSpell(this._owner);

                    Vector2 sourceVector = new Vector2(0, -this._random.Next(40, 100));
                    Vector2 rotationVector = Vector2.Rotate(sourceVector, this.Angle);

                    spell.Position = this.Position + rotationVector;

                    this.Environment.Add(spell);
                }
                else
                {
                    this.Destroy();
                }
            }

            base.Tick(elapsed);
        }
        #endregion
    }
}
