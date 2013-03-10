using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Rendering.Sprites;
using Xemio.PrincessDefense.Entities.Rendering;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Entities.Components.Attributes;
using Xemio.PrincessDefense.Entities.Particles;

namespace Xemio.PrincessDefense.Entities.Characters
{
    public class Character : BaseEntity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        public Character()
        {
            this.Renderer = new CharacterRenderer(this);

            this.Components.Add(new CollidableComponent(this, 16));
            this.Components.Add(new AnimationComponent(this));

            this.Components.Add(new HealthComponent(this));
            this.Components.Add(new DamageComponent(this));

            this.Components.Add(new PhysicsComponent(this));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the facing.
        /// </summary>
        public Direction Facing { get; set; }
        /// <summary>
        /// Gets a value indicating whether this instance is walking.
        /// </summary>
        public bool IsWalking { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Character"/> is locked.
        /// </summary>
        public bool Locked { get; set; }
        /// <summary>
        /// Gets the speed.
        /// </summary>
        public virtual float Speed
        {
            get { return 0.8f; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public override void Destroy()
        {
            if (!this.IsDestroyed)
            {
                Sounds.Play(Sounds.Explosion, this);

                ExplosionEmitter explosion = new ExplosionEmitter();
                explosion.Position = this.Position;

                this.Environment.Add(explosion);
            }

            base.Destroy();
        }
        /// <summary>
        /// Ticks the specified elapsed.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            AnimationComponent animation = this.GetComponent<AnimationComponent>();

            if (!this.IsWalking && !this.Locked)
            {
                animation.PlayAnimation("Idle" + DirectionHelper.GetAnimationName(this.Facing));
            }

            this.IsWalking = false;
            base.Tick(elapsed);
        }
        /// <summary>
        /// Accelerates the specified direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void Accelerate(Vector2 direction)
        {
            PhysicsComponent physics = this.GetComponent<PhysicsComponent>();
            physics.Velocity += direction * this.Speed;
        }
        /// <summary>
        /// Performs a walk.
        /// </summary>
        /// <param name="walkDirection">The direction.</param>
        public void Walk(Direction walkDirection)
        {
            if (!this.Locked)
            {
                Vector2 direction = DirectionHelper.GetDirection(walkDirection);
                direction.Normalize();

                this.Facing = walkDirection;
                this.IsWalking = true;

                this.Accelerate(direction);

                AnimationComponent animation = this.GetComponent<AnimationComponent>();
                animation.PlayAnimation("Walk" + DirectionHelper.GetAnimationName(this.Facing));
            }
        }
        /// <summary>
        /// Gets the direction.
        /// </summary>
        public Vector2 GetDirection()
        {
            return DirectionHelper.GetDirection(this.Facing);
        }
        #endregion
    }
}
