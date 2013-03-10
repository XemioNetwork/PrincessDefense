using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Entities.Components.Behavior;
using Xemio.PrincessDefense.Entities.Components.Attributes;

namespace Xemio.PrincessDefense.Entities.Enemies
{
    public class SlimeEnemy : Enemy
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SlimeEnemy"/> class.
        /// </summary>
        public SlimeEnemy()
        {
            AnimationComponent animations = this.GetComponent<AnimationComponent>();
            animations.Add(Art.Slime);

            HealthComponent health = this.GetComponent<HealthComponent>();
            health.SetHealth(1);

            DamageComponent damage = this.GetComponent<DamageComponent>();
            damage.Damage = 1;
            damage.DamageOnImpact = true;

            this.Components.Add(new TargetingBehavior(this, Team.Princess));
            this.Components.Add(new ExperienceComponent(this, 1));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the speed.
        /// </summary>
        public override float Speed
        {
            get { return 0.15f; }
        }
        #endregion
    }
}
