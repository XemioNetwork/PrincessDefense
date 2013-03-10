using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Entities.Spells;

namespace Xemio.PrincessDefense.Entities.Enemies
{
    public class Enemy : Character
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        public Enemy()
        {
            KnockbackComponent knockback = new KnockbackComponent(this);
            knockback.Entries.Add(new Knockback<Projectile>(4));
            knockback.Entries.Add(new Knockback<Spell>(3));

            this.Components.Add(knockback);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the team.
        /// </summary>
        public override Team Team
        {
            get { return Team.Enemies; }
        }
        #endregion
    }
}
