using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;
using PrincessDefense.Entities.Characters;

namespace PrincessDefense.Entities.Components
{
    public class PhysicsComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PhysicsComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public PhysicsComponent(Entity entity) : base(entity)
        {
            this.Friction = 0.25f;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the friction.
        /// </summary>
        public float Friction { get; set; }
        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        public Vector2 Velocity { get; set; }
        /// <summary>
        /// Gets or sets the acceleration.
        /// </summary>
        public Vector2 Acceleration { get; set; }
        /// <summary>
        /// Gets the mob.
        /// </summary>
        private Character Character
        {
            get { return this.Entity as Character; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this.Character.Position += this.Velocity;
            this.Velocity += this.Acceleration * elapsed * 0.05f;

            this.Acceleration *= (1 - this.Friction);
            this.Velocity *= (1 - this.Friction);
        }
        #endregion
    }
}
