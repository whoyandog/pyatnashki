using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace pyatnashki
{
    /// <summary>
    /// Логика взаимодействия для Results.xaml
    /// </summary>
    public partial class Results : Window
    {
        public Results()
        {
            InitializeComponent();
            EventHandler.GetInstance().onLoadResults += LoadResults;

            FileHandler.ReadResults();
        }

        private void OnClickParameters(object sender, RoutedEventArgs e)
        {
            Helpers.OpenParameters();
        }

        private void OnClickRestart(object sender, RoutedEventArgs e)
        {
            EventHandler.GetInstance().OnSendParametersToMainWindow(0, 0);
            Close();
        }

        private void OnClickClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void LoadResults(List<DataResult> listResults)
        {
            for (int i = listResults.Count - 1; i >= 0; i--)
            {
                dataGridResults.Items.Add(listResults[i]);
            }
        }
    }
}
