namespace RockPaperScissors
{
    public partial class Form1 : Form
    {

        int rounds = 3;
        int timerPerRound = 6;

        bool gameover = false;

        string[] CPUchoiceList = { "rock", "paper", "scissor", "paper", "scissor", "rock" };

        int randomNumber = 0;

        Random rnd = new Random();

        string CPUchoice;

        string playerChoice;

        int playerwins;
        int CPUwins;


        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;
            playerChoice = "none";
            Score.Text = "5";
        }

        private void button_Rock_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.rock;
            playerChoice = "rock";
        }

        private void button_Paper_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.paper;
            playerChoice = "paper";
        }

        private void button_Scissor_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.scissors;
            playerChoice = "scissor";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timerPerRound -= 1;

            Score.Text = timerPerRound.ToString();
            R_score.Text = "Rounds: " + rounds;

            if (timerPerRound < 1)
            {
                timer1.Enabled = false;
                timerPerRound = 6;

                randomNumber = rnd.Next(0, CPUchoiceList.Length);

                CPUchoice = CPUchoiceList[randomNumber];

                switch (CPUchoice)
                {
                    case "rock":
                        pictureBox2.Image = Properties.Resources.rock;
                        break;
                    case "paper":
                        pictureBox2.Image = Properties.Resources.paper;
                        break;
                    case "scissor":
                        pictureBox2.Image = Properties.Resources.scissors;
                        break;
                }


                if (rounds > 0)
                {
                    checkGame();
                }
                else
                {
                    if (playerwins > CPUwins)
                    {
                        MessageBox.Show("Игрок выигрывает!");
                    }
                    else
                    {
                        MessageBox.Show("CPU выигрывает");
                    }

                    gameover = true;
                }


            }
        }


        private void checkGame()
        {

            // AI and player win rules

            if (playerChoice == "rock" && CPUchoice == "paper")
            {

                CPUwins += 1;

                rounds -= 1;

                MessageBox.Show("CPU выигрывает, Бумага покрывает камни");

            }
            else if (playerChoice == "scissor" && CPUchoice == "rock")
            {
                CPUwins += 1;

                rounds -= 1;

                MessageBox.Show("CPU выигрывает, Камень ломает ножницы");
            }
            else if (playerChoice == "paper" && CPUchoice == "scissor")
            {

                CPUwins += 1;

                rounds -= 1;

                MessageBox.Show("CPU выигрывает, Ножницы режут бумагу");

            }
            else if (playerChoice == "rock" && CPUchoice == "scissor")
            {

                playerwins += 1;

                rounds -= 1;

                MessageBox.Show("Игрок выигрывает, Камень ломает ножницы");

            }
            else if (playerChoice == "paper" && CPUchoice == "rock")
            {

                playerwins += 1;

                rounds -= 1;

                MessageBox.Show("Игрок выигрывает, Бумага покрывает камни");

            }
            else if (playerChoice == "scissor" && CPUchoice == "paper")
            {
                playerwins += 1;

                rounds -= 1;

                MessageBox.Show("Игрок выигрывает, Ножницы режут бумагу");

            }
            else if (playerChoice == "none")
            {
                MessageBox.Show("Сделай выбор!");
            }
            else
            {
                MessageBox.Show("Ничья");

            }
            startNextRound();
        }

        public void startNextRound()
        {

            if (gameover)
            {
                return;
            }

            txtMessage.Text = "Player: " + playerwins + " - " + "CPU: " + CPUwins;

            playerChoice = "none";

            timer1.Enabled = true;

            pictureBox1.Image = Properties.Resources.qq;
            pictureBox2.Image = Properties.Resources.qq;
        }

        private void restartGame(object sender, EventArgs e)
        {
            playerwins = 0;
            CPUwins = 0;
            rounds = 3;
            txtMessage.Text = "Player: " + playerwins + " - " + "CPU: " + CPUwins;

            playerChoice = "none";
            Score.Text = "5";

            timer1.Enabled = true;

            pictureBox1.Image = Properties.Resources.qq;
            pictureBox2.Image = Properties.Resources.qq;

            gameover = false;
        }
    }
}
