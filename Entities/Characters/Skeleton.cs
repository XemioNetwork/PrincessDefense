using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary;
using Xemio.PrincessDefense.Entities.Events;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Entities.Components.Behavior;

namespace Xemio.PrincessDefense.Entities.Characters
{
    public class Skeleton : Character
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Skeleton"/> class.
        /// </summary>
        public Skeleton()
        {
            AnimationComponent animation = this.GetComponent<AnimationComponent>();
            animation.Add(Art.Skeleton);

            KnockbackComponent knockback = new KnockbackComponent(this);
            knockback.Entries.Add(new Knockback<Projectile>(3));

            DamageComponent damage = this.GetComponent<DamageComponent>();
            damage.Damage = 2;
            damage.DamageOnContact = true;

            HealthComponent health = this.GetComponent<HealthComponent>();
            health.SetHealth(3);

            this.Components.Add(knockback);
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
            get { return 0.2f; }
        }
        #endregion
    }
}
