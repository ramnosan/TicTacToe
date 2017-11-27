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

        //Player p1;
        //Player p2;
        Player ai;
        Player ai2;

        Board board;

        Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();

            board = new Board();

            //Test vvvvv
            ai = new Player(false, 'o');
            //ai.makeDecision(board, buttonList);
            //Test ^^^^^
            ai2 = new Player(false, 'x');
            //p1 = new Player(false, x);
           // p2 = new Player(false, o);

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
            //board = new Board();  
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
                /*board.MarksTurn = p2.Marker;
                if (!p2.IsHuman)
                {
                    p2.generateRandomComputerMove(board, buttonList);
                }*/
                board.MarksTurn = ai.Marker;
                if (ai2.IsHuman == true)
                {
                    ai.makeDecision(board, buttonList);
                }
                else
                ai.RandMove(board, buttonList);
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
            if(counter == 20000)
            {
                ai2.IsHuman = true;
            }

            board.submitMove(row, column, board.MarksTurn);
            button.IsEnabled = false;
            button.Content = board.MarksTurn;
            
            if (board.isWin())
            {
                labelWinner.Content = board.MarksTurn + " won the game!";
                if(ai.Marker == board.MarksTurn)
                {
                    //ai.transverReward(100, board);
                    //ai2.transverReward(-100, board);
                    ai.Wins = ai.Wins+1;
                    for (int i = 0; i < ai.memory.Count; i++)
                    {
                        ai.trainNN();
                    }
                    ai.memory.Clear();
                }
                else if(ai2.Marker == board.MarksTurn)
                {
                    //ai.transverReward(-1000, board);
                    //ai2.transverReward(100, board);
                    for (int i = 0; i < ai2.memory.Count; i++)
                    {
                        ai2.trainNN();
                    }
                    ai2.memory.Clear();
                    ai2.Wins = ai2.Wins + 1;
                    
                }
                restart(); counter++; ai.memory.Clear();
            }
            else if (board.isCat())
            {
                labelWinner.Content = "It's a tie!";
                
                //ai.transverReward(5, board);
                //ai2.transverReward(5, board);
                restart(); counter++;
                ai.memory.Clear();
            }
            else
            {
                board.changeMarksTurn();
                labelWinner.Content = board.MarksTurn;

                if (ai2.IsHuman == false && ai2.Marker == board.MarksTurn)
                {
                    //ai2.BoardForComp = board;

                    ai2.generateRandomComputerMove(buttonList);
                }
                /*else if (p2.IsHuman == false && p2.Marker == board.MarksTurn)
                {
                    //p2.BoardForComp = board;
                    p2.generateRandomComputerMove(board, buttonList);
                }*/
                else if(ai.Marker == board.MarksTurn)///TODO____________________________________________________________________________!!!
                {
                    if (ai2.IsHuman == true)
                    {
                        ai.makeDecision(board, buttonList);
                    }
                    else
                        ai.RandMove(board, buttonList);
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
            //ai.Time = 0;
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
            //ai.Time = 0;
            //ai2.Time = 0;
            startGame();
        }

        private void btnExchangeNN_Click(object sender, RoutedEventArgs e)
        {
            ai.nn = ai2.nn;
        }
    }
}
