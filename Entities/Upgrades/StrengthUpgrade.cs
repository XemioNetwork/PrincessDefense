using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.GameLibrary.Rendering;

namespace Xemio.PrincessDefense.Entities.Upgrades
{
    public class StrengthUpgrade : IUpgrade
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StrengthUpgrade"/> class.
        /// </summary>
        public StrengthUpgrade(Player player)
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
            get { return 6; }
        }
        /// <summary>
        /// Gets the upgrade costs.
        /// </summary>
        public int Costs
        {
            get { return this.Level * this.Level * 2 + 3; }
        }
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return "Strength"; }
        }
        /// <summary>
        /// Gets the icon.
        /// </summary>
        public ITexture Icon
        {
            get { return Art.StrengthUpgrade; }
        }
        /// <summary>
        /// Upgrades this instance.
        /// </summary>
        public void Upgrade()
        {
            this.Level++;

            DamageComponent damage = this._player.GetComponent<DamageComponent>();
            damage.Damage++;
        }
        #endregion
    }
}
