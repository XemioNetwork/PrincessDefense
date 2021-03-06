﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.PrincessDefense.Entities;

namespace Xemio.PrincessDefense.Levels.Waves.Spawners
{
	public interface IMobSpawner
	{
		/// <summary>
		/// Gets the mob icon.
		/// </summary>
		ITexture Icon { get; }
		/// <summary>
		/// Spawns an entity at the specified position.
		/// </summary>
		/// <param name="position">The position.</param>
		BaseEntity Spawn(Vector2 position);
	}
}
