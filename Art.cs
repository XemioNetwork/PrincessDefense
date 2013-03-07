using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering.Sprites;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Rendering.Fonts;

namespace Xemio.PrincessDefense
{
    public static class Art
    {
        #region Factory
        /// <summary>
        /// Gets the factory.
        /// </summary>
        public static ITextureFactory Factory { get; private set; }
        #endregion

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
        /// Gets the sprite animations for "slime.png".
        /// </summary>
        public static SpriteAnimation[] Slime { get; private set; }
        /// <summary>
        /// Gets the textures for the arrow.
        /// </summary>
        public static ITexture[] Arrow { get; private set; }
        /// <summary>
        /// Gets the sprite animations for the fire lion spell.
        /// </summary>
        public static SpriteAnimation[] FireLion { get; private set; }
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
        /// Gets the texture for "fireLion.png".
        /// </summary>
        public static ITexture FireLionUpgrade { get; private set; }
        /// <summary>
        /// Gets the texture for "knockback.png".
        /// </summary>
        public static ITexture KnockbackUpgrade { get; private set; }
        /// <summary>
        /// Gets the texture for "regeneration.png".
        /// </summary>
        public static ITexture RegenerationUpgrade { get; private set; }
        /// <summary>
        /// Gets the texture for "bowSpeed.png".
        /// </summary>
        public static ITexture BowSpeedUpgrade { get; private set; }
        /// <summary>
        /// Gets the texture for "health.png".
        /// </summary>
        public static ITexture HealthUpgrade { get; private set; }
        /// <summary>
        /// Gets the texture for "skillPoint.png".
        /// </summary>
        public static ITexture SkillPoint { get; private set; }
        #endregion

        #region User Interface
        /// <summary>
        /// Gets the texture for "gameOver.png".
        /// </summary>
        public static ITexture GameOver { get; private set; }
        /// <summary>
        /// Gets the texture for "scrollTop.png".
        /// </summary>
        public static ITexture ScrollTop { get; private set; }
        /// <summary>
        /// Gets the texture for "scrollBottom.png".
        /// </summary>
        public static ITexture ScrollBottom { get; private set; }
        /// <summary>
        /// Gets the texture for "scroll.png".
        /// </summary>
        public static ITexture Scroll { get; private set; }
        /// <summary>
        /// Gets the texture for "buttons.png".
        /// </summary>
        public static ITexture Buttons { get; private set; }
        #endregion

        #region Cups
        /// <summary>
        /// Gets the texture for "tutorial.png".
        /// </summary>
        public static ITexture Tutorial { get; private set; }
        /// <summary>
        /// Gets the texture for "bread.png".
        /// </summary>
        public static ITexture Bread { get; private set; }
        /// <summary>
        /// Gets the texture for "mushroom.png".
        /// </summary>
        public static ITexture Mushroom { get; private set; }
        /// <summary>
        /// Gets the texture for "cherry.png".
        /// </summary>
        public static ITexture Cherry { get; private set; }
        /// <summary>
        /// Gets the texture for "bone.png".
        /// </summary>
        public static ITexture Bone { get; private set; }
        /// <summary>
        /// Gets the texture for "iron.png".
        /// </summary>
        public static ITexture Iron { get; private set; }
        #endregion

        #region Map
        /// <summary>
        /// Gets the texture for "map.png".
        /// </summary>
        public static ITexture Map { get; private set; }
        #endregion

        #region Fonts
        /// <summary>
        /// Gets the font.
        /// </summary>
        public static SpriteFont Font { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public static void LoadContent()
        {
            Art.Factory = XGL.GetComponent<ITextureFactory>();

            Art.HeroWalking = LoadWalkingAnimation(@"Resources\characters\heroWalking.png");
            Art.HeroShooting = LoadBowAnimation(@"Resources\characters\heroShooting.png");

            Art.Skeleton = LoadWalkingAnimation(@"Resources\characters\skeleton.png");
            Art.Princess = LoadWalkingAnimation(@"Resources\characters\princess.png");
            Art.Slime = LoadEnemyAnimation(@"Resources\characters\slime.png");

            Art.Arrow = LoadTextures(
                @"Resources\projectiles\arrowUp.png",
                @"Resources\projectiles\arrowDown.png",
                @"Resources\projectiles\arrowLeft.png",
                @"Resources\projectiles\arrowRight.png");

            Art.FireLion = new SpriteAnimation[] 
            {
                LoadDirectionalAnimation(@"Resources\spells\firelionLeft.png", "Left", 128, 128),
                LoadDirectionalAnimation(@"Resources\spells\firelionRight.png", "Right", 128, 128),
                LoadDirectionalAnimation(@"Resources\spells\firelionUp.png", "Up", 128, 128),
                LoadDirectionalAnimation(@"Resources\spells\firelionDown.png", "Down", 128, 128)
            };

            Art.StrengthUpgrade = LoadTexture(@"Resources\upgrades\strength.png");
            Art.SpeedUpgrade = LoadTexture(@"Resources\upgrades\speed.png");
            Art.MultipleArrowsUpgrade = LoadTexture(@"Resources\upgrades\multiArrows.png");
            Art.FireLionUpgrade = LoadTexture(@"Resources\upgrades\fireLion.png");
            Art.KnockbackUpgrade = LoadTexture(@"Resources\upgrades\knockback.png");
            Art.RegenerationUpgrade = LoadTexture(@"Resources\upgrades\regeneration.png");
            Art.BowSpeedUpgrade = LoadTexture(@"Resources\upgrades\bowSpeed.png");
            Art.HealthUpgrade = LoadTexture(@"Resources\upgrades\health.png");

            Art.SkillPoint = LoadTexture(@"Resources\ui\skillPoint.png");

            Art.GameOver = LoadTexture(@"Resources\ui\gameOver.png");
            Art.ScrollTop = LoadTexture(@"Resources\ui\scrollTop.png");
            Art.ScrollBottom = LoadTexture(@"Resources\ui\scrollBottom.png");
            Art.Scroll = LoadTexture(@"Resources\ui\scroll.png");
            Art.Buttons = LoadTexture(@"Resources\ui\buttons.png");

            Art.Tutorial = LoadTexture(@"Resources\cups\tutorial.png");
            Art.Bread = LoadTexture(@"Resources\cups\bread.png");
            Art.Mushroom = LoadTexture(@"Resources\cups\mushroom.png");
            Art.Cherry = LoadTexture(@"Resources\cups\cherry.png");
            Art.Bone = LoadTexture(@"Resources\cups\bone.png");
            Art.Iron = LoadTexture(@"Resources\cups\iron.png");

            Art.Map = LoadTexture(@"Resources\map.png");
            Art.Font = Art.Factory.CreateSpriteFont(@"Resources\fonts\kenPixel.sf");
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
        /// Loads an enemy animation.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private static SpriteAnimation[] LoadEnemyAnimation(string fileName)
        {
            List<SpriteAnimation> animations = new List<SpriteAnimation>();
            ITexture texture = Art.Factory.CreateTexture(fileName);

            int frameWidth = texture.Width / 3;
            int frameHeight = texture.Height / 4;

            SpriteSheet idleDown = new SpriteSheet(fileName, frameWidth, frameHeight, 0, 1);
            SpriteSheet idleRight = new SpriteSheet(fileName, frameWidth, frameHeight, 3, 1);
            SpriteSheet idleUp = new SpriteSheet(fileName, frameWidth, frameHeight, 6, 1);
            SpriteSheet idleLeft = new SpriteSheet(fileName, frameWidth, frameHeight, 9, 1);

            SpriteSheet walkDown = new SpriteSheet(fileName, frameWidth, frameHeight, 0, 3);
            SpriteSheet walkRight = new SpriteSheet(fileName, frameWidth, frameHeight, 3, 3);
            SpriteSheet walkUp = new SpriteSheet(fileName, frameWidth, frameHeight, 6, 3);
            SpriteSheet walkLeft = new SpriteSheet(fileName, frameWidth, frameHeight, 9, 3);

            animations.Add(new SpriteAnimation("IdleUp", idleUp, 100));
            animations.Add(new SpriteAnimation("IdleLeft", idleLeft, 100));
            animations.Add(new SpriteAnimation("IdleDown", idleDown, 100));
            animations.Add(new SpriteAnimation("IdleRight", idleRight, 100));

            animations.Add(new SpriteAnimation("WalkUp", walkUp, 120));
            animations.Add(new SpriteAnimation("WalkLeft", walkLeft, 120));
            animations.Add(new SpriteAnimation("WalkDown", walkDown, 120));
            animations.Add(new SpriteAnimation("WalkRight", walkRight, 120));

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

            animations.Add(new SpriteAnimation("ShootUp", shootUp, 30, false));
            animations.Add(new SpriteAnimation("ShootLeft", shootLeft, 30, false));
            animations.Add(new SpriteAnimation("ShootDown", shootDown, 30, false));
            animations.Add(new SpriteAnimation("ShootRight", shootRight, 30, false));

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
        /// <summary>
        /// Loads a directional animation.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private static SpriteAnimation LoadDirectionalAnimation(string fileName, string name, int width, int height)
        {
            SpriteSheet sheet = new SpriteSheet(fileName, width, height, 0, 16);
            SpriteAnimation animation = new SpriteAnimation(name, sheet, 40, false);

            return animation;
        }
        #endregion
    }
}
