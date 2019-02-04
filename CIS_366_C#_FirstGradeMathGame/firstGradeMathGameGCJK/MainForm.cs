using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * First Grade Math Game
 * Garrison Castor & Jhansi Kesireddy
 */

namespace firstGradeMathGameGCJK
{
    public partial class firstGradeMathGameForm : Form
    {
        //Running Totals/Round Count
        
        private double totalCorrect = 0.0;
        private int currentRound = 0;

        //Field Level User Inputed Data
        private string name = "";
        private int highestNum = 0;
        private int rounds = 0;


        public firstGradeMathGameForm()
        {
            InitializeComponent();
        }

        private void firstGradeMathGameForm_Load(object sender, EventArgs e)
        {
            //Create an instance of the IntroForm class
            IntroForm myIntroForm = new IntroForm();

            //Dsiplay the form
            myIntroForm.ShowDialog();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            //Start of Game
                        
            //Input Validation and recording information for the game
            if (nameTextBox.Text != "")
            {
                if (int.TryParse(roundsTextBox.Text, out rounds))
                {
                    if (int.TryParse(highestTextBox.Text, out highestNum))
                    {

                        //capture name
                        name = nameTextBox.Text;
                        
                        //reveal the playable game, hide start game info
                        startMenuGroupBox.Visible = false;
                        activeGameGroupBox.Visible = true;


                        //display first round's numbers
                        randDisplay();


                        //start the round counter
                        roundLabel.Text = "Round " + currentRound.ToString();
                        currentRound++;
                    }
                    else
                    {
                        MessageBox.Show("Please enter the highest number that can appear.");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter how many rounds the student will play.");
                }
            }
            else
            {
                MessageBox.Show("Please enter the student's name.");
            }
        
        }//end of Start Game button

        private void answerButton_Click(object sender, EventArgs e)
        {
           
            //round counter
            roundLabel.Text = "Round " + currentRound.ToString();           

            if (currentRound < rounds)
            {
                //check answer then change display
                checkAnswer();

                randDisplay();

                //advance the round counter
                currentRound++;

            }
            else
            {
                //check last round's answer
                checkAnswer();

                //hide the game
                activeGameGroupBox.Visible = false;
             
                MessageBox.Show("Game Over. " + name + "'s Total Correct: " + totalCorrect + " out of " + rounds);

                //Create an instance of the GameOverForm class
                GameOverForm myGameOverForm = new GameOverForm();

                //display the form
                myGameOverForm.ShowDialog();

                //close the game
                this.Close();
            }                     

        }//end of Answer button            

        private void randDisplay()
        {
            int num1 = 0;
            int num2 = 0;

            //create rand object and assign to numbers
            Random rand = new Random();


            //Assign random numbers to variables
            num1 = rand.Next(highestNum + 1);
            num2 = rand.Next(highestNum + 1);

            //put numbers into labels
            randNumLabel1.Text = num1.ToString();
            randNumLabel2.Text = num2.ToString();
            
        }//end randDisplay

       private void checkAnswer()
        {
            int num1 = 0;
            int num2 = 0;
            int answer;
            int studentAnswer;


            //fetch random numbers from labels
            int.TryParse(randNumLabel1.Text, out num1);
            int.TryParse(randNumLabel2.Text, out num2);

            //calculate the random answer
            answer = num1 + num2;
            

            //Get Student Answer
            int.TryParse(studentAnswerTextBox.Text, out studentAnswer);


            //Interpret student answer
            if(studentAnswer == answer)
            {               
                //Create an instance of the CorrectForm class
                CorrectForm myCorrectForm = new CorrectForm();

                //Display the form
                myCorrectForm.ShowDialog();

                totalCorrect++;
            }
            else
            {
                MessageBox.Show("Incorrect, the correct answer was " + answer);
            }        
        }
    }
}
