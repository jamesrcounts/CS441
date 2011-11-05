﻿namespace PhotoBuddy.Controls
{
    partial class AlbumIconUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlbumIconUserControl));
            this.foundationTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.thumbnailPanel = new System.Windows.Forms.Panel();
            this.albumCountLabel = new System.Windows.Forms.Label();
            this.coverPhotoPictureBox = new System.Windows.Forms.PictureBox();
            this.thumbnailPictureBox = new System.Windows.Forms.PictureBox();
            this.albumNameTextBox = new System.Windows.Forms.TextBox();
            this.albumContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.foundationTableLayoutPanel.SuspendLayout();
            this.thumbnailPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.coverPhotoPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thumbnailPictureBox)).BeginInit();
            this.albumContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // foundationTableLayoutPanel
            // 
            this.foundationTableLayoutPanel.BackColor = System.Drawing.SystemColors.Window;
            this.foundationTableLayoutPanel.ColumnCount = 1;
            this.foundationTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.foundationTableLayoutPanel.Controls.Add(this.thumbnailPanel, 0, 0);
            this.foundationTableLayoutPanel.Controls.Add(this.albumNameTextBox, 0, 1);
            this.foundationTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foundationTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.foundationTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.foundationTableLayoutPanel.Name = "foundationTableLayoutPanel";
            this.foundationTableLayoutPanel.RowCount = 2;
            this.foundationTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 124F));
            this.foundationTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.foundationTableLayoutPanel.Size = new System.Drawing.Size(164, 150);
            this.foundationTableLayoutPanel.TabIndex = 1;
            // 
            // thumbnailPanel
            // 
            this.thumbnailPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.thumbnailPanel.AutoSize = true;
            this.thumbnailPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.thumbnailPanel.BackColor = System.Drawing.Color.White;
            this.thumbnailPanel.Controls.Add(this.albumCountLabel);
            this.thumbnailPanel.Controls.Add(this.coverPhotoPictureBox);
            this.thumbnailPanel.Controls.Add(this.thumbnailPictureBox);
            this.thumbnailPanel.Location = new System.Drawing.Point(0, 0);
            this.thumbnailPanel.Margin = new System.Windows.Forms.Padding(0);
            this.thumbnailPanel.MinimumSize = new System.Drawing.Size(10, 10);
            this.thumbnailPanel.Name = "thumbnailPanel";
            this.thumbnailPanel.Padding = new System.Windows.Forms.Padding(2);
            this.thumbnailPanel.Size = new System.Drawing.Size(164, 124);
            this.thumbnailPanel.TabIndex = 0;
            // 
            // albumCountLabel
            // 
            this.albumCountLabel.BackColor = System.Drawing.Color.White;
            this.albumCountLabel.Location = new System.Drawing.Point(22, 5);
            this.albumCountLabel.Name = "albumCountLabel";
            this.albumCountLabel.Size = new System.Drawing.Size(48, 14);
            this.albumCountLabel.TabIndex = 2;
            this.albumCountLabel.Text = "57 ";
            this.albumCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // coverPhotoPictureBox
            // 
            this.coverPhotoPictureBox.BackColor = System.Drawing.Color.Black;
            this.coverPhotoPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.coverPhotoPictureBox.ContextMenuStrip = this.albumContextMenuStrip;
            this.coverPhotoPictureBox.Location = new System.Drawing.Point(35, 36);
            this.coverPhotoPictureBox.Name = "coverPhotoPictureBox";
            this.coverPhotoPictureBox.Size = new System.Drawing.Size(92, 61);
            this.coverPhotoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.coverPhotoPictureBox.TabIndex = 1;
            this.coverPhotoPictureBox.TabStop = false;
            this.coverPhotoPictureBox.Click += new System.EventHandler(this.HandleCoverImagePictureBoxClick);
            // 
            // thumbnailPictureBox
            // 
            this.thumbnailPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.thumbnailPictureBox.ContextMenuStrip = this.albumContextMenuStrip;
            this.thumbnailPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thumbnailPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("thumbnailPictureBox.Image")));
            this.thumbnailPictureBox.Location = new System.Drawing.Point(2, 2);
            this.thumbnailPictureBox.Margin = new System.Windows.Forms.Padding(5);
            this.thumbnailPictureBox.Name = "thumbnailPictureBox";
            this.thumbnailPictureBox.Size = new System.Drawing.Size(160, 120);
            this.thumbnailPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.thumbnailPictureBox.TabIndex = 0;
            this.thumbnailPictureBox.TabStop = false;
            this.thumbnailPictureBox.Click += new System.EventHandler(this.HandleCoverImagePictureBoxClick);
            this.thumbnailPictureBox.MouseEnter += new System.EventHandler(this.HighlightPhoto);
            this.thumbnailPictureBox.MouseLeave += new System.EventHandler(this.RemovePhotoHighlight);
            // 
            // albumNameTextBox
            // 
            this.albumNameTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.albumNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.albumNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.albumNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.albumNameTextBox.Location = new System.Drawing.Point(1, 125);
            this.albumNameTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.albumNameTextBox.Name = "albumNameTextBox";
            this.albumNameTextBox.ReadOnly = true;
            this.albumNameTextBox.Size = new System.Drawing.Size(162, 13);
            this.albumNameTextBox.TabIndex = 1;
            this.albumNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // albumContextMenuStrip
            // 
            this.albumContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem});
            this.albumContextMenuStrip.Name = "albumContextMenuStrip";
            this.albumContextMenuStrip.Size = new System.Drawing.Size(118, 48);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.HandleDeleteToolStripItemClick);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.HandleRenameAlbumClick);
            // 
            // AlbumIconUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.foundationTableLayoutPanel);
            this.Name = "AlbumIconUserControl";
            this.Size = new System.Drawing.Size(164, 150);
            this.foundationTableLayoutPanel.ResumeLayout(false);
            this.foundationTableLayoutPanel.PerformLayout();
            this.thumbnailPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.coverPhotoPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thumbnailPictureBox)).EndInit();
            this.albumContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel foundationTableLayoutPanel;
        private System.Windows.Forms.Panel thumbnailPanel;
        private System.Windows.Forms.PictureBox thumbnailPictureBox;
        private System.Windows.Forms.TextBox albumNameTextBox;
        private System.Windows.Forms.Label albumCountLabel;
        private System.Windows.Forms.PictureBox coverPhotoPictureBox;
        private System.Windows.Forms.ContextMenuStrip albumContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
    }
}
