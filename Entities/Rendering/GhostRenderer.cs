using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.PrincessDefense.Entities.Enemies;

namespace Xemio.PrincessDefense.Entities.Rendering
{
    public class GhostRenderer : CharacterRenderer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GhostRenderer"/> class.
        /// </summary>
        /// <param name="ghost">The ghost.</param>
        public GhostRenderer(GhostEnemy ghost) : base(ghost)
        {
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the ghost.
        /// </summary>
        public GhostEnemy Ghost
        {
            get { return this.Entity as GhostEnemy; }
        }
        /// <summary>
        /// Gets the opacity.
        /// </summary>
        public override float Opacity
        {
            get { return this.Ghost.IsInvincible ? 0.5f : 1.0f; }
        }
        #endregion
    }
}
