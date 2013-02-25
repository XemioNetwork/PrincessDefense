using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Rendering.Sprites;
using PrincessDefense.Resources;
using PrincessDefense.Entities.Rendering;
using PrincessDefense.Entities.Components;

namespace PrincessDefense.Entities.Characters
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
            if (!this.IsWalking && !this.Locked)
            {
                AnimationComponent animation = this.GetComponent<AnimationComponent>();
                animation.PlayAnimation("Idle" + this.GetAnimationName(this.Facing));
            }

            this.IsWalking = false;
            base.Tick(elapsed);
        }
        /// <summary>
        /// Loads the animations.
        /// </summary>
        /// <param name="registryID">The registry ID.</param>
        /// <param name="fileName">The name.</param>
        protected void LoadAnimations(string registryID, string fileName)
        {
            this.LoadAnimations(registryID, fileName, 50);
        }
        /// <summary>
        /// Loads the animations.
        /// </summary>
        /// <param name="registryID">The registry ID.</param>
        /// <param name="fileName">The name.</param>
        /// <param name="frameTime">The framerate.</param>
        protected void LoadAnimations(string registryID, string fileName, int frameTime)
        {
            AnimationComponent component = this.GetComponent<AnimationComponent>();

            SpriteSheet idleUp = SpriteRegistry.Load(registryID + ".IdleUp", fileName, 64, 64, 0, 1);
            SpriteSheet idleLeft = SpriteRegistry.Load(registryID + ".IdleLeft", fileName, 64, 64, 9, 1);
            SpriteSheet idleDown = SpriteRegistry.Load(registryID + ".IdleDown", fileName, 64, 64, 18, 1);
            SpriteSheet idleRight = SpriteRegistry.Load(registryID + ".IdleRight", fileName, 64, 64, 27, 1);

            SpriteSheet walkUp = SpriteRegistry.Load(registryID + ".WalkUp", fileName, 64, 64, 0, 9);
            SpriteSheet walkLeft = SpriteRegistry.Load(registryID + ".WalkLeft", fileName, 64, 64, 9, 9);
            SpriteSheet walkDown = SpriteRegistry.Load(registryID + ".WalkDown", fileName, 64, 64, 18, 9);
            SpriteSheet walkRight = SpriteRegistry.Load(registryID + ".WalkRight", fileName, 64, 64, 27, 9);

            component.Animations.Add(new SpriteAnimation("IdleUp", idleUp, frameTime));
            component.Animations.Add(new SpriteAnimation("IdleLeft", idleLeft, frameTime));
            component.Animations.Add(new SpriteAnimation("IdleDown", idleDown, frameTime));
            component.Animations.Add(new SpriteAnimation("IdleRight", idleRight, frameTime));

            component.Animations.Add(new SpriteAnimation("WalkUp", walkUp, frameTime));
            component.Animations.Add(new SpriteAnimation("WalkLeft", walkLeft, frameTime));
            component.Animations.Add(new SpriteAnimation("WalkDown", walkDown, frameTime));
            component.Animations.Add(new SpriteAnimation("WalkRight", walkRight, frameTime));
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

            if (degrees >= 315 || degrees < 45) result = Direction.Left;
            if (degrees >= 45 && degrees < 135) result = Direction.Up;
            if (degrees >= 135 && degrees < 225) result = Direction.Right;
            if (degrees >= 225 && degrees < 315) result = Direction.Down;

            return result;
        }
        /// <summary>
        /// Gets the direction vector for a specific walkdirection.
        /// </summary>
        /// <param name="direction">The direction.</param>
        protected Vector2 GetDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down: return new Vector2(0, 1);
                case Direction.DownLeft: return new Vector2(-1, 1);
                case Direction.Left: return new Vector2(-1, 0);
                case Direction.LeftUp: return new Vector2(-1, -1);
                case Direction.Right: return new Vector2(1, 0);
                case Direction.RightDown: return new Vector2(1, 1);
                case Direction.Up: return new Vector2(0, -1);
                case Direction.UpRight: return new Vector2(1, -1);

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
