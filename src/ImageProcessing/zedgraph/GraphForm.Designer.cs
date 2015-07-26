namespace zedgraph
{
    partial class GraphForm
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
            this.graph = new ZedGraph.ZedGraphControl();
            this.itog = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.selectedBest = new System.Windows.Forms.RadioButton();
            this.selected0 = new System.Windows.Forms.RadioButton();
            this.selected90 = new System.Windows.Forms.RadioButton();
            this.selected180 = new System.Windows.Forms.RadioButton();
            this.selected270 = new System.Windows.Forms.RadioButton();
            this.pathFolderButton = new System.Windows.Forms.Button();
            this.pathButton = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.BuildButton = new System.Windows.Forms.Button();
            this.pathEtalon = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // graph
            // 
            this.graph.Location = new System.Drawing.Point(23, 92);
            this.graph.Name = "graph";
            this.graph.ScrollGrace = 0D;
            this.graph.ScrollMaxX = 0D;
            this.graph.ScrollMaxY = 0D;
            this.graph.ScrollMaxY2 = 0D;
            this.graph.ScrollMinX = 0D;
            this.graph.ScrollMinY = 0D;
            this.graph.ScrollMinY2 = 0D;
            this.graph.Size = new System.Drawing.Size(558, 390);
            this.graph.TabIndex = 3;
            // 
            // itog
            // 
            this.itog.AutoSize = true;
            this.itog.Location = new System.Drawing.Point(29, 495);
            this.itog.Name = "itog";
            this.itog.Size = new System.Drawing.Size(0, 13);
            this.itog.TabIndex = 4;
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(602, 63);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(86, 23);
            this.buttonNext.TabIndex = 5;
            this.buttonNext.Text = "Следующий";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Visible = false;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonPrev
            // 
            this.buttonPrev.Location = new System.Drawing.Point(602, 92);
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(86, 23);
            this.buttonPrev.TabIndex = 6;
            this.buttonPrev.Text = "Предыдущий";
            this.buttonPrev.UseVisualStyleBackColor = true;
            this.buttonPrev.Visible = false;
            this.buttonPrev.Click += new System.EventHandler(this.buttonPrev_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(602, 133);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(86, 23);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // selectedBest
            // 
            this.selectedBest.AutoSize = true;
            this.selectedBest.Checked = true;
            this.selectedBest.Location = new System.Drawing.Point(602, 252);
            this.selectedBest.Name = "selectedBest";
            this.selectedBest.Size = new System.Drawing.Size(63, 17);
            this.selectedBest.TabIndex = 8;
            this.selectedBest.TabStop = true;
            this.selectedBest.Text = "Лучший";
            this.selectedBest.UseVisualStyleBackColor = true;
            this.selectedBest.CheckedChanged += new System.EventHandler(this.selectedBest_CheckedChanged);
            // 
            // selected0
            // 
            this.selected0.AutoSize = true;
            this.selected0.Location = new System.Drawing.Point(602, 275);
            this.selected0.Name = "selected0";
            this.selected0.Size = new System.Drawing.Size(80, 17);
            this.selected0.TabIndex = 9;
            this.selected0.Text = "0 градусов";
            this.selected0.UseVisualStyleBackColor = true;
            this.selected0.CheckedChanged += new System.EventHandler(this.selected0_CheckedChanged);
            // 
            // selected90
            // 
            this.selected90.AutoSize = true;
            this.selected90.Location = new System.Drawing.Point(602, 298);
            this.selected90.Name = "selected90";
            this.selected90.Size = new System.Drawing.Size(86, 17);
            this.selected90.TabIndex = 10;
            this.selected90.TabStop = true;
            this.selected90.Text = "90 градусов";
            this.selected90.UseVisualStyleBackColor = true;
            this.selected90.CheckedChanged += new System.EventHandler(this.selected90_CheckedChanged);
            // 
            // selected180
            // 
            this.selected180.AutoSize = true;
            this.selected180.Location = new System.Drawing.Point(602, 321);
            this.selected180.Name = "selected180";
            this.selected180.Size = new System.Drawing.Size(92, 17);
            this.selected180.TabIndex = 11;
            this.selected180.TabStop = true;
            this.selected180.Text = "180 градусов";
            this.selected180.UseVisualStyleBackColor = true;
            this.selected180.CheckedChanged += new System.EventHandler(this.selected180_CheckedChanged);
            // 
            // selected270
            // 
            this.selected270.AutoSize = true;
            this.selected270.Location = new System.Drawing.Point(602, 344);
            this.selected270.Name = "selected270";
            this.selected270.Size = new System.Drawing.Size(92, 17);
            this.selected270.TabIndex = 12;
            this.selected270.TabStop = true;
            this.selected270.Text = "270 градусов";
            this.selected270.UseVisualStyleBackColor = true;
            this.selected270.CheckedChanged += new System.EventHandler(this.selected270_CheckedChanged);
            // 
            // pathFolderButton
            // 
            this.pathFolderButton.Location = new System.Drawing.Point(538, 33);
            this.pathFolderButton.Name = "pathFolderButton";
            this.pathFolderButton.Size = new System.Drawing.Size(43, 23);
            this.pathFolderButton.TabIndex = 18;
            this.pathFolderButton.Text = "...";
            this.pathFolderButton.UseVisualStyleBackColor = true;
            this.pathFolderButton.Click += new System.EventHandler(this.pathFolderButton_Click);
            // 
            // pathButton
            // 
            this.pathButton.Location = new System.Drawing.Point(538, 4);
            this.pathButton.Name = "pathButton";
            this.pathButton.Size = new System.Drawing.Size(43, 23);
            this.pathButton.TabIndex = 17;
            this.pathButton.Text = "...";
            this.pathButton.UseVisualStyleBackColor = true;
            this.pathButton.Click += new System.EventHandler(this.pathButton_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(160, 33);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(372, 20);
            this.textBox2.TabIndex = 16;
            // 
            // textBox1
            // 
            this.textBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.textBox1.Location = new System.Drawing.Point(159, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(373, 20);
            this.textBox1.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Папка конечного файла";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Путь к исходному файлу";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // BuildButton
            // 
            this.BuildButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.BuildButton.Location = new System.Drawing.Point(602, 4);
            this.BuildButton.Name = "BuildButton";
            this.BuildButton.Size = new System.Drawing.Size(86, 53);
            this.BuildButton.TabIndex = 19;
            this.BuildButton.Text = "Построить";
            this.BuildButton.UseVisualStyleBackColor = true;
            this.BuildButton.Click += new System.EventHandler(this.BuildButton_Click);
            // 
            // pathEtalon
            // 
            this.pathEtalon.Location = new System.Drawing.Point(538, 60);
            this.pathEtalon.Name = "pathEtalon";
            this.pathEtalon.Size = new System.Drawing.Size(43, 23);
            this.pathEtalon.TabIndex = 22;
            this.pathEtalon.Text = "...";
            this.pathEtalon.UseVisualStyleBackColor = true;
            this.pathEtalon.Click += new System.EventHandler(this.pathEtalon_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(160, 60);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(372, 20);
            this.textBox3.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Папка с эталонами";
            // 
            // GraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 589);
            this.Controls.Add(this.pathEtalon);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BuildButton);
            this.Controls.Add(this.pathFolderButton);
            this.Controls.Add(this.pathButton);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.selected270);
            this.Controls.Add(this.selected180);
            this.Controls.Add(this.selected90);
            this.Controls.Add(this.selected0);
            this.Controls.Add(this.selectedBest);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonPrev);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.itog);
            this.Controls.Add(this.graph);
            this.Name = "GraphForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl graph;
        private System.Windows.Forms.Label itog;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.RadioButton selectedBest;
        private System.Windows.Forms.RadioButton selected0;
        private System.Windows.Forms.RadioButton selected90;
        private System.Windows.Forms.RadioButton selected180;
        private System.Windows.Forms.RadioButton selected270;
        private System.Windows.Forms.Button pathFolderButton;
        private System.Windows.Forms.Button pathButton;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button BuildButton;
        private System.Windows.Forms.Button pathEtalon;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
    }
}

