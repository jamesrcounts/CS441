﻿// <autogenerated />
using System.Windows.Forms;
namespace PhotoBuddy.Controls
{
    partial class ThumbnailUserControl
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
            this.foundationTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.photoPanel = new System.Windows.Forms.Panel();
            this.thumbnailPictureBox = new System.Windows.Forms.PictureBox();
            this.thumbnailContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.photoNameTextBox = new System.Windows.Forms.TextBox();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.foundationTableLayoutPanel.SuspendLayout();
            this.photoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thumbnailPictureBox)).BeginInit();
            this.thumbnailContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // foundationTableLayoutPanel
            // 
            this.foundationTableLayoutPanel.BackColor = System.Drawing.SystemColors.Window;
            this.foundationTableLayoutPanel.ColumnCount = 1;
            this.foundationTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.foundationTableLayoutPanel.Controls.Add(this.photoPanel, 0, 0);
            this.foundationTableLayoutPanel.Controls.Add(this.photoNameTextBox, 0, 1);
            this.foundationTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foundationTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.foundationTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.foundationTableLayoutPanel.Name = "foundationTableLayoutPanel";
            this.foundationTableLayoutPanel.RowCount = 2;
            this.foundationTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 124F));
            this.foundationTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.foundationTableLayoutPanel.Size = new System.Drawing.Size(164, 150);
            this.foundationTableLayoutPanel.TabIndex = 0;
            // 
            // photoPanel
            // 
            this.photoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.photoPanel.AutoSize = true;
            this.photoPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.photoPanel.BackColor = System.Drawing.Color.White;
            this.photoPanel.Controls.Add(this.thumbnailPictureBox);
            this.photoPanel.Location = new System.Drawing.Point(0, 0);
            this.photoPanel.Margin = new System.Windows.Forms.Padding(0);
            this.photoPanel.MinimumSize = new System.Drawing.Size(10, 10);
            this.photoPanel.Name = "photoPanel";
            this.photoPanel.Padding = new System.Windows.Forms.Padding(2);
            this.photoPanel.Size = new System.Drawing.Size(164, 124);
            this.photoPanel.TabIndex = 0;
            // 
            // thumbnailPictureBox
            // 
            this.thumbnailPictureBox.BackColor = System.Drawing.Color.White;
            this.thumbnailPictureBox.ContextMenuStrip = this.thumbnailContextMenu;
            this.thumbnailPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thumbnailPictureBox.Location = new System.Drawing.Point(2, 2);
            this.thumbnailPictureBox.Margin = new System.Windows.Forms.Padding(5);
            this.thumbnailPictureBox.Name = "thumbnailPictureBox";
            this.thumbnailPictureBox.Size = new System.Drawing.Size(160, 120);
            this.thumbnailPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.thumbnailPictureBox.TabIndex = 0;
            this.thumbnailPictureBox.TabStop = false;
            this.thumbnailPictureBox.MouseEnter += new System.EventHandler(this.HighlightPhoto);
            this.thumbnailPictureBox.MouseLeave += new System.EventHandler(this.RemovePhotoHighlight);
            // 
            // thumbnailContextMenu
            // 
            this.thumbnailContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem});
            this.thumbnailContextMenu.Name = "contextMenuStrip1";
            this.thumbnailContextMenu.Size = new System.Drawing.Size(153, 70);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.HandleDeleteToolStripMenuItemClick);
            // 
            // photoNameTextBox
            // 
            this.photoNameTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.photoNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.photoNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.photoNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.photoNameTextBox.Location = new System.Drawing.Point(1, 125);
            this.photoNameTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.photoNameTextBox.Name = "photoNameTextBox";
            this.photoNameTextBox.ReadOnly = true;
            this.photoNameTextBox.Size = new System.Drawing.Size(162, 13);
            this.photoNameTextBox.TabIndex = 1;
            this.photoNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.photoNameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HandleKeyPress);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.RenamePhoto);
            // 
            // ThumbnailUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.foundationTableLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.Name = "ThumbnailUserControl";
            this.Size = new System.Drawing.Size(164, 150);
            this.foundationTableLayoutPanel.ResumeLayout(false);
            this.foundationTableLayoutPanel.PerformLayout();
            this.photoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.thumbnailPictureBox)).EndInit();
            this.thumbnailContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel foundationTableLayoutPanel;
        private System.Windows.Forms.Panel photoPanel;
        private PictureBox thumbnailPictureBox;
        private System.Windows.Forms.TextBox photoNameTextBox;
        private ContextMenuStrip thumbnailContextMenu;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem renameToolStripMenuItem;
    }
}
