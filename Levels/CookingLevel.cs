using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary;
using Xemio.PrincessDefense.Scenes.Menues;

namespace Xemio.PrincessDefense.Levels
{
    public class CookingLevel : Level
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CookingLevel"/> class.
        /// </summary>
        public CookingLevel() : base(null, 0)
        {
            this.Container.IsUnlocked = true;
            this.Neighbors[DirectionIndex.Right] = new TutorialLevel(this, DirectionIndex.Right);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name
        {
            get { return "Cookbook"; }
        }
        /// <summary>
        /// Gets the icon.
        /// </summary>
        public override ITexture Icon
        {
            get { return Art.Cooking; }
        }
        /// <summary>
        /// Gets a value indicating whether to show the level description.
        /// </summary>
        public override bool ShowDescription
        {
            get { return false; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Plays this level.
        /// </summary>
        public override void Play()
        {
            SceneManager sceneManager = XGL.GetComponent<SceneManager>();
            Scene levelSelection = sceneManager.GetScene<LevelSelection>();

            levelSelection.Add(new CookbookMenu());
        }
        #endregion
    }
}
