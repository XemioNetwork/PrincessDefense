using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.PrincessDefense.Levels.Waves.Spawners;
using Xemio.PrincessDefense.Entities;
using Xemio.PrincessDefense.Entities.Characters;

namespace Xemio.PrincessDefense.Levels.Waves.Spawners
{
    public class SlimeSpawner : IMobSpawner
    {
        #region IMobSpawner Member
        /// <summary>
        /// Spawns an entity at the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public BaseEntity Spawn(Vector2 position)
        {
            return new Slime { Position = position };
        }
        #endregion
    }
}
