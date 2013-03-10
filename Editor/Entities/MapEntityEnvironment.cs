using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;

namespace Xemio.PrincessDefense.Editor.Entities
{
    public class MapEntityEnvironment : EntityEnvironment
    {
        #region Methods
        /// <summary>
        /// Returns a sorted entity colleciton.
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<Entity> SortedEntityCollection()
        {
            return this.Entities.OrderBy(e => e.Position.Y);
        }
        #endregion
    }
}
