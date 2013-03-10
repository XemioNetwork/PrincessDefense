using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;

namespace Xemio.PrincessDefense.Entities.Components
{
    public class DamageComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DamageComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public DamageComponent(Entity entity) : base(entity)
        {            
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DamageComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="damage">The damage.</param>
        public DamageComponent(Entity entity, int damage) : this(entity, damage, false)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DamageComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="damage">The damage.</param>
        /// <param name="damageOnContact">if set to <c>true</c> the entity damages other entites on contact.</param>
        public DamageComponent(Entity entity, int damage, bool damageOnContact) : base(entity)
        {
            this.Damage = damage;
            this.DamageOnImpact = true;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        public int Damage { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the entity damages other entities on contact.
        /// </summary>
        public bool DamageOnImpact { get; set; }
        #endregion
    }
}
