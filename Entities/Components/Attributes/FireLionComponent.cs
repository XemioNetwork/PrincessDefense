using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Common.Randomization;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary;
using Xemio.PrincessDefense.Entities.Events;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Entities.Spells;

namespace Xemio.PrincessDefense.Entities.Components.Attributes
{
    public class FireLionComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FireLionComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="propability">The propability.</param>
        public FireLionComponent(Entity entity, float propability) : base(entity)
        {
            EventManager eventManager = XGL.GetComponent<EventManager>();
            eventManager.Subscribe<CollisionEvent>(this.OnCollide);

            this.Propability = propability;
        }
        #endregion

        #region Fields
        private static IRandom random = new RandomProxy();
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the propability.
        /// </summary>
        public float Propability { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles an entity collision.
        /// </summary>
        /// <param name="collisionEvent">The collision event.</param>
        protected void OnCollide(CollisionEvent collisionEvent)
        {
            if (collisionEvent.IsIntersectionOf<Projectile, Skeleton>())
            {
                if (!random.NextBoolean(this.Propability)) return;

                Projectile projectile = collisionEvent.Get<Projectile>();
                if (projectile == this.Entity)
                {
                    FireLion lion = new FireLion(
                        projectile.Owner as Player,
                        projectile.Vector * projectile.Speed * 0.4f,
                        projectile.Facing);

                    lion.Position = projectile.Position;

                    this.Entity.Environment.Add(lion);
                }
            }
        }
        #endregion
    }
}
