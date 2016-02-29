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
using Windows.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Prototype
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<TextBox> TaxaText;

        public MainPage()
        {
            this.InitializeComponent();
            TaxaText = new List<TextBox>();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            App.f.C = new CharactersBlock();
            List<string> errors = new List<string>();
            String s = textBox.Text.ToString();
            String[] array = s.Split(new char[] { ' ' });
            foreach (object child in BodyPanel.Children)
            {
                TextBox box = (TextBox)child; // we should tyoe check this
                if (box.Text.Length == 0)
                {
                    errors.Add("Must enter taxa or adjust the number of fields");
                    box.Background = new SolidColorBrush(Colors.LightSalmon);
                }
                else
                {
                    App.f.C.taxa.Add(box.Text.ToString());
                }

            }
            if (errors.Count == 0)
            {
                this.Frame.Navigate(typeof(CharactersPage), App.f.C);
            }
            else
            {
            
            if (errors.Count > 0)
            {
                TextBox ErrorText = new TextBox();
                ErrorText.Name = "errors";
                ErrorText.Width = 300;
                ErrorText.Height = 300;
                ErrorText.FontSize = 12;
                ErrorText.TextWrapping = TextWrapping.Wrap;
                ErrorText.Background = new SolidColorBrush(Colors.Gainsboro);
                ErrorText.Text = "The following " + errors.Count + " errors must be fixed before you can continue: " + System.Environment.NewLine;
                for (int i = 0; i < errors.Count; i++)
                {
                    ErrorText.Text += errors[i] + System.Environment.NewLine;
                }
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.HorizontalAlignment = HorizontalAlignment.Center;
                sp.Children.Add(ErrorText);
                ScrollError.Content = sp;
            }
        }
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var fields = Int32.Parse(textBox.Text.ToString()); //need to validate data to prevent exception.
            for (int j = 0; j < BodyPanel.Children.Count(); j++)
            {
                BodyPanel.Children.RemoveAt(j);
                TaxaText.RemoveAt(j);
            }
            for (var i = 0; i < fields; i++)
            {
                TextBox tb = new TextBox() { Text = "animal" + i, Name = "textBox" + i, Margin = new Thickness(0, 10, 0, 0), Width = 150, HorizontalAlignment = HorizontalAlignment.Center };
                TaxaText.Add(tb);
                BodyPanel.Children.Add(tb);
            }
        }
    }
}

