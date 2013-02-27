using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;

namespace Xemio.PrincessDefense.Entities.Components.Attributes
{
    public class ProjectileKnockback : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes the <see cref="ProjectileKnockback"/> class.
        /// </summary>
        static ProjectileKnockback()
        {
            Strength = 4;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileKnockback"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public ProjectileKnockback(Entity entity) : base(entity)
        {
        }
        #endregion

        #region Static Properties
        /// <summary>
        /// Gets or sets the strength.
        /// </summary>
        public static int Strength { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            KnockbackComponent knockback = this.Entity.GetComponent<KnockbackComponent>();
            Knockback projectileKnockback = knockback.GetKnockback(typeof(Projectile));

            projectileKnockback.Strength = ProjectileKnockback.Strength;
        }
        #endregion
    }
}
