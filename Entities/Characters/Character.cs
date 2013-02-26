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
        /// Ticks the specified elapsed.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            AnimationComponent animation = this.GetComponent<AnimationComponent>();

            if (!this.IsWalking && !this.Locked)
            {
                animation.PlayAnimation("Idle" + this.GetAnimationName(this.Facing));
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
                Vector2 direction = this.GetDirection(walkDirection);
                direction.Normalize();

                this.Facing = walkDirection;
                this.IsWalking = true;

                this.Accelerate(direction);

                AnimationComponent animation = this.GetComponent<AnimationComponent>();
                animation.PlayAnimation("Walk" + this.GetAnimationName(this.Facing));
            }
        }
        /// <summary>
        /// Gets the closest animation.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public Direction GetClosestFacing(Vector2 direction)
        {
            float angle = MathHelper.ToAngle(direction);
            float degrees = MathHelper.ToDegrees(angle);

            Direction result = Direction.Down;

            if (degrees >= 337.5 || degrees < 22.5) result = Direction.Left;
            if (degrees >= 22.5 && degrees < 67.5) result = Direction.LeftUp;
            if (degrees >= 67.5 && degrees < 112.5) result = Direction.Up;
            if (degrees >= 112.5 && degrees < 157.5) result = Direction.UpRight;
            if (degrees >= 157.5 && degrees < 202.5) result = Direction.Right;
            if (degrees >= 202.5 && degrees < 247.5) result = Direction.RightDown;
            if (degrees >= 247.5 && degrees < 292.5) result = Direction.Down;
            if (degrees >= 292.5 && degrees < 337.5) result = Direction.DownLeft;

            return result;
        }
        /// <summary>
        /// Gets the direction.
        /// </summary>
        public Vector2 GetDirection()
        {
            return this.GetDirection(this.Facing);
        }
        /// <summary>
        /// Gets the direction vector for a specific walkdirection.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public Vector2 GetDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    return new Vector2(0, 1);
                case Direction.DownLeft: 
                    return new Vector2(-1, 1);
                case Direction.Left: 
                    return new Vector2(-1, 0);
                case Direction.LeftUp: 
                    return new Vector2(-1, -1);
                case Direction.Right: 
                    return new Vector2(1, 0);
                case Direction.RightDown: 
                    return new Vector2(1, 1);
                case Direction.Up: 
                    return new Vector2(0, -1);
                case Direction.UpRight:
                    return new Vector2(1, -1);

                default: return Vector2.Zero;
            }
        }
        /// <summary>
        /// Gets the name of the animation.
        /// </summary>
        /// <param name="direction">The direction.</param>
        protected string GetAnimationName(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                case Direction.DownLeft:
                    return "Down";
                case Direction.Left:
                case Direction.LeftUp:
                    return "Left";
                case Direction.Right:
                case Direction.RightDown:
                    return "Right";
                case Direction.Up:
                case Direction.UpRight:
                    return "Up";

                default: return "None";
            }
        }
        #endregion
    }
}
