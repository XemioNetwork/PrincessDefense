using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.PrincessDefense.Entities.Environment;
using Xemio.GameLibrary.Common.Link;

namespace Xemio.PrincessDefense.Levels.Maps
{
    public interface IMapCommand : ILinkable<string>
    {
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="arguments">The arguments.</param>
        void Execute(World world, string[] arguments);
    }
}
