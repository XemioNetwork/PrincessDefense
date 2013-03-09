using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.PrincessDefense.Levels.Waves;

namespace Xemio.PrincessDefense.Levels
{
    public class BreadLevel : Level
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BreadLevel"/> class.
        /// </summary>
        public BreadLevel() : this(null, 0)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BreadLevel"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="directionIndex">The direction.</param>
        public BreadLevel(ILevel parent, int directionIndex) : base(parent, directionIndex)
        {
            this.WaveProvider = new BreadWaveProvider();
            this.Container.IsUnlocked = true;
            
            this.Neighbors[DirectionIndex.Right] = new MushroomLevel(this, DirectionIndex.Right);
        }
        #endregion

        #region ILevel Member
        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name
        {
            get { return "Bread"; }
        }
        /// <summary>
        /// Gets the icon.
        /// </summary>
        public override ITexture Icon
        {
            get { return Art.Bread; }
        }
        #endregion
    }
}
