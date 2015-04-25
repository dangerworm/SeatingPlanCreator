namespace SeatingPlanCreator
{
    partial class RoomSetup
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
            this.btnRight = new System.Windows.Forms.Button();
            this.grpLayout = new System.Windows.Forms.GroupBox();
            this.btnLeft = new System.Windows.Forms.Button();
            this.grpTableSettings = new System.Windows.Forms.GroupBox();
            this.btnRemoveTable = new System.Windows.Forms.Button();
            this.btnRemoveSeat = new System.Windows.Forms.Button();
            this.btnAddSeat = new System.Windows.Forms.Button();
            this.lblTableWidth = new System.Windows.Forms.Label();
            this.txtTableWidth = new System.Windows.Forms.TextBox();
            this.lblTableDepth = new System.Windows.Forms.Label();
            this.txtTableDepth = new System.Windows.Forms.TextBox();
            this.picTableAndSeats = new System.Windows.Forms.PictureBox();
            this.grpChangeLayout = new System.Windows.Forms.GroupBox();
            this.grpTableSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTableAndSeats)).BeginInit();
            this.grpChangeLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRight
            // 
            this.btnRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRight.Location = new System.Drawing.Point(125, 19);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(23, 23);
            this.btnRight.TabIndex = 20;
            this.btnRight.Text = ">";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // grpLayout
            // 
            this.grpLayout.Location = new System.Drawing.Point(12, 12);
            this.grpLayout.Name = "grpLayout";
            this.grpLayout.Size = new System.Drawing.Size(600, 418);
            this.grpLayout.TabIndex = 23;
            this.grpLayout.TabStop = false;
            this.grpLayout.Text = "Room Layout";
            // 
            // btnLeft
            // 
            this.btnLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLeft.Location = new System.Drawing.Point(6, 19);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(23, 23);
            this.btnLeft.TabIndex = 21;
            this.btnLeft.Text = "<";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // grpTableSettings
            // 
            this.grpTableSettings.Controls.Add(this.btnRemoveTable);
            this.grpTableSettings.Controls.Add(this.btnRemoveSeat);
            this.grpTableSettings.Controls.Add(this.btnAddSeat);
            this.grpTableSettings.Controls.Add(this.lblTableWidth);
            this.grpTableSettings.Controls.Add(this.txtTableWidth);
            this.grpTableSettings.Controls.Add(this.lblTableDepth);
            this.grpTableSettings.Controls.Add(this.txtTableDepth);
            this.grpTableSettings.Controls.Add(this.picTableAndSeats);
            this.grpTableSettings.Enabled = false;
            this.grpTableSettings.Location = new System.Drawing.Point(618, 12);
            this.grpTableSettings.Name = "grpTableSettings";
            this.grpTableSettings.Size = new System.Drawing.Size(154, 220);
            this.grpTableSettings.TabIndex = 24;
            this.grpTableSettings.TabStop = false;
            this.grpTableSettings.Text = "Table Settings";
            // 
            // btnRemoveTable
            // 
            this.btnRemoveTable.Location = new System.Drawing.Point(6, 160);
            this.btnRemoveTable.Name = "btnRemoveTable";
            this.btnRemoveTable.Size = new System.Drawing.Size(142, 23);
            this.btnRemoveTable.TabIndex = 39;
            this.btnRemoveTable.Text = "Remove Table From Room";
            this.btnRemoveTable.UseVisualStyleBackColor = true;
            this.btnRemoveTable.Click += new System.EventHandler(this.btnRemoveTable_Click);
            // 
            // btnRemoveSeat
            // 
            this.btnRemoveSeat.Location = new System.Drawing.Point(68, 189);
            this.btnRemoveSeat.Name = "btnRemoveSeat";
            this.btnRemoveSeat.Size = new System.Drawing.Size(80, 23);
            this.btnRemoveSeat.TabIndex = 38;
            this.btnRemoveSeat.Text = "Remove Seat";
            this.btnRemoveSeat.UseVisualStyleBackColor = true;
            // 
            // btnAddSeat
            // 
            this.btnAddSeat.Location = new System.Drawing.Point(6, 189);
            this.btnAddSeat.Name = "btnAddSeat";
            this.btnAddSeat.Size = new System.Drawing.Size(59, 23);
            this.btnAddSeat.TabIndex = 37;
            this.btnAddSeat.Text = "Add Seat";
            this.btnAddSeat.UseVisualStyleBackColor = true;
            // 
            // lblTableWidth
            // 
            this.lblTableWidth.AutoSize = true;
            this.lblTableWidth.Location = new System.Drawing.Point(7, 137);
            this.lblTableWidth.Name = "lblTableWidth";
            this.lblTableWidth.Size = new System.Drawing.Size(68, 13);
            this.lblTableWidth.TabIndex = 36;
            this.lblTableWidth.Text = "Table Width:";
            // 
            // txtTableWidth
            // 
            this.txtTableWidth.Location = new System.Drawing.Point(81, 134);
            this.txtTableWidth.Name = "txtTableWidth";
            this.txtTableWidth.Size = new System.Drawing.Size(20, 20);
            this.txtTableWidth.TabIndex = 35;
            // 
            // lblTableDepth
            // 
            this.lblTableDepth.AutoSize = true;
            this.lblTableDepth.Location = new System.Drawing.Point(6, 111);
            this.lblTableDepth.Name = "lblTableDepth";
            this.lblTableDepth.Size = new System.Drawing.Size(69, 13);
            this.lblTableDepth.TabIndex = 34;
            this.lblTableDepth.Text = "Table Depth:";
            // 
            // txtTableDepth
            // 
            this.txtTableDepth.Location = new System.Drawing.Point(81, 108);
            this.txtTableDepth.Name = "txtTableDepth";
            this.txtTableDepth.Size = new System.Drawing.Size(20, 20);
            this.txtTableDepth.TabIndex = 33;
            // 
            // picTableAndSeats
            // 
            this.picTableAndSeats.Location = new System.Drawing.Point(6, 19);
            this.picTableAndSeats.Name = "picTableAndSeats";
            this.picTableAndSeats.Size = new System.Drawing.Size(142, 83);
            this.picTableAndSeats.TabIndex = 0;
            this.picTableAndSeats.TabStop = false;
            // 
            // grpChangeLayout
            // 
            this.grpChangeLayout.Controls.Add(this.btnLeft);
            this.grpChangeLayout.Controls.Add(this.btnRight);
            this.grpChangeLayout.Enabled = false;
            this.grpChangeLayout.Location = new System.Drawing.Point(618, 238);
            this.grpChangeLayout.Name = "grpChangeLayout";
            this.grpChangeLayout.Size = new System.Drawing.Size(154, 48);
            this.grpChangeLayout.TabIndex = 26;
            this.grpChangeLayout.TabStop = false;
            this.grpChangeLayout.Text = "Change Seating Plan";
            // 
            // RoomSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 442);
            this.Controls.Add(this.grpChangeLayout);
            this.Controls.Add(this.grpTableSettings);
            this.Controls.Add(this.grpLayout);
            this.Name = "RoomSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Room Setup";
            this.Load += new System.EventHandler(this.RoomSetup_Load);
            this.SizeChanged += new System.EventHandler(this.RoomLayout_SizeChanged);
            this.grpTableSettings.ResumeLayout(false);
            this.grpTableSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTableAndSeats)).EndInit();
            this.grpChangeLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.GroupBox grpLayout;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.GroupBox grpTableSettings;
        private System.Windows.Forms.PictureBox picTableAndSeats;
        private System.Windows.Forms.Label lblTableDepth;
        private System.Windows.Forms.TextBox txtTableDepth;
        private System.Windows.Forms.Label lblTableWidth;
        private System.Windows.Forms.TextBox txtTableWidth;
        private System.Windows.Forms.Button btnRemoveSeat;
        private System.Windows.Forms.Button btnAddSeat;
        private System.Windows.Forms.GroupBox grpChangeLayout;
        private System.Windows.Forms.Button btnRemoveTable;
    }
}