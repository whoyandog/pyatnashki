using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace pyatnashki
{
    internal class EventHandler
    {
        private static EventHandler instance;

        public static EventHandler GetInstance()
        {
            if (instance == null)   
                instance = new EventHandler();

            return instance;
        }

        public delegate void EventSendParametersToMainWindow(int rows, int columns);
        public delegate void EventSendParametersToGameLogic(Grid grid, int rows, int columns);
        public delegate void EventSendTileToGameLogic(Tile tile);
        public delegate int EventGetFieldWidth();
        public delegate void EventSetCounter(int number);
        public delegate void EventAddScores();
        public delegate void EventChangeDate(DateTime date);
        public delegate void EventChangeScores(int scores);
        public delegate void EventLoadResults(List<DataResult> results);
        public event EventSendParametersToMainWindow onSendParametersToMainWindow;
        public event EventSendParametersToGameLogic onSendParametersToGameLogic;
        public event EventSendTileToGameLogic onSendTileToGameLogic;
        public event EventGetFieldWidth onGetFieldWidth;
        public event EventSetCounter onSetCounter;
        public event EventAddScores onAddScores;
        public event EventChangeDate onChangeDate;
        public event EventChangeScores onChangeScores;
        public event EventLoadResults onLoadResults;

        public void OnSendParametersToMainWindow (int rows, int columns)
        {
            if (onSendParametersToMainWindow != null)
                onSendParametersToMainWindow(rows, columns);
        }

        public void OnSendParametersToGameLogic (Grid grid, int rows, int columns)
        {
            if (onSendParametersToGameLogic != null)
                onSendParametersToGameLogic(grid, rows, columns);
        }

        public void OnSendTileToGameLogic (Tile tile)
        {
            if (onSendTileToGameLogic != null)
                onSendTileToGameLogic(tile);
        }

        public int OnGetFieldWidth ()
        {
            if (onGetFieldWidth != null) 
                return onGetFieldWidth();

            return 0;
        }

        public void OnSetCounter(int number)
        {
            if (onSetCounter != null)
                onSetCounter(number);
        }

        public void OnAddScores()
        {
            if (onAddScores != null)
                onAddScores();
        }

        public void OnChangeDate(DateTime date)
        {
            if (onChangeDate != null)
                onChangeDate(date);
        }

        public void OnChangeScores(int scores)
        {
            if (onChangeScores != null) 
                onChangeScores(scores);
        }

        public void OnLoadResults(List<DataResult> dataResults)
        {
            if (onLoadResults != null)
                onLoadResults(dataResults);
        }
    }
}
