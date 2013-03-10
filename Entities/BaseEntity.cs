using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;

namespace Xemio.PrincessDefense.Entities
{
    public class BaseEntity : Entity
    {
        #region Properties
        /// <summary>
        /// Gets the owner.
        /// </summary>
        public virtual BaseEntity Owner
        {
            get { return null; }
        }
        /// <summary>
        /// Gets the team.
        /// </summary>
        public virtual Team Team
        {
            get { return Team.None; }
        }
        #endregion
    }
}
