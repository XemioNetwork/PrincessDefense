using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering.Sprites;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary;

namespace PrincessDefense
{
    public static class Art
    {
        #region Characters & Projectiles
        /// <summary>
        /// Gets the sprite animations for "heroWalking.png".
        /// </summary>
        public static SpriteAnimation[] HeroWalking { get; private set; }
        /// <summary>
        /// Gets the sprite animations for "heroShooting.png".
        /// </summary>
        public static SpriteAnimation[] HeroShooting { get; private set; }
        /// <summary>
        /// Gets the sprite animations for "skeleton.png".
        /// </summary>
        public static SpriteAnimation[] Skeleton { get; private set; }
        /// <summary>
        /// Gets the sprite animations for "princess.png".
        /// </summary>
        public static SpriteAnimation[] Princess { get; private set; }
        /// <summary>
        /// Gets the textures for the arrow.
        /// </summary>
        public static ITexture[] Arrow { get; private set; }
        #endregion

        #region Icons
        /// <summary>
        /// Gets the texture for "strength.png".
        /// </summary>
        public static ITexture StrengthUpgrade { get; private set; }
        /// <summary>
        /// Gets the texture for "speed.png".
        /// </summary>
        public static ITexture SpeedUpgrade { get; private set; }
        /// <summary>
        /// Gets the texture for "multiArrows.png".
        /// </summary>
        public static ITexture MultipleArrowsUpgrade { get; private set; }
        /// <summary>
        /// Gets the texture for "skillPoint.png".
        /// </summary>
        public static ITexture SkillPoint { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public static void LoadContent()
        {
            Art.HeroWalking = LoadWalkingAnimation(@"Resources\characters\heroWalking.png");
            Art.HeroShooting = LoadBowAnimation(@"Resources\characters\heroShooting.png");

            Art.Skeleton = LoadWalkingAnimation(@"Resources\characters\skeleton.png");
            Art.Princess = LoadWalkingAnimation(@"Resources\characters\princess.png");

            Art.Arrow = LoadTextures(
                @"Resources\projectiles\arrowUp.png",
                @"Resources\projectiles\arrowDown.png",
                @"Resources\projectiles\arrowLeft.png",
                @"Resources\projectiles\arrowRight.png");

            Art.StrengthUpgrade = LoadTexture(@"Resources\upgrades\strength.png");
            Art.SpeedUpgrade = LoadTexture(@"Resources\upgrades\speed.png");
            Art.MultipleArrowsUpgrade = LoadTexture(@"Resources\upgrades\multiArrows.png");

            Art.SkillPoint = LoadTexture(@"Resources\ui\skillPoint.png");
        }
        /// <summary>
        /// Loads a walking animation.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private static SpriteAnimation[] LoadWalkingAnimation(string fileName)
        {
            List<SpriteAnimation> animations = new List<SpriteAnimation>();

            SpriteSheet idleUp = new SpriteSheet(fileName, 64, 64, 0, 1);
            SpriteSheet idleLeft = new SpriteSheet(fileName, 64, 64, 9, 1);
            SpriteSheet idleDown = new SpriteSheet(fileName, 64, 64, 18, 1);
            SpriteSheet idleRight = new SpriteSheet(fileName, 64, 64, 27, 1);

            SpriteSheet walkUp = new SpriteSheet(fileName, 64, 64, 0, 9);
            SpriteSheet walkLeft = new SpriteSheet(fileName, 64, 64, 9, 9);
            SpriteSheet walkDown = new SpriteSheet(fileName, 64, 64, 18, 9);
            SpriteSheet walkRight = new SpriteSheet(fileName, 64, 64, 27, 9);

            animations.Add(new SpriteAnimation("IdleUp", idleUp, 100));
            animations.Add(new SpriteAnimation("IdleLeft", idleLeft, 100));
            animations.Add(new SpriteAnimation("IdleDown", idleDown, 100));
            animations.Add(new SpriteAnimation("IdleRight", idleRight, 100));

            animations.Add(new SpriteAnimation("WalkUp", walkUp, 60));
            animations.Add(new SpriteAnimation("WalkLeft", walkLeft, 60));
            animations.Add(new SpriteAnimation("WalkDown", walkDown, 60));
            animations.Add(new SpriteAnimation("WalkRight", walkRight, 60));

            return animations.ToArray();
        }
        /// <summary>
        /// Loads a bow animation.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private static SpriteAnimation[] LoadBowAnimation(string fileName)
        {
            List<SpriteAnimation> animations = new List<SpriteAnimation>();

            SpriteSheet shootUp = new SpriteSheet(fileName, 64, 64, 0, 11);
            SpriteSheet shootLeft = new SpriteSheet(fileName, 64, 64, 13, 11);
            SpriteSheet shootDown = new SpriteSheet(fileName, 64, 64, 26, 11);
            SpriteSheet shootRight = new SpriteSheet(fileName, 64, 64, 39, 11);

            animations.Add(new SpriteAnimation("ShootUp", shootUp, 40, false));
            animations.Add(new SpriteAnimation("ShootLeft", shootLeft, 40, false));
            animations.Add(new SpriteAnimation("ShootDown", shootDown, 40, false));
            animations.Add(new SpriteAnimation("ShootRight", shootRight, 40, false));

            return animations.ToArray();
        }
        /// <summary>
        /// Loads a texture.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private static ITexture LoadTexture(string fileName)
        {
            return XGL.GetComponent<ITextureFactory>().CreateTexture(fileName);
        }
        /// <summary>
        /// Loads multiple textures.
        /// </summary>
        /// <param name="fileNames">The file names.</param>
        private static ITexture[] LoadTextures(params string[] fileNames)
        {
            List<ITexture> textures = new List<ITexture>();
            ITextureFactory factory = XGL.GetComponent<ITextureFactory>();

            foreach (string fileName in fileNames)
            {
                textures.Add(factory.CreateTexture(fileName));
            }

            return textures.ToArray();
        }
        #endregion
    }
}
