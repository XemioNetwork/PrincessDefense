using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.PrincessDefense.Levels.Waves;

namespace Xemio.PrincessDefense.Levels
{
    public interface ILevel
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets the seed.
        /// </summary>
        string FileName { get; }
        /// <summary>
        /// Gets the icon.
        /// </summary>
        ITexture Icon { get; }
        /// <summary>
        /// Gets the wave provider.
        /// </summary>
        IWaveProvider WaveProvider { get; }
        /// <summary>
        /// Gets the neighbors.
        /// </summary>
        ILevel[] Neighbors { get; }
        /// <summary>
        /// Gets the container.
        /// </summary>
        LevelContainer Container { get; set; }
        /// <summary>
        /// Gets the parent.
        /// </summary>
        ILevel Parent { get; }
        /// <summary>
        /// Gets the root.
        /// </summary>
        ILevel Root { get; }
        /// <summary>
        /// Gets a value indicating whether this instance is hidden.
        /// </summary>
        bool IsHidden { get; }
        /// <summary>
        /// Gets a value indicating whether to show the level description.
        /// </summary>
        bool ShowDescription { get; }
        /// <summary>
        /// Plays this level.
        /// </summary>
        void Play();
        /// <summary>
        /// Unlocks this level.
        /// </summary>
        void Unlock();
    }
}
