using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Rendering.Sprites;
using Xemio.PrincessDefense.Entities.Events;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Entities.Components.Behavior;
using Xemio.PrincessDefense.Entities.Components.Attributes;

namespace Xemio.PrincessDefense.Entities.Characters
{
    public class Princess : Character
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Princess"/> class.
        /// </summary>
        public Princess()
        {
            this.Components.Add(new TargetingBehavior(this, Team.Princess, 32, 128));
            this.Components.Add(new PrincessBehavior(this));

            HealthComponent health = this.GetComponent<HealthComponent>();
            health.SetHealth(10);

            AnimationComponent animations = this.GetComponent<AnimationComponent>();
            animations.Add(Art.Princess);
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
            get { return 0.2f; }
        }
        #endregion
    }
}
