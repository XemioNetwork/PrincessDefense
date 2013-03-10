using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Rendering;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Entities.Spells;
using Xemio.GameLibrary.Math;

namespace Xemio.PrincessDefense.Entities.Items
{
    public class Thunderstorm : Item
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Thunderstorm"/> class.
        /// </summary>
        public Thunderstorm()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name
        {
            get { return "Thunderstorm"; }
        }
        /// <summary>
        /// Gets the name of the action.
        /// </summary>
        public override string ActionName
        {
            get { return "Cast"; }
        }
        /// <summary>
        /// Gets the icon.
        /// </summary>
        public override ITexture Icon
        {
            get { return Art.Thunderstorm; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Called when a player activates this item.
        /// </summary>
        /// <param name="player">The player.</param>
        public override void OnActivate(Player player)
        {
            LightningclawSpawner spawner = new LightningclawSpawner(player);
            spawner.Position = player.Position;

            player.Environment.Add(spawner);
        }
        #endregion
    }
}
