using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xemio.PrincessDefense.Editor.Scenes;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Game;
using Xemio.PrincessDefense.Editor.Entities;
using System.IO;
using Xemio.PrincessDefense.Editor.Commands;

namespace Xemio.PrincessDefense.Editor.App
{
    public partial class MainForm : Form
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            foreach (string fileName in Directory.GetFiles(@"Resources\objects", "*.png"))
            {
                MenuItem item = new MenuItem(Path.GetFileName(fileName));
                mObjects.MenuItems.Add(item);

                item.Click += new EventHandler(itemClick);
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the editor.
        /// </summary>
        public EditorScene Editor
        {
            get { return XGL.GetComponent<SceneManager>().GetScene<EditorScene>(); }
        }
        /// <summary>
        /// Gets the surface.
        /// </summary>
        public IntPtr Surface
        {
            get { return this.pSurface.Handle; }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the Click event of the item control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void itemClick(object sender, EventArgs e)
        {
            MenuItem item = sender as MenuItem;
            if (item != null)
            {
                this.Editor.Creator = new MapEntityCreator(
                    Art.Get(@"Resources\objects\" + item.Text), 
                    Registry.GetCommand(item.Text));
            }
        }
        /// <summary>
        /// Handles the Click event of the mGrid16 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mGrid16Click(object sender, EventArgs e)
        {
            this.Editor.GridSize = 16;
        }
        /// <summary>
        /// Handles the Click event of the mGrid32 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mGrid32Click(object sender, EventArgs e)
        {
            this.Editor.GridSize = 32;
        }
        /// <summary>
        /// Handles the Click event of the mGrid64 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mGrid64Click(object sender, EventArgs e)
        {
            this.Editor.GridSize = 64;
        }
        /// <summary>
        /// Handles the Click event of the mGridNone control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mGridNoneClick(object sender, EventArgs e)
        {
            this.Editor.GridSize = 1;
        }
        /// <summary>
        /// Handles the Click event of the mOpen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mOpenClick(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Handles the Click event of the mSave control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mSaveClick(object sender, EventArgs e)
        {
            using (SaveFileDialog fileDialog = new SaveFileDialog())
            {
                fileDialog.Filter = "Text files (*.txt)|*.txt";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder contentBuilder = new StringBuilder();
                    foreach (MapEntity entity in this.Editor.Environment)
                    {
                        contentBuilder.Append(entity.Command);
                        contentBuilder.Append(' ');
                        contentBuilder.Append(entity.Position.X);
                        contentBuilder.Append(' ');
                        contentBuilder.Append(entity.Position.Y);

                        contentBuilder.AppendLine();
                    }

                    File.WriteAllText(fileDialog.FileName, contentBuilder.ToString());
                }
            }
        }
        /// <summary>
        /// Handles the Click event of the mUndo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mUndoClick(object sender, EventArgs e)
        {
            CommandManager.Instance.Undo();
        }
        /// <summary>
        /// Handles the Click event of the mRedo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mRedoClick(object sender, EventArgs e)
        {
            CommandManager.Instance.Redo();
        }
        #endregion
    }
}
