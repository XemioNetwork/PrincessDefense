using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Rendering;
using Xemio.PrincessDefense.Levels.Waves;
using Xemio.PrincessDefense.Scenes;
using Xemio.PrincessDefense.Scenes.Menues;

namespace Xemio.PrincessDefense.Levels.Base
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

        #region Methods
        /// <summary>
        /// Gets the game.
        /// </summary>
        public PrincessGame GetGame()
        {
            return XGL.GetComponent<SceneManager>().GetScene<PrincessGame>();
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
        public virtual string FileName
        {
            get { return @"Resources\maps\Forest.txt"; }
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
        /// Gets a value indicating whether to show the level description.
        /// </summary>
        public virtual bool ShowDescription
        {
            get { return true; }
        }
        /// <summary>
        /// Plays this level.
        /// </summary>
        public virtual void Play()
        {
            Sounds.Play(Sounds.PlayLevel);

            SceneManager sceneManager = XGL.GetComponent<SceneManager>();
            sceneManager.Add(new PrincessGame(this));
            sceneManager.Remove(sceneManager.GetScene<LevelSelection>());
        }
        /// <summary>
        /// Unlocks this level.
        /// </summary>
        public void Unlock()
        {
            this.Container.IsCompleted = true;

            foreach (ILevel level in this.Neighbors)
            {
                if (level != null) level.Container.IsUnlocked = true;
            }
        }
        #endregion
    }
}
