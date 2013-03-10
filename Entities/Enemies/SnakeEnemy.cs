using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Entities.Components.Attributes;
using Xemio.PrincessDefense.Entities.Components.Behavior;

namespace Xemio.PrincessDefense.Entities.Enemies
{
    public class SnakeEnemy : Enemy
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SnakeEnemy"/> class.
        /// </summary>
        public SnakeEnemy()
        {
            AnimationComponent animations = this.GetComponent<AnimationComponent>();
            animations.Add(Art.Snake);

            HealthComponent health = this.GetComponent<HealthComponent>();
            health.SetHealth(4);

            DamageComponent damage = this.GetComponent<DamageComponent>();
            damage.Damage = 1;
            damage.DamageOnImpact = true;

            this.Components.Add(new TargetingBehavior(this, Team.Princess));
            this.Components.Add(new ExperienceComponent(this, 4));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the speed.
        /// </summary>
        public override float Speed
        {
            get { return 0.2f; }
        }
        #endregion
    }
}
