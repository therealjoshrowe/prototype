using Shared_Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
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
    /// The Characters Page
    /// </summary>
    public sealed partial class CharactersPage : Page
    {
        private List<string> stringErrors;
        private List<TextBox> symbolTextBoxList;
        private List<TextBox> symbolErrors;
        private TextBox ErrorText;
        private StackPanel s;

        /// <summary>
        /// Captures Model obj to be usable on this page
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.f.C = new CharactersBlock();
            App.f.C = e.Parameter as CharactersBlock;
            if (!string.IsNullOrEmpty(App.f.C.ncharValue.ToString()) || !string.IsNullOrEmpty(App.f.C.missingChar.ToString()) || !string.IsNullOrEmpty(App.f.C.gapChar.ToString()))
                {
                LoadPreviousDataToScreen(App.f.C);
            }
        }
        /// <summary>
        /// Constructor initializing:
        /// --static lostfocus events
        /// --the Error textbox
        /// -- hiden extra choices available if Morphilogical data is selected
        /// --Help popup button info
        /// </summary>
        public CharactersPage()
        {
            this.InitializeComponent();
            //initialize lostfocus events
            comboBox.LostFocus += new RoutedEventHandler(ValidateDataType_Click);
            charNum.LostFocus += new RoutedEventHandler(ValidateCharNum_Click);
            GapChar.LostFocus += new RoutedEventHandler(ValidateGapChar_Click);
            MissingChar.LostFocus += new RoutedEventHandler(ValidateMissingChar_Click);
            symbolBox.LostFocus += new RoutedEventHandler(ValidateSymbolBox_Click);


            stringErrors = new List<string>();
            symbolTextBoxList = new List<TextBox>();
            symbolErrors = new List<TextBox>();
            App.f.C.symbols = new List<string>();
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
            
            symbolBox.Visibility = Visibility.Collapsed;
            //symbolText.Visibility = Visibility.Collapsed;
            //btnRemove.Visibility = Visibility.Collapsed;
            btnSymbol.Visibility = Visibility.Collapsed;
            EnterSymbol.Visibility = Visibility.Collapsed;
            AddSymbol.Visibility = Visibility.Collapsed;

            HelpPopup.TextWrapping = TextWrapping.Wrap;
            HelpPopup.Text = "Step 1: Choose the type of data being entered into this Nexus file \n";
            HelpPopup.Text += "\n Note: If you are entering Morphological data, you will then be given the option to setup symbols for your data matrix. However, symbols are not required.\n";
            HelpPopup.Text += "\n Step 2: Enter the amount of character traits per taxa that this Nexus file will represent. \n";
            HelpPopup.Text += "\n Step 3: Choose a GAP character. You are required to choose a GAP character to continue, however, you are not required to enter this GAP character into the data matrix. \n";
            HelpPopup.Text += "\n Step 4: Choose a MISSING character. You are required to choose a MISSING character to continue, however, you are not required to enter this MISSING character into the data matrix. \n";
            HelpPopup.Text += "\n Click 'Next' when finished entering the desired character information. \n";
        }

/// <summary>
/// If data is present in the Model obj, reloads that data onto the screen
/// </summary>
/// <param name="charBlock"></param>
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
        /// <summary>
        /// Enables the Error textbox to be visible and shows current errors
        /// </summary>
        private void EnableErrorScroll()
        {
            ErrorText.Visibility = Visibility.Visible;
            ErrorText.Text = "The following " + stringErrors.Count + " errors must be fixed before you can continue: " + System.Environment.NewLine;


            for (int i = 0; i < stringErrors.Count; i++)
            {
                ErrorText.Text += stringErrors[i] + System.Environment.NewLine;
            }

        }
        /// <summary>
        /// Validates the Data Type combo box after LostFocus event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if(App.f.C.dataSelection == 4)
                {
                   AddSymbol.Visibility = Visibility.Visible;
                    symbolBox.Visibility = Visibility.Visible;
                  //  btnSymbol.Visibility = Visibility.Visible;
                }

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
        /// <summary>
        /// Validates Symbol combo box after LostFocus event and initializes controls to add symbols
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidateSymbolBox_Click(object sender, RoutedEventArgs e)
        {
            
                var symbolChoice = symbolBox.SelectedIndex;
                if (symbolChoice == 0)
                {
                    App.f.C.useSymbol = true;
                    //  symbolText.Visibility = Visibility.Visible;
                    //btnRemove.Visibility = Visibility.Visible;
                     //call add symbol fxn
                     if(spSymbol.Children.Count == 0)
                    {
                        btnSymbol_Click(sender, e);
                    }
                     foreach(var x in spSymbol.Children)
                    {
                        x.Visibility = Visibility.Visible;
                    }
                     EnterSymbol.Visibility = Visibility.Visible;
                    AddSymbol.Visibility = Visibility.Visible;
                    btnSymbol.Visibility = Visibility.Visible;
                  //  btnSymbol_Click(sender, e);
                }
                else
                {
                    App.f.C.useSymbol = false;
                //remove all symbol stacks
                EstablishFinalSymbols();
                EnterSymbol.Visibility = Visibility.Collapsed;
                    for (int i=0; i< spSymbol.Children.Count; i++)
                    {
                        spSymbol.Children[i].Visibility = Visibility.Collapsed;
                    }
                    btnSymbol.Visibility = Visibility.Collapsed;
                if (stringErrors.Count > 0)
                {
                    EnableErrorScroll();
                }
                else
                {
                    ErrorText.Visibility = Visibility.Collapsed;
                    ErrorText.Text = "";
                }
            }

            
           
           
        }
        /// <summary>
        /// Validates Symbol text box after LostFocus event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SymbolTextLostFocus(object sender, RoutedEventArgs e)
        {
            var x = (TextBox)sender;
            //errors.Clear();
            x.Background = new SolidColorBrush(Colors.LightGray);
            string tbString = x.Text;
            tbString = tbString.Replace(" ", String.Empty);
            if (symbolErrors.Contains(x))
            {
                symbolErrors.Remove(x);
                if (x.Name.Equals("error0"))
                {
                    stringErrors.Remove("Must enter symbol information in the format: 'A=(012)'");
                }
                else if (x.Name.Equals("error1"))
                {
                    stringErrors.Remove("Symbols must have a single letter following an equals sign in the format: 'A=(012)");
                }
                else if (x.Name.Equals("error2"))
                {
                    stringErrors.Remove("Symbols must have numeric character traits in parentheses in the format: 'A=(012)");
                }
            }
            ValidateSymbolString(tbString, x);

            if (stringErrors.Count > 0)
            {
                EnableErrorScroll();
            }
            else
            {
                x.Text = x.Text.ToUpper();
                ErrorText.Visibility = Visibility.Collapsed;
                ErrorText.Text = "";
            }

        }
        /// <summary>
        /// Validates the Symbol string. Called from SymbolTextLostFocus
        /// </summary>
        /// <param name="tbString"></param>
        /// <param name="x"></param>
        private void ValidateSymbolString(string tbString, TextBox x)
        {
            if (string.IsNullOrEmpty(tbString))
            {
                x.Name = "error0";
                symbolErrors.Add(x);
                stringErrors.Add("Must enter symbol information in the format: 'A=(012)'");
                x.Background = new SolidColorBrush(Colors.LightSalmon);
            }
            else if (!string.IsNullOrEmpty(tbString) && !Char.IsLetter(tbString[0]))
            {
                x.Name = "error1";
                symbolErrors.Add(x);
                stringErrors.Add("Symbols must have a single letter following an equals sign in the format: 'A=(012)");
                x.Background = new SolidColorBrush(Colors.LightSalmon);
            }
            else if (tbString.Length>1 && tbString[1] != '=')
            {
                x.Name = "error1";
                symbolErrors.Add(x);
                stringErrors.Add("Symbols must have a single letter following an equals sign in the format: 'A=(012)");
                x.Background = new SolidColorBrush(Colors.LightSalmon);
            }
            else if (tbString.Length > 2 && tbString[2] != '(' || tbString[tbString.Length - 1] != ')')
            {
                x.Name = "error2";
                symbolErrors.Add(x);
                stringErrors.Add("Symbols must have numeric character traits in parentheses in the format: 'A=(012)");
                x.Background = new SolidColorBrush(Colors.LightSalmon);
            }
            else {
                //if all whitespace is removed, then symbolString should look like A=(012)
                //Following for loop is intended to make sure the data in parens are numbers
                for (int i = 3; i < tbString.Length - 1; i++)
                {
                    try
                    {
                        int val = (int)Char.GetNumericValue(tbString[i]);
                    }
                    catch (Exception)
                    {
                        x.Name = "error2";
                        symbolErrors.Add(x);
                        stringErrors.Add("Symbols must have numeric character traits in parentheses in the format: 'A=(012)");
                        x.Background = new SolidColorBrush(Colors.LightSalmon);
                    }

                }
            }
        }
        /// <summary>
        /// Validates the number of characters option after LostFocus event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Validates Gap char text box after LostFocus event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                
                }
            if (stringErrors.Count > 0)
            {
                EnableErrorScroll();
            }
            else
            {
                ErrorText.Visibility = Visibility.Collapsed;
                ErrorText.Text = "";
                GapChar.Text = GapChar.Text.ToUpper();
                App.f.C.gapChar = GapChar.Text[0];
                GapChar.Background = new SolidColorBrush(Colors.LightGray);
            }
        }
        /// <summary>
        /// Validates Missing char text box after LostFocus event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                
            }
            if (stringErrors.Count > 0)
            {
                EnableErrorScroll();
            }
            else
            {
                ErrorText.Visibility = Visibility.Collapsed;
                ErrorText.Text = "";
                MissingChar.Text = MissingChar.Text.ToUpper();
                App.f.C.missingChar = MissingChar.Text[0];
                MissingChar.Background = new SolidColorBrush(Colors.LightGray);
            }
        }
        /// <summary>
        /// Calls all validate methods again and if no errors occur, go to next page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {           
            if (stringErrors.Count() == 0)
            {
                ValidateDataType_Click(sender, e);
                ValidateCharNum_Click(sender, e);
                ValidateGapChar_Click(sender, e);
                ValidateMissingChar_Click(sender, e);
                EstablishFinalSymbols();
                if(stringErrors.Count==0)
                {
                    this.Frame.Navigate(typeof(SequenceDataInput), App.f.C);
                }                   
                
            }
        }
        /// <summary>
        /// Validate Symbol strings then if valid, add to Model obj
        /// </summary>
        private void EstablishFinalSymbols()
        {
            if (App.f.C.useSymbol)
            {
                foreach (var x in symbolTextBoxList)
                {

                    string tbString = x.Text;
                    tbString = tbString.Replace(" ", String.Empty);
                    ValidateSymbolString(tbString, x);
                }
                if (stringErrors.Count > 0)
                {
                    EnableErrorScroll();
                }
                else
                {
                    ErrorText.Visibility = Visibility.Collapsed;
                    ErrorText.Text = "";
                    foreach (var x in symbolTextBoxList)
                    {
                        string tbString = x.Text;
                        tbString = tbString.Replace(" ", String.Empty);
                        App.f.C.symbols.Add(tbString);
                    }
                }
            }
            else//else at some point, decided not to use symbols, so remove all symbol related errors
            {
                foreach (var x in symbolTextBoxList)
                {
                    if (symbolErrors.Contains(x))
                    {
                        symbolErrors.Remove(x);
                        if (x.Name.Equals("error0"))
                        {
                            stringErrors.Remove("Must enter symbol information in the format: 'A=(012)'");
                        }
                        else if (x.Name.Equals("error1"))
                        {
                            stringErrors.Remove("Symbols must have a single letter following an equals sign in the format: 'A=(012)");
                        }
                        else if (x.Name.Equals("error2"))
                        {
                            stringErrors.Remove("Symbols must have numeric character traits in parentheses in the format: 'A=(012)");
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Returns back to the Taxa page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {//Goes back to Taxa Page
            this.Frame.Navigate(typeof(MainPage), App.f.C);
        }
        /// <summary>
        /// Adds a new text box and remove button to enter another symbol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSymbol_Click(object sender, RoutedEventArgs e)
        {
            TextBox tbSymbol = new TextBox();
            tbSymbol.AcceptsReturn = true;

            tbSymbol.FontSize = 12;
            tbSymbol.Height = 45;
            // tbTaxa.Padding = 15;
            tbSymbol.LostFocus += new RoutedEventHandler(SymbolTextLostFocus);
            symbolTextBoxList.Add(tbSymbol);

            Button btnRemove = new Button();
            btnRemove.Content = "Remove";
            btnRemove.IsEnabled = true;
         //   btnRemove.HorizontalAlignment = HorizontalAlignment.Right;
            btnRemove.Click += btnRemove_Click;

            StackPanel s = new StackPanel();
            s.Orientation = Orientation.Horizontal;
            s.Children.Add(tbSymbol);
            s.Children.Add(btnRemove);
            // s.Children.Add();
            //need to add remove button for each row then add button at the bottim
            spSymbol.Children.Add(s);

            SymbolScroll.Content = spSymbol;
        }
        /// <summary>
        /// Remove the Symbol entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            //remove the stackpanel that this button is located in
            //   sender.
            var dc = (sender as Button).Parent as StackPanel;
            for (int i = 0; i < symbolTextBoxList.Count; i++)
            {
                if (dc.Children.Contains(symbolTextBoxList[i]))
                {
                    if (symbolErrors.Contains(symbolTextBoxList[i]))
                    {
                        symbolErrors.Remove(symbolTextBoxList[i]);
                        if (symbolTextBoxList[i].Name.Equals("error0"))
                        {
                            stringErrors.Remove("Must enter symbol information in the format: 'A=(012)'");
                        }
                        else if (symbolTextBoxList[i].Name.Equals("error1"))
                        {
                            stringErrors.Remove("Symbols must have a single letter following an equals sign in the format: 'A=(012)");
                        }
                        else if (symbolTextBoxList[i].Name.Equals("error2"))
                        {
                            stringErrors.Remove("Symbols must have numeric character traits in parentheses in the format: 'A=(012)");
                        }
                    }
                    symbolTextBoxList.Remove(symbolTextBoxList[i]);
                }
            }

           (dc.Parent as StackPanel).Children.Remove(dc);
            
            //remove the textboxes from the list of textboxes a the tpo
        }
        /// <summary>
        /// Opens 'Help' popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            if (!StandardPopup.IsOpen) { StandardPopup.IsOpen = true; }
        }
        /// <summary>
        /// Closes 'Help' popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosePopupClicked(object sender, RoutedEventArgs e)
        {
            // if the Popup is open, then close it 

            if (StandardPopup.IsOpen) { StandardPopup.IsOpen = false; }
        }
    }
}
