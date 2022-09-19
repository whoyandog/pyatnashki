using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace pyatnashki
{
    public class GameLogic
    {
        private Field field = new Field();
        private int counterComplete = 0;
        private int counterScores = 0;
        public bool isTimerActive = false;
        private bool isGameOver = false;
        DispatcherTimer liveTime = null;
        DateTime date = new DateTime(0, 0); 

        public GameLogic(Grid grid, int rows = 4, int columns = 4)
        {
            var eventHandler = EventHandler.GetInstance();
            eventHandler.onSendParametersToGameLogic += UpdateField;
            eventHandler.onSendTileToGameLogic += MoveTiles;
            eventHandler.onSetCounter += SetCounter;
            eventHandler.onAddScores += AddScores;

            InitializeTimer();
            field.CreateField(grid, rows, columns);
        }

        public void UpdateField(Grid grid, int rows, int columns)
        {
            ResetGame();
            field.UpdateField(grid, rows, columns);
        }

        public void MoveTiles(Tile tile)
        {
            if (isGameOver) return;

            if (!isTimerActive)
            {
                isTimerActive = true;
                liveTime.Tick += TimerTick;
            }

            field.Move(tile);
        }

        public void SetCounter(int number) // Почему сначала сохраняет результат, а потом прибавляет счетчик
        {
            counterComplete += number;

            if (counterComplete == field.GetRows() * field.GetColumns() - 1)
            {
                isTimerActive = false;
                isGameOver = true;
                liveTime.Tick -= TimerTick;

                FileHandler.SaveResults(FileHandler.ConvertToSave(date, field.GetRows(), field.GetColumns(), counterScores));

                Helpers.OpenResults();
            }
        }

        public void AddScores()
        {
            EventHandler.GetInstance().OnChangeScores(++counterScores);
        }

        private void ResetGame()
        {
            counterComplete = 0;
            counterScores = 0;
            isGameOver = false;
            date = new DateTime(0, 0);
            isTimerActive = false;
            liveTime.Tick -= TimerTick;
        }

        private void InitializeTimer()
        {
            liveTime = new DispatcherTimer();
            liveTime.Interval = TimeSpan.FromSeconds(1);
            liveTime.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            date = date.AddSeconds(1);
            EventHandler.GetInstance().OnChangeDate(date);
        }
    }
}
