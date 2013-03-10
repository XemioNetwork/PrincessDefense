using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.PrincessDefense.Entities.Environment;
using Xemio.PrincessDefense.Entities;

namespace Xemio.PrincessDefense.Levels.Maps.Commands
{
    public class TreeCommand : EntityCommand
    {
        #region Methods
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public override string Identifier
        {
            get { return "TREE"; }
        }
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="position">The position.</param>
        protected override void Execute(World world, Vector2 position)
        {
            world.Add(new Tree { Position = position });
        }
        #endregion
    }
}
