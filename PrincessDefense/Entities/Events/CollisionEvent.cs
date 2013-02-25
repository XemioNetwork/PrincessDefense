using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math;

namespace PrincessDefense.Entities.Events
{
    public class CollisionEvent : Event
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CollisionEvent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="collidingEntity">The colliding entity.</param>
        public CollisionEvent(BaseEntity entity, BaseEntity collidingEntity, Vector2 translation)
        {
            this.Entity = entity;
            this.CollidingEntity = collidingEntity;
            this.Translation = translation;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public BaseEntity Entity { get; private set; }
        /// <summary>
        /// Gets the colliding entity.
        /// </summary>
        public BaseEntity CollidingEntity { get; private set; }
        /// <summary>
        /// Gets the translation vector.
        /// </summary>
        public Vector2 Translation { get; private set; }
        #endregion
    }
}
