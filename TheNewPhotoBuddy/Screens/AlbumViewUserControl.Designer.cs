namespace TheNewPhotoBuddy.Screens
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelAlbumName = new System.Windows.Forms.Label();
            this.renameAlbumButton = new System.Windows.Forms.Button();
            this.AlbumsLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.AddPhotosButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.photosFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.photosFlowPanel, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(375, 325);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.AlbumsLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(375, 49);
            this.panel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.labelAlbumName);
            this.flowLayoutPanel1.Controls.Add(this.renameAlbumButton);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(127, 17);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(254, 39);
            this.flowLayoutPanel1.TabIndex = 3;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // labelAlbumName
            // 
            this.labelAlbumName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAlbumName.AutoSize = true;
            this.labelAlbumName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAlbumName.ForeColor = System.Drawing.Color.White;
            this.labelAlbumName.Location = new System.Drawing.Point(2, 7);
            this.labelAlbumName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAlbumName.Name = "labelAlbumName";
            this.labelAlbumName.Size = new System.Drawing.Size(115, 24);
            this.labelAlbumName.TabIndex = 2;
            this.labelAlbumName.Text = "album name";
            this.labelAlbumName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // renameAlbumButton
            // 
            this.renameAlbumButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.renameAlbumButton.AutoSize = true;
            this.renameAlbumButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.renameAlbumButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.renameAlbumButton.FlatAppearance.BorderSize = 0;
            this.renameAlbumButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(225)))), ((int)(((byte)(237)))));
            this.renameAlbumButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.renameAlbumButton.Location = new System.Drawing.Point(127, 8);
            this.renameAlbumButton.Margin = new System.Windows.Forms.Padding(8, 8, 38, 8);
            this.renameAlbumButton.Name = "renameAlbumButton";
            this.renameAlbumButton.Size = new System.Drawing.Size(89, 23);
            this.renameAlbumButton.TabIndex = 1;
            this.renameAlbumButton.Text = "Rename Album";
            this.renameAlbumButton.UseVisualStyleBackColor = false;
            this.renameAlbumButton.Click += new System.EventHandler(this.renameAlbumButton_Click);
            // 
            // AlbumsLabel
            // 
            this.AlbumsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AlbumsLabel.AutoSize = true;
            this.AlbumsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlbumsLabel.Location = new System.Drawing.Point(38, 30);
            this.AlbumsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.AlbumsLabel.Name = "AlbumsLabel";
            this.AlbumsLabel.Size = new System.Drawing.Size(96, 17);
            this.AlbumsLabel.TabIndex = 0;
            this.AlbumsLabel.Text = "Album Name: ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.AddPhotosButton);
            this.panel2.Controls.Add(this.backButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 260);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(375, 65);
            this.panel2.TabIndex = 1;
            // 
            // AddPhotosButton
            // 
            this.AddPhotosButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddPhotosButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AddPhotosButton.FlatAppearance.BorderSize = 0;
            this.AddPhotosButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(225)))), ((int)(((byte)(237)))));
            this.AddPhotosButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddPhotosButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddPhotosButton.Location = new System.Drawing.Point(227, 17);
            this.AddPhotosButton.Margin = new System.Windows.Forms.Padding(8, 8, 38, 8);
            this.AddPhotosButton.Name = "AddPhotosButton";
            this.AddPhotosButton.Size = new System.Drawing.Size(110, 40);
            this.AddPhotosButton.TabIndex = 0;
            this.AddPhotosButton.Text = "Add Photos";
            this.AddPhotosButton.UseVisualStyleBackColor = false;
            this.AddPhotosButton.Click += new System.EventHandler(this.AddPhotosButton_Click);
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.backButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(225)))), ((int)(((byte)(237)))));
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backButton.Location = new System.Drawing.Point(38, 17);
            this.backButton.Margin = new System.Windows.Forms.Padding(38, 8, 8, 8);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(92, 40);
            this.backButton.TabIndex = 1;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // photosFlowPanel
            // 
            this.photosFlowPanel.AutoScroll = true;
            this.photosFlowPanel.BackColor = System.Drawing.Color.White;
            this.photosFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.photosFlowPanel.Location = new System.Drawing.Point(38, 57);
            this.photosFlowPanel.Margin = new System.Windows.Forms.Padding(38, 8, 38, 3);
            this.photosFlowPanel.Name = "photosFlowPanel";
            this.photosFlowPanel.Size = new System.Drawing.Size(299, 200);
            this.photosFlowPanel.TabIndex = 2;
            // 
            // AlbumViewUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AlbumViewUserControl";
            this.Size = new System.Drawing.Size(375, 325);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button renameAlbumButton;
        private System.Windows.Forms.Label AlbumsLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button AddPhotosButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Label labelAlbumName;
        private System.Windows.Forms.FlowLayoutPanel photosFlowPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;

    }
}
