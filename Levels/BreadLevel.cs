using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.PrincessDefense.Levels.Waves;
using Xemio.PrincessDefense.Scenes;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Game;

namespace Xemio.PrincessDefense.Levels
{
    public class BreadLevel : Level
    {
        #region Constructors
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
            this.Unlock();
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
        /// Gets the seed.
        /// </summary>
        public override string FileName
        {
            get { return @"Resources\maps\BreadLevel.txt"; }
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
