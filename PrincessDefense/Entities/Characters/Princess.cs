using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Rendering.Sprites;
using PrincessDefense.Entities.Components;
using PrincessDefense.Entities.Events;

namespace PrincessDefense.Entities.Characters
{
    public class Princess : Character
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Princess"/> class.
        /// </summary>
        public Princess()
        {
            this.Components.Add(new TargetingBehavior(this, Team.Princess, 32, 128));
            this.Components.Add(new PrincessBehavior(this));

            EventManager eventManager = XGL.GetComponent<EventManager>();
            eventManager.Subscribe<CollisionEvent>(this.OnCollide);

            this.LoadAnimations("Princess", @"Resources\characters\princess.png");
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the team.
        /// </summary>
        public override Team Team
        {
            get { return Team.Princess; }
        }
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
        /// Handles an entity collision.
        /// </summary>
        /// <param name="collisionEvent">The collision event.</param>
        protected void OnCollide(CollisionEvent collisionEvent)
        {
            if (collisionEvent.Entity is Princess && collisionEvent.CollidingEntity is Skeleton ||
                collisionEvent.Entity is Skeleton && collisionEvent.CollidingEntity is Princess)
            {
                this.Destroy();
            }
        }
        #endregion
    }
}
