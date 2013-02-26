using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;

namespace Xemio.PrincessDefense.Entities.Components
{
    public class CollidableComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CollidableComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="radius">The radius.</param>
        public CollidableComponent(Entity entity, float radius) : this(entity, radius, false)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CollidableComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="isStatic">if set to <c>true</c> [is static].</param>
        public CollidableComponent(Entity entity, float radius, bool isStatic) : base(entity)
        {
            this.Radius = radius;
            this.IsStatic = isStatic;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the collision radius.
        /// </summary>
        public float Radius { get; set; }
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public Vector2 Offset { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is static.
        /// </summary>
        public bool IsStatic { get; set; }
        #endregion
    }
}
