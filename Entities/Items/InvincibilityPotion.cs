using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.PrincessDefense.Entities.Characters;

namespace Xemio.PrincessDefense.Entities.Items
{
    public class InvincibilityPotion : Item
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HealthPotion"/> class.
        /// </summary>
        public InvincibilityPotion()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name
        {
            get { return "Invincibility"; }
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
            get { return Art.Invincibility; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Called when a player activates this item.
        /// </summary>
        /// <param name="player">The player.</param>
        public override void OnActivate(Player player)
        {
        }
        #endregion
    }
}
