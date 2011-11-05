namespace PhotoBuddy.Controls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlbumIconUserControl));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.countLabel = new System.Windows.Forms.Label();
            this.coverPhotoPictureBox = new System.Windows.Forms.PictureBox();
            this.thumbnail = new System.Windows.Forms.PictureBox();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.coverPhotoPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thumbnail)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.NameTextBox, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 124F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(164, 150);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.countLabel);
            this.panel1.Controls.Add(this.coverPhotoPictureBox);
            this.panel1.Controls.Add(this.thumbnail);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.MinimumSize = new System.Drawing.Size(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(2);
            this.panel1.Size = new System.Drawing.Size(164, 124);
            this.panel1.TabIndex = 0;
            // 
            // countLabel
            // 
            this.countLabel.BackColor = System.Drawing.Color.White;
            this.countLabel.Location = new System.Drawing.Point(22, 5);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(48, 14);
            this.countLabel.TabIndex = 2;
            this.countLabel.Text = "57 ";
            this.countLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // coverPhotoPictureBox
            // 
            this.coverPhotoPictureBox.BackColor = System.Drawing.Color.Black;
            this.coverPhotoPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.coverPhotoPictureBox.Location = new System.Drawing.Point(35, 36);
            this.coverPhotoPictureBox.Name = "coverPhotoPictureBox";
            this.coverPhotoPictureBox.Size = new System.Drawing.Size(92, 61);
            this.coverPhotoPictureBox.TabIndex = 1;
            this.coverPhotoPictureBox.TabStop = false;
            // 
            // thumbnail
            // 
            this.thumbnail.BackColor = System.Drawing.Color.Transparent;
            this.thumbnail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thumbnail.Image = ((System.Drawing.Image)(resources.GetObject("thumbnail.Image")));
            this.thumbnail.Location = new System.Drawing.Point(2, 2);
            this.thumbnail.Margin = new System.Windows.Forms.Padding(5);
            this.thumbnail.Name = "thumbnail";
            this.thumbnail.Size = new System.Drawing.Size(160, 120);
            this.thumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.thumbnail.TabIndex = 0;
            this.thumbnail.TabStop = false;
            this.thumbnail.MouseEnter += new System.EventHandler(this.HighlightPhoto);
            this.thumbnail.MouseLeave += new System.EventHandler(this.RemovePhotoHighlight);
            // 
            // NameTextBox
            // 
            this.NameTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.NameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameTextBox.Location = new System.Drawing.Point(1, 125);
            this.NameTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.ReadOnly = true;
            this.NameTextBox.Size = new System.Drawing.Size(162, 13);
            this.NameTextBox.TabIndex = 1;
            this.NameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AlbumIconUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AlbumIconUserControl";
            this.Size = new System.Drawing.Size(164, 150);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.coverPhotoPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thumbnail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox thumbnail;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.PictureBox coverPhotoPictureBox;
    }
}
