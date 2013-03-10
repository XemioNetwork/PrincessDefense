using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.PrincessDefense.Entities;
using Xemio.PrincessDefense.Entities.Enemies;

namespace Xemio.PrincessDefense.Levels.Waves.Spawners
{
    public class BatSpawner : IMobSpawner
    {
        #region IMobSpawner Member   
        /// <summary>
        /// Gets the mob icon.
        /// </summary>
        public ITexture Icon
        {
            get { return Art.Bat[2].Sheet.Textures[0]; }
        }
        /// <summary>
        /// Spawns an entity at the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        public BaseEntity Spawn(Vector2 position)
        {
            return new BatEnemy { Position = position };
        }
        #endregion
    }
}
