using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.PrincessDefense.Levels.Base;

namespace Xemio.PrincessDefense.Levels
{
    public class TutorialLevel : Level
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BreadLevel"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="directionIndex">The direction.</param>
        public TutorialLevel(ILevel parent, int directionIndex) : base(parent, directionIndex)
        {
            this.WaveProvider = new TutorialWaveProvider();
            this.Container.IsUnlocked = true;
            
            this.Neighbors[DirectionIndex.Right] = new BreadLevel(this, DirectionIndex.Right);
        }
        #endregion

        #region ILevel Member
        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name
        {
            get { return "Tutorial"; }
        }
        /// <summary>
        /// Gets the seed.
        /// </summary>
        public override string FileName
        {
            get { return string.Empty; }
        }
        /// <summary>
        /// Gets the icon.
        /// </summary>
        public override ITexture Icon
        {
            get { return Art.Tutorial; }
        }
        #endregion
    }
}
