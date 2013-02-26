using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Rendering.Sprites;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Math;
using PrincessDefense.Entities.Events;
using PrincessDefense.Entities.Rendering;
using PrincessDefense.Entities.Components;

namespace PrincessDefense.Entities
{
    public class Tree : BaseEntity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Tree"/> class.
        /// </summary>
        public Tree()
        {
            AnimationComponent animation = new AnimationComponent(this);
            SpriteSheet idle = new SpriteSheet(@"Resources\terrain\tree.png", 95, 117, 0, 1);

            animation.Add(new SpriteAnimation("Idle", idle, 100));
            animation.PlayAnimation("Idle");

            CollidableComponent collision = new CollidableComponent(this, 20, true);
            collision.Offset = new Vector2(0, 20);

            this.Components.Add(collision);
            this.Components.Add(animation);

            EventManager eventManager = XGL.GetComponent<EventManager>();
            eventManager.Subscribe<CollisionEvent>(this.OnCollide);

            this.Renderer = new TreeRenderer(this);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the team.
        /// </summary>
        public override Team Team
        {
            get { return Team.Neutral; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles an entity collision.
        /// </summary>
        /// <param name="collisionEvent">The collision event.</param>
        protected void OnCollide(CollisionEvent collisionEvent)
        {
            if (collisionEvent.Entity is Tree && collisionEvent.CollidingEntity is Projectile ||
                collisionEvent.Entity is Projectile && collisionEvent.CollidingEntity is Tree)
            {
                Projectile projectile = collisionEvent.Entity as Projectile;
                if (projectile == null)
                {
                    projectile = collisionEvent.CollidingEntity as Projectile;
                }

                projectile.Destroy();
            }
        }
        #endregion
    }
}
