using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;

namespace PrincessDefense.Entities.Components
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
        }
        #endregion

        #region Fields
        private int _maxHealth;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the health.
        /// </summary>
        public int Health { get; set; }
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

        #endregion
    }
}
