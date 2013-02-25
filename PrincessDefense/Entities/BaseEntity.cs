using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;

namespace PrincessDefense.Entities
{
    public class BaseEntity : Entity
    {
        #region Properties
        /// <summary>
        /// Gets the team.
        /// </summary>
        public virtual Team Team
        {
            get { return Team.Skeletons; }
        }
        #endregion
    }
}
