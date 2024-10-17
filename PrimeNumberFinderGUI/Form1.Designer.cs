namespace PrimeNumberFinderGUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblStatus = new Label();
            tmrMain = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            lblElapsedTime = new Label();
            tmrRemaining = new System.Windows.Forms.Timer(components);
            btnStartStop = new Button();
            groupBox1 = new GroupBox();
            lblCalculatedNumber = new Label();
            lblNumberCalculationTime = new Label();
            lblTotalCycleTime = new Label();
            lblCycleID = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            pbrCalculationProgress = new ProgressBar();
            btnLoadData = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.Location = new Point(173, 240);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(264, 20);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "Ready";
            lblStatus.TextAlign = ContentAlignment.TopRight;
            // 
            // tmrMain
            // 
            tmrMain.Interval = 10;
            tmrMain.Tick += tmrMain_Tick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 240);
            label1.Name = "label1";
            label1.Size = new Size(121, 20);
            label1.TabIndex = 4;
            label1.Text = "Remaining time: ";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblElapsedTime
            // 
            lblElapsedTime.AutoSize = true;
            lblElapsedTime.Location = new Point(123, 240);
            lblElapsedTime.Name = "lblElapsedTime";
            lblElapsedTime.Size = new Size(44, 20);
            lblElapsedTime.TabIndex = 5;
            lblElapsedTime.Text = "00:00";
            lblElapsedTime.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tmrRemaining
            // 
            tmrRemaining.Interval = 50;
            tmrRemaining.Tick += tmrElapsed_Tick;
            // 
            // btnStartStop
            // 
            btnStartStop.Location = new Point(12, 173);
            btnStartStop.Name = "btnStartStop";
            btnStartStop.Size = new Size(425, 29);
            btnStartStop.TabIndex = 6;
            btnStartStop.Text = "Start";
            btnStartStop.UseVisualStyleBackColor = true;
            btnStartStop.Click += btnStartStop_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblCalculatedNumber);
            groupBox1.Controls.Add(lblNumberCalculationTime);
            groupBox1.Controls.Add(lblTotalCycleTime);
            groupBox1.Controls.Add(lblCycleID);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(12, 47);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(425, 120);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Last cycle";
            // 
            // lblCalculatedNumber
            // 
            lblCalculatedNumber.AutoSize = true;
            lblCalculatedNumber.Location = new Point(179, 83);
            lblCalculatedNumber.Name = "lblCalculatedNumber";
            lblCalculatedNumber.Size = new Size(17, 20);
            lblCalculatedNumber.TabIndex = 12;
            lblCalculatedNumber.Text = "0";
            // 
            // lblNumberCalculationTime
            // 
            lblNumberCalculationTime.AutoSize = true;
            lblNumberCalculationTime.Location = new Point(179, 63);
            lblNumberCalculationTime.Name = "lblNumberCalculationTime";
            lblNumberCalculationTime.Size = new Size(52, 20);
            lblNumberCalculationTime.TabIndex = 11;
            lblNumberCalculationTime.Text = "000ms";
            // 
            // lblTotalCycleTime
            // 
            lblTotalCycleTime.AutoSize = true;
            lblTotalCycleTime.Location = new Point(179, 43);
            lblTotalCycleTime.Name = "lblTotalCycleTime";
            lblTotalCycleTime.Size = new Size(23, 20);
            lblTotalCycleTime.TabIndex = 10;
            lblTotalCycleTime.Text = "0s";
            // 
            // lblCycleID
            // 
            lblCycleID.AutoSize = true;
            lblCycleID.Location = new Point(179, 23);
            lblCycleID.Name = "lblCycleID";
            lblCycleID.Size = new Size(17, 20);
            lblCycleID.TabIndex = 9;
            lblCycleID.Text = "0";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(45, 83);
            label5.Name = "label5";
            label5.Size = new Size(137, 20);
            label5.TabIndex = 3;
            label5.Text = "Calculated number:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 63);
            label4.Name = "label4";
            label4.Size = new Size(176, 20);
            label4.TabIndex = 2;
            label4.Text = "Number calculation time:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(66, 43);
            label3.Name = "label3";
            label3.Size = new Size(116, 20);
            label3.TabIndex = 1;
            label3.Text = "Total cycle time:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(116, 23);
            label2.Name = "label2";
            label2.Size = new Size(66, 20);
            label2.TabIndex = 0;
            label2.Text = "Cycle ID:";
            // 
            // pbrCalculationProgress
            // 
            pbrCalculationProgress.Location = new Point(12, 208);
            pbrCalculationProgress.Name = "pbrCalculationProgress";
            pbrCalculationProgress.Size = new Size(425, 29);
            pbrCalculationProgress.TabIndex = 8;
            // 
            // btnLoadData
            // 
            btnLoadData.Location = new Point(12, 12);
            btnLoadData.Name = "btnLoadData";
            btnLoadData.Size = new Size(94, 29);
            btnLoadData.TabIndex = 9;
            btnLoadData.Text = "Load XML";
            btnLoadData.UseVisualStyleBackColor = true;
            btnLoadData.Click += btnLoadData_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(449, 268);
            Controls.Add(btnLoadData);
            Controls.Add(pbrCalculationProgress);
            Controls.Add(groupBox1);
            Controls.Add(btnStartStop);
            Controls.Add(lblElapsedTime);
            Controls.Add(label1);
            Controls.Add(lblStatus);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Prime numbers finder";
            FormClosing += Form1_FormClosing;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblStatus;
        private System.Windows.Forms.Timer tmrMain;
        private Label label1;
        private Label lblElapsedTime;
        private System.Windows.Forms.Timer tmrRemaining;
        private Button btnStartStop;
        private GroupBox groupBox1;
        private ProgressBar pbrCalculationProgress;
        private Label label3;
        private Label label2;
        private Label label4;
        private Label lblTotalCycleTime;
        private Label lblCycleID;
        private Label label5;
        private Label lblCalculatedNumber;
        private Label lblNumberCalculationTime;
        private Button btnLoadData;
    }
}
