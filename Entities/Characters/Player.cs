using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering.Sprites;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary;
using Xemio.PrincessDefense.Entities.Events;
using Xemio.PrincessDefense.Entities.Upgrades;
using Xemio.PrincessDefense.Entities;
using Xemio.PrincessDefense.Entities.Components;

namespace Xemio.PrincessDefense.Entities.Characters
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
            DamageComponent damage = this.GetComponent<DamageComponent>();

            health.MaxHealth = 20;
            damage.Damage = 1;

            animation.Add(Art.HeroWalking);
            animation.Add(Art.HeroShooting);

            KnockbackComponent knockback = new KnockbackComponent(this);
            knockback.Entries.Add(new Knockback<Skeleton>(3));

            this.Components.Add(new InputComponent(this));
            this.Components.Add(new SpeedComponent(this));
            this.Components.Add(knockback);
            this.Components.Add(new ExperienceComponent(this));

            this.Upgrades = new List<IUpgrade>();
            this.Upgrades.Add(new StrengthUpgrade(this));
            this.Upgrades.Add(new SpeedUpgrade(this));
            this.Upgrades.Add(new MultipleArrowUpgrade(this));

            this.ArrowsPerShot = 3;
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
        /// <summary>
        /// Gets the speed.
        /// </summary>
        public override float Speed
        {
            get { return this.GetComponent<SpeedComponent>().Speed; }
        }
        /// <summary>
        /// Gets or sets the arrows per shot.
        /// </summary>
        public int ArrowsPerShot { get; set; }
        /// <summary>
        /// Gets the upgrades.
        /// </summary>
        public List<IUpgrade> Upgrades { get; private set; }
        #endregion

        #region Methods
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

                    float maxAngle = MathHelper.ToRadians(45);
                    float segmentSize = maxAngle / (float)this.ArrowsPerShot;

                    Projectile projectile = new Projectile(this, this.Facing);
                    projectile.Position = this.Position + new Vector2(0, 25);

                    /*projectile.Vector += MathHelper.ToVector(segmentSize * i);
                    projectile.Vector.Normalize();*/

                    this.Environment.Add(projectile);

                    this.Locked = false;
                }
            }
            base.Tick(elapsed);
        }
        #endregion
    }
}
