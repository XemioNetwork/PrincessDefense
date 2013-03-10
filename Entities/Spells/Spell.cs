using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.PrincessDefense.Entities.Characters;

namespace Xemio.PrincessDefense.Entities.Spells
{
    public class Spell : BaseEntity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Spell"/> class.
        /// </summary>
        /// <param name="owner">The player.</param>
        public Spell(Player owner)
        {
            this._owner = owner;
        }
        #endregion

        #region Fields
        private Player _owner;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the owner.
        /// </summary>
        public override BaseEntity Owner
        {
            get { return this._owner; }
        }
        #endregion
    }
}
