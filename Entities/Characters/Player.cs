using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering.Sprites;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Common;
using Xemio.PrincessDefense.Scenes;
using Xemio.PrincessDefense.Entities;
using Xemio.PrincessDefense.Entities.Events;
using Xemio.PrincessDefense.Entities.Upgrades;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Entities.Components.Attributes;
using Xemio.PrincessDefense.Entities.Enemies;

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
            knockback.Entries.Add(new Knockback<Enemy>(3));

            this.Components.Add(new InputComponent(this));
            this.Components.Add(new SpeedComponent(this));
            this.Components.Add(knockback);
            this.Components.Add(new ExperienceComponent(this));
            this.Components.Add(new RegenerationComponent(this));
            this.Components.Add(new BowComponent(this));

            this.Upgrades = new List<IUpgrade>();
        }
        #endregion

        #region Fields
        #endregion

        #region Singleton
        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static Player Instance
        {
            get { return Singleton<Player>.Value; }
        }
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
        /// Initializes the upgrades.
        /// </summary>
        public void InitializeUpgrades(int count)
        {
            IList<IUpgrade> availableUpgrades = UpgradeManager.GetAvailableUpgrades();
            if (availableUpgrades.Count >= count)
            {
                this.Upgrades = availableUpgrades.Take(count).ToList();
            }
        }
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
