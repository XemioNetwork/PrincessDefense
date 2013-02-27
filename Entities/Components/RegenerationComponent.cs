using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;

namespace Xemio.PrincessDefense.Entities.Components
{
    public class RegenerationComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RegenerationComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public RegenerationComponent(Entity entity) : base(entity)
        {
            this.HealTime = -1;
        }
        #endregion

        #region Fields
        private float _elapsed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the heal time.
        /// </summary>
        public float HealTime { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this.HealTime < 0) 
                return;

            this._elapsed += elapsed;
            if (this._elapsed >= this.HealTime)
            {
                this._elapsed = 0;

                HealthComponent health = this.Entity.GetComponent<HealthComponent>();
                health.Heal();
            }
        }
        #endregion
    }
}
