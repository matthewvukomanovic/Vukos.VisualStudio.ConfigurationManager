using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VukosConfigurationManager
{
    /// <summary>
    /// Interaction logic for ConfigurationWindow.xaml
    /// </summary>
    public partial class ConfigurationWindow : Window
    {
        public ConfigurationWindow()
        {
            InitializeComponent();
        }

        public void ListView_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void activeSolutionConfig_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void activeSolutionPlatform_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
