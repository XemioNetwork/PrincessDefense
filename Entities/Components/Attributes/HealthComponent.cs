using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Events;
using Xemio.PrincessDefense.Entities.Events;

namespace Xemio.PrincessDefense.Entities.Components.Attributes
{
    public class HealthComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HealthComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public HealthComponent(Entity entity) : base(entity)
        {
            this.Health = -1;

            EventManager eventManager = XGL.GetComponent<EventManager>();
            eventManager.Subscribe<CollisionEvent>(this.OnCollide);
        }
        #endregion

        #region Fields
        private int _maxHealth;
        private float _elapsed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HealthComponent"/> is invincible.
        /// </summary>
        public bool Invincible { get; set; }
        /// <summary>
        /// Gets or sets the health.
        /// </summary>
        public int Health { get; set; }
        /// <summary>
        /// Gets a value indicating whether this instance is hurt.
        /// </summary>
        public bool IsHurt { get; private set; }
        /// <summary>
        /// Gets the hurt percentage.
        /// </summary>
        public float HurtPercentage { get; private set; }
        /// <summary>
        /// Gets or sets the max health.
        /// </summary>
        public int MaxHealth 
        {
            get { return this._maxHealth; }
            set
            {
                this._maxHealth = value;
                if (this.Health < 0)
                {
                    this.Health = this._maxHealth;
                }
            }
        }
        /// <summary>
        /// Gets the percentage.
        /// </summary>
        public float Percentage
        {
            get { return this.Health / (float)this.MaxHealth; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Heals this entity.
        /// </summary>
        public void Heal()
        {
            if (this.Health < this.MaxHealth)
            {
                this.Health++;
            }
        }
        /// <summary>
        /// Sets the health.
        /// </summary>
        /// <param name="health">The health.</param>
        public void SetHealth(int health)
        {
            if (health > this.MaxHealth)
            {
                this.MaxHealth = health;
            }

            this.Health = health;
        }
        /// <summary>
        /// Hurts the entity.
        /// </summary>
        /// <param name="damage">The damage.</param>
        public void Hurt(int damage)
        {
            this._elapsed = 0;

            if (!this.Invincible)
            {
                if (damage > 0)
                {
                    this.Health -= damage;
                    this.IsHurt = true;

                    if (this.Health > 0)
                    {
                        Sounds.Play(Sounds.Hit, this.Entity);
                    }
                }

                if (this.Health <= 0)
                {
                    this.Entity.Destroy();
                }
            }
        }
        /// <summary>
        /// Ticks the specified elapsed.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this.IsHurt)
            {
                this._elapsed += elapsed;
                if (this._elapsed >= 500.0f)
                {
                    this._elapsed = 0;
                    this.IsHurt = false;
                }

                this.HurtPercentage = this._elapsed / 500.0f;
            }
        }
        /// <summary>
        /// Handles an entity collision.
        /// </summary>
        /// <param name="collisionEvent">The collision event.</param>
        protected void OnCollide(CollisionEvent collisionEvent)
        {
            if (collisionEvent.Entity.Team == collisionEvent.CollidingEntity.Team)
                return;
            
            if (collisionEvent.Entity == this.Entity ||
                collisionEvent.CollidingEntity == this.Entity)
            {
                Entity collidingEntity = collisionEvent.CollidingEntity;
                if (collidingEntity == this.Entity)
                {
                    collidingEntity = collisionEvent.Entity;
                }

                DamageComponent damageComponent = collidingEntity.GetComponent<DamageComponent>();
                int damageAmount = 0;

                if (damageComponent != null && damageComponent.DamageOnImpact)
                {
                    damageAmount = damageComponent.Damage;
                }

                BaseEntity entity = collidingEntity as BaseEntity;
                while (entity.Owner != null)
                {
                    entity = entity.Owner;
                }

                this.Hurt(damageAmount);
                if (this.Entity.IsDestroyed)
                {
                    ExperienceComponent targetExperience = entity.GetComponent<ExperienceComponent>();
                    ExperienceComponent sourceExperience = this.Entity.GetComponent<ExperienceComponent>();

                    if (targetExperience != null && sourceExperience != null)
                    {
                        targetExperience.Add(sourceExperience.Experience);
                    }
                }
            }
        }
        #endregion
    }
}
