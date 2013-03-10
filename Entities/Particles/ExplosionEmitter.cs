using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Randomization;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;

namespace Xemio.PrincessDefense.Entities.Particles
{
    public class ExplosionEmitter : BaseEntity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ExplosionEmitter"/> class.
        /// </summary>
        public ExplosionEmitter()
        {
            this.ParticleCount = 10;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the particle count.
        /// </summary>
        public int ParticleCount { get; set; }
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
            IRandom random = new RandomProxy();
            for (int i = 0; i < this.ParticleCount; i++)
            {
                Particle particle = new Particle();
                particle.Velocity = new Vector2(
                    random.NextFloat() * 2 - 1.0f,
                    random.NextFloat() * 2 - 1.0f);

                particle.Velocity *= random.NextFloat() * 3 + 1;
                particle.Position = this.Position;

                particle.LifeTime = 100;
                particle.Radius = random.Next(5, 10);
                particle.Color = new Color(0.2f, 0.2f, 0.2f, 1.0f);

                this.Environment.Add(particle);
            }

            this.Destroy();
            base.Tick(elapsed);
        }
        #endregion
    }
}
