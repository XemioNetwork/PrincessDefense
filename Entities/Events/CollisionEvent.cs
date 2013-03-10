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
            return (T)this.Get(typeof(T));
        }
        /// <summary>
        /// Gets an entity by a specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public BaseEntity Get(Type type)
        {
            if (type.IsAssignableFrom(this.Entity.GetType()))
                return this.Entity;
            if (type.IsAssignableFrom(this.CollidingEntity.GetType()))
                return this.CollidingEntity;

            return null;
        }
        /// <summary>
        /// Determines whether the collision contains an entity with the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        public bool Has(Type type)
        {
            if (type.IsAssignableFrom(this.Entity.GetType())) return true;
            if (type.IsAssignableFrom(this.CollidingEntity.GetType())) return true;

            return false;
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
