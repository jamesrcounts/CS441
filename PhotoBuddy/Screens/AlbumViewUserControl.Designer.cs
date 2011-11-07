﻿// <autogenerated />
namespace PhotoBuddy.Screens
{
    partial class AlbumViewUserControl
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
            this.foundationTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.headerFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.albumNameLabel = new System.Windows.Forms.Label();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.addPhotosButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.photosFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.addPhotosFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.albumSizeLabel = new System.Windows.Forms.Label();
            this.foundationTableLayoutPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.headerFlowLayoutPanel.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // foundationTableLayoutPanel
            // 
            this.foundationTableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.foundationTableLayoutPanel.ColumnCount = 1;
            this.foundationTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.foundationTableLayoutPanel.Controls.Add(this.headerPanel, 0, 0);
            this.foundationTableLayoutPanel.Controls.Add(this.footerPanel, 0, 3);
            this.foundationTableLayoutPanel.Controls.Add(this.photosFlowPanel, 0, 2);
            this.foundationTableLayoutPanel.Controls.Add(this.contentPanel, 0, 1);
            this.foundationTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foundationTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.foundationTableLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.foundationTableLayoutPanel.Name = "foundationTableLayoutPanel";
            this.foundationTableLayoutPanel.RowCount = 4;
            this.foundationTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.foundationTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.foundationTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.foundationTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.foundationTableLayoutPanel.Size = new System.Drawing.Size(500, 400);
            this.foundationTableLayoutPanel.TabIndex = 2;
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.albumSizeLabel);
            this.headerPanel.Controls.Add(this.headerFlowLayoutPanel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(500, 46);
            this.headerPanel.TabIndex = 0;
            // 
            // headerFlowLayoutPanel
            // 
            this.headerFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.headerFlowLayoutPanel.AutoSize = true;
            this.headerFlowLayoutPanel.Controls.Add(this.albumNameLabel);
            this.headerFlowLayoutPanel.Location = new System.Drawing.Point(13, -7);
            this.headerFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.headerFlowLayoutPanel.Name = "headerFlowLayoutPanel";
            this.headerFlowLayoutPanel.Size = new System.Drawing.Size(319, 50);
            this.headerFlowLayoutPanel.TabIndex = 3;
            this.headerFlowLayoutPanel.WrapContents = false;
            // 
            // albumNameLabel
            // 
            this.albumNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.albumNameLabel.AutoSize = true;
            this.albumNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.albumNameLabel.ForeColor = System.Drawing.Color.Black;
            this.albumNameLabel.Location = new System.Drawing.Point(0, 12);
            this.albumNameLabel.Margin = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.albumNameLabel.MaximumSize = new System.Drawing.Size(533, 38);
            this.albumNameLabel.Name = "albumNameLabel";
            this.albumNameLabel.Size = new System.Drawing.Size(204, 38);
            this.albumNameLabel.TabIndex = 2;
            this.albumNameLabel.Text = "album name";
            this.albumNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // footerPanel
            // 
            this.footerPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.footerPanel.Controls.Add(this.addPhotosButton);
            this.footerPanel.Controls.Add(this.backButton);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.footerPanel.Location = new System.Drawing.Point(0, 356);
            this.footerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(500, 44);
            this.footerPanel.TabIndex = 1;
            // 
            // addPhotosButton
            // 
            this.addPhotosButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addPhotosButton.AutoSize = true;
            this.addPhotosButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addPhotosButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(70)))), ((int)(((byte)(102)))));
            this.addPhotosButton.FlatAppearance.BorderSize = 0;
            this.addPhotosButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.addPhotosButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addPhotosButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addPhotosButton.ForeColor = System.Drawing.Color.White;
            this.addPhotosButton.Location = new System.Drawing.Point(362, 4);
            this.addPhotosButton.Margin = new System.Windows.Forms.Padding(0, 0, 13, 0);
            this.addPhotosButton.Name = "addPhotosButton";
            this.addPhotosButton.Size = new System.Drawing.Size(124, 35);
            this.addPhotosButton.TabIndex = 0;
            this.addPhotosButton.Text = "Add Photos";
            this.addPhotosButton.UseVisualStyleBackColor = false;
            this.addPhotosButton.Click += new System.EventHandler(this.HandleAddPhotosButtonClick);
            this.addPhotosButton.MouseEnter += new System.EventHandler(this.HandleButtonMouseEnter);
            this.addPhotosButton.MouseLeave += new System.EventHandler(this.HandleButtonMouseLeave);
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.backButton.AutoSize = true;
            this.backButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.backButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(70)))), ((int)(((byte)(102)))));
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backButton.ForeColor = System.Drawing.Color.White;
            this.backButton.Location = new System.Drawing.Point(274, 4);
            this.backButton.Margin = new System.Windows.Forms.Padding(0, 0, 11, 0);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(66, 35);
            this.backButton.TabIndex = 1;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.HandleBackButtonClick);
            this.backButton.MouseEnter += new System.EventHandler(this.HandleButtonMouseEnter);
            this.backButton.MouseLeave += new System.EventHandler(this.HandleButtonMouseLeave);
            // 
            // photosFlowPanel
            // 
            this.photosFlowPanel.AutoScroll = true;
            this.photosFlowPanel.BackColor = System.Drawing.Color.White;
            this.photosFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.photosFlowPanel.Location = new System.Drawing.Point(13, 51);
            this.photosFlowPanel.Margin = new System.Windows.Forms.Padding(13, 4, 13, 4);
            this.photosFlowPanel.Name = "photosFlowPanel";
            this.photosFlowPanel.Size = new System.Drawing.Size(474, 301);
            this.photosFlowPanel.TabIndex = 2;
            this.photosFlowPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.HandlePhotosFlowPanelMouseClick);
            this.photosFlowPanel.MouseEnter += new System.EventHandler(this.HandlePhotosFlowPanelMouseEnter);
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.Black;
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(13, 46);
            this.contentPanel.Margin = new System.Windows.Forms.Padding(13, 0, 13, 0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(474, 1);
            this.contentPanel.TabIndex = 3;
            // 
            // addPhotosFileDialog
            // 
            this.addPhotosFileDialog.Filter = "jpg files (*.jpg)|*.jpg|png files (*.png)|*.png|bmp files (*.bmp)|*.bmp|gif files" +
                " (*.gif)|*.gif";
            this.addPhotosFileDialog.Multiselect = true;
            // 
            // albumSizeLabel
            // 
            this.albumSizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.albumSizeLabel.AutoSize = true;
            this.albumSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.albumSizeLabel.ForeColor = System.Drawing.Color.Black;
            this.albumSizeLabel.Location = new System.Drawing.Point(385, 21);
            this.albumSizeLabel.Margin = new System.Windows.Forms.Padding(3, 0, 40, 0);
            this.albumSizeLabel.Name = "albumSizeLabel";
            this.albumSizeLabel.Size = new System.Drawing.Size(73, 20);
            this.albumSizeLabel.TabIndex = 4;
            this.albumSizeLabel.Text = "0 photos";
            // 
            // AlbumViewUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.foundationTableLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AlbumViewUserControl";
            this.Size = new System.Drawing.Size(500, 400);
            this.foundationTableLayoutPanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.headerFlowLayoutPanel.ResumeLayout(false);
            this.headerFlowLayoutPanel.PerformLayout();
            this.footerPanel.ResumeLayout(false);
            this.footerPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel foundationTableLayoutPanel;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.Button addPhotosButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Label albumNameLabel;
        private System.Windows.Forms.FlowLayoutPanel photosFlowPanel;
        private System.Windows.Forms.FlowLayoutPanel headerFlowLayoutPanel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.OpenFileDialog addPhotosFileDialog;
        private System.Windows.Forms.Label albumSizeLabel;
    }
}
