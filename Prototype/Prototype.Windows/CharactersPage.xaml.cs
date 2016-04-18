using Shared_Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
        private TextBox ErrorText;
        private StackPanel s;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.f.C = new CharactersBlock();
            App.f.C = e.Parameter as CharactersBlock;
            if (!string.IsNullOrEmpty(App.f.C.ncharValue.ToString()) || !string.IsNullOrEmpty(App.f.C.missingChar.ToString()) || !string.IsNullOrEmpty(App.f.C.gapChar.ToString()))
                {
                LoadPreviousDataToScreen(App.f.C);
            }
        }
        public CharactersPage()
        {
            this.InitializeComponent();
            //initialize lostfocus events
            comboBox.LostFocus += new RoutedEventHandler(ValidateDataType_Click);
            charNum.LostFocus += new RoutedEventHandler(ValidateCharNum_Click);
            GapChar.LostFocus += new RoutedEventHandler(ValidateGapChar_Click);
            MissingChar.LostFocus += new RoutedEventHandler(ValidateMissingChar_Click);
        

            stringErrors = new List<string>();
            ErrorText = new TextBox();
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
            s = new StackPanel();
            s.Orientation = Orientation.Horizontal;
            s.HorizontalAlignment = HorizontalAlignment.Center;
            s.Children.Add(ErrorText);
            ErrorScroll.Content = s;
            ErrorText.Visibility = Visibility.Collapsed;
        }


        private void LoadPreviousDataToScreen(CharactersBlock charBlock)
        {
            // ((ComboBoxItem)comboBox.SelectedItem).SetValue(App.f.C.dataSelection);
            if (!string.IsNullOrEmpty(App.f.C.ncharValue.ToString()))
            {
                charNum.Text = App.f.C.ncharValue.ToString();
            }
            if (!string.IsNullOrEmpty(App.f.C.missingChar.ToString()))
            {
                MissingChar.Text = App.f.C.missingChar.ToString();
            }
            if (!string.IsNullOrEmpty(App.f.C.gapChar.ToString()))
            {
                GapChar.Text = App.f.C.gapChar.ToString();
            }

           
        }
        private void EnableErrorScroll()
        {
            ErrorText.Visibility = Visibility.Visible;
            ErrorText.Text = "The following " + stringErrors.Count + " errors must be fixed before you can continue: " + System.Environment.NewLine;


            for (int i = 0; i < stringErrors.Count; i++)
            {
                ErrorText.Text += stringErrors[i] + System.Environment.NewLine;
            }

        }
        
        private void ValidateDataType_Click(object sender, RoutedEventArgs e)
        {
            //turn everything back to gray
            comboBox.Background = new SolidColorBrush(Colors.LightGray);
           if(stringErrors.Contains("Must choose a data type to create a Nexus file"))
            {
                stringErrors.Remove("Must choose a data type to create a Nexus file");
            }
            try
            {
                string x = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();
                App.f.C.dataSelection = (int)(CharactersBlock.InputDataType)(Enum.Parse(typeof(CharactersBlock.InputDataType), x));

            }
            catch (Exception ex)
            {

                stringErrors.Add("Must choose a data type to create a Nexus file");
                comboBox.Background = new SolidColorBrush(Colors.LightSalmon);
            }
            if (stringErrors.Count > 0)
            {
                EnableErrorScroll();
            }
            else
            {
                ErrorText.Visibility = Visibility.Collapsed;
                ErrorText.Text = "";
                comboBox.Background = new SolidColorBrush(Colors.LightGray);
            }
        }
        private void ValidateCharNum_Click(object sender, RoutedEventArgs e)
        {
            int nCharNum;
            charNum.Background = new SolidColorBrush(Colors.LightGray);
            if (stringErrors.Contains("Must enter a positive integer into the number of characters per matrix field."))
            {
                stringErrors.Remove("Must enter a positive integer into the number of characters per matrix field.");
            }
            try
            {
                nCharNum = Int32.Parse(charNum.Text);
                if (nCharNum<=0)
                {
                    charNum.Background = new SolidColorBrush(Colors.LightSalmon);
                    stringErrors.Add("Must enter a positive integer into the number of characters per matrix field.");
                }
            }
            catch (Exception)
            {
                nCharNum = 0;
                stringErrors.Add("Must enter a positive integer into the number of characters per matrix field.");
                charNum.Background = new SolidColorBrush(Colors.LightSalmon);
            }
            if (stringErrors.Count > 0)
            {
                EnableErrorScroll();
            }
            else
            {
                ErrorText.Visibility = Visibility.Collapsed;
                ErrorText.Text = "";
                App.f.C.ncharValue = nCharNum;
                charNum.Background = new SolidColorBrush(Colors.LightGray);
            }
        }
        private void ValidateGapChar_Click(object sender, RoutedEventArgs e)
        {//()[]{} / \ , ; : = * '  ` < > ^
            GapChar.Background = new SolidColorBrush(Colors.LightGray);
            List<int> illegal = new List<int> {32, 34, 39, 40, 41, 42, 44, 47, 58, 59, 60, 62, 61, 94, 96, 91, 92, 93, 123, 125, 127 };
            if (stringErrors.Contains("Must enter one character into the GAP character field."))
            {
                //remove
                stringErrors.Remove("Must enter one character into the GAP character field.");
            }
            else if (stringErrors.Contains("Illegal GAP character. Cannot use the following characters: ()[]{} / , ; : = * '  ` < > ^"))
            {
                //remove
                stringErrors.Remove("Illegal GAP character. Cannot use the following characters: ()[]{} / , ; : = * '  ` < > ^");
            }

            if (GapChar.Text.Length == 0 || GapChar.Text.Length > 1)
            {
                stringErrors.Add("Must enter one character into the GAP character field.");
                GapChar.Background = new SolidColorBrush(Colors.LightSalmon);
            }
            else
                {
                    byte[] asciiBytes = Encoding.UTF8.GetBytes(GapChar.Text);
                if (illegal.Contains(asciiBytes[0]))
                {
                    stringErrors.Add("Illegal GAP character. Cannot use the following characters: ()[]{} / , ; : = * '  ` < > ^");
                    GapChar.Background = new SolidColorBrush(Colors.LightSalmon);
                }
                
                        // stringErrors.Remove("Must enter an integer into the number of characters per matrix field.");
                }
            if (stringErrors.Count > 0)
            {
                EnableErrorScroll();
            }
            else
            {
                ErrorText.Visibility = Visibility.Collapsed;
                ErrorText.Text = "";
                App.f.C.gapChar = GapChar.Text[0];
                GapChar.Background = new SolidColorBrush(Colors.LightGray);
            }
        }
        private void ValidateMissingChar_Click(object sender, RoutedEventArgs e)
        {
            MissingChar.Background = new SolidColorBrush(Colors.LightGray);
            List<int> illegal = new List<int> { 32, 34, 39, 40, 41, 42, 44, 47, 58, 59, 60, 62, 61, 94, 96, 91, 92, 93, 123, 125, 127 };
            if (stringErrors.Contains("Must enter one character into the MISSING character field."))
            {
                //remove
                stringErrors.Remove("Must enter one character into the MISSING character field.");
            }
            else if (stringErrors.Contains("Illegal MISSING character. Cannot use the following characters: ()[]{} / , ; : = * '  ` < > ^"))
            {
                //remove
                stringErrors.Remove("Illegal MISSING character. Cannot use the following characters: ()[]{} / , ; : = * '  ` < > ^");
            }

            if (MissingChar.Text.Length == 0 || MissingChar.Text.Length > 1)
            {
                stringErrors.Add("Must enter one character into the MISSING character field.");
                MissingChar.Background = new SolidColorBrush(Colors.LightSalmon);
            }
            else
            {
                byte[] asciiBytes = Encoding.UTF8.GetBytes(MissingChar.Text);
                if (illegal.Contains(asciiBytes[0]))
                {
                    stringErrors.Add("Illegal MISSING character. Cannot use the following characters: ()[]{} / , ; : = * '  ` < > ^");
                    MissingChar.Background = new SolidColorBrush(Colors.LightSalmon);
                }

                // stringErrors.Remove("Must enter an integer into the number of characters per matrix field.");
            }
            if (stringErrors.Count > 0)
            {
                EnableErrorScroll();
            }
            else
            {
                ErrorText.Visibility = Visibility.Collapsed;
                ErrorText.Text = "";
                App.f.C.missingChar = MissingChar.Text[0];
                MissingChar.Background = new SolidColorBrush(Colors.LightGray);
            }
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {           
            if (stringErrors.Count() == 0)
            {
                ValidateDataType_Click(sender, e);
                ValidateCharNum_Click(sender, e);
                ValidateGapChar_Click(sender, e);
                ValidateMissingChar_Click(sender, e);
                if(stringErrors.Count==0)
                {
                    this.Frame.Navigate(typeof(SequenceDataInput), App.f.C);
                }                   
                
            }
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {//Goes back to Taxa Page
            this.Frame.Navigate(typeof(MainPage), App.f.C);
        }
        
    }
}
