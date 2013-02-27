using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;
using Xemio.PrincessDefense.Entities.Characters;

namespace Xemio.PrincessDefense.Entities.Components.Attributes
{
    public class BowComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BowComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public BowComponent(Entity entity) : base(entity)
        {
            this.ArrowsPerShot = 1;
            this.BowTime = 300;
        }
        #endregion

        #region Fields
        private float _shootingTime;
        private float _shootingDelay;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the arrows per shot.
        /// </summary>
        public int ArrowsPerShot { get; set; }
        /// <summary>
        /// Gets or sets the fire lion propability.
        /// </summary>
        public float FireLionPropability { get; set; }
        /// <summary>
        /// Gets or sets the time between shots.
        /// </summary>
        public int BowTime { get; set; }
        /// <summary>
        /// Gets the character.
        /// </summary>
        public Character Character
        {
            get { return this.Entity as Character; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Shoots an arrow.
        /// </summary>
        public void Shoot(Direction direction)
        {
            if (!this.Character.Locked && this._shootingDelay < 0)
            {
                Vector2 projectileDirection = DirectionHelper.GetDirection(direction);
                string animationName = "Shoot" + DirectionHelper.GetAnimationName(direction);

                AnimationComponent animation = this.Entity.GetComponent<AnimationComponent>();
                animation.PlayAnimation(animationName);

                this.Character.Locked = true;
                this.Character.Facing = direction;

                this._shootingTime = 30 * 10;
            }
        }
        /// <summary>
        /// Handles a game tick.
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
                    this._shootingDelay = this.BowTime;
                    this._shootingTime = 0;

                    float angle = MathHelper.ToRadians(30);
                    float segmentSize = angle / (float)this.ArrowsPerShot;
                    float maxAngle = (this.ArrowsPerShot - 1) * segmentSize;

                    for (int i = 0; i < this.ArrowsPerShot; i++)
                    {
                        Projectile projectile = new Projectile(this.Character, this.Character.Facing);
                        projectile.Position = this.Character.Position + new Vector2(0, 25);
                        projectile.Components.Add(new FireLionComponent(projectile, this.FireLionPropability));

                        float rotation = -maxAngle * 0.5f + segmentSize * i;
                        projectile.Vector = Vector2.Rotate(projectile.Vector, rotation);

                        this.Character.Environment.Add(projectile);
                    }

                    this.Character.Locked = false;
                }
            }
            base.Tick(elapsed);
        }
        #endregion
    }
}
