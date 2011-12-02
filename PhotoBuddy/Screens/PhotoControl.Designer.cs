﻿// <autogenerated />
namespace PhotoBuddy.Screens
{
    partial class CropPhotoControl
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CropPhotoControl));
            this.foundationTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.EditLabel = new System.Windows.Forms.Label();
            this.EditFooterPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ConfirmFooterPanel = new System.Windows.Forms.Panel();
            this.BlackAndWhiteButton = new System.Windows.Forms.Button();
            this.ConfirmCropButton = new System.Windows.Forms.Button();
            this.CancelFooterPanel = new System.Windows.Forms.Panel();
            this.CancelEditButton = new System.Windows.Forms.Button();
            this.photoCropBox = new PhotoBuddy.Controls.CropBox();
            this.foundationTableLayoutPanel.SuspendLayout();
            this.EditFooterPanel.SuspendLayout();
            this.ConfirmFooterPanel.SuspendLayout();
            this.CancelFooterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.photoCropBox)).BeginInit();
            this.SuspendLayout();
            // 
            // foundationTableLayoutPanel
            // 
            this.foundationTableLayoutPanel.BackColor = System.Drawing.Color.White;
            this.foundationTableLayoutPanel.ColumnCount = 1;
            this.foundationTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.foundationTableLayoutPanel.Controls.Add(this.EditLabel, 0, 1);
            this.foundationTableLayoutPanel.Controls.Add(this.EditFooterPanel, 0, 2);
            this.foundationTableLayoutPanel.Controls.Add(this.photoCropBox, 0, 0);
            this.foundationTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foundationTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.foundationTableLayoutPanel.Margin = new System.Windows.Forms.Padding(2);
            this.foundationTableLayoutPanel.Name = "foundationTableLayoutPanel";
            this.foundationTableLayoutPanel.RowCount = 3;
            this.foundationTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.foundationTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.foundationTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.foundationTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.foundationTableLayoutPanel.Size = new System.Drawing.Size(586, 462);
            this.foundationTableLayoutPanel.TabIndex = 2;
            // 
            // EditLabel
            // 
            this.EditLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.EditLabel.AutoSize = true;
            this.EditLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditLabel.ForeColor = System.Drawing.Color.Black;
            this.EditLabel.Location = new System.Drawing.Point(0, 388);
            this.EditLabel.Margin = new System.Windows.Forms.Padding(0);
            this.EditLabel.Name = "EditLabel";
            this.EditLabel.Size = new System.Drawing.Size(586, 29);
            this.EditLabel.TabIndex = 9;
            this.EditLabel.Text = "Edit Photo";
            this.EditLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EditFooterPanel
            // 
            this.EditFooterPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.EditFooterPanel.ColumnCount = 5;
            this.EditFooterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.EditFooterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.EditFooterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.EditFooterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.EditFooterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.EditFooterPanel.Controls.Add(this.ConfirmFooterPanel, 4, 0);
            this.EditFooterPanel.Controls.Add(this.CancelFooterPanel, 0, 0);
            this.EditFooterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditFooterPanel.Location = new System.Drawing.Point(0, 426);
            this.EditFooterPanel.Margin = new System.Windows.Forms.Padding(0);
            this.EditFooterPanel.Name = "EditFooterPanel";
            this.EditFooterPanel.RowCount = 1;
            this.EditFooterPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.EditFooterPanel.Size = new System.Drawing.Size(586, 36);
            this.EditFooterPanel.TabIndex = 3;
            // 
            // ConfirmFooterPanel
            // 
            this.ConfirmFooterPanel.Controls.Add(this.BlackAndWhiteButton);
            this.ConfirmFooterPanel.Controls.Add(this.ConfirmCropButton);
            this.ConfirmFooterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfirmFooterPanel.Location = new System.Drawing.Point(383, 0);
            this.ConfirmFooterPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ConfirmFooterPanel.Name = "ConfirmFooterPanel";
            this.ConfirmFooterPanel.Size = new System.Drawing.Size(203, 36);
            this.ConfirmFooterPanel.TabIndex = 5;
            // 
            // BlackAndWhiteButton
            // 
            this.BlackAndWhiteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BlackAndWhiteButton.AutoSize = true;
            this.BlackAndWhiteButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BlackAndWhiteButton.BackColor = System.Drawing.Color.Transparent;
            this.BlackAndWhiteButton.FlatAppearance.BorderSize = 0;
            this.BlackAndWhiteButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.BlackAndWhiteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BlackAndWhiteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BlackAndWhiteButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(70)))), ((int)(((byte)(102)))));
            this.BlackAndWhiteButton.Location = new System.Drawing.Point(44, 4);
            this.BlackAndWhiteButton.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.BlackAndWhiteButton.Name = "BlackAndWhiteButton";
            this.BlackAndWhiteButton.Size = new System.Drawing.Size(49, 30);
            this.BlackAndWhiteButton.TabIndex = 12;
            this.BlackAndWhiteButton.Text = "B/W";
            this.BlackAndWhiteButton.UseVisualStyleBackColor = false;
            this.BlackAndWhiteButton.Click += new System.EventHandler(this.Click_BlacknWhite);
            // 
            // ConfirmCropButton
            // 
            this.ConfirmCropButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfirmCropButton.AutoSize = true;
            this.ConfirmCropButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ConfirmCropButton.BackColor = System.Drawing.Color.Transparent;
            this.ConfirmCropButton.FlatAppearance.BorderSize = 0;
            this.ConfirmCropButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.ConfirmCropButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConfirmCropButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfirmCropButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(70)))), ((int)(((byte)(102)))));
            this.ConfirmCropButton.Location = new System.Drawing.Point(140, 4);
            this.ConfirmCropButton.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.ConfirmCropButton.Name = "ConfirmCropButton";
            this.ConfirmCropButton.Size = new System.Drawing.Size(53, 30);
            this.ConfirmCropButton.TabIndex = 11;
            this.ConfirmCropButton.Text = "Crop";
            this.ConfirmCropButton.UseVisualStyleBackColor = false;
            this.ConfirmCropButton.Click += new System.EventHandler(this.HandleCropButtonClick);
            // 
            // CancelFooterPanel
            // 
            this.CancelFooterPanel.Controls.Add(this.CancelEditButton);
            this.CancelFooterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CancelFooterPanel.Location = new System.Drawing.Point(0, 0);
            this.CancelFooterPanel.Margin = new System.Windows.Forms.Padding(0);
            this.CancelFooterPanel.Name = "CancelFooterPanel";
            this.CancelFooterPanel.Size = new System.Drawing.Size(203, 36);
            this.CancelFooterPanel.TabIndex = 6;
            // 
            // CancelEditButton
            // 
            this.CancelEditButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelEditButton.AutoSize = true;
            this.CancelEditButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelEditButton.BackColor = System.Drawing.Color.Transparent;
            this.CancelEditButton.FlatAppearance.BorderSize = 0;
            this.CancelEditButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.CancelEditButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelEditButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelEditButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(70)))), ((int)(((byte)(102)))));
            this.CancelEditButton.Location = new System.Drawing.Point(10, 4);
            this.CancelEditButton.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.CancelEditButton.Name = "CancelEditButton";
            this.CancelEditButton.Size = new System.Drawing.Size(68, 30);
            this.CancelEditButton.TabIndex = 1;
            this.CancelEditButton.Text = "Cancel";
            this.CancelEditButton.UseMnemonic = false;
            this.CancelEditButton.UseVisualStyleBackColor = false;
            this.CancelEditButton.Click += new System.EventHandler(this.HandleCancelButtonClick);
            // 
            // photoCropBox
            // 
            this.photoCropBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.photoCropBox.Image = ((System.Drawing.Image)(resources.GetObject("photoCropBox.Image")));
            this.photoCropBox.Location = new System.Drawing.Point(3, 3);
            this.photoCropBox.Name = "photoCropBox";
            this.photoCropBox.Photo = ((System.Drawing.Image)(resources.GetObject("photoCropBox.Photo")));
            this.photoCropBox.Size = new System.Drawing.Size(580, 374);
            this.photoCropBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.photoCropBox.TabIndex = 10;
            this.photoCropBox.TabStop = false;
            // 
            // CropPhotoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.foundationTableLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CropPhotoControl";
            this.Size = new System.Drawing.Size(586, 462);
            this.foundationTableLayoutPanel.ResumeLayout(false);
            this.foundationTableLayoutPanel.PerformLayout();
            this.EditFooterPanel.ResumeLayout(false);
            this.ConfirmFooterPanel.ResumeLayout(false);
            this.ConfirmFooterPanel.PerformLayout();
            this.CancelFooterPanel.ResumeLayout(false);
            this.CancelFooterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.photoCropBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel foundationTableLayoutPanel;
        private System.Windows.Forms.Label EditLabel;
        private System.Windows.Forms.TableLayoutPanel EditFooterPanel;
        private System.Windows.Forms.Panel ConfirmFooterPanel;
        private System.Windows.Forms.Panel CancelFooterPanel;
        private System.Windows.Forms.Button CancelEditButton;
        private System.Windows.Forms.Button ConfirmCropButton;
        private Controls.CropBox photoCropBox;
        private System.Windows.Forms.Button BlackAndWhiteButton;
    }
}
