using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.PrincessDefense.Levels.Base;

namespace Xemio.PrincessDefense.Levels
{
    public class MushroomLevel : Level
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MushroomLevel"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="directionIndex">The direction.</param>
        public MushroomLevel(ILevel parent, int directionIndex) : base(parent, directionIndex)
        {
            this.WaveProvider = new MushroomWaveProvider();

            this.Neighbors[DirectionIndex.Right] = new PepperLevel(this, DirectionIndex.Right);
            this.Neighbors[DirectionIndex.Bottom] = new MeatLevel(this, DirectionIndex.Bottom);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name
        {
            get { return "Mushroom"; }
        }
        /// <summary>
        /// Gets the icon.
        /// </summary>
        public override ITexture Icon
        {
            get { return Art.Mushroom; }
        }
        #endregion
    }
}
