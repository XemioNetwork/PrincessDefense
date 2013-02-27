using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Entities.Components;

namespace Xemio.PrincessDefense.Entities.Upgrades
{
    public class RegenerationUpgrade : IUpgrade
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RegenerationUpgrade"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        public RegenerationUpgrade(Player player)
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
            get { return 5; }
        }
        /// <summary>
        /// Gets the upgrade costs.
        /// </summary>
        public int Costs
        {
            get { return this.Level * 10 + 5; }
        }
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return "Regeneration"; }
        }
        /// <summary>
        /// Gets the icon.
        /// </summary>
        public ITexture Icon
        {
            get { return Art.RegenerationUpgrade; }
        }
        /// <summary>
        /// Upgrades this instance.
        /// </summary>
        public void Upgrade()
        {
            this.Level++;

            RegenerationComponent regeneration = this._player.GetComponent<RegenerationComponent>();
            regeneration.HealTime = 2000 / (float)(this.Level * this.Level);
        }
        #endregion
    }
}
