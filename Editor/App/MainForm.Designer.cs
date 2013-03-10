namespace Xemio.PrincessDefense.Editor.App
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mOpen = new System.Windows.Forms.MenuItem();
            this.mSave = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.mExit = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mUndo = new System.Windows.Forms.MenuItem();
            this.mRedo = new System.Windows.Forms.MenuItem();
            this.mObjects = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.mGrid16 = new System.Windows.Forms.MenuItem();
            this.mGrid32 = new System.Windows.Forms.MenuItem();
            this.mGrid64 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.mGridNone = new System.Windows.Forms.MenuItem();
            this.pSurface = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.mObjects,
            this.menuItem8});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mOpen,
            this.mSave,
            this.menuItem5,
            this.mExit});
            this.menuItem1.Text = "File";
            // 
            // mOpen
            // 
            this.mOpen.Index = 0;
            this.mOpen.Text = "Open";
            this.mOpen.Click += new System.EventHandler(this.mOpenClick);
            // 
            // mSave
            // 
            this.mSave.Index = 1;
            this.mSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.mSave.Text = "Save";
            this.mSave.Click += new System.EventHandler(this.mSaveClick);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 2;
            this.menuItem5.Text = "-";
            // 
            // mExit
            // 
            this.mExit.Index = 3;
            this.mExit.Text = "Exit";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mUndo,
            this.mRedo});
            this.menuItem2.Text = "Edit";
            // 
            // mUndo
            // 
            this.mUndo.Index = 0;
            this.mUndo.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
            this.mUndo.Text = "Undo";
            this.mUndo.Click += new System.EventHandler(this.mUndoClick);
            // 
            // mRedo
            // 
            this.mRedo.Index = 1;
            this.mRedo.Shortcut = System.Windows.Forms.Shortcut.CtrlY;
            this.mRedo.Text = "Redo";
            this.mRedo.Click += new System.EventHandler(this.mRedoClick);
            // 
            // mObjects
            // 
            this.mObjects.Index = 2;
            this.mObjects.Text = "Objects";
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 3;
            this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mGrid16,
            this.mGrid32,
            this.mGrid64,
            this.menuItem9,
            this.mGridNone});
            this.menuItem8.Text = "Grid";
            // 
            // mGrid16
            // 
            this.mGrid16.Index = 0;
            this.mGrid16.Text = "16x16";
            this.mGrid16.Click += new System.EventHandler(this.mGrid16Click);
            // 
            // mGrid32
            // 
            this.mGrid32.Index = 1;
            this.mGrid32.Text = "32x32";
            this.mGrid32.Click += new System.EventHandler(this.mGrid32Click);
            // 
            // mGrid64
            // 
            this.mGrid64.Index = 2;
            this.mGrid64.Text = "64x64";
            this.mGrid64.Click += new System.EventHandler(this.mGrid64Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 3;
            this.menuItem9.Text = "-";
            // 
            // mGridNone
            // 
            this.mGridNone.Index = 4;
            this.mGridNone.Text = "None";
            this.mGridNone.Click += new System.EventHandler(this.mGridNoneClick);
            // 
            // pSurface
            // 
            this.pSurface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pSurface.Location = new System.Drawing.Point(0, 0);
            this.pSurface.Name = "pSurface";
            this.pSurface.Size = new System.Drawing.Size(640, 639);
            this.pSurface.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 639);
            this.Controls.Add(this.pSurface);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Defend the Princess! - Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem mOpen;
        private System.Windows.Forms.MenuItem mSave;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem mExit;
        private System.Windows.Forms.Panel pSurface;
        private System.Windows.Forms.MenuItem mObjects;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem mUndo;
        private System.Windows.Forms.MenuItem mRedo;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem mGrid16;
        private System.Windows.Forms.MenuItem mGrid32;
        private System.Windows.Forms.MenuItem mGrid64;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem mGridNone;
    }
}

