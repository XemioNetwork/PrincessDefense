using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.PrincessDefense.Entities.Particles;
using Xemio.GameLibrary.Rendering;

namespace Xemio.PrincessDefense.Entities.Rendering
{
    public class ParticleRenderer : EntityRenderer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleRenderer"/> class.
        /// </summary>
        /// <param name="particle">The particle.</param>
        public ParticleRenderer(Particle particle) : base(particle)
        {
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the particle.
        /// </summary>
        public Particle Particle
        {
            get { return this.Entity as Particle; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            this.Geometry.FillCircle(
                this.Geometry.Factory.CreateSolid(this.Particle.Color),
                this.Particle.Position,
                this.Particle.Radius);
        }
        #endregion
    }
}
