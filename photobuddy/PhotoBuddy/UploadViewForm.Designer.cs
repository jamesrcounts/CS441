﻿// <autogenerated />
namespace PhotoBuddy
{
    partial class UploadViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadViewForm));
            this.RenameLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.UploadViewFooterPanel = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.continueButton = new System.Windows.Forms.Button();
            this.UploadViewBox = new System.Windows.Forms.PictureBox();
            this.messageLabel = new System.Windows.Forms.Label();
            this.UndeRenamePanel = new System.Windows.Forms.Panel();
            this.RenamePanel = new System.Windows.Forms.TableLayoutPanel();
            this.displayNameTextBox = new System.Windows.Forms.TextBox();
            this.NameYourPhotoLabel = new System.Windows.Forms.Label();
            this.RenameLayoutPanel.SuspendLayout();
            this.UploadViewFooterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UploadViewBox)).BeginInit();
            this.UndeRenamePanel.SuspendLayout();
            this.RenamePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // RenameLayoutPanel
            // 
            this.RenameLayoutPanel.BackColor = System.Drawing.Color.White;
            this.RenameLayoutPanel.ColumnCount = 1;
            this.RenameLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.RenameLayoutPanel.Controls.Add(this.UploadViewFooterPanel, 0, 3);
            this.RenameLayoutPanel.Controls.Add(this.UploadViewBox, 0, 0);
            this.RenameLayoutPanel.Controls.Add(this.messageLabel, 0, 1);
            this.RenameLayoutPanel.Controls.Add(this.UndeRenamePanel, 0, 2);
            this.RenameLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RenameLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.RenameLayoutPanel.Margin = new System.Windows.Forms.Padding(2);
            this.RenameLayoutPanel.Name = "RenameLayoutPanel";
            this.RenameLayoutPanel.RowCount = 4;
            this.RenameLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.RenameLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.RenameLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.RenameLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.RenameLayoutPanel.Size = new System.Drawing.Size(586, 451);
            this.RenameLayoutPanel.TabIndex = 8;
            // 
            // UploadViewFooterPanel
            // 
            this.UploadViewFooterPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.UploadViewFooterPanel.Controls.Add(this.cancelButton);
            this.UploadViewFooterPanel.Controls.Add(this.continueButton);
            this.UploadViewFooterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UploadViewFooterPanel.Location = new System.Drawing.Point(0, 415);
            this.UploadViewFooterPanel.Margin = new System.Windows.Forms.Padding(0);
            this.UploadViewFooterPanel.Name = "UploadViewFooterPanel";
            this.UploadViewFooterPanel.Size = new System.Drawing.Size(586, 36);
            this.UploadViewFooterPanel.TabIndex = 7;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.AutoSize = true;
            this.cancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cancelButton.BackColor = System.Drawing.Color.Gainsboro;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.Black;
            this.cancelButton.Location = new System.Drawing.Point(508, 3);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0, 0, 10, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(68, 30);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.HandleCancelButtonClick);
            // 
            // continueButton
            // 
            this.continueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.continueButton.AutoSize = true;
            this.continueButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.continueButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(70)))), ((int)(((byte)(102)))));
            this.continueButton.FlatAppearance.BorderSize = 0;
            this.continueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.continueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continueButton.ForeColor = System.Drawing.Color.White;
            this.continueButton.Location = new System.Drawing.Point(459, 3);
            this.continueButton.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(41, 30);
            this.continueButton.TabIndex = 1;
            this.continueButton.Text = "OK";
            this.continueButton.UseVisualStyleBackColor = false;
            this.continueButton.Click += new System.EventHandler(this.HandleContinueButtonClick);
            this.continueButton.MouseEnter += new System.EventHandler(this.HandleButtonMouseEnter);
            this.continueButton.MouseLeave += new System.EventHandler(this.HandleButtonMouseLeave);
            // 
            // UploadViewBox
            // 
            this.UploadViewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UploadViewBox.Location = new System.Drawing.Point(10, 10);
            this.UploadViewBox.Margin = new System.Windows.Forms.Padding(10);
            this.UploadViewBox.Name = "UploadViewBox";
            this.UploadViewBox.Size = new System.Drawing.Size(566, 314);
            this.UploadViewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.UploadViewBox.TabIndex = 2;
            this.UploadViewBox.TabStop = false;
            // 
            // messageLabel
            // 
            this.messageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.messageLabel.AutoSize = true;
            this.messageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageLabel.Location = new System.Drawing.Point(2, 340);
            this.messageLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(582, 20);
            this.messageLabel.TabIndex = 4;
            this.messageLabel.Text = "This is the photo selected to upload.";
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UndeRenamePanel
            // 
            this.UndeRenamePanel.Controls.Add(this.RenamePanel);
            this.UndeRenamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UndeRenamePanel.Location = new System.Drawing.Point(0, 366);
            this.UndeRenamePanel.Margin = new System.Windows.Forms.Padding(0);
            this.UndeRenamePanel.Name = "UndeRenamePanel";
            this.UndeRenamePanel.Size = new System.Drawing.Size(586, 49);
            this.UndeRenamePanel.TabIndex = 6;
            // 
            // RenamePanel
            // 
            this.RenamePanel.ColumnCount = 3;
            this.RenamePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.RenamePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 229F));
            this.RenamePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.RenamePanel.Controls.Add(this.displayNameTextBox, 1, 0);
            this.RenamePanel.Controls.Add(this.NameYourPhotoLabel, 0, 0);
            this.RenamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RenamePanel.Location = new System.Drawing.Point(0, 0);
            this.RenamePanel.Margin = new System.Windows.Forms.Padding(2);
            this.RenamePanel.Name = "RenamePanel";
            this.RenamePanel.RowCount = 1;
            this.RenamePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.RenamePanel.Size = new System.Drawing.Size(586, 49);
            this.RenamePanel.TabIndex = 9;
            // 
            // displayNameTextBox
            // 
            this.displayNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.displayNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayNameTextBox.Location = new System.Drawing.Point(180, 11);
            this.displayNameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.displayNameTextBox.Name = "displayNameTextBox";
            this.displayNameTextBox.Size = new System.Drawing.Size(225, 26);
            this.displayNameTextBox.TabIndex = 0;
            this.displayNameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HandleDisplayNameTextBoxKeyDown);
            // 
            // NameYourPhotoLabel
            // 
            this.NameYourPhotoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.NameYourPhotoLabel.AutoSize = true;
            this.NameYourPhotoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameYourPhotoLabel.Location = new System.Drawing.Point(2, 14);
            this.NameYourPhotoLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NameYourPhotoLabel.Name = "NameYourPhotoLabel";
            this.NameYourPhotoLabel.Size = new System.Drawing.Size(174, 20);
            this.NameYourPhotoLabel.TabIndex = 5;
            this.NameYourPhotoLabel.Text = "Name Your Photo";
            this.NameYourPhotoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UploadViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 451);
            this.Controls.Add(this.RenameLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UploadViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UploadViewForm";
            this.RenameLayoutPanel.ResumeLayout(false);
            this.RenameLayoutPanel.PerformLayout();
            this.UploadViewFooterPanel.ResumeLayout(false);
            this.UploadViewFooterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UploadViewBox)).EndInit();
            this.UndeRenamePanel.ResumeLayout(false);
            this.RenamePanel.ResumeLayout(false);
            this.RenamePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel RenameLayoutPanel;
        private System.Windows.Forms.Panel UploadViewFooterPanel;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.PictureBox UploadViewBox;
        private System.Windows.Forms.Panel UndeRenamePanel;
        private System.Windows.Forms.TextBox displayNameTextBox;
        private System.Windows.Forms.Label NameYourPhotoLabel;
        private System.Windows.Forms.TableLayoutPanel RenamePanel;
    }
}