using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;

namespace Xemio.PrincessDefense.Levels
{
    public class CherryLevel : Level
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MushroomLevel"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="directionIndex">The direction.</param>
        public CherryLevel(ILevel parent, int directionIndex) : base(parent, directionIndex)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name
        {
            get { return "Cherry"; }
        }
        /// <summary>
        /// Gets the icon.
        /// </summary>
        public override ITexture Icon
        {
            get { return Art.Cherry; }
        }
        #endregion
    }
}
