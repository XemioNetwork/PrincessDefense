using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using PrincessDefense.Entities.Characters;
using PrincessDefense.Entities.Components;

namespace PrincessDefense.Entities.Upgrades
{
    public class SpeedUpgrade : IUpgrade
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SpeedUpgrade"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        public SpeedUpgrade(Player player)
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
            get { return this.Level * this.Level * this.Level + 2; }
        }
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return "Speed"; }
        }
        /// <summary>
        /// Gets the icon.
        /// </summary>
        public ITexture Icon
        {
            get { return Art.SpeedUpgrade; }
        }
        /// <summary>
        /// Upgrades this instance.
        /// </summary>
        public void Upgrade()
        {
            this.Level++;

            SpeedComponent speed = this._player.GetComponent<SpeedComponent>();
            speed.Speed += 0.25f * this.Level;
        }
        #endregion
    }
}
