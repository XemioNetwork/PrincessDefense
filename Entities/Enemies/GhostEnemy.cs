using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Randomization;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Entities.Components.Behavior;
using Xemio.PrincessDefense.Entities.Components.Attributes;
using Xemio.PrincessDefense.Entities.Rendering;

namespace Xemio.PrincessDefense.Entities.Enemies
{
    public class GhostEnemy : Enemy
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SlimeEnemy"/> class.
        /// </summary>
        public GhostEnemy()
        {
            AnimationComponent animations = this.GetComponent<AnimationComponent>();
            animations.Add(Art.Ghost);

            HealthComponent health = this.GetComponent<HealthComponent>();
            health.SetHealth(6);

            DamageComponent damage = this.GetComponent<DamageComponent>();
            damage.Damage = 2;
            damage.DamageOnImpact = true;

            this.Components.Add(new TargetingBehavior(this, Team.Princess));
            this.Components.Add(new ExperienceComponent(this, 8));

            this._random = new RandomProxy();

            CollidableComponent collision = this.GetComponent<CollidableComponent>();
            collision.IsSeperating = false;

            this.Renderer = new GhostRenderer(this);
        }
        #endregion

        #region Fields
        private IRandom _random;
        private float _elapsedChangeTime;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this instance is ghosty.
        /// </summary>
        public bool IsInvincible { get; set; }
        /// <summary>
        /// Gets the speed.
        /// </summary>
        public override float Speed
        {
            get { return this.IsInvincible ? 0.2f : 0.1f; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Changes the ghosty state.
        /// </summary>
        private void TryChangeInvincibility()
        {
            if (this._elapsedChangeTime > 2000)
            {
                this._elapsedChangeTime = 0;
                this.IsInvincible = this._random.NextBoolean(0.3f);

                HealthComponent health = this.GetComponent<HealthComponent>();
                health.Invincible = this.IsInvincible;
            }
        }
        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public override void Destroy()
        {
            if (!this.IsInvincible)
            {
                base.Destroy();
            }
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this._elapsedChangeTime += elapsed;

            this.TryChangeInvincibility();
            base.Tick(elapsed);
        }
        #endregion
    }
}
