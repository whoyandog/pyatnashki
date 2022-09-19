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
using System.Windows.Threading;

namespace pyatnashki
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            EventHandler eventHandler = EventHandler.GetInstance();
            eventHandler.onSendParametersToMainWindow += UpdateField;
            eventHandler.onSetCounter += GetCounter;
            eventHandler.onChangeDate += ChangeDate;
            eventHandler.onChangeScores += ChangeScores;

            int r = 4, c = 4;

            ResetTextData();
            SetCounter(r, c);
            GameLogic gameLogic = new GameLogic(mainGrid, r, c);

            this.MinWidth = 256;
            this.MinHeight = 256;
        }

        private void SetCounter(int row, int columns, int number = 0)
        {
            textBlockComplete.Text = number + " / " + (row * columns - 1);
        }

        private void ResetTextData()
        {
            textBlockTime.Text = DateTime.MinValue.ToString("HH:mm:ss");
            textBlockScores.Text = 0.ToString();
        }

        private void UpdateField(int rows, int columns)
        {
            ResetTextData();
            SetCounter(rows, columns);
            EventHandler.GetInstance().OnSendParametersToGameLogic(mainGrid, rows, columns);
        }

        private void OnOpenParameters(object sender, RoutedEventArgs e)
        {
            Helpers.OpenParameters();
        }

        private void OnOpenResults(object sender, RoutedEventArgs e)
        {
            Helpers.OpenResults();
        }

        private void OnCloseApp(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnUpdateField(object sender, RoutedEventArgs e)
        {
            ResetTextData();
            SetCounter(mainGrid.RowDefinitions.Count, mainGrid.ColumnDefinitions.Count);
            EventHandler.GetInstance().OnSendParametersToGameLogic(mainGrid, 0, 0);
        }

        public void GetCounter(int number = 0)
        {
            string substring = textBlockComplete.Text.Substring(0, textBlockComplete.Text.IndexOf(" / "));
            SetCounter(mainGrid.RowDefinitions.Count, mainGrid.ColumnDefinitions.Count, Convert.ToInt32(substring) + number);
        }

        public void ChangeDate(DateTime date)
        {
            textBlockTime.Text = date.ToString("HH:mm:ss");
        }

        public void ChangeScores(int scores)
        {
            textBlockScores.Text = scores.ToString();
        }
    }
}
