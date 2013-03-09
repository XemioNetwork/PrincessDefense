using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.PrincessDefense.Entities.Rendering;
using Xemio.GameLibrary.Rendering;

namespace Xemio.PrincessDefense.Entities.Particles
{
    public class Particle : BaseEntity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Particle"/> class.
        /// </summary>
        public Particle()
        {
            this.Renderer = new ParticleRenderer(this);
        }
        #endregion

        #region Fields
        private float _elapsed;
        private float _elapsedFadeTime;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the life time.
        /// </summary>
        public float LifeTime { get; set; }
        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        public Vector2 Velocity { get; set; }
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        public float Radius { get; set; }
        /// <summary>
        /// Gets the team.
        /// </summary>
        public override Team Team
        {
            get { return Team.None; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this._elapsed += elapsed;
            this.Position += this.Velocity;

            if (this._elapsed >= this.LifeTime)
            {
                this.Radius -= 0.5f;
                if (this.Radius <= 0)
                {
                    this.Destroy();
                }
            }

            base.Tick(elapsed);
        }
        #endregion
    }
}
