using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Entities.Components;

namespace Xemio.PrincessDefense.Entities.Upgrades
{
    public static class UpgradeManager
    {
        #region Fields
        private static List<IUpgrade> _upgrades;
        #endregion

        #region Methods
        /// <summary>
        /// Gets the available upgrades.
        /// </summary>
        public static List<IUpgrade> GetAvailableUpgrades()
        {
            if (_upgrades == null)
            {
                _upgrades = new List<IUpgrade>();

                _upgrades.Add(new HealthUpgrade(Player.Instance));
                _upgrades.Add(new StrengthUpgrade(Player.Instance));
                _upgrades.Add(new SpeedUpgrade(Player.Instance));
                _upgrades.Add(new RegenerationUpgrade(Player.Instance));
                _upgrades.Add(new BowSpeedUpgrade(Player.Instance));
                _upgrades.Add(new ArrowUpgrade(Player.Instance));
                _upgrades.Add(new FireLionUpgrade(Player.Instance));
                _upgrades.Add(new KnockbackUpgrade(Player.Instance));

                Player.Instance.InitializeUpgrades(3);
            }

            return _upgrades;
        }
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
