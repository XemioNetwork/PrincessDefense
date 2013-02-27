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
using Xemio.PrincessDefense.Scenes;
using Xemio.GameLibrary.Game;

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
            damage.Damage = 2;

            animation.Add(Art.HeroWalking);
            animation.Add(Art.HeroShooting);

            KnockbackComponent knockback = new KnockbackComponent(this);
            knockback.Entries.Add(new Knockback<Skeleton>(3));

            this.Components.Add(new InputComponent(this));
            this.Components.Add(new SpeedComponent(this));
            this.Components.Add(knockback);
            this.Components.Add(new ExperienceComponent(this));
            this.Components.Add(new RegenerationComponent(this));
            this.Components.Add(new BowComponent(this));

            this.Upgrades = new List<IUpgrade>();
            this.Upgrades.Add(new HealthUpgrade(this));
            this.Upgrades.Add(new RegenerationUpgrade(this));
            this.Upgrades.Add(new StrengthUpgrade(this));
            this.Upgrades.Add(new SpeedUpgrade(this));
            this.Upgrades.Add(new BowSpeedUpgrade(this));
            this.Upgrades.Add(new ArrowUpgrade(this));
            this.Upgrades.Add(new FireLionUpgrade(this));
            this.Upgrades.Add(new KnockbackUpgrade(this));
        }
        #endregion

        #region Fields
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
        /// Gets the upgrades.
        /// </summary>
        public List<IUpgrade> Upgrades { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public override void Destroy()
        {
            SceneManager sceneManager = XGL.GetComponent<SceneManager>();
            sceneManager.Add(new GameOver());

            base.Destroy();
        }
        #endregion
    }
}
