using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering.Sprites;
using PrincessDefense.Entities;
using PrincessDefense.Entities.Components;
using Xemio.GameLibrary.Math;
using PrincessDefense.Resources;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary;
using PrincessDefense.Entities.Events;

namespace PrincessDefense.Entities.Characters
{
    public class Player : Character
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
        {
            AnimationComponent animation = this.GetComponent<AnimationComponent>();

            HealthComponent health = this.GetComponent<HealthComponent>();
            health.MaxHealth = 20;

            this.LoadAnimations("Player", @"Resources\characters\heroWalking.png");
            this.Components.Add(new InputComponent(this));

            SpriteSheet shootUp = SpriteRegistry.Load("Player.ShootUp", @"Resources\characters\heroShooting.png", 64, 64, 0, 11);
            SpriteSheet shootLeft = SpriteRegistry.Load("Player.ShootLeft", @"Resources\characters\heroShooting.png", 64, 64, 13, 11);
            SpriteSheet shootDown = SpriteRegistry.Load("Player.ShootDown", @"Resources\characters\heroShooting.png", 64, 64, 26, 11);
            SpriteSheet shootRight = SpriteRegistry.Load("Player.ShootRight", @"Resources\characters\heroShooting.png", 64, 64, 39, 11);

            animation.Animations.Add(new SpriteAnimation("ShootUp", shootUp, 40, false));
            animation.Animations.Add(new SpriteAnimation("ShootLeft", shootLeft, 40, false));
            animation.Animations.Add(new SpriteAnimation("ShootDown", shootDown, 40, false));
            animation.Animations.Add(new SpriteAnimation("ShootRight", shootRight, 40, false));

            EventManager eventManager = XGL.GetComponent<EventManager>();
            eventManager.Subscribe<CollisionEvent>(this.OnCollide);
        }
        #endregion

        #region Fields
        private float _shootingTime;
        private float _shootingDelay;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the team.
        /// </summary>
        public override Team Team
        {
            get { return Team.Princess; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles an incoming collision event.
        /// </summary>
        /// <param name="collisionEvent">The collision event.</param>
        protected void OnCollide(CollisionEvent collisionEvent)
        {
            if (collisionEvent.Entity is Skeleton && collisionEvent.CollidingEntity is Player ||
                collisionEvent.Entity is Player && collisionEvent.CollidingEntity is Skeleton)
            {
                Skeleton skeleton = collisionEvent.Entity as Skeleton;
                if (skeleton == null)
                {
                    skeleton = collisionEvent.CollidingEntity as Skeleton;
                }

                PhysicsComponent physics = this.GetComponent<PhysicsComponent>();
                HealthComponent health = this.GetComponent<HealthComponent>();

                health.Health -= 2;

                Vector2 distance = this.Position - skeleton.Position;
                distance.Normalize();

                physics.Velocity = distance * 2;
                physics.Acceleration = distance * 4;

                if (health.Health <= 0)
                {
                    this.Destroy();
                }
            }
        }
        /// <summary>
        /// Shoots an arrow.
        /// </summary>
        public void Shoot(Direction direction)
        {
            if (!this.Locked && this._shootingDelay < 0)
            {
                Vector2 projectileDirection = this.GetDirection(direction);
                string animationName = "Shoot" + this.GetAnimationName(direction);

                AnimationComponent animation = this.GetComponent<AnimationComponent>();
                animation.PlayAnimation(animationName);

                this.Locked = true;
                this.Facing = direction;

                this._shootingTime = 40 * 10;
            }
        }
        /// <summary>
        /// Ticks the specified elapsed.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this._shootingDelay -= elapsed;

            if (this._shootingTime > 0)
            {
                this._shootingTime -= elapsed;
                if (this._shootingTime <= 0)
                {
                    this._shootingDelay = 100;
                    this._shootingTime = 0;

                    Projectile projectile = new Projectile(this.Facing);
                    projectile.Position = this.Position + new Vector2(0, 25);

                    this.Environment.Add(projectile);
                    this.Locked = false;
                }
            }
            base.Tick(elapsed);
        }
        #endregion
    }
}
