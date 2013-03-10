using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Math;
using Xemio.PrincessDefense.Extensions;
using Xemio.PrincessDefense.Levels;
using Xemio.PrincessDefense.Entities.Environment;
using Xemio.PrincessDefense.Levels.Waves;

namespace Xemio.PrincessDefense.Scenes.Menues
{
    public class LevelSelection : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LevelSelection"/> class.
        /// </summary>
        public LevelSelection() : this(new CookingLevel())
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="LevelSelection"/> class.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="selectedLevel">The selected level.</param>
        public LevelSelection(ILevel root) : this(root, root)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="LevelSelection"/> class.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="selectedLevel">The selected level.</param>
        public LevelSelection(ILevel root, ILevel selectedLevel)
        {
            this._renderedLevels = new Dictionary<ILevel, bool>();

            this.RootLevel = root;
            this.SelectedLevel = selectedLevel;

            this.Camera = new Camera();
            this.Camera.Clamped = false;

            this.Camera.MoveTo(this.CalculatePosition(selectedLevel));
        }
        #endregion

        #region Fields
        private IPen _levelPen;
        private IPen _selectedPen;
        private IPen _lockedPen;
        private IBrush _panelBrush;

        private Dictionary<ILevel, bool> _renderedLevels;

        private float _elapsed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the root level.
        /// </summary>
        public ILevel RootLevel { get; set; }
        /// <summary>
        /// Gets the selected level.
        /// </summary>
        public ILevel SelectedLevel { get; private set; }
        /// <summary>
        /// Gets the camera.
        /// </summary>
        public Camera Camera { get; private set; }
        /// <summary>
        /// Gets the offset.
        /// </summary>
        public float Offset { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            IGeometryProvider geometry = this.GraphicsDevice.Geometry;
            ITextureFactory textureFactory = this.GraphicsDevice.TextureFactory;

            this._levelPen = geometry.Factory.CreatePen(new Color(0, 0, 0, 0.75f), 5);
            this._lockedPen = geometry.Factory.CreatePen(new Color(0, 0, 0, 0.4f), 5);
            this._selectedPen = geometry.Factory.CreatePen(Color.White, 3);
            this._panelBrush = geometry.Factory.CreateSolid(new Color(0, 0, 0, 0.8f));
        }
        /// <summary>
        /// Selects the level.
        /// </summary>
        /// <param name="level">The level.</param>
        private void SelectLevel(ILevel level)
        {
            this._elapsed = 0;
            this.Offset = 0;

            Sounds.Play(Sounds.Select);
            this.SelectedLevel = level;
        }
        /// <summary>
        /// Gets the mob icons.
        /// </summary>
        /// <param name="level">The level.</param>
        private ITexture[] GetMobIcons(ILevel level)
        {
            List<ITexture> textures = new List<ITexture>();

            if (level.WaveProvider != null)
            {
                int waveIndex = 0;
                WaveInstruction wave;

                while ((wave = level.WaveProvider.CreateWave(waveIndex++)) != null)
                {
                    foreach (SpawnInstruction spawn in wave.Spawns)
                    {
                        if (!textures.Contains(spawn.Spawner.Icon))
                        {
                            textures.Add(spawn.Spawner.Icon);
                        }
                    }
                }
            }

            return textures.ToArray();
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this.Camera.Position = this.CalculatePosition(this.SelectedLevel);
            this.Camera.Tick(elapsed);

            this._elapsed += elapsed;
            this.Offset = MathHelper.Sin(this._elapsed / 100.0f) * 5;

            if (this.Count() == 0)
            {
                ILevel top = this.SelectedLevel.Neighbors[DirectionIndex.Top];
                if (this.Keyboard.IsKeyPressed(Keys.Up) && top != null && top.Container.IsUnlocked)
                {
                    this.SelectLevel(top);
                }

                ILevel right = this.SelectedLevel.Neighbors[DirectionIndex.Right];
                if (this.Keyboard.IsKeyPressed(Keys.Right) && right != null && right.Container.IsUnlocked)
                {
                    this.SelectLevel(right);
                }

                ILevel bottom = this.SelectedLevel.Neighbors[DirectionIndex.Bottom];
                if (this.Keyboard.IsKeyPressed(Keys.Down) && bottom != null && bottom.Container.IsUnlocked)
                {
                    this.SelectLevel(bottom);
                }

                ILevel left = this.SelectedLevel.Neighbors[DirectionIndex.Left];
                if (this.Keyboard.IsKeyPressed(Keys.Left) && left != null && left.Container.IsUnlocked)
                {
                    this.SelectLevel(left);
                }

                if (this.Keyboard.IsKeyPressed(Keys.Enter))
                {
                    this.SelectedLevel.Play();
                }
            }
        }
        /// <summary>
        /// Calculates the position.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        private Vector2 CalculatePosition(ILevel level)
        {
            Vector2 position = Vector2.Zero;
            while (level.Parent != null)
            {
                Vector2 offset = Vector2.Zero;

                if (level.Neighbors[DirectionIndex.Top] == level.Parent) offset = new Vector2(0, -1);
                if (level.Neighbors[DirectionIndex.Right] == level.Parent) offset = new Vector2(1, 0);
                if (level.Neighbors[DirectionIndex.Bottom] == level.Parent) offset = new Vector2(0, 1);
                if (level.Neighbors[DirectionIndex.Left] == level.Parent) offset = new Vector2(-1, 0);

                level = level.Parent;
                position -= offset * 92;
            }

            return position;
        }
        /// <summary>
        /// Renders the level.
        /// </summary>
        /// <param name="level">The level.</param>
        private void RenderLevel(ILevel level, Vector2 position)
        {
            IGeometryProvider geometry = this.GraphicsDevice.Geometry;
            IRenderManager renderManager = this.GraphicsDevice.RenderManager;

            float increment = 80;
            bool isSelected = (level == this.SelectedLevel);

            this._renderedLevels.Add(level, true);

            if (level.Neighbors != null)
            {
                ILevel top = level.Neighbors[DirectionIndex.Top];
                ILevel right = level.Neighbors[DirectionIndex.Right];
                ILevel bottom = level.Neighbors[DirectionIndex.Bottom];
                ILevel left = level.Neighbors[DirectionIndex.Left];

                IPen topPen = top != null && top.Container.IsUnlocked ? this._levelPen : this._lockedPen;
                IPen rightPen = right != null && right.Container.IsUnlocked ? this._levelPen : this._lockedPen;
                IPen bottomPen = bottom != null && bottom.Container.IsUnlocked ? this._levelPen : this._lockedPen;
                IPen leftPen = left != null && left.Container.IsUnlocked ? this._levelPen : this._lockedPen;

                if (top != null && !this._renderedLevels.ContainsKey(top))
                {
                    geometry.DrawLine(topPen,
                        position + new Vector2(0, -13), 
                        position + new Vector2(0, -increment));

                    this.RenderLevel(top, position + new Vector2(0, -increment - 12));
                }
                if (right != null && !this._renderedLevels.ContainsKey(right))
                {
                    geometry.DrawLine(rightPen, 
                        position + new Vector2(13, 0),
                        position + new Vector2(increment, 0));

                    this.RenderLevel(right, position + new Vector2(increment + 12, 0));
                }
                if (bottom != null && !this._renderedLevels.ContainsKey(bottom))
                {
                    geometry.DrawLine(bottomPen,
                        position + new Vector2(0, 13),
                        position + new Vector2(0, increment));

                    this.RenderLevel(bottom, position + new Vector2(0, increment + 12));
                }
                if (left != null && !this._renderedLevels.ContainsKey(left))
                {
                    geometry.DrawLine(leftPen,
                        position + new Vector2(-13, 0),
                        position + new Vector2(-increment, 0));

                    this.RenderLevel(left, position + new Vector2(-increment - 12, 0));
                }
            }

            IPen levelPen = this._lockedPen;
            if (level.Container.IsUnlocked) levelPen = this._levelPen;

            geometry.DrawCircle(levelPen, position, 10);

            Vector2 iconPosition = position - new Vector2(17, 28);
            if (isSelected)
            {
                iconPosition += new Vector2(0, this.Offset);
            }

            if (isSelected)
            {
                geometry.DrawCircle(this._selectedPen, position, 14);
            }
            if (!level.Container.IsUnlocked)
            {
                renderManager.Tint(new Color(0.3f, 0.3f, 0.3f, 0.8f));
            }

            renderManager.Render(level.Icon, iconPosition);
            renderManager.Tint(Color.White);
        }
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            this.GraphicsDevice.Clear(new Color(0.9f, 0.82f, 0.8f));
            this._renderedLevels.Clear();

            Vector2 offset = this.GraphicsDevice.DisplayMode.Center;
            offset -= this.Camera.DisplayOffset;

            this.GraphicsDevice.RenderManager.Offset(offset);
            this.RenderLevel(this.RootLevel, Vector2.Zero);

            IGeometryProvider geometry = this.GraphicsDevice.Geometry;
            IRenderManager renderManager = this.GraphicsDevice.RenderManager;

            renderManager.Offset(Vector2.Zero);

            geometry.FillRectangle(this._panelBrush, new Rectangle(0, 240, 400, 60));
            geometry.DrawLine(new Color(0, 0, 0, 0.4f), new Vector2(0, 240), new Vector2(400, 240));

            string levelInformation = this.SelectedLevel.Name + "\n";
            if (this.SelectedLevel.ShowDescription)
            {
                levelInformation += string.Format("Best time: {0}", this.SelectedLevel.Container.BestTime) + "\n";
                levelInformation += string.Format("Highscore: {0}", this.SelectedLevel.Container.Highscore) + "\n";
            }

            Art.Font.RenderShadowed(levelInformation, new Vector2(10, 250), 0.7f);

            Vector2 iconPosition = new Vector2(395, 270);
            ITexture[] icons = this.GetMobIcons(this.SelectedLevel);

            for (int i = icons.Length - 1; i >= 0; i--)
            {
                iconPosition -= new Vector2(icons[i].Width + 5, 0);

                renderManager.Render(icons[i],
                    iconPosition - new Vector2(0, icons[i].Height * 0.5f));
            }
        }
        #endregion
    }
}
