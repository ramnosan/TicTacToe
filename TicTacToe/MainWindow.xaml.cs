using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Web;
using System.IO;
namespace TicTacToe
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        char x = 'x';
        char o = 'o';

        Player ai;
        Player ai2;

        Board board;

        Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();

            board = new Board();

            ai = new Player(false, 'o');
            ai2 = new Player(false, 'x');

            buttonList.Add(btn1);
            buttonList.Add(btn2);
            buttonList.Add(btn3);
            buttonList.Add(btn4);
            buttonList.Add(btn5);
            buttonList.Add(btn6);
            buttonList.Add(btn7);
            buttonList.Add(btn8);
            buttonList.Add(btn9);
                        
            startGame();
        }

        int counter = 0;
        public void startGame()
        {
            if(labelAi2IsHuman.IsChecked == true)
            {
                ai2.IsHuman = true;
            }
            else
            {
                ai2.IsHuman = false;
            }

            int rand = random.Next(0, 2);
            if (rand == 1)
            {
                board.MarksTurn = ai2.Marker;
                if (!ai2.IsHuman)
                {
                    ai2.generateRandomComputerMove(buttonList);
                }
            }
            else if(rand == 0)
            {
                board.MarksTurn = ai.Marker;
                ai.makeDecision(board, buttonList);
            }
                        
            btnRestart.Content = rand.ToString();
            labelWinner.Content = board.MarksTurn;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int row = 0;
            int column = 0;
            string name = button.Name;

            if (name == "btn1")
            {
                row = 0; column = 0;
            }
            else if (name == "btn2")
            {
                row = 0; column = 1;
            }
            else if (name == "btn3")
            {
                row = 0; column = 2;
            }


            else if (name == "btn4")
            {
                row = 1; column = 0;
            }
            else if (name == "btn5")
            {
                row = 1; column = 1;
            }
            else if (name == "btn6")
            {
                row = 1; column = 2;
            }


            else if (name == "btn7")
            {
                row = 2; column = 0;
            }
            else if (name == "btn8")
            {
                row = 2; column = 1;
            }
            else if (name == "btn9")
            {
                row = 2; column = 2;
            }

            board.submitMove(row, column, board.MarksTurn);
            
            button.IsEnabled = false;
            button.Content = board.MarksTurn;
            
            if (board.isWin())
            {
                labelWinner.Content = board.MarksTurn + " won the game!";
                if(ai.Marker == board.MarksTurn)
                {
                    ai.Wins = ai.Wins+1;
                }
                else if(ai2.Marker == board.MarksTurn)
                {
                    ai2.Wins = ai2.Wins + 1;
                }
                restart(); counter++;
            }
            else if (board.isCat())
            {
                labelWinner.Content = "It's a tie!";
                restart(); counter++;
            }
            else
            {
                board.changeMarksTurn();
                labelWinner.Content = board.MarksTurn;//Change Player who can submit a move

                if (ai2.Marker == board.MarksTurn)
                {
                    if (ai2.IsHuman == false)
                    {
                        /*if (ai.makeDecision(board, buttonList))
                        {
                            lblRandActionWasUsed.Content = "true";
                        }*///Für den Anfang disabled
                        ai2.generateRandomComputerMove(buttonList);
                    }
                }
                else if(ai.Marker == board.MarksTurn)///TODO____________________________________________________________________________!!!
                {
                    if (ai.IsHuman == false)
                    {
                       if (ai.makeDecision(board, buttonList))
                        {
                            lblRandActionWasUsed.Content = true;
                        }
                    }
                }
            }

            labelRandOrNot.Content = "ai2: " + ai2.Wins.ToString() + "\nai: " + ai.Wins.ToString();
        }

        List<Button> buttonList = new List<Button>();
        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            foreach (var button in buttonList)
            {
                button.IsEnabled = true;
                button.Content = "";
            }
            board = new Board();
            startGame();
        }

        private void restart()
        {
            foreach (var button in buttonList)
            {
                button.IsEnabled = true;
                button.Content = "";
            }
            board = new Board();
            startGame();
        }

        private void btnExchangeNN_Click(object sender, RoutedEventArgs e)
        {
            ai.nn = ai2.nn;
        }
    }
}
