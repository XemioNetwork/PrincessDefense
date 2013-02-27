using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;
using Xemio.PrincessDefense.Entities.Characters;

namespace Xemio.PrincessDefense.Entities.Components.Behavior
{
    public class TargetingBehavior : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TargetingBehavior"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="targetTeam">The target team.</param>
        public TargetingBehavior(Character entity, Team targetTeam) : this(entity, targetTeam, 0, 1024)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TargetingBehavior"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="targetTeam">The target team.</param>
        /// <param name="minRadius">The radius.</param>
        public TargetingBehavior(Character entity, Team targetTeam, float minRadius, float maxRadius) : base(entity)
        {
            this.FocusNearestTarget = true;

            this.MinimumRadius = minRadius;
            this.MaximumRadius = maxRadius;
            this.TargetTeam = targetTeam;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the team entity.
        /// </summary>
        public Character Character
        {
            get { return this.Entity as Character; }
        }
        /// <summary>
        /// Gets or sets the target team.
        /// </summary>
        public Team TargetTeam { get; set; }
        /// <summary>
        /// Gets or sets the maximum radius.
        /// </summary>
        public float MaximumRadius { get; set; }
        /// <summary>
        /// Gets or sets the minimum radius.
        /// </summary>
        public float MinimumRadius { get; set; }
        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public Character Target { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to automatically focus the nearest target.
        /// </summary>
        public bool FocusNearestTarget { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this.FocusNearestTarget)
            {
                Character nearestTarget = null;

                float nearestDistance = float.MaxValue;
                float maxRadiusSquared = this.MaximumRadius * this.MaximumRadius;
                float minRadiusSquared = this.MinimumRadius * this.MinimumRadius;

                foreach (Entity entity in this.Entity.Environment)
                {
                    if (!(entity is Character)) continue;
                    if (entity == this.Entity) continue;

                    Character character = entity as Character;
                    if (character.Team == this.TargetTeam)
                    {
                        float distance = (character.Position - this.Character.Position).LengthSquared;
                        if (distance < nearestDistance &&
                            distance < maxRadiusSquared && distance > minRadiusSquared)
                        {
                            nearestTarget = character;
                            nearestDistance = distance;
                        }
                    }
                }

                this.Target = nearestTarget;
            }

            if (this.Target != null)
            {
                Vector2 direction = this.Target.Position - this.Character.Position;
                direction.Normalize();

                Direction closestFacing = DirectionHelper.GetClosestFacing(direction);
                this.Character.Walk(closestFacing);
            }
        }
        #endregion
    }
}
