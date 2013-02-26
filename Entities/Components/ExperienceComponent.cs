using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;

namespace PrincessDefense.Entities.Components
{
    public class ExperienceComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ExperienceComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public ExperienceComponent(Entity entity) : base(entity)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ExperienceComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="experience">The experience.</param>
        public ExperienceComponent(Entity entity, float experience) : base(entity)
        {
            this.Add(experience);
        }
        #endregion

        #region Fields
        private int _lastLevel;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the experience.
        /// </summary>
        public float Experience { get; set; }
        /// <summary>
        /// Gets the skill points.
        /// </summary>
        public int SkillPoints { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public void Add(float amount)
        {
            this.Experience += amount;
        }
        /// <summary>
        /// Gets the level.
        /// </summary>
        public int GetLevel()
        {
            int level = 1;
            float experience = this.Experience;

            while (experience >= 0)
            {
                experience -= (level * 2) + 1;
                if (experience >= 0)
                {
                    level++;
                }
            }

            return level;
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            int level = this.GetLevel();

            if (level != this._lastLevel)
            {
                int skillPoints = 1;
                if (level % 2 == 0) skillPoints++;
                if (level % 3 == 0) skillPoints++;

                this.SkillPoints += skillPoints;
            }

            this._lastLevel = level;
        }
        #endregion
    }
}
