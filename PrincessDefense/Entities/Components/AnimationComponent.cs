using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Rendering.Sprites;
using Xemio.GameLibrary.Rendering;

namespace PrincessDefense.Entities.Components
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
            this.Animations = new List<SpriteAnimation>();
        }
        #endregion

        #region Fields
        private bool _paused;
        private float _pauseTime;

        private SpriteAnimation _currentAnimation;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the animations.
        /// </summary>
        public List<SpriteAnimation> Animations { get; private set; }
        /// <summary>
        /// Gets the current frame.
        /// </summary>
        public ITexture CurrentFrame { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Plays the animation.
        /// </summary>
        /// <param name="name">The name.</param>
        public void PlayAnimation(string name)
        {
            SpriteAnimation animation = this.Animations.FirstOrDefault(
                anim => anim.Name == name);

            if (animation != null)
            {
                if (this._currentAnimation != null && this._currentAnimation.Name == animation.Name)
                    return;

                this._currentAnimation = animation;
                this._currentAnimation.Reset();
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
            this.CurrentFrame = null;
            if (this._currentAnimation != null && !this._paused)
            {
                this._currentAnimation.Tick(elapsed);
                this.CurrentFrame = this._currentAnimation.CurrentFrame;
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
