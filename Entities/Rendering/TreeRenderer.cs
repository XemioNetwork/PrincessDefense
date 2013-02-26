using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using PrincessDefense.Entities.Components;
using Xemio.GameLibrary.Math;

namespace PrincessDefense.Entities.Rendering
{
    public class TreeRenderer : EntityRenderer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeRenderer"/> class.
        /// </summary>
        /// <param name="tree">The tree.</param>
        public TreeRenderer(Tree tree) : base(tree)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the tree.
        /// </summary>
        public Tree Tree
        {
            get { return this.Entity as Tree; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            AnimationComponent animation = this.Entity.GetComponent<AnimationComponent>();

            if (animation != null && animation.CurrentFrame != null)
            {
                int width = animation.CurrentFrame.Width;
                int height = animation.CurrentFrame.Height;

                Vector2 position = this.Tree.Position;

                position -= new Vector2(width * 0.5f, height * 0.5f);

                this.RenderManager.Render(animation.CurrentFrame, position);
            }
        }
        #endregion
    }
}
