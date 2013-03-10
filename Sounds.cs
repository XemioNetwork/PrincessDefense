using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Sound;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Math;
using Xemio.PrincessDefense.Entities;
using Xemio.PrincessDefense.Entities.Characters;
using Xemio.GameLibrary.Entities;

namespace Xemio.PrincessDefense
{
    public static class Sounds
    {
        #region Properties
        /// <summary>
        /// Gets the sound for "select.wav".
        /// </summary>
        public static ISound Select { get; private set; }
        /// <summary>
        /// Gets the sound for "playLevel.wav".
        /// </summary>
        public static ISound PlayLevel { get; private set; }
        /// <summary>
        /// Gets the sound for "hit.wav".
        /// </summary>
        public static ISound Hit { get; private set; }
        /// <summary>
        /// Gets the sound for "explosion.wav".
        /// </summary>
        public static ISound Explosion { get; private set; }
        /// <summary>
        /// Gets the sound for "fireLion.wav".
        /// </summary>
        public static ISound FireLion { get; private set; }
        /// <summary>
        /// Gets the sound for "completed.wav".
        /// </summary>
        public static ISound Completed { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public static void LoadContent()
        {
            SoundManager soundManager = XGL.GetComponent<SoundManager>();

            Sounds.Select = soundManager.CreateSound(@"Resources\sounds\select.wav");
            Sounds.PlayLevel = soundManager.CreateSound(@"Resources\sounds\playLevel.wav");

            Sounds.Hit = soundManager.CreateSound(@"Resources\sounds\hit.wav");
            Sounds.Hit.Radius = 300;

            Sounds.Explosion = soundManager.CreateSound(@"Resources\sounds\explosion.wav");
            Sounds.Explosion.Radius = 1000;

            Sounds.FireLion = soundManager.CreateSound(@"Resources\sounds\fireLion.wav");
            Sounds.FireLion.Radius = 800;

            Sounds.Completed = soundManager.CreateSound(@"Resources\sounds\completed.wav");
        }
        /// <summary>
        /// Plays the specified sound.
        /// </summary>
        /// <param name="sound">The sound.</param>
        public static void Play(ISound sound)
        {
            XGL.GetComponent<SoundManager>().Play(sound);
        }
        /// <summary>
        /// Plays the specified sound.
        /// </summary>
        /// <param name="sound">The sound.</param>
        /// <param name="entity">The entity.</param>
        public static void Play(ISound sound, Entity entity)
        {
            XGL.GetComponent<SoundManager>().Play(sound, entity.Position - Player.Instance.Position);
        }
        /// <summary>
        /// Locates the specified sound.
        /// </summary>
        /// <param name="sound">The sound.</param>
        /// <param name="entity">The entity.</param>
        public static void Locate(ISound sound, Entity entity)
        {
            XGL.GetComponent<SoundManager>().Locate(sound, entity.Position - Player.Instance.Position);
        }
        #endregion
    }
}
