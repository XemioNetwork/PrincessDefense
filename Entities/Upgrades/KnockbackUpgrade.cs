using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Entities;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Entities.Components.Attributes;

namespace Xemio.PrincessDefense.Entities.Upgrades
{
    public class KnockbackUpgrade : IUpgrade
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="KnockbackUpgrade"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        public KnockbackUpgrade(Player player)
        {
            this._player = player;
        }
        #endregion

        #region Fields
        private Player _player;
        #endregion

        #region IUpgrade Member
        /// <summary>
        /// Gets the level.
        /// </summary>
        public int Level { get; private set; }
        /// <summary>
        /// Gets the maximum level.
        /// </summary>
        public int MaximumLevel
        {
            get { return 3; }
        }
        /// <summary>
        /// Gets the upgrade costs.
        /// </summary>
        public int Costs
        {
            get { return this.Level * 4 + 2; }
        }
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return "Knockback"; }
        }
        /// <summary>
        /// Gets the icon.
        /// </summary>
        public ITexture Icon
        {
            get { return Art.KnockbackUpgrade; }
        }
        /// <summary>
        /// Upgrades this instance.
        /// </summary>
        public void Upgrade()
        {
            this.Level++;
            ProjectileKnockback.Strength = this.Level * 2 + 3;
        }
        #endregion
    }
}
