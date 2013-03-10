using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.PrincessDefense.Levels.Base;

namespace Xemio.PrincessDefense.Levels
{
    public class PepperLevel : Level
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MushroomLevel"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="directionIndex">The direction.</param>
        public PepperLevel(ILevel parent, int directionIndex) : base(parent, directionIndex)
        {
            this.WaveProvider = new PepperWaveProvider();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name
        {
            get { return "Red Pepper"; }
        }
        /// <summary>
        /// Gets the icon.
        /// </summary>
        public override ITexture Icon
        {
            get { return Art.RedPepper; }
        }
        #endregion
    }
}
