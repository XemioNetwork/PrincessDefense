using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math;

namespace Xemio.PrincessDefense.Entities.Events
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

        #region Methods
        /// <summary>
        /// Gets an entity by a specified type.
        /// </summary>
        public T Get<T>() where T : BaseEntity
        {
            if (this.Entity is T)
                return (T)this.Entity;
            if (this.CollidingEntity is T)
                return (T)this.CollidingEntity;

            return default(T);
        }
        /// <summary>
        /// Determines whether is an intersection of the specific type.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        public bool IsIntersectionOf<T1, T2>()
        {
            return this.Entity is T1 && this.CollidingEntity is T2 || 
                this.Entity is T2 && this.CollidingEntity is T1;
        }
        #endregion
    }
}
