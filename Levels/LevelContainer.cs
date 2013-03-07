using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.PrincessDefense.Levels
{
    public class LevelContainer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LevelContainer"/> class.
        /// </summary>
        public LevelContainer()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the best time.
        /// </summary>
        public TimeSpan BestTime { get; set; }
        /// <summary>
        /// Gets or sets the highscore.
        /// </summary>
        public int Highscore { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is unlocked.
        /// </summary>
        public bool IsUnlocked { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is completed.
        /// </summary>
        public bool IsCompleted { get; set; }
        #endregion
    }
}
