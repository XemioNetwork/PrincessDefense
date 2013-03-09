using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Entities.Components.Attributes;

namespace Xemio.PrincessDefense.Entities.Upgrades
{
    public class FireLionUpgrade : IUpgrade
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FireLionUpgrade"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        public FireLionUpgrade(Player player)
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
            get { return 8; }
        }
        /// <summary>
        /// Gets the upgrade costs.
        /// </summary>
        public int Costs
        {
            get { return this.Level * this.Level * this.Level * 2 + 8; }
        }
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return "Magical Fire Arrows"; }
        }
        /// <summary>
        /// Gets the icon.
        /// </summary>
        public ITexture Icon
        {
            get { return Art.FireLionUpgrade; }
        }
        /// <summary>
        /// Upgrades this instance.
        /// </summary>
        public void Upgrade()
        {
            this.Level++;

            BowComponent bow = this._player.GetComponent<BowComponent>();
            bow.FireLionPropability += 0.1f;
        }
        #endregion
    }
}
