namespace PhotoBuddy.Screens
{
    partial class RotatePhotoControl
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
            this.photoRotateBox = new System.Windows.Forms.PictureBox();
            this.RotateLabel = new System.Windows.Forms.Label();
            this.EditFooterPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ConfirmFooterPanel = new System.Windows.Forms.Panel();
            this.SaveRotateButton = new System.Windows.Forms.Button();
            this.CancelFooterPanel = new System.Windows.Forms.Panel();
            this.CancelRotateButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FlipVerticalButton = new System.Windows.Forms.Button();
            this.RotateRightButton = new System.Windows.Forms.Button();
            this.RotateLeftButton = new System.Windows.Forms.Button();
            this.FlipHorizontalButton = new System.Windows.Forms.Button();
            this.foundationTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.photoRotateBox)).BeginInit();
            this.EditFooterPanel.SuspendLayout();
            this.ConfirmFooterPanel.SuspendLayout();
            this.CancelFooterPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // foundationTableLayoutPanel
            // 
            this.foundationTableLayoutPanel.BackColor = System.Drawing.Color.White;
            this.foundationTableLayoutPanel.ColumnCount = 1;
            this.foundationTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.foundationTableLayoutPanel.Controls.Add(this.photoRotateBox, 0, 0);
            this.foundationTableLayoutPanel.Controls.Add(this.RotateLabel, 0, 1);
            this.foundationTableLayoutPanel.Controls.Add(this.EditFooterPanel, 0, 2);
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
            this.foundationTableLayoutPanel.TabIndex = 4;
            // 
            // photoRotateBox
            // 
            this.photoRotateBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.photoRotateBox.Location = new System.Drawing.Point(3, 3);
            this.photoRotateBox.Name = "photoRotateBox";
            this.photoRotateBox.Size = new System.Drawing.Size(580, 374);
            this.photoRotateBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.photoRotateBox.TabIndex = 10;
            this.photoRotateBox.TabStop = false;
            // 
            // RotateLabel
            // 
            this.RotateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RotateLabel.AutoSize = true;
            this.RotateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RotateLabel.ForeColor = System.Drawing.Color.Black;
            this.RotateLabel.Location = new System.Drawing.Point(0, 388);
            this.RotateLabel.Margin = new System.Windows.Forms.Padding(0);
            this.RotateLabel.Name = "RotateLabel";
            this.RotateLabel.Size = new System.Drawing.Size(586, 29);
            this.RotateLabel.TabIndex = 9;
            this.RotateLabel.Text = "Rotate Photo";
            this.RotateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EditFooterPanel
            // 
            this.EditFooterPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.EditFooterPanel.ColumnCount = 3;
            this.EditFooterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00125F));
            this.EditFooterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.99751F));
            this.EditFooterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00125F));
            this.EditFooterPanel.Controls.Add(this.ConfirmFooterPanel, 2, 0);
            this.EditFooterPanel.Controls.Add(this.CancelFooterPanel, 0, 0);
            this.EditFooterPanel.Controls.Add(this.panel1, 1, 0);
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
            this.ConfirmFooterPanel.Controls.Add(this.SaveRotateButton);
            this.ConfirmFooterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfirmFooterPanel.Location = new System.Drawing.Point(438, 0);
            this.ConfirmFooterPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ConfirmFooterPanel.Name = "ConfirmFooterPanel";
            this.ConfirmFooterPanel.Size = new System.Drawing.Size(148, 36);
            this.ConfirmFooterPanel.TabIndex = 5;
            // 
            // SaveRotateButton
            // 
            this.SaveRotateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveRotateButton.AutoSize = true;
            this.SaveRotateButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SaveRotateButton.BackColor = System.Drawing.Color.Transparent;
            this.SaveRotateButton.FlatAppearance.BorderSize = 0;
            this.SaveRotateButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.SaveRotateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveRotateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveRotateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(70)))), ((int)(((byte)(102)))));
            this.SaveRotateButton.Location = new System.Drawing.Point(83, 4);
            this.SaveRotateButton.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.SaveRotateButton.Name = "SaveRotateButton";
            this.SaveRotateButton.Size = new System.Drawing.Size(55, 30);
            this.SaveRotateButton.TabIndex = 11;
            this.SaveRotateButton.Text = "Save";
            this.SaveRotateButton.UseVisualStyleBackColor = false;
            this.SaveRotateButton.Click += new System.EventHandler(this.SaveRotateButton_Click);
            // 
            // CancelFooterPanel
            // 
            this.CancelFooterPanel.Controls.Add(this.CancelRotateButton);
            this.CancelFooterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CancelFooterPanel.Location = new System.Drawing.Point(0, 0);
            this.CancelFooterPanel.Margin = new System.Windows.Forms.Padding(0);
            this.CancelFooterPanel.Name = "CancelFooterPanel";
            this.CancelFooterPanel.Size = new System.Drawing.Size(146, 36);
            this.CancelFooterPanel.TabIndex = 6;
            // 
            // CancelRotateButton
            // 
            this.CancelRotateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelRotateButton.AutoSize = true;
            this.CancelRotateButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelRotateButton.BackColor = System.Drawing.Color.Transparent;
            this.CancelRotateButton.FlatAppearance.BorderSize = 0;
            this.CancelRotateButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.CancelRotateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelRotateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelRotateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(70)))), ((int)(((byte)(102)))));
            this.CancelRotateButton.Location = new System.Drawing.Point(10, 4);
            this.CancelRotateButton.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.CancelRotateButton.Name = "CancelRotateButton";
            this.CancelRotateButton.Size = new System.Drawing.Size(68, 30);
            this.CancelRotateButton.TabIndex = 1;
            this.CancelRotateButton.Text = "Cancel";
            this.CancelRotateButton.UseMnemonic = false;
            this.CancelRotateButton.UseVisualStyleBackColor = false;
            this.CancelRotateButton.Click += new System.EventHandler(this.CancelRotateButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.FlipVerticalButton);
            this.panel1.Controls.Add(this.RotateRightButton);
            this.panel1.Controls.Add(this.RotateLeftButton);
            this.panel1.Controls.Add(this.FlipHorizontalButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(146, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(292, 36);
            this.panel1.TabIndex = 7;
            // 
            // FlipVerticalButton
            // 
            this.FlipVerticalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FlipVerticalButton.AutoSize = true;
            this.FlipVerticalButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FlipVerticalButton.BackColor = System.Drawing.Color.Transparent;
            this.FlipVerticalButton.FlatAppearance.BorderSize = 0;
            this.FlipVerticalButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.FlipVerticalButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FlipVerticalButton.Font = new System.Drawing.Font("Wingdings 3", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.FlipVerticalButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(70)))), ((int)(((byte)(102)))));
            this.FlipVerticalButton.Location = new System.Drawing.Point(91, 6);
            this.FlipVerticalButton.Margin = new System.Windows.Forms.Padding(0);
            this.FlipVerticalButton.Name = "FlipVerticalButton";
            this.FlipVerticalButton.Size = new System.Drawing.Size(33, 27);
            this.FlipVerticalButton.TabIndex = 15;
            this.FlipVerticalButton.Text = "E";
            this.FlipVerticalButton.UseVisualStyleBackColor = false;
            this.FlipVerticalButton.Click += new System.EventHandler(this.FlipVerticalButton_Click);
            // 
            // RotateRightButton
            // 
            this.RotateRightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RotateRightButton.AutoSize = true;
            this.RotateRightButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.RotateRightButton.BackColor = System.Drawing.Color.Transparent;
            this.RotateRightButton.FlatAppearance.BorderSize = 0;
            this.RotateRightButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.RotateRightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RotateRightButton.Font = new System.Drawing.Font("Wingdings 3", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.RotateRightButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(70)))), ((int)(((byte)(102)))));
            this.RotateRightButton.Location = new System.Drawing.Point(240, 6);
            this.RotateRightButton.Margin = new System.Windows.Forms.Padding(0);
            this.RotateRightButton.Name = "RotateRightButton";
            this.RotateRightButton.Size = new System.Drawing.Size(33, 27);
            this.RotateRightButton.TabIndex = 14;
            this.RotateRightButton.Text = "P";
            this.RotateRightButton.UseVisualStyleBackColor = false;
            this.RotateRightButton.Click += new System.EventHandler(this.RotateRightButton_Click);
            // 
            // RotateLeftButton
            // 
            this.RotateLeftButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RotateLeftButton.AutoSize = true;
            this.RotateLeftButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.RotateLeftButton.BackColor = System.Drawing.Color.Transparent;
            this.RotateLeftButton.FlatAppearance.BorderSize = 0;
            this.RotateLeftButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.RotateLeftButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RotateLeftButton.Font = new System.Drawing.Font("Wingdings 3", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.RotateLeftButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(70)))), ((int)(((byte)(102)))));
            this.RotateLeftButton.Location = new System.Drawing.Point(169, 6);
            this.RotateLeftButton.Margin = new System.Windows.Forms.Padding(0);
            this.RotateLeftButton.Name = "RotateLeftButton";
            this.RotateLeftButton.Size = new System.Drawing.Size(33, 27);
            this.RotateLeftButton.TabIndex = 13;
            this.RotateLeftButton.Text = "Q";
            this.RotateLeftButton.UseVisualStyleBackColor = false;
            this.RotateLeftButton.Click += new System.EventHandler(this.RotateLeftButton_Click);
            // 
            // FlipHorizontalButton
            // 
            this.FlipHorizontalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FlipHorizontalButton.AutoSize = true;
            this.FlipHorizontalButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FlipHorizontalButton.BackColor = System.Drawing.Color.Transparent;
            this.FlipHorizontalButton.FlatAppearance.BorderSize = 0;
            this.FlipHorizontalButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.FlipHorizontalButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FlipHorizontalButton.Font = new System.Drawing.Font("Wingdings 3", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.FlipHorizontalButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(70)))), ((int)(((byte)(102)))));
            this.FlipHorizontalButton.Location = new System.Drawing.Point(21, 6);
            this.FlipHorizontalButton.Margin = new System.Windows.Forms.Padding(0);
            this.FlipHorizontalButton.Name = "FlipHorizontalButton";
            this.FlipHorizontalButton.Size = new System.Drawing.Size(33, 27);
            this.FlipHorizontalButton.TabIndex = 12;
            this.FlipHorizontalButton.Text = "D";
            this.FlipHorizontalButton.UseVisualStyleBackColor = false;
            this.FlipHorizontalButton.Click += new System.EventHandler(this.FlipHorizontalButton_Click);
            // 
            // RotatePhotoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.foundationTableLayoutPanel);
            this.Name = "RotatePhotoControl";
            this.Size = new System.Drawing.Size(586, 462);
            this.foundationTableLayoutPanel.ResumeLayout(false);
            this.foundationTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.photoRotateBox)).EndInit();
            this.EditFooterPanel.ResumeLayout(false);
            this.ConfirmFooterPanel.ResumeLayout(false);
            this.ConfirmFooterPanel.PerformLayout();
            this.CancelFooterPanel.ResumeLayout(false);
            this.CancelFooterPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel foundationTableLayoutPanel;
        private System.Windows.Forms.PictureBox photoRotateBox;
        private System.Windows.Forms.Label RotateLabel;
        private System.Windows.Forms.TableLayoutPanel EditFooterPanel;
        private System.Windows.Forms.Panel ConfirmFooterPanel;
        private System.Windows.Forms.Button SaveRotateButton;
        private System.Windows.Forms.Panel CancelFooterPanel;
        private System.Windows.Forms.Button CancelRotateButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button FlipVerticalButton;
        private System.Windows.Forms.Button RotateRightButton;
        private System.Windows.Forms.Button RotateLeftButton;
        private System.Windows.Forms.Button FlipHorizontalButton;
    }
}
