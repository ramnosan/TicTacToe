using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;

namespace TicTacToe
{
    public class ReinforcementLearningAI : Player
    {
        public ReinforcementLearningAI(bool isHuman, char marker) : base(isHuman, marker)
        {
            this.IsHuman = isHuman;
            this.Marker = marker;

        }
        //private float[] aktionen = new float[9];
        private float[,] qMatrix = new float[500000, 9];
        

        float gamma = 0.9f;
        float alpha = 1f;
        

        private List<char[,]> zuständeListe = new List<char[,]>();//IMPORTANT
        private List<float> rewardList = new List<float>();
        
                
        int time = 0;
        public int Time { get { return this.time; } set { this.time = value; } }

        Random rand = new Random();

        public void makeDecision(Board _board, List<Button> buttonList)
        {
            int bestAction = -1;
            int nextAction = 0;
            int currentState;

            if ((currentState = zuständeListe.IndexOf(_board.GameBoard)) == -1)
            {
                zuständeListe.Add(_board.GameBoard);
                rewardList.Add(0);
                currentState = zuständeListe.Count - 1;
            }

            bestAction = getActionWithHighestQValueForCurrentState(/*TODO*/2, buttonList);
            nextAction = bestAction;
            
                        
            qMatrix[currentState, nextAction] += alpha * ((computeQ(nextAction, currentState, _board) 
                - qMatrix[currentState, nextAction]) - 0.5f);

            performAction(bestAction, buttonList);
        }

        private float computeQ(int nextAction, int currentState, Board _board)
        {
            int nextState = getStateFromAction(nextAction, currentState, _board);

            return (rewardList[getStateFromAction(nextAction, currentState, _board)] +
                gamma * getHighestQValueForStateInQMatrix(nextState));
        }

        private float getHighestQValueForStateInQMatrix(int state)
        {
            float helper = qMatrix[state, 0];

            for (int i = 1; i < 5; i++)
            {
                if (helper < qMatrix[state, i])
                {
                    helper = qMatrix[state, i];
                }
            }
            return helper;
        }

        private int getStateFromAction(int action, int currentState, Board _board)
        {
            Board nextBoard = new Board(_board);
            nextBoard.submitMove(action, Marker);
            int nextState = zuständeListe.IndexOf(nextBoard.GameBoard);
            if (nextState == -1)
            {
                zuständeListe.Add(nextBoard.GameBoard);
                rewardList.Add(0);
                return zuständeListe.Count - 1;
            }
            else
                return nextState;
        }

        private bool performAction(int bestAction, List<Button> buttonList)
        {
            ButtonAutomationPeer peer =
            new ButtonAutomationPeer(buttonList[bestAction]);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
            return true;
        }

        private int getActionWithHighestQValueForCurrentState(int currentState, List<Button> buttonList)
        {
            List<int> possibleActions = new List<int>();
            foreach (var button in buttonList)
            {
                if (button.IsEnabled)
                {
                    int action;
                    //Int32.TryParse(button.Name[3], out action);
                    action =(int) Char.GetNumericValue(button.Name[3])-1; 
                    possibleActions.Add(action);
                }
            }
                        
            List<float> listWithQValuesOfPossibleActions = new List<float>();
            foreach (int item in possibleActions)
            {
                listWithQValuesOfPossibleActions.Add(qMatrix[currentState, item]);
            }

            float maxValue = listWithQValuesOfPossibleActions.Max();
            List<int> bestValuedActions = new List<int>();
            for (int i = 0; i < possibleActions.Count; i++)
            {
                if (qMatrix[currentState, possibleActions[i]].Equals(maxValue))
                {
                    bestValuedActions.Add(possibleActions[i]);
                }
            }

            return bestValuedActions[rand.Next(0, bestValuedActions.Count)];
        }

        public void transverReward(float reward, Board _board)
        {
            int index = 0;
            if ((index = zuständeListe.IndexOf(_board.GameBoard)) != -1)
            {
                rewardList[index] = reward;
            }
            else if(index == -1)
            {
                zuständeListe.Add(_board.GameBoard);
                rewardList.Add(reward);
            }
        }
    }
}
