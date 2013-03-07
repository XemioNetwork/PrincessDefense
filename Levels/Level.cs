using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.PrincessDefense.Levels.Waves;

namespace Xemio.PrincessDefense.Levels
{
    public abstract class Level : ILevel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Level"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="directionIndex">The direction.</param>
        public Level(ILevel parent, int directionIndex)
        {
            int index = directionIndex + 2;
            if (index >= 4)
            {
                index -= 4;
            }

            this.Neighbors = new ILevel[4];
            this.Neighbors[index] = parent;

            this.Container = new LevelContainer();
            this.Parent = parent;
        }
        #endregion

        #region ILevel Member
        /// <summary>
        /// Gets the name.
        /// </summary>
        public virtual string Name
        {
            get { return "Level"; }
        }
        /// <summary>
        /// Gets the seed.
        /// </summary>
        public virtual string Seed
        {
            get { return "Forest"; }
        }
        /// <summary>
        /// Gets the icon.
        /// </summary>
        public virtual ITexture Icon
        {
            get { return null; }
        }
        /// <summary>
        /// Gets the wave provider.
        /// </summary>
        public IWaveProvider WaveProvider { get; protected set; }
        /// <summary>
        /// Gets the neighbors.
        /// </summary>
        public ILevel[] Neighbors { get; protected set; }
        /// <summary>
        /// Gets the container.
        /// </summary>
        public LevelContainer Container { get; set; }
        /// <summary>
        /// Gets the parent.
        /// </summary>
        public ILevel Parent { get; private set; }
        /// <summary>
        /// Gets the root.
        /// </summary>
        public ILevel Root
        {
            get
            {
                ILevel level = this;
                while (level.Parent != null)
                {
                    level = level.Parent;
                }

                return level;
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance is hidden.
        /// </summary>
        public virtual bool IsHidden
        {
            get { return false; }
        }
        /// <summary>
        /// Unlocks this level.
        /// </summary>
        public void Unlock()
        {
            foreach (ILevel level in this.Neighbors)
            {
                if (level != null) level.Container.IsUnlocked = true;
            }
        }
        #endregion
    }
}
