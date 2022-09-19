using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace pyatnashki
{
    public class Tile : IButtonView, IMovable
    {
        // button
        public Button button { get; set; }

        private bool isOnCorrectPosition = false;

        public Tile (int row, int column, string name)
        {
            InitializeButton(row, column, name);
        }

        public void InitializeButton(int row, int column, string name)
        {
            button = new Button();
            button.Content = name;
            button.FontSize = 36;
            button.FontWeight = FontWeights.Bold;
            button.HorizontalAlignment = HorizontalAlignment.Stretch;
            button.VerticalAlignment = VerticalAlignment.Stretch;
            Move(row, column);


            // CLICK TESTS
            button.Click += new RoutedEventHandler(TileClick);
        }

        private void TileClick (object sender, RoutedEventArgs e)
        {
            EventHandler.GetInstance().OnSendTileToGameLogic(this);
        }

        public void Move(int row, int column)
        {
            Grid.SetRow(button, row);
            Grid.SetColumn(button, column);

            CheckCorrectPosition();
        }

        private void CheckCorrectPosition()
        {
            if (Convert.ToInt32(button.Content) == (Grid.GetRow(button) * EventHandler.GetInstance().OnGetFieldWidth() + Grid.GetColumn(button) + 1))
            {
                if (!isOnCorrectPosition)
                {
                    isOnCorrectPosition = true;
                    EventHandler.GetInstance().OnSetCounter(1);
                    
                    return;
                }
            }

            if (isOnCorrectPosition)
            {
                isOnCorrectPosition = false;
                EventHandler.GetInstance().OnSetCounter(-1);
            }
        }
    }
}
