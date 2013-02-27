using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Rendering.Sprites;
using Xemio.GameLibrary.Rendering;

namespace Xemio.PrincessDefense.Entities.Components
{
    public class AnimationComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public AnimationComponent(Entity entity) : base(entity)
        {
            this.Instances = new List<SpriteAnimationInstance>();
        }
        #endregion

        #region Fields
        private bool _paused;
        private float _pauseTime;

        private SpriteAnimationInstance _currentInstance;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the animations.
        /// </summary>
        public List<SpriteAnimationInstance> Instances { get; private set; }
        /// <summary>
        /// Gets the instance.
        /// </summary>
        public SpriteAnimationInstance Instance
        {
            get { return this._currentInstance; }
        }
        /// <summary>
        /// Gets the current frame.
        /// </summary>
        public ITexture Frame { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified animation.
        /// </summary>
        /// <param name="animation">The animation.</param>
        public void Add(SpriteAnimation animation)
        {
            this.Instances.Add(animation.CreateInstance());
        }
        /// <summary>
        /// Adds the specified animations.
        /// </summary>
        /// <param name="animations">The animations.</param>
        public void Add(IEnumerable<SpriteAnimation> animations)
        {
            foreach (SpriteAnimation animation in animations)
            {
                this.Add(animation);
            }
        }
        /// <summary>
        /// Plays the animation.
        /// </summary>
        /// <param name="name">The name.</param>
        public void PlayAnimation(string name)
        {
            SpriteAnimationInstance instance = this.Instances.FirstOrDefault(
                i => i.Animation.Name == name);

            if (instance != null)
            {
                if (this._currentInstance != null &&
                    this._currentInstance.Animation.Name == instance.Animation.Name)
                {
                    return;
                }

                this._currentInstance = instance;
                this._currentInstance.Reset();
            }
        }
        /// <summary>
        /// Pauses the animation.
        /// </summary>
        public void PauseAnimation()
        {
            this._paused = true;
        }
        /// <summary>
        /// Pauses the animation.
        /// </summary>
        /// <param name="time">The time.</param>
        public void PauseAnimation(float time)
        {
            this.PauseAnimation();
            this._pauseTime = time;
        }
        /// <summary>
        /// Resumes the animation.
        /// </summary>
        public void ResumeAnimation()
        {
            this._pauseTime = 0;
            this._paused = false;
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this.Frame = null;
            if (this._currentInstance != null && !this._paused)
            {
                this._currentInstance.Tick(elapsed);
                this.Frame = this._currentInstance.Frame;
            }

            if (this._paused && this._pauseTime > 0)
            {
                this._pauseTime -= elapsed;
                if (this._pauseTime <= 0)
                {
                    this.ResumeAnimation();
                }
            }
        }
        #endregion
    }
}
