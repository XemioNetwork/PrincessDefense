using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PrincessDefense.Entities.Characters;
using PrincessDefense.Entities.Components;

namespace PrincessDefense.Entities.Upgrades
{
    public static class UpgradeManager
    {
        #region Methods
        /// <summary>
        /// Upgrades the specified player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="upgrade">The upgrade.</param>
        public static bool Upgrade(Player player, IUpgrade upgrade)
        {
            ExperienceComponent experience = player.GetComponent<ExperienceComponent>();

            if (UpgradeManager.CanUpgrade(player, upgrade) && 
                upgrade.Level < upgrade.MaximumLevel)
            {
                experience.SkillPoints -= upgrade.Costs;
                upgrade.Upgrade();

                return true;
            }

            return false;
        }
        /// <summary>
        /// Determines whether the specified can upgrade the specified upgrade.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="upgrade">The upgrade.</param>
        public static bool CanUpgrade(Player player, IUpgrade upgrade)
        {
            return player.GetComponent<ExperienceComponent>().SkillPoints >= upgrade.Costs;
        }
        #endregion
    }
}
