namespace SeatingPlanCreator
{
    partial class ClassRoomSetup
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassRoomSetup));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highlightNextStepsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpGroups = new System.Windows.Forms.GroupBox();
            this.btnRemoveClass = new System.Windows.Forms.Button();
            this.btnAddClass = new System.Windows.Forms.Button();
            this.rdoRoom = new System.Windows.Forms.RadioButton();
            this.lblGroupBy = new System.Windows.Forms.Label();
            this.rdoClass = new System.Windows.Forms.RadioButton();
            this.trvClassesRooms = new System.Windows.Forms.TreeView();
            this.grpClass = new System.Windows.Forms.GroupBox();
            this.btnAddRoom = new System.Windows.Forms.Button();
            this.btnRandomise = new System.Windows.Forms.Button();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.lblClassName = new System.Windows.Forms.Label();
            this.btnRemoveStudent = new System.Windows.Forms.Button();
            this.btnAddStudent = new System.Windows.Forms.Button();
            this.btnRemoveDB = new System.Windows.Forms.Button();
            this.btnAddDB = new System.Windows.Forms.Button();
            this.btnRemoveWW = new System.Windows.Forms.Button();
            this.btnAddWW = new System.Windows.Forms.Button();
            this.lstDistractedBy = new System.Windows.Forms.ListBox();
            this.lstWorksWell = new System.Windows.Forms.ListBox();
            this.lblDistractedBy = new System.Windows.Forms.Label();
            this.lblWorksWell = new System.Windows.Forms.Label();
            this.lstStudents = new System.Windows.Forms.ListBox();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblDoB = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.picSilhouette = new System.Windows.Forms.PictureBox();
            this.grpRoomLayout = new System.Windows.Forms.GroupBox();
            this.grpLayout = new System.Windows.Forms.GroupBox();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.pnlRoomSettings = new System.Windows.Forms.Panel();
            this.lblTableWidth = new System.Windows.Forms.Label();
            this.txtTableWidth = new System.Windows.Forms.TextBox();
            this.lblTableDepth = new System.Windows.Forms.Label();
            this.txtTableDepth = new System.Windows.Forms.TextBox();
            this.txtRows = new System.Windows.Forms.TextBox();
            this.lblRows = new System.Windows.Forms.Label();
            this.lblColumns = new System.Windows.Forms.Label();
            this.txtColumns = new System.Windows.Forms.TextBox();
            this.chkSideSeats = new System.Windows.Forms.CheckBox();
            this.btnResetRoom = new System.Windows.Forms.Button();
            this.lblNumSeats = new System.Windows.Forms.Label();
            this.lblNumTables = new System.Windows.Forms.Label();
            this.btnEditRoom = new System.Windows.Forms.Button();
            this.grpPlanBasis = new System.Windows.Forms.GroupBox();
            this.nudNumGenerations = new System.Windows.Forms.NumericUpDown();
            this.lblNumGenerations = new System.Windows.Forms.Label();
            this.nudNumBreeders = new System.Windows.Forms.NumericUpDown();
            this.lblNumBreeders = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.rdoAttainment = new System.Windows.Forms.RadioButton();
            this.rdoStudentSettings = new System.Windows.Forms.RadioButton();
            this.txtRoomName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.txtMagicFloatingTextBox = new System.Windows.Forms.TextBox();
            this.cmbMagicFloatingComboBox = new System.Windows.Forms.ComboBox();
            this.stsStatusStrip = new System.Windows.Forms.StatusStrip();
            this.stsText = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tmrFlashStatus = new System.Windows.Forms.Timer(this.components);
            this.tipToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip.SuspendLayout();
            this.grpGroups.SuspendLayout();
            this.grpClass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSilhouette)).BeginInit();
            this.grpRoomLayout.SuspendLayout();
            this.grpLayout.SuspendLayout();
            this.pnlRoomSettings.SuspendLayout();
            this.grpPlanBasis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumGenerations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumBreeders)).BeginInit();
            this.stsStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(784, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.importToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator3,
            this.loginToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.importToolStripMenuItem.Text = "&Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(111, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(111, 6);
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.loginToolStripMenuItem.Text = "&Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(111, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.highlightNextStepsToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // highlightNextStepsToolStripMenuItem
            // 
            this.highlightNextStepsToolStripMenuItem.Name = "highlightNextStepsToolStripMenuItem";
            this.highlightNextStepsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.highlightNextStepsToolStripMenuItem.Text = "&Highlight Next Steps";
            this.highlightNextStepsToolStripMenuItem.Click += new System.EventHandler(this.highlightNextStepsToolStripMenuItem_Click);
            // 
            // grpGroups
            // 
            this.grpGroups.Controls.Add(this.btnRemoveClass);
            this.grpGroups.Controls.Add(this.btnAddClass);
            this.grpGroups.Controls.Add(this.rdoRoom);
            this.grpGroups.Controls.Add(this.lblGroupBy);
            this.grpGroups.Controls.Add(this.rdoClass);
            this.grpGroups.Controls.Add(this.trvClassesRooms);
            this.grpGroups.Location = new System.Drawing.Point(12, 27);
            this.grpGroups.Name = "grpGroups";
            this.grpGroups.Size = new System.Drawing.Size(180, 497);
            this.grpGroups.TabIndex = 1;
            this.grpGroups.TabStop = false;
            this.grpGroups.Text = "Groups";
            // 
            // btnRemoveClass
            // 
            this.btnRemoveClass.Location = new System.Drawing.Point(93, 465);
            this.btnRemoveClass.Name = "btnRemoveClass";
            this.btnRemoveClass.Size = new System.Drawing.Size(81, 23);
            this.btnRemoveClass.TabIndex = 3;
            this.btnRemoveClass.Text = "Remove";
            this.tipToolTip.SetToolTip(this.btnRemoveClass, "Remove the selected class or room from the tree above");
            this.btnRemoveClass.UseVisualStyleBackColor = true;
            this.btnRemoveClass.Click += new System.EventHandler(this.btnRemoveClass_Click);
            // 
            // btnAddClass
            // 
            this.btnAddClass.Location = new System.Drawing.Point(6, 465);
            this.btnAddClass.Name = "btnAddClass";
            this.btnAddClass.Size = new System.Drawing.Size(81, 23);
            this.btnAddClass.TabIndex = 2;
            this.btnAddClass.Text = "Add Class";
            this.tipToolTip.SetToolTip(this.btnAddClass, "Add an empty class or load student details from a file");
            this.btnAddClass.UseVisualStyleBackColor = true;
            this.btnAddClass.Click += new System.EventHandler(this.btnAddClass_Click);
            // 
            // rdoRoom
            // 
            this.rdoRoom.AutoSize = true;
            this.rdoRoom.Location = new System.Drawing.Point(121, 19);
            this.rdoRoom.Name = "rdoRoom";
            this.rdoRoom.Size = new System.Drawing.Size(53, 17);
            this.rdoRoom.TabIndex = 1;
            this.rdoRoom.Text = "Room";
            this.rdoRoom.UseVisualStyleBackColor = true;
            this.rdoRoom.CheckedChanged += new System.EventHandler(this.rdoRoom_CheckedChanged);
            // 
            // lblGroupBy
            // 
            this.lblGroupBy.AutoSize = true;
            this.lblGroupBy.Location = new System.Drawing.Point(6, 21);
            this.lblGroupBy.Name = "lblGroupBy";
            this.lblGroupBy.Size = new System.Drawing.Size(53, 13);
            this.lblGroupBy.TabIndex = 2;
            this.lblGroupBy.Text = "Group by:";
            // 
            // rdoClass
            // 
            this.rdoClass.AutoSize = true;
            this.rdoClass.Checked = true;
            this.rdoClass.Location = new System.Drawing.Point(65, 19);
            this.rdoClass.Name = "rdoClass";
            this.rdoClass.Size = new System.Drawing.Size(50, 17);
            this.rdoClass.TabIndex = 0;
            this.rdoClass.TabStop = true;
            this.rdoClass.Text = "Class";
            this.rdoClass.UseVisualStyleBackColor = true;
            this.rdoClass.CheckedChanged += new System.EventHandler(this.rdoClass_CheckedChanged);
            // 
            // trvClassesRooms
            // 
            this.trvClassesRooms.Location = new System.Drawing.Point(6, 42);
            this.trvClassesRooms.Name = "trvClassesRooms";
            this.trvClassesRooms.Size = new System.Drawing.Size(168, 417);
            this.trvClassesRooms.TabIndex = 0;
            this.trvClassesRooms.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvClassesRooms_AfterSelect);
            this.trvClassesRooms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.trvClassesRooms_KeyDown);
            // 
            // grpClass
            // 
            this.grpClass.Controls.Add(this.btnAddRoom);
            this.grpClass.Controls.Add(this.btnRandomise);
            this.grpClass.Controls.Add(this.txtClassName);
            this.grpClass.Controls.Add(this.lblClassName);
            this.grpClass.Controls.Add(this.btnRemoveStudent);
            this.grpClass.Controls.Add(this.btnAddStudent);
            this.grpClass.Controls.Add(this.btnRemoveDB);
            this.grpClass.Controls.Add(this.btnAddDB);
            this.grpClass.Controls.Add(this.btnRemoveWW);
            this.grpClass.Controls.Add(this.btnAddWW);
            this.grpClass.Controls.Add(this.lstDistractedBy);
            this.grpClass.Controls.Add(this.lstWorksWell);
            this.grpClass.Controls.Add(this.lblDistractedBy);
            this.grpClass.Controls.Add(this.lblWorksWell);
            this.grpClass.Controls.Add(this.lstStudents);
            this.grpClass.Controls.Add(this.lblAge);
            this.grpClass.Controls.Add(this.lblDoB);
            this.grpClass.Controls.Add(this.lblName);
            this.grpClass.Controls.Add(this.picSilhouette);
            this.grpClass.Location = new System.Drawing.Point(198, 27);
            this.grpClass.Name = "grpClass";
            this.grpClass.Size = new System.Drawing.Size(200, 497);
            this.grpClass.TabIndex = 2;
            this.grpClass.TabStop = false;
            this.grpClass.Text = "Class";
            // 
            // btnAddRoom
            // 
            this.btnAddRoom.Location = new System.Drawing.Point(119, 465);
            this.btnAddRoom.Name = "btnAddRoom";
            this.btnAddRoom.Size = new System.Drawing.Size(75, 23);
            this.btnAddRoom.TabIndex = 15;
            this.btnAddRoom.Text = "Add Room";
            this.tipToolTip.SetToolTip(this.btnAddRoom, "Add a room this class uses for teaching - requires that all students have at\r\nlea" +
        "st one other they work well with and one other they are distracted by");
            this.btnAddRoom.UseVisualStyleBackColor = true;
            this.btnAddRoom.Click += new System.EventHandler(this.btnAddRoom_Click);
            // 
            // btnRandomise
            // 
            this.btnRandomise.Location = new System.Drawing.Point(6, 465);
            this.btnRandomise.Name = "btnRandomise";
            this.btnRandomise.Size = new System.Drawing.Size(107, 23);
            this.btnRandomise.TabIndex = 14;
            this.btnRandomise.Text = "Fill Relationships";
            this.tipToolTip.SetToolTip(this.btnRandomise, resources.GetString("btnRandomise.ToolTip"));
            this.btnRandomise.UseVisualStyleBackColor = true;
            this.btnRandomise.Click += new System.EventHandler(this.btnRandomise_Click);
            // 
            // txtClassName
            // 
            this.txtClassName.Location = new System.Drawing.Point(76, 18);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(118, 20);
            this.txtClassName.TabIndex = 4;
            this.txtClassName.TextChanged += new System.EventHandler(this.txtClassName_TextChanged);
            // 
            // lblClassName
            // 
            this.lblClassName.AutoSize = true;
            this.lblClassName.Location = new System.Drawing.Point(6, 21);
            this.lblClassName.Name = "lblClassName";
            this.lblClassName.Size = new System.Drawing.Size(64, 13);
            this.lblClassName.TabIndex = 17;
            this.lblClassName.Text = "Class name:";
            // 
            // btnRemoveStudent
            // 
            this.btnRemoveStudent.Location = new System.Drawing.Point(171, 179);
            this.btnRemoveStudent.Name = "btnRemoveStudent";
            this.btnRemoveStudent.Size = new System.Drawing.Size(23, 23);
            this.btnRemoveStudent.TabIndex = 7;
            this.btnRemoveStudent.Text = "-";
            this.btnRemoveStudent.UseVisualStyleBackColor = true;
            this.btnRemoveStudent.Click += new System.EventHandler(this.btnRemoveStudent_Click);
            // 
            // btnAddStudent
            // 
            this.btnAddStudent.Location = new System.Drawing.Point(171, 150);
            this.btnAddStudent.Name = "btnAddStudent";
            this.btnAddStudent.Size = new System.Drawing.Size(23, 23);
            this.btnAddStudent.TabIndex = 6;
            this.btnAddStudent.Text = "+";
            this.btnAddStudent.UseVisualStyleBackColor = true;
            this.btnAddStudent.Click += new System.EventHandler(this.btnAddStudent_Click);
            // 
            // btnRemoveDB
            // 
            this.btnRemoveDB.Location = new System.Drawing.Point(171, 429);
            this.btnRemoveDB.Name = "btnRemoveDB";
            this.btnRemoveDB.Size = new System.Drawing.Size(23, 23);
            this.btnRemoveDB.TabIndex = 13;
            this.btnRemoveDB.Text = "-";
            this.btnRemoveDB.UseVisualStyleBackColor = true;
            this.btnRemoveDB.Click += new System.EventHandler(this.btnRemoveDB_Click);
            // 
            // btnAddDB
            // 
            this.btnAddDB.Location = new System.Drawing.Point(171, 396);
            this.btnAddDB.Name = "btnAddDB";
            this.btnAddDB.Size = new System.Drawing.Size(23, 23);
            this.btnAddDB.TabIndex = 12;
            this.btnAddDB.Text = "+";
            this.btnAddDB.UseVisualStyleBackColor = true;
            this.btnAddDB.Click += new System.EventHandler(this.btnAddDB_Click);
            // 
            // btnRemoveWW
            // 
            this.btnRemoveWW.Location = new System.Drawing.Point(171, 345);
            this.btnRemoveWW.Name = "btnRemoveWW";
            this.btnRemoveWW.Size = new System.Drawing.Size(23, 23);
            this.btnRemoveWW.TabIndex = 10;
            this.btnRemoveWW.Text = "-";
            this.btnRemoveWW.UseVisualStyleBackColor = true;
            this.btnRemoveWW.Click += new System.EventHandler(this.btnRemoveWW_Click);
            // 
            // btnAddWW
            // 
            this.btnAddWW.Location = new System.Drawing.Point(171, 312);
            this.btnAddWW.Name = "btnAddWW";
            this.btnAddWW.Size = new System.Drawing.Size(23, 23);
            this.btnAddWW.TabIndex = 9;
            this.btnAddWW.Text = "+";
            this.btnAddWW.UseVisualStyleBackColor = true;
            this.btnAddWW.Click += new System.EventHandler(this.btnAddWW_Click);
            // 
            // lstDistractedBy
            // 
            this.lstDistractedBy.FormattingEnabled = true;
            this.lstDistractedBy.Location = new System.Drawing.Point(6, 396);
            this.lstDistractedBy.Name = "lstDistractedBy";
            this.lstDistractedBy.Size = new System.Drawing.Size(159, 56);
            this.lstDistractedBy.TabIndex = 11;
            // 
            // lstWorksWell
            // 
            this.lstWorksWell.FormattingEnabled = true;
            this.lstWorksWell.Location = new System.Drawing.Point(6, 312);
            this.lstWorksWell.Name = "lstWorksWell";
            this.lstWorksWell.Size = new System.Drawing.Size(159, 56);
            this.lstWorksWell.TabIndex = 8;
            // 
            // lblDistractedBy
            // 
            this.lblDistractedBy.AutoSize = true;
            this.lblDistractedBy.Location = new System.Drawing.Point(6, 380);
            this.lblDistractedBy.Name = "lblDistractedBy";
            this.lblDistractedBy.Size = new System.Drawing.Size(95, 13);
            this.lblDistractedBy.TabIndex = 8;
            this.lblDistractedBy.Text = "Gets distracted by:";
            // 
            // lblWorksWell
            // 
            this.lblWorksWell.AutoSize = true;
            this.lblWorksWell.Location = new System.Drawing.Point(6, 296);
            this.lblWorksWell.Name = "lblWorksWell";
            this.lblWorksWell.Size = new System.Drawing.Size(84, 13);
            this.lblWorksWell.TabIndex = 7;
            this.lblWorksWell.Text = "Works well with:";
            // 
            // lstStudents
            // 
            this.lstStudents.FormattingEnabled = true;
            this.lstStudents.Location = new System.Drawing.Point(6, 150);
            this.lstStudents.Name = "lstStudents";
            this.lstStudents.Size = new System.Drawing.Size(159, 134);
            this.lstStudents.TabIndex = 5;
            this.lstStudents.SelectedIndexChanged += new System.EventHandler(this.lstStudents_SelectedIndexChanged);
            this.lstStudents.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstStudents_KeyPress);
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(58, 86);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(26, 13);
            this.lblAge.TabIndex = 3;
            this.lblAge.Text = "Age";
            // 
            // lblDoB
            // 
            this.lblDoB.AutoSize = true;
            this.lblDoB.Location = new System.Drawing.Point(58, 66);
            this.lblDoB.Name = "lblDoB";
            this.lblDoB.Size = new System.Drawing.Size(28, 13);
            this.lblDoB.TabIndex = 2;
            this.lblDoB.Text = "DoB";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(58, 46);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            // 
            // picSilhouette
            // 
            this.picSilhouette.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSilhouette.Image = ((System.Drawing.Image)(resources.GetObject("picSilhouette.Image")));
            this.picSilhouette.Location = new System.Drawing.Point(6, 44);
            this.picSilhouette.Name = "picSilhouette";
            this.picSilhouette.Size = new System.Drawing.Size(46, 100);
            this.picSilhouette.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSilhouette.TabIndex = 0;
            this.picSilhouette.TabStop = false;
            // 
            // grpRoomLayout
            // 
            this.grpRoomLayout.Controls.Add(this.grpLayout);
            this.grpRoomLayout.Controls.Add(this.pnlRoomSettings);
            this.grpRoomLayout.Controls.Add(this.chkSideSeats);
            this.grpRoomLayout.Controls.Add(this.btnResetRoom);
            this.grpRoomLayout.Controls.Add(this.lblNumSeats);
            this.grpRoomLayout.Controls.Add(this.lblNumTables);
            this.grpRoomLayout.Controls.Add(this.btnEditRoom);
            this.grpRoomLayout.Controls.Add(this.grpPlanBasis);
            this.grpRoomLayout.Controls.Add(this.txtRoomName);
            this.grpRoomLayout.Controls.Add(this.label1);
            this.grpRoomLayout.Location = new System.Drawing.Point(404, 27);
            this.grpRoomLayout.Name = "grpRoomLayout";
            this.grpRoomLayout.Size = new System.Drawing.Size(368, 497);
            this.grpRoomLayout.TabIndex = 3;
            this.grpRoomLayout.TabStop = false;
            this.grpRoomLayout.Text = "Room Layout";
            // 
            // grpLayout
            // 
            this.grpLayout.Controls.Add(this.btnLeft);
            this.grpLayout.Controls.Add(this.btnRight);
            this.grpLayout.Location = new System.Drawing.Point(6, 78);
            this.grpLayout.Name = "grpLayout";
            this.grpLayout.Size = new System.Drawing.Size(356, 283);
            this.grpLayout.TabIndex = 37;
            this.grpLayout.TabStop = false;
            this.grpLayout.Text = "Room Layout";
            // 
            // btnLeft
            // 
            this.btnLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLeft.Location = new System.Drawing.Point(6, 254);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(23, 23);
            this.btnLeft.TabIndex = 27;
            this.btnLeft.Text = "<";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnRight
            // 
            this.btnRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRight.Location = new System.Drawing.Point(327, 254);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(23, 23);
            this.btnRight.TabIndex = 28;
            this.btnRight.Text = ">";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // pnlRoomSettings
            // 
            this.pnlRoomSettings.Controls.Add(this.lblTableWidth);
            this.pnlRoomSettings.Controls.Add(this.txtTableWidth);
            this.pnlRoomSettings.Controls.Add(this.lblTableDepth);
            this.pnlRoomSettings.Controls.Add(this.txtTableDepth);
            this.pnlRoomSettings.Controls.Add(this.txtRows);
            this.pnlRoomSettings.Controls.Add(this.lblRows);
            this.pnlRoomSettings.Controls.Add(this.lblColumns);
            this.pnlRoomSettings.Controls.Add(this.txtColumns);
            this.pnlRoomSettings.Location = new System.Drawing.Point(6, 45);
            this.pnlRoomSettings.Name = "pnlRoomSettings";
            this.pnlRoomSettings.Size = new System.Drawing.Size(356, 27);
            this.pnlRoomSettings.TabIndex = 19;
            // 
            // lblTableWidth
            // 
            this.lblTableWidth.AutoSize = true;
            this.lblTableWidth.Location = new System.Drawing.Point(155, 7);
            this.lblTableWidth.Name = "lblTableWidth";
            this.lblTableWidth.Size = new System.Drawing.Size(68, 13);
            this.lblTableWidth.TabIndex = 35;
            this.lblTableWidth.Text = "Table Width:";
            // 
            // txtTableWidth
            // 
            this.txtTableWidth.Location = new System.Drawing.Point(229, 4);
            this.txtTableWidth.Name = "txtTableWidth";
            this.txtTableWidth.Size = new System.Drawing.Size(20, 20);
            this.txtTableWidth.TabIndex = 22;
            this.txtTableWidth.Text = "1";
            this.tipToolTip.SetToolTip(this.txtTableWidth, "Use these text boxes to set an initial (rough) layout for the room -\r\ntables and " +
        "seats can be rearranged by clicking the Edit button");
            this.txtTableWidth.Click += new System.EventHandler(this.txtRoomSettings_Click);
            this.txtTableWidth.TextChanged += new System.EventHandler(this.txtRoomSettings_TextChanged);
            this.txtTableWidth.Enter += new System.EventHandler(this.txtRoomSettings_Enter);
            this.txtTableWidth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRoomSettings_KeyDown);
            this.txtTableWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRoomSettings_KeyPress);
            // 
            // lblTableDepth
            // 
            this.lblTableDepth.AutoSize = true;
            this.lblTableDepth.Location = new System.Drawing.Point(255, 7);
            this.lblTableDepth.Name = "lblTableDepth";
            this.lblTableDepth.Size = new System.Drawing.Size(69, 13);
            this.lblTableDepth.TabIndex = 32;
            this.lblTableDepth.Text = "Table Depth:";
            // 
            // txtTableDepth
            // 
            this.txtTableDepth.Location = new System.Drawing.Point(330, 4);
            this.txtTableDepth.Name = "txtTableDepth";
            this.txtTableDepth.Size = new System.Drawing.Size(20, 20);
            this.txtTableDepth.TabIndex = 23;
            this.txtTableDepth.Text = "1";
            this.tipToolTip.SetToolTip(this.txtTableDepth, "Use these text boxes to set an initial (rough) layout for the room -\r\ntables and " +
        "seats can be rearranged by clicking the Edit button");
            this.txtTableDepth.Click += new System.EventHandler(this.txtRoomSettings_Click);
            this.txtTableDepth.TextChanged += new System.EventHandler(this.txtRoomSettings_TextChanged);
            this.txtTableDepth.Enter += new System.EventHandler(this.txtRoomSettings_Enter);
            this.txtTableDepth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRoomSettings_KeyDown);
            this.txtTableDepth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRoomSettings_KeyPress);
            // 
            // txtRows
            // 
            this.txtRows.Location = new System.Drawing.Point(129, 4);
            this.txtRows.Name = "txtRows";
            this.txtRows.Size = new System.Drawing.Size(20, 20);
            this.txtRows.TabIndex = 21;
            this.txtRows.Text = "1";
            this.tipToolTip.SetToolTip(this.txtRows, "Use these text boxes to set an initial (rough) layout for the room -\r\ntables and " +
        "seats can be rearranged by clicking the Edit button");
            this.txtRows.Click += new System.EventHandler(this.txtRoomSettings_Click);
            this.txtRows.TextChanged += new System.EventHandler(this.txtRoomSettings_TextChanged);
            this.txtRows.Enter += new System.EventHandler(this.txtRoomSettings_Enter);
            this.txtRows.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRoomSettings_KeyDown);
            this.txtRows.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRoomSettings_KeyPress);
            // 
            // lblRows
            // 
            this.lblRows.AutoSize = true;
            this.lblRows.Location = new System.Drawing.Point(86, 7);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(37, 13);
            this.lblRows.TabIndex = 31;
            this.lblRows.Text = "Rows:";
            // 
            // lblColumns
            // 
            this.lblColumns.AutoSize = true;
            this.lblColumns.Location = new System.Drawing.Point(4, 7);
            this.lblColumns.Name = "lblColumns";
            this.lblColumns.Size = new System.Drawing.Size(50, 13);
            this.lblColumns.TabIndex = 30;
            this.lblColumns.Text = "Columns:";
            // 
            // txtColumns
            // 
            this.txtColumns.Location = new System.Drawing.Point(60, 4);
            this.txtColumns.Name = "txtColumns";
            this.txtColumns.Size = new System.Drawing.Size(20, 20);
            this.txtColumns.TabIndex = 20;
            this.txtColumns.Text = "1";
            this.tipToolTip.SetToolTip(this.txtColumns, "Use these text boxes to set an initial (rough) layout for the room -\r\ntables and " +
        "seats can be rearranged by clicking the Edit button");
            this.txtColumns.Click += new System.EventHandler(this.txtRoomSettings_Click);
            this.txtColumns.TextChanged += new System.EventHandler(this.txtRoomSettings_TextChanged);
            this.txtColumns.Enter += new System.EventHandler(this.txtRoomSettings_Enter);
            this.txtColumns.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRoomSettings_KeyDown);
            this.txtColumns.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRoomSettings_KeyPress);
            // 
            // chkSideSeats
            // 
            this.chkSideSeats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSideSeats.AutoSize = true;
            this.chkSideSeats.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSideSeats.Checked = true;
            this.chkSideSeats.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSideSeats.Location = new System.Drawing.Point(6, 367);
            this.chkSideSeats.Name = "chkSideSeats";
            this.chkSideSeats.Size = new System.Drawing.Size(128, 17);
            this.chkSideSeats.TabIndex = 27;
            this.chkSideSeats.Text = "Seats between tables";
            this.chkSideSeats.UseVisualStyleBackColor = true;
            this.chkSideSeats.CheckedChanged += new System.EventHandler(this.chkSideSeats_CheckedChanged);
            // 
            // btnResetRoom
            // 
            this.btnResetRoom.Location = new System.Drawing.Point(284, 16);
            this.btnResetRoom.Name = "btnResetRoom";
            this.btnResetRoom.Size = new System.Drawing.Size(78, 23);
            this.btnResetRoom.TabIndex = 18;
            this.btnResetRoom.Text = "Reset Room";
            this.tipToolTip.SetToolTip(this.btnResetRoom, "Reset layout to settings given by number\r\nof columns/rows and table dimensions");
            this.btnResetRoom.UseVisualStyleBackColor = true;
            this.btnResetRoom.Click += new System.EventHandler(this.btnResetRoom_Click);
            // 
            // lblNumSeats
            // 
            this.lblNumSeats.AutoSize = true;
            this.lblNumSeats.Location = new System.Drawing.Point(6, 406);
            this.lblNumSeats.Name = "lblNumSeats";
            this.lblNumSeats.Size = new System.Drawing.Size(87, 13);
            this.lblNumSeats.TabIndex = 36;
            this.lblNumSeats.Text = "Number of seats:";
            // 
            // lblNumTables
            // 
            this.lblNumTables.AutoSize = true;
            this.lblNumTables.Location = new System.Drawing.Point(6, 387);
            this.lblNumTables.Name = "lblNumTables";
            this.lblNumTables.Size = new System.Drawing.Size(90, 13);
            this.lblNumTables.TabIndex = 35;
            this.lblNumTables.Text = "Number of tables:";
            // 
            // btnEditRoom
            // 
            this.btnEditRoom.Location = new System.Drawing.Point(203, 16);
            this.btnEditRoom.Name = "btnEditRoom";
            this.btnEditRoom.Size = new System.Drawing.Size(75, 23);
            this.btnEditRoom.TabIndex = 17;
            this.btnEditRoom.Text = "Edit Room";
            this.tipToolTip.SetToolTip(this.btnEditRoom, "Move tables and seats around the room");
            this.btnEditRoom.UseVisualStyleBackColor = true;
            this.btnEditRoom.Click += new System.EventHandler(this.btnEditRoom_Click);
            // 
            // grpPlanBasis
            // 
            this.grpPlanBasis.Controls.Add(this.nudNumGenerations);
            this.grpPlanBasis.Controls.Add(this.lblNumGenerations);
            this.grpPlanBasis.Controls.Add(this.nudNumBreeders);
            this.grpPlanBasis.Controls.Add(this.lblNumBreeders);
            this.grpPlanBasis.Controls.Add(this.btnGenerate);
            this.grpPlanBasis.Controls.Add(this.rdoAttainment);
            this.grpPlanBasis.Controls.Add(this.rdoStudentSettings);
            this.grpPlanBasis.Location = new System.Drawing.Point(153, 367);
            this.grpPlanBasis.Name = "grpPlanBasis";
            this.grpPlanBasis.Size = new System.Drawing.Size(209, 124);
            this.grpPlanBasis.TabIndex = 33;
            this.grpPlanBasis.TabStop = false;
            this.grpPlanBasis.Text = "Seating Plan Generation";
            // 
            // nudNumGenerations
            // 
            this.nudNumGenerations.Location = new System.Drawing.Point(129, 68);
            this.nudNumGenerations.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudNumGenerations.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudNumGenerations.Name = "nudNumGenerations";
            this.nudNumGenerations.Size = new System.Drawing.Size(48, 20);
            this.nudNumGenerations.TabIndex = 36;
            this.nudNumGenerations.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // lblNumGenerations
            // 
            this.lblNumGenerations.AutoSize = true;
            this.lblNumGenerations.Location = new System.Drawing.Point(6, 70);
            this.lblNumGenerations.Name = "lblNumGenerations";
            this.lblNumGenerations.Size = new System.Drawing.Size(117, 13);
            this.lblNumGenerations.TabIndex = 35;
            this.lblNumGenerations.Text = "Number of generations:";
            // 
            // nudNumBreeders
            // 
            this.nudNumBreeders.Location = new System.Drawing.Point(129, 42);
            this.nudNumBreeders.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudNumBreeders.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudNumBreeders.Name = "nudNumBreeders";
            this.nudNumBreeders.Size = new System.Drawing.Size(48, 20);
            this.nudNumBreeders.TabIndex = 34;
            this.nudNumBreeders.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblNumBreeders
            // 
            this.lblNumBreeders.AutoSize = true;
            this.lblNumBreeders.Location = new System.Drawing.Point(6, 44);
            this.lblNumBreeders.Name = "lblNumBreeders";
            this.lblNumBreeders.Size = new System.Drawing.Size(103, 13);
            this.lblNumBreeders.TabIndex = 33;
            this.lblNumBreeders.Text = "Number of breeders:";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Location = new System.Drawing.Point(6, 95);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(197, 23);
            this.btnGenerate.TabIndex = 32;
            this.btnGenerate.Text = "Generate Seating Plan";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // rdoAttainment
            // 
            this.rdoAttainment.AutoSize = true;
            this.rdoAttainment.Enabled = false;
            this.rdoAttainment.Location = new System.Drawing.Point(6, 19);
            this.rdoAttainment.Name = "rdoAttainment";
            this.rdoAttainment.Size = new System.Drawing.Size(75, 17);
            this.rdoAttainment.TabIndex = 28;
            this.rdoAttainment.Text = "Attainment";
            this.rdoAttainment.UseVisualStyleBackColor = true;
            // 
            // rdoStudentSettings
            // 
            this.rdoStudentSettings.AutoSize = true;
            this.rdoStudentSettings.Checked = true;
            this.rdoStudentSettings.Location = new System.Drawing.Point(87, 19);
            this.rdoStudentSettings.Name = "rdoStudentSettings";
            this.rdoStudentSettings.Size = new System.Drawing.Size(116, 17);
            this.rdoStudentSettings.TabIndex = 31;
            this.rdoStudentSettings.TabStop = true;
            this.rdoStudentSettings.Text = "Class Relationships";
            this.rdoStudentSettings.UseVisualStyleBackColor = true;
            // 
            // txtRoomName
            // 
            this.txtRoomName.Location = new System.Drawing.Point(79, 18);
            this.txtRoomName.Name = "txtRoomName";
            this.txtRoomName.Size = new System.Drawing.Size(118, 20);
            this.txtRoomName.TabIndex = 16;
            this.txtRoomName.TextChanged += new System.EventHandler(this.txtRoomName_TextChanged);
            this.txtRoomName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRoomName_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Room name:";
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.FileName = "setup.sps";
            // 
            // txtMagicFloatingTextBox
            // 
            this.txtMagicFloatingTextBox.Location = new System.Drawing.Point(404, 530);
            this.txtMagicFloatingTextBox.Name = "txtMagicFloatingTextBox";
            this.txtMagicFloatingTextBox.Size = new System.Drawing.Size(159, 20);
            this.txtMagicFloatingTextBox.TabIndex = 6;
            this.tipToolTip.SetToolTip(this.txtMagicFloatingTextBox, "Press [Enter] to add the selected student or [Escape] to cancel");
            this.txtMagicFloatingTextBox.Visible = false;
            this.txtMagicFloatingTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMagicFloatingTextBox_KeyPress);
            // 
            // cmbMagicFloatingComboBox
            // 
            this.cmbMagicFloatingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMagicFloatingComboBox.FormattingEnabled = true;
            this.cmbMagicFloatingComboBox.Location = new System.Drawing.Point(569, 530);
            this.cmbMagicFloatingComboBox.Name = "cmbMagicFloatingComboBox";
            this.cmbMagicFloatingComboBox.Size = new System.Drawing.Size(159, 21);
            this.cmbMagicFloatingComboBox.TabIndex = 7;
            this.tipToolTip.SetToolTip(this.cmbMagicFloatingComboBox, "Press [Enter] to add the selected student or [Escape] to cancel");
            this.cmbMagicFloatingComboBox.Visible = false;
            this.cmbMagicFloatingComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbMagicFloatingComboBox_KeyPress);
            // 
            // stsStatusStrip
            // 
            this.stsStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsText,
            this.stsProgress});
            this.stsStatusStrip.Location = new System.Drawing.Point(0, 536);
            this.stsStatusStrip.Name = "stsStatusStrip";
            this.stsStatusStrip.Size = new System.Drawing.Size(784, 22);
            this.stsStatusStrip.TabIndex = 8;
            // 
            // stsText
            // 
            this.stsText.Name = "stsText";
            this.stsText.Size = new System.Drawing.Size(0, 17);
            // 
            // stsProgress
            // 
            this.stsProgress.Name = "stsProgress";
            this.stsProgress.Size = new System.Drawing.Size(100, 16);
            this.stsProgress.Visible = false;
            // 
            // tmrFlashStatus
            // 
            this.tmrFlashStatus.Interval = 500;
            this.tmrFlashStatus.Tick += new System.EventHandler(this.tmrFlashStatus_Tick);
            // 
            // tipToolTip
            // 
            this.tipToolTip.AutoPopDelay = 5000;
            this.tipToolTip.InitialDelay = 50;
            this.tipToolTip.ReshowDelay = 100;
            // 
            // ClassRoomSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 558);
            this.Controls.Add(this.txtMagicFloatingTextBox);
            this.Controls.Add(this.cmbMagicFloatingComboBox);
            this.Controls.Add(this.grpRoomLayout);
            this.Controls.Add(this.stsStatusStrip);
            this.Controls.Add(this.grpClass);
            this.Controls.Add(this.grpGroups);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "ClassRoomSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seating Plans";
            this.Activated += new System.EventHandler(this.SeatingPlans_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SeatingPlans_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.grpGroups.ResumeLayout(false);
            this.grpGroups.PerformLayout();
            this.grpClass.ResumeLayout(false);
            this.grpClass.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSilhouette)).EndInit();
            this.grpRoomLayout.ResumeLayout(false);
            this.grpRoomLayout.PerformLayout();
            this.grpLayout.ResumeLayout(false);
            this.pnlRoomSettings.ResumeLayout(false);
            this.pnlRoomSettings.PerformLayout();
            this.grpPlanBasis.ResumeLayout(false);
            this.grpPlanBasis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumGenerations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumBreeders)).EndInit();
            this.stsStatusStrip.ResumeLayout(false);
            this.stsStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox grpGroups;
        private System.Windows.Forms.RadioButton rdoRoom;
        private System.Windows.Forms.Label lblGroupBy;
        private System.Windows.Forms.RadioButton rdoClass;
        private System.Windows.Forms.TreeView trvClassesRooms;
        private System.Windows.Forms.GroupBox grpClass;
        private System.Windows.Forms.PictureBox picSilhouette;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblDoB;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblWorksWell;
        private System.Windows.Forms.ListBox lstStudents;
        private System.Windows.Forms.ListBox lstDistractedBy;
        private System.Windows.Forms.ListBox lstWorksWell;
        private System.Windows.Forms.Label lblDistractedBy;
        private System.Windows.Forms.Button btnRemoveDB;
        private System.Windows.Forms.Button btnAddDB;
        private System.Windows.Forms.Button btnRemoveWW;
        private System.Windows.Forms.Button btnAddWW;
        private System.Windows.Forms.GroupBox grpRoomLayout;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.SaveFileDialog dlgSaveFile;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.Label lblClassName;
        private System.Windows.Forms.Button btnRemoveStudent;
        private System.Windows.Forms.Button btnAddStudent;
        private System.Windows.Forms.TextBox txtRoomName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddClass;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem highlightNextStepsToolStripMenuItem;
        private System.Windows.Forms.Button btnRandomise;
        private System.Windows.Forms.TextBox txtMagicFloatingTextBox;
        private System.Windows.Forms.ComboBox cmbMagicFloatingComboBox;
        private System.Windows.Forms.StatusStrip stsStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel stsText;
        private System.Windows.Forms.GroupBox grpPlanBasis;
        private System.Windows.Forms.RadioButton rdoAttainment;
        private System.Windows.Forms.RadioButton rdoStudentSettings;
        private System.Windows.Forms.Button btnAddRoom;
        private System.Windows.Forms.Timer tmrFlashStatus;
        private System.Windows.Forms.Button btnRemoveClass;
        private System.Windows.Forms.ToolTip tipToolTip;
        private System.Windows.Forms.Button btnEditRoom;
        private System.Windows.Forms.Label lblNumSeats;
        private System.Windows.Forms.Label lblNumTables;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ToolStripProgressBar stsProgress;
        private System.Windows.Forms.Button btnResetRoom;
        private System.Windows.Forms.CheckBox chkSideSeats;
        private System.Windows.Forms.Panel pnlRoomSettings;
        private System.Windows.Forms.Label lblTableDepth;
        private System.Windows.Forms.TextBox txtTableDepth;
        private System.Windows.Forms.TextBox txtRows;
        private System.Windows.Forms.Label lblRows;
        private System.Windows.Forms.Label lblColumns;
        private System.Windows.Forms.TextBox txtColumns;
        private System.Windows.Forms.Label lblTableWidth;
        private System.Windows.Forms.TextBox txtTableWidth;
        private System.Windows.Forms.GroupBox grpLayout;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.NumericUpDown nudNumBreeders;
        private System.Windows.Forms.Label lblNumBreeders;
        private System.Windows.Forms.NumericUpDown nudNumGenerations;
        private System.Windows.Forms.Label lblNumGenerations;
    }
}