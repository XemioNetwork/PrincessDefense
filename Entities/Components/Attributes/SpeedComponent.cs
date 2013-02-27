using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;

namespace Xemio.PrincessDefense.Entities.Components.Attributes
{
    public class SpeedComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SpeedComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public SpeedComponent(Entity entity) : base(entity)
        {
            this.Speed = 0.8f;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        public float Speed { get; set; }
        #endregion
    }
}
