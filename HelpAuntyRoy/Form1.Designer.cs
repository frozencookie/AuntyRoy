namespace HelpAuntyRoy
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.canvas = new System.Windows.Forms.PictureBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.ckb_randomDust = new System.Windows.Forms.CheckBox();
            this.lab1 = new System.Windows.Forms.Label();
            this.txtb_moveTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtb_probDust = new System.Windows.Forms.TextBox();
            this.ckb_hamster = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtb_hamsterShit = new System.Windows.Forms.TextBox();
            this.ckb_hansterView = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.Black;
            this.canvas.Location = new System.Drawing.Point(231, 12);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(800, 600);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(28, 38);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 29);
            this.btn_start.TabIndex = 1;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(127, 37);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(75, 31);
            this.btn_stop.TabIndex = 2;
            this.btn_stop.Text = "Stop";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // ckb_randomDust
            // 
            this.ckb_randomDust.AutoSize = true;
            this.ckb_randomDust.Location = new System.Drawing.Point(28, 91);
            this.ckb_randomDust.Name = "ckb_randomDust";
            this.ckb_randomDust.Size = new System.Drawing.Size(86, 21);
            this.ckb_randomDust.TabIndex = 3;
            this.ckb_randomDust.Text = "随机灰尘";
            this.ckb_randomDust.UseVisualStyleBackColor = true;
            // 
            // lab1
            // 
            this.lab1.AutoSize = true;
            this.lab1.Location = new System.Drawing.Point(28, 163);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(64, 17);
            this.lab1.TabIndex = 4;
            this.lab1.Text = "动画速度";
            // 
            // txtb_moveTime
            // 
            this.txtb_moveTime.Location = new System.Drawing.Point(100, 161);
            this.txtb_moveTime.Name = "txtb_moveTime";
            this.txtb_moveTime.Size = new System.Drawing.Size(57, 22);
            this.txtb_moveTime.TabIndex = 5;
            this.txtb_moveTime.Text = "1000";
            this.txtb_moveTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(164, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "ms";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "灰尘概率";
            // 
            // txtb_probDust
            // 
            this.txtb_probDust.Location = new System.Drawing.Point(100, 125);
            this.txtb_probDust.Name = "txtb_probDust";
            this.txtb_probDust.Size = new System.Drawing.Size(57, 22);
            this.txtb_probDust.TabIndex = 8;
            this.txtb_probDust.Text = "0.01";
            this.txtb_probDust.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ckb_hamster
            // 
            this.ckb_hamster.AutoSize = true;
            this.ckb_hamster.Location = new System.Drawing.Point(28, 213);
            this.ckb_hamster.Name = "ckb_hamster";
            this.ckb_hamster.Size = new System.Drawing.Size(72, 21);
            this.ckb_hamster.TabIndex = 9;
            this.ckb_hamster.Text = "仓鼠君";
            this.ckb_hamster.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 253);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "便便概率：";
            // 
            // txtb_hamsterShit
            // 
            this.txtb_hamsterShit.Location = new System.Drawing.Point(100, 252);
            this.txtb_hamsterShit.Name = "txtb_hamsterShit";
            this.txtb_hamsterShit.Size = new System.Drawing.Size(57, 22);
            this.txtb_hamsterShit.TabIndex = 11;
            this.txtb_hamsterShit.Text = "0.1";
            this.txtb_hamsterShit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ckb_hansterView
            // 
            this.ckb_hansterView.AutoSize = true;
            this.ckb_hansterView.Location = new System.Drawing.Point(28, 289);
            this.ckb_hansterView.Name = "ckb_hansterView";
            this.ckb_hansterView.Size = new System.Drawing.Size(86, 21);
            this.ckb_hansterView.TabIndex = 12;
            this.ckb_hansterView.Text = "仓鼠视角";
            this.ckb_hansterView.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 620);
            this.Controls.Add(this.ckb_hansterView);
            this.Controls.Add(this.txtb_hamsterShit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ckb_hamster);
            this.Controls.Add(this.txtb_probDust);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtb_moveTime);
            this.Controls.Add(this.lab1);
            this.Controls.Add(this.ckb_randomDust);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.canvas);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Aunty Roy\'s House";
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.CheckBox ckb_randomDust;
        private System.Windows.Forms.Label lab1;
        private System.Windows.Forms.TextBox txtb_moveTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtb_probDust;
        private System.Windows.Forms.CheckBox ckb_hamster;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtb_hamsterShit;
        private System.Windows.Forms.CheckBox ckb_hansterView;
    }
}

