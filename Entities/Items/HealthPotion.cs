using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.PrincessDefense.Entities.Components.Attributes;

namespace Xemio.PrincessDefense.Entities.Items
{
    public class HealthPotion : Item
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HealthPotion"/> class.
        /// </summary>
        public HealthPotion()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name
        {
            get { return "Potion"; }
        }
        /// <summary>
        /// Gets the icon.
        /// </summary>
        public override ITexture Icon
        {
            get { return Art.Potion; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Called when a player activates this item.
        /// </summary>
        /// <param name="player">The player.</param>
        public override void OnActivate(Player player)
        {
            HealthComponent health = player.GetComponent<HealthComponent>();
            for (int i = 0; i < 10; i++)
            {
                health.Heal();
            }
        }
        #endregion
    }
}
