using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;

namespace Xemio.PrincessDefense.Entities.Upgrades
{
    public interface IUpgrade
    {
        /// <summary>
        /// Gets the level.
        /// </summary>
        int Level { get; }
        /// <summary>
        /// Gets the maximum level.
        /// </summary>
        int MaximumLevel { get; }
        /// <summary>
        /// Gets the upgrade costs.
        /// </summary>
        int Costs { get; }
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets the icon.
        /// </summary>
        ITexture Icon { get; }
        /// <summary>
        /// Upgrades this instance.
        /// </summary>
        void Upgrade();
    }
}
