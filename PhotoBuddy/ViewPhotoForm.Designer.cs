﻿namespace PhotoBuddy
{
    partial class ViewPhotoForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.photoNameLabel = new System.Windows.Forms.Label();
            this.AppNameLabel = new System.Windows.Forms.Label();
            this.currentAlbumLabel = new System.Windows.Forms.Label();
            this.albumLabel = new System.Windows.Forms.Label();
            this.photoLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.previousPhotoButton = new System.Windows.Forms.Button();
            this.nextPhotoButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(586, 451);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.photoNameLabel);
            this.panel1.Controls.Add(this.AppNameLabel);
            this.panel1.Controls.Add(this.currentAlbumLabel);
            this.panel1.Controls.Add(this.albumLabel);
            this.panel1.Controls.Add(this.photoLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(582, 61);
            this.panel1.TabIndex = 0;
            // 
            // photoNameLabel
            // 
            this.photoNameLabel.AutoSize = true;
            this.photoNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.photoNameLabel.ForeColor = System.Drawing.Color.White;
            this.photoNameLabel.Location = new System.Drawing.Point(85, 25);
            this.photoNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.photoNameLabel.Name = "photoNameLabel";
            this.photoNameLabel.Size = new System.Drawing.Size(51, 20);
            this.photoNameLabel.TabIndex = 9;
            this.photoNameLabel.Text = "label1";
            // 
            // AppNameLabel
            // 
            this.AppNameLabel.AutoSize = true;
            this.AppNameLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.AppNameLabel.Font = new System.Drawing.Font("Magneto", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppNameLabel.ForeColor = System.Drawing.Color.DimGray;
            this.AppNameLabel.Location = new System.Drawing.Point(343, 0);
            this.AppNameLabel.Name = "AppNameLabel";
            this.AppNameLabel.Size = new System.Drawing.Size(239, 41);
            this.AppNameLabel.TabIndex = 8;
            this.AppNameLabel.Text = "Photo Buddy";
            // 
            // currentAlbumLabel
            // 
            this.currentAlbumLabel.AutoSize = true;
            this.currentAlbumLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.currentAlbumLabel.Location = new System.Drawing.Point(86, 5);
            this.currentAlbumLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentAlbumLabel.Name = "currentAlbumLabel";
            this.currentAlbumLabel.Size = new System.Drawing.Size(79, 13);
            this.currentAlbumLabel.TabIndex = 4;
            this.currentAlbumLabel.Text = "Name of Album";
            // 
            // albumLabel
            // 
            this.albumLabel.AutoSize = true;
            this.albumLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.albumLabel.Location = new System.Drawing.Point(13, 5);
            this.albumLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.albumLabel.Name = "albumLabel";
            this.albumLabel.Size = new System.Drawing.Size(70, 13);
            this.albumLabel.TabIndex = 3;
            this.albumLabel.Text = "Album Name:";
            // 
            // photoLabel
            // 
            this.photoLabel.AutoSize = true;
            this.photoLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.photoLabel.Location = new System.Drawing.Point(13, 25);
            this.photoLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.photoLabel.Name = "photoLabel";
            this.photoLabel.Size = new System.Drawing.Size(69, 13);
            this.photoLabel.TabIndex = 0;
            this.photoLabel.Text = "Photo Name:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(38, 67);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(38, 2, 38, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(510, 350);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.previousPhotoButton, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.nextPhotoButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.backButton, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 419);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(586, 32);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // previousPhotoButton
            // 
            this.previousPhotoButton.AutoSize = true;
            this.previousPhotoButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.previousPhotoButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.previousPhotoButton.FlatAppearance.BorderSize = 0;
            this.previousPhotoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.previousPhotoButton.Font = new System.Drawing.Font("Wingdings 3", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.previousPhotoButton.ForeColor = System.Drawing.Color.DarkGray;
            this.previousPhotoButton.Location = new System.Drawing.Point(255, 0);
            this.previousPhotoButton.Margin = new System.Windows.Forms.Padding(0);
            this.previousPhotoButton.Name = "previousPhotoButton";
            this.previousPhotoButton.Size = new System.Drawing.Size(37, 32);
            this.previousPhotoButton.TabIndex = 2;
            this.previousPhotoButton.Text = "t";
            this.previousPhotoButton.UseVisualStyleBackColor = false;
            this.previousPhotoButton.Click += new System.EventHandler(this.previousPhotoButton_Click);
            this.previousPhotoButton.MouseEnter += new System.EventHandler(this.previousPhotoButton_MouseEnter);
            this.previousPhotoButton.MouseLeave += new System.EventHandler(this.previousPhotoButton_MouseLeave);
            // 
            // nextPhotoButton
            // 
            this.nextPhotoButton.AutoSize = true;
            this.nextPhotoButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.nextPhotoButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.nextPhotoButton.FlatAppearance.BorderSize = 0;
            this.nextPhotoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextPhotoButton.Font = new System.Drawing.Font("Wingdings 3", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.nextPhotoButton.ForeColor = System.Drawing.Color.DarkGray;
            this.nextPhotoButton.Location = new System.Drawing.Point(293, 0);
            this.nextPhotoButton.Margin = new System.Windows.Forms.Padding(0);
            this.nextPhotoButton.Name = "nextPhotoButton";
            this.nextPhotoButton.Size = new System.Drawing.Size(37, 32);
            this.nextPhotoButton.TabIndex = 0;
            this.nextPhotoButton.Text = "u";
            this.nextPhotoButton.UseVisualStyleBackColor = false;
            this.nextPhotoButton.Click += new System.EventHandler(this.nextPhotoButton_Click);
            this.nextPhotoButton.MouseEnter += new System.EventHandler(this.previousPhotoButton_MouseEnter);
            this.nextPhotoButton.MouseLeave += new System.EventHandler(this.previousPhotoButton_MouseLeave);
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.backButton.AutoSize = true;
            this.backButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.backButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backButton.ForeColor = System.Drawing.Color.DarkGray;
            this.backButton.Location = new System.Drawing.Point(38, 2);
            this.backButton.Margin = new System.Windows.Forms.Padding(38, 2, 2, 2);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(55, 28);
            this.backButton.TabIndex = 1;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            this.backButton.MouseEnter += new System.EventHandler(this.previousPhotoButton_MouseEnter);
            this.backButton.MouseLeave += new System.EventHandler(this.previousPhotoButton_MouseLeave);
            // 
            // ViewPhotoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 451);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ViewPhotoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ViewPhotoForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label currentAlbumLabel;
        private System.Windows.Forms.Label albumLabel;
        private System.Windows.Forms.Label photoLabel;
        private System.Windows.Forms.Button previousPhotoButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button nextPhotoButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label AppNameLabel;
        private System.Windows.Forms.Label photoNameLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}