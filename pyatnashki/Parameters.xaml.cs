using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    /// Логика взаимодействия для Parameters.xaml
    /// </summary>
    public partial class Parameters : Window
    {
        public Parameters()
        {
            InitializeComponent();
        }

        private void OnParametersAccept(object sender, RoutedEventArgs e)
        {

            EventHandler.GetInstance()
                        .OnSendParametersToMainWindow(
                        (int)sliderRows.Value,
                        (int)sliderColumns.Value);
            if (Helpers.IsWindowOpen<Results>())
                Helpers.GetWindow<Results>().Close();
            Close();
        }

        private void SliderRows_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = sender as Slider;
            textBlockSliderRows.Text = ((int)slider.Value).ToString();
        }

        private void SliderColumns_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = sender as Slider;
            textBlockSliderColumns.Text = ((int)slider.Value).ToString();
        }
    }
}
