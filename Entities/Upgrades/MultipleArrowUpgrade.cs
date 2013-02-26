using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using PrincessDefense.Entities.Characters;

namespace PrincessDefense.Entities.Upgrades
{
    public class MultipleArrowUpgrade : IUpgrade
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleArrowUpgrade"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        public MultipleArrowUpgrade(Player player)
        {
        }
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
            get { return 4; }
        }
        /// <summary>
        /// Gets the upgrade costs.
        /// </summary>
        public int Costs
        {
            get { return this.Level * this.Level * this.Level * 2 + 6; }
        }
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return "Multiple Arrows"; }
        }
        /// <summary>
        /// Gets the icon.
        /// </summary>
        public ITexture Icon
        {
            get { return Art.MultipleArrowsUpgrade; }
        }
        /// <summary>
        /// Upgrades this instance.
        /// </summary>
        public void Upgrade()
        {
            this.Level++;
        }
        #endregion
    }
}
