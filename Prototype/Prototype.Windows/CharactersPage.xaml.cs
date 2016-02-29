using Shared_Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Prototype
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CharactersPage : Page
    {
        private List<string> stringErrors;
        private List<String> Taxa;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.f.C = new CharactersBlock();
            App.f.C = e.Parameter as CharactersBlock;
          //  DynamicText(Taxa);
        }
        public CharactersPage()
        {
            this.InitializeComponent();
            
            stringErrors = new List<string>();
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            //turn everything back to gray
            comboBox.Background = new SolidColorBrush(Colors.LightGray);
            charNum.Background = new SolidColorBrush(Colors.LightGray);
            GapChar.Background = new SolidColorBrush(Colors.LightGray);
            MissingChar.Background = new SolidColorBrush(Colors.LightGray);

            try
            {
                string x = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();
                App.f.C.dataSelection = (int)(CharactersBlock.InputDataType)(Enum.Parse(typeof(CharactersBlock.InputDataType), x));

            }
            catch (Exception)
            {

                stringErrors.Add("Must choose a data type to create a Nexus file");
                comboBox.Background = new SolidColorBrush(Colors.LightSalmon);


            }            
            int nCharNum;
          // var dataSelect = (App.f.C.Inpj) Enum.Parse(typeof(App.f.C.InputDataType), x);
            try {
                 nCharNum = Int32.Parse(charNum.Text);
            }
            catch(Exception)
            {
                nCharNum = 0;
                stringErrors.Add("Must enter an integer into the number of characters per matrix field.");
                charNum.Background = new SolidColorBrush(Colors.LightSalmon);
            }
            if (GapChar.Text.Length == 0 || GapChar.Text.Length>1)
            {
                stringErrors.Add("Must enter one character into the GAP character field.");
                GapChar.Background = new SolidColorBrush(Colors.LightSalmon);
            }
            if (MissingChar.Text.Length == 0 || MissingChar.Text.Length > 1)
            {
                stringErrors.Add("Must enter one character into the MISSING character field.");
                MissingChar.Background = new SolidColorBrush(Colors.LightSalmon);
            }
            if(stringErrors.Count ==0)
            {
                App.f.C.gapChar =GapChar.Text[0];
                App.f.C.missingChar = MissingChar.Text[0];
                App.f.C.ncharValue = nCharNum;
                this.Frame.Navigate(typeof(SequenceDataInput), App.f.C);
            }
            else
            {
                //report errors
                TextBox ErrorText = new TextBox();
                ErrorText.Name = "errors";
                ErrorText.Width = 300;
                ErrorText.Height = 300;
                ErrorText.FontSize = 12;
                ErrorText.TextWrapping = TextWrapping.Wrap;
                ErrorText.Background = new SolidColorBrush(Colors.Gainsboro);
                ErrorText.Text = "The following " + stringErrors.Count + " errors must be fixed before you can continue: " + System.Environment.NewLine;
                for (int i = 0; i < stringErrors.Count; i++)
                {
                    ErrorText.Text += stringErrors[i] + System.Environment.NewLine;
                }
                StackPanel s = new StackPanel();
                s.Orientation = Orientation.Horizontal;
                s.HorizontalAlignment = HorizontalAlignment.Center;
                s.Children.Add(ErrorText);
                ErrorScroll.Content = s;
            }
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {//Goes back to Taxa Page
            this.Frame.Navigate(typeof(MainPage), App.f.C);
        }
    }
}
