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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.f.C = new CharactersBlock();
            App.f.C = e.Parameter as CharactersBlock;
            if(App.f.C != null)
            {
                LoadPreviousDataToScreen(App.f.C);
            }
            //  DynamicText(Taxa);
        }

        public MainPage()
        {
            this.InitializeComponent();
            TaxaText = new List<TextBox>();
            
        }
        private void LoadPreviousDataToScreen(CharactersBlock charBlock)
        {
            var fields = charBlock.taxa.Count;
            for (var i = 0; i < fields; i++)
            {
                TextBox tb = new TextBox() { Text = charBlock.taxa[i], Name = "textBox" + i, Margin = new Thickness(0, 10, 0, 0), Width = 150, HorizontalAlignment = HorizontalAlignment.Center };
                TaxaText.Add(tb);
                BodyPanel.Children.Add(tb);
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            App.f.C = new CharactersBlock();
            List<string> errors = new List<string>();
            List<TextBox> tbErrors = new List<TextBox>();
            for (int i = 0; i < TaxaText.Count; i++)
            {
                TaxaText[i].Background = new SolidColorBrush(Colors.LightGray);
                if (string.IsNullOrEmpty(TaxaText[i].Text))
                {
                    tbErrors.Add(TaxaText[i]);
                    errors.Add("Empty Taxa value. Must enter Taxa or remove row.");
                    TaxaText[i].Background = new SolidColorBrush(Colors.LightSalmon);
                }
            }
                if (errors.Count == 0)
            {
                this.Frame.Navigate(typeof(CharactersPage), App.f.C);
            }
            else
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

        //private void btnMakeFields_Click(object sender, RoutedEventArgs e)
        //{
        //    var fields = Int32.Parse(textBox.Text.ToString()); //need to validate data to prevent exception.
        //    for (int j = 0; j < BodyPanel.Children.Count(); j++)
        //    {
        //        BodyPanel.Children.RemoveAt(j);
        //        TaxaText.RemoveAt(j);
        //    }
        //    for (var i = 0; i < fields; i++)
        //    {
        //        TextBox tb = new TextBox() { Text = "animal" + i, Name = "textBox" + i, Margin = new Thickness(0, 10, 0, 0), Width = 150, HorizontalAlignment = HorizontalAlignment.Center };
        //        TaxaText.Add(tb);
        //        BodyPanel.Children.Add(tb);
        //    }
        //}





        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            //remove the stackpanel that this button is located in
            //   sender.
            var dc = (sender as Button).Parent as StackPanel;
            for (int i = 0; i < TaxaText.Count; i++)
            {
                if (dc.Children.Contains(TaxaText[i]))
                {
                    TaxaText.Remove(TaxaText[i]);
                }
            }

           (dc.Parent as StackPanel).Children.Remove(dc);

            TaxaCount.Text = TaxaText.Count.ToString();
            //remove the textboxes from the list of textboxes a the tpo
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //add stackpanel with text boxes and remove button
            TextBox tbTaxa = new TextBox();
            tbTaxa.AcceptsReturn = true;
            Thickness m1 = tbTaxa.Margin;
            m1.Left = 10;
            tbTaxa.Margin = m1;
            tbTaxa.Width = 100;
            tbTaxa.Height = 30;
            Thickness m3 = tbTaxa.Padding;
            m3.Left = 200;
            m3.Bottom = 20;
            tbTaxa.FontSize = 14;
            // tbTaxa.Padding = 15;
            TaxaText.Add(tbTaxa);

            Button btnRemove = new Button();
            btnRemove.Content = "Remove";
            btnRemove.IsEnabled = true;
            btnRemove.HorizontalAlignment = HorizontalAlignment.Right;
            btnRemove.Click += btnRemove_Click;

            StackPanel s = new StackPanel();
            s.Orientation = Orientation.Horizontal;
            s.HorizontalAlignment = HorizontalAlignment.Center;
            s.Children.Add(tbTaxa);
            s.Children.Add(btnRemove);
            // s.Children.Add();
            //need to add remove button for each row then add button at the bottim
            BodyPanel.Children.Add(s);

            ScrollTaxa.Content = BodyPanel;
            TaxaCount.Text = TaxaText.Count.ToString();

        }





    }
}

