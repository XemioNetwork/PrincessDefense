using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.PrincessDefense.Entities.Components;
using Xemio.PrincessDefense.Entities.Terrain;

namespace Xemio.PrincessDefense.Entities.Rendering
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

        #region Fields
        private IBrush _shadowBrush;
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

            if (this._shadowBrush == null)
            {
                this._shadowBrush = this.Geometry.Factory.CreateSolid(new Color(0, 0, 0, 0.2f));
            }

            if (animation != null && animation.Frame != null)
            {
                int width = animation.Frame.Width;
                int height = animation.Frame.Height;

                Vector2 position = this.Tree.Position;
                position -= new Vector2(width * 0.5f, height * 0.5f);

                this.Geometry.FillEllipse(this._shadowBrush, new Rectangle(18, 88, 54, 30) + position);
                this.RenderManager.Render(animation.Frame, position);
            }
        }
        #endregion
    }
}
