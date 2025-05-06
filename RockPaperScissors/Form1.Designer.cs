namespace RockPaperScissors
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
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            button_Rock = new Button();
            button_Paper = new Button();
            button_Scissor = new Button();
            button_Restart = new Button();
            player_name = new Label();
            CPU = new Label();
            Score = new Label();
            R_score = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            txtMessage = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.qq;
            pictureBox1.Location = new Point(74, 70);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(200, 200);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.qq;
            pictureBox2.Location = new Point(639, 70);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(200, 200);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // button_Rock
            // 
            button_Rock.Location = new Point(918, 70);
            button_Rock.Name = "button_Rock";
            button_Rock.Size = new Size(94, 47);
            button_Rock.TabIndex = 2;
            button_Rock.Text = "Камень";
            button_Rock.UseVisualStyleBackColor = true;
            button_Rock.Click += button_Rock_Click;
            // 
            // button_Paper
            // 
            button_Paper.Location = new Point(918, 150);
            button_Paper.Name = "button_Paper";
            button_Paper.Size = new Size(94, 47);
            button_Paper.TabIndex = 3;
            button_Paper.Text = "Ножницы";
            button_Paper.UseVisualStyleBackColor = true;
            button_Paper.Click += button_Paper_Click;
            // 
            // button_Scissor
            // 
            button_Scissor.Location = new Point(918, 223);
            button_Scissor.Name = "button_Scissor";
            button_Scissor.Size = new Size(94, 47);
            button_Scissor.TabIndex = 4;
            button_Scissor.Text = "Бумага";
            button_Scissor.UseVisualStyleBackColor = true;
            button_Scissor.Click += button_Scissor_Click;
            // 
            // button_Restart
            // 
            button_Restart.Location = new Point(918, 352);
            button_Restart.Name = "button_Restart";
            button_Restart.Size = new Size(94, 47);
            button_Restart.TabIndex = 5;
            button_Restart.Text = "Restart";
            button_Restart.UseVisualStyleBackColor = true;
            button_Restart.Click += restartGame;
            // 
            // player_name
            // 
            player_name.AutoSize = true;
            player_name.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            player_name.Location = new Point(122, 28);
            player_name.Name = "player_name";
            player_name.Size = new Size(85, 29);
            player_name.TabIndex = 6;
            player_name.Text = "player";
            // 
            // CPU
            // 
            CPU.AutoSize = true;
            CPU.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            CPU.Location = new Point(704, 28);
            CPU.Name = "CPU";
            CPU.Size = new Size(66, 29);
            CPU.TabIndex = 7;
            CPU.Text = "CPU";
            // 
            // Score
            // 
            Score.AutoSize = true;
            Score.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            Score.Location = new Point(430, 188);
            Score.Name = "Score";
            Score.Size = new Size(27, 29);
            Score.TabIndex = 9;
            Score.Text = "5";
            // 
            // R_score
            // 
            R_score.AutoSize = true;
            R_score.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold);
            R_score.Location = new Point(384, 370);
            R_score.Name = "R_score";
            R_score.Size = new Size(130, 29);
            R_score.TabIndex = 11;
            R_score.Text = "Rounds: 3";
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // txtMessage
            // 
            txtMessage.AutoSize = true;
            txtMessage.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            txtMessage.Location = new Point(337, 124);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(219, 29);
            txtMessage.TabIndex = 12;
            txtMessage.Text = "Player: 0 - CPU: 0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1042, 445);
            Controls.Add(txtMessage);
            Controls.Add(R_score);
            Controls.Add(Score);
            Controls.Add(CPU);
            Controls.Add(player_name);
            Controls.Add(button_Restart);
            Controls.Add(button_Scissor);
            Controls.Add(button_Paper);
            Controls.Add(button_Rock);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Rock Paper Scissors";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button button_Rock;
        private Button button_Paper;
        private Button button_Scissor;
        private Button button_Restart;
        private Label player_name;
        private Label CPU;
        private Label Score;
        private Label R_score;
        private System.Windows.Forms.Timer timer1;
        private Label txtMessage;
    }
}
