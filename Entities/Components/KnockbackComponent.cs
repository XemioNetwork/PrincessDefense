using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math;
using Xemio.PrincessDefense.Entities.Events;

namespace Xemio.PrincessDefense.Entities.Components
{
    public class KnockbackComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="KnockbackComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public KnockbackComponent(Entity entity) : base(entity)
        {
            this.Entries = new List<Knockback>();

            EventManager eventManager = XGL.GetComponent<EventManager>();
            eventManager.Subscribe<CollisionEvent>(this.OnCollide);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the entries.
        /// </summary>
        public List<Knockback> Entries { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the knockback for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        public Knockback GetKnockback(Type type)
        {
            return this.Entries.FirstOrDefault(k => k.Type == type);
        }
        /// <summary>
        /// Handles an entity collision.
        /// </summary>
        /// <param name="collisionEvent">The collision event.</param>
        protected void OnCollide(CollisionEvent collisionEvent)
        {
            if (collisionEvent.Entity == this.Entity ||
                collisionEvent.CollidingEntity == this.Entity)
            {
                Entity entity = null;

                Type entityType = collisionEvent.Entity.GetType();
                Type collidingType = collisionEvent.CollidingEntity.GetType();

                foreach (Knockback knockback in this.Entries)
                {
                    if (knockback.Type.IsAssignableFrom(entityType))
                    {
                        entity = collisionEvent.Entity;
                        break;
                    }
                    if (knockback.Type.IsAssignableFrom(collidingType))
                    {
                        entity = collisionEvent.CollidingEntity;
                        break;
                    }
                }

                if (entity != null)
                {
                    PhysicsComponent physics = this.Entity.GetComponent<PhysicsComponent>();
                    float strength = this.GetKnockback(entity.GetType()).Strength;

                    Vector2 distance = this.Entity.Position - entity.Position;
                    distance.Normalize();

                    physics.Velocity += distance * strength * 0.5f;
                    physics.Acceleration += distance * strength;
                }
            }
        }
        /// <summary>
        /// Ticks the specified elapsed.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            base.Tick(elapsed);
        }
        #endregion
    }
}
