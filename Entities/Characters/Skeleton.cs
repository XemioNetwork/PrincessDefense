using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PrincessDefense.Entities.Components;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary;
using PrincessDefense.Entities.Events;

namespace PrincessDefense.Entities.Characters
{
    public class Skeleton : Character
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Skeleton"/> class.
        /// </summary>
        public Skeleton()
        {
            this.Components.Add(new TargetingBehavior(this, Team.Princess));

            EventManager eventManager = XGL.GetComponent<EventManager>();
            eventManager.Subscribe<CollisionEvent>(this.OnCollide);

            this.LoadAnimations("Skeleton", @"Resources\characters\skeleton.png");
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the speed.
        /// </summary>
        public override float Speed
        {
            get { return 0.2f; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles an entity event.
        /// </summary>
        /// <param name="collisionEvent">The collision event.</param>
        protected void OnCollide(CollisionEvent collisionEvent)
        {
            if (collisionEvent.Entity is Projectile && collisionEvent.CollidingEntity is Skeleton ||
                collisionEvent.Entity is Skeleton && collisionEvent.CollidingEntity is Projectile)
            {
                collisionEvent.Entity.Destroy();
                collisionEvent.CollidingEntity.Destroy();
            }
        }
        #endregion
    }
}
