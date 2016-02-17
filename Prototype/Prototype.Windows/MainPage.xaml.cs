using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Shared_Code;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Prototype
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            App.f.T = new TaxaBlock();
            String s = textBox.Text.ToString();
            String[] array = s.Split(new char[] { ' ' });
            foreach (object child in panel.Children) {
                TextBox box = (TextBox)child; // we should tyoe check this
                App.f.T.taxa.Add(box.Text.ToString());
            }
            this.Frame.Navigate(typeof(Page2));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var fields = Int32.Parse(textBox.Text.ToString()); //need to validate data to prevent exception.
            for (var i = 0; i < fields; i++) {
                panel.Children.Add(new TextBox() { Text = "animal" + i, Name = "textBox" + i, Margin = new Thickness(0, 10, 0, 0) });
            }
        }
    }
}
