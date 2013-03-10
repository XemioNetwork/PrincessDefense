using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.PrincessDefense.Entities.Environment;

namespace Xemio.PrincessDefense.Levels.Maps.Commands
{
    public abstract class EntityCommand : IMapCommand
    {
        #region Methods
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="position">The position.</param>
        protected virtual void Execute(World world, Vector2 position)
        {
        }
        #endregion

        #region IMapCommand Member
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public virtual string Identifier
        {
            get { return "ENTITY"; }
        }
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="arguments">The arguments.</param>
        public void Execute(World world, string[] arguments)
        {
            float x = float.Parse(arguments[0]);
            float y = float.Parse(arguments[1]);

            this.Execute(world, new Vector2(x, y));
        }
        #endregion
    }
}
