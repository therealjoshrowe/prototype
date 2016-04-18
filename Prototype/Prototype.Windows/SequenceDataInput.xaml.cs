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
    public sealed partial class SequenceDataInput : Page
    {
        private int numOfTaxaPanels;
        private List<TextBox> TaxaText;
        private List<TextBox> DataText;
        private int charLength;
        List<TextBox> tbErrors;
        List<TextBox> taxaErrors;
        List<String> stringErrors;
        TextBox ErrorText;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.f.C = new CharactersBlock();
            App.f.C = e.Parameter as CharactersBlock;
            charLength = App.f.C.ncharValue;
            //The following if's are old bad validation?
            if (charLength == 0)
            {
                charLength = 20;
            }

            if (App.f.C.dataSelection == 0)
            {
                App.f.C.dataSelection = 3;
            }
            DynamicText(App.f.C.taxa);
        }
        public SequenceDataInput()
        {
            this.InitializeComponent();
            taxaErrors = new List<TextBox>();
            stringErrors = new List<String>();
            tbErrors = new List<TextBox>();
            ErrorText = new TextBox();


            ErrorText.Name = "errors";
            ErrorText.Width = 300;
            ErrorText.Height = 300;
            ErrorText.FontSize = 12;
            ErrorText.TextWrapping = TextWrapping.Wrap;
            ErrorText.Background = new SolidColorBrush(Colors.Gainsboro);
            ErrorText.Visibility = Visibility.Collapsed;
          
            StackPanel s = new StackPanel();
            s.Orientation = Orientation.Horizontal;
            s.HorizontalAlignment = HorizontalAlignment.Center;
            s.Children.Add(ErrorText);
            ScrollError.Content = s;

        }
        public void DynamicText(List<String> x)
        {
            this.InitializeComponent();
            numOfTaxaPanels = x.Count;//reference main obj and however many taxa were previously entered
            TaxaText = new List<TextBox>();
            DataText = new List<TextBox>();
            for (int i = 0; i < numOfTaxaPanels; i++)// starts at 1 b/c the first stackpanel is already displayed
            {

                TextBox tbTaxa = new TextBox();
                tbTaxa.Name = "taxa" + i;
                tbTaxa.AcceptsReturn = true;
                Thickness m1 = tbTaxa.Margin;
                m1.Left = 10;
                tbTaxa.Margin = m1;
                tbTaxa.Width = 100;
                tbTaxa.Height = 30;
                Thickness m3 = tbTaxa.Padding;
                m3.Left = 20;
                m3.Bottom = 20;
                tbTaxa.Text = x[i];
                tbTaxa.FontSize = 14;
                tbTaxa.Foreground = new SolidColorBrush(Colors.Orange);
                tbTaxa.Background = new SolidColorBrush(Colors.Black);
                tbTaxa.LostFocus += new RoutedEventHandler(TaxaLostFocusEvent);
                // tbTaxa.Padding = 15;
                TaxaText.Add(tbTaxa);


                ///////////////////////////padding
                TextBox tbData = new TextBox();
                tbData.Name = "data" + i;
                tbData.AcceptsReturn = true;
                Thickness m2 = tbData.Margin;
                m2.Left = 10;
                tbData.Margin = m2;
                Thickness m4 = tbData.Padding;
                m4.Left = 20;
                m4.Bottom = 50;
                tbData.Width = 300;
                tbData.Height = 150;
                tbData.FontSize = 14;
                tbData.Background = new SolidColorBrush(Colors.LightGray);
                tbData.TextWrapping = TextWrapping.Wrap;
                tbData.LostFocus += new RoutedEventHandler(MatrixLostFocusEvent);
                DataText.Add(tbData);

                Button btnRemove = new Button();
                btnRemove.Content = "Remove";
                btnRemove.IsEnabled = true;
                btnRemove.HorizontalAlignment = HorizontalAlignment.Right;
                btnRemove.Click += btnRemove_Click;
                //       < Button x: Name = "button" Content = "Next" IsEnabled = "True" HorizontalAlignment = "Right" Click = "btnValidate_Click" />


                StackPanel s = new StackPanel();
                s.Name = "SequencePanel" + i;
                s.Orientation = Orientation.Horizontal;
                s.HorizontalAlignment = HorizontalAlignment.Center;
                s.Children.Add(tbTaxa);
                s.Children.Add(tbData);
                s.Children.Add(btnRemove);
                // s.Children.Add();
                //need to add remove button for each row then add button at the bottim
                SeqFrame.Children.Add(s);
            }
            ScrollSeq.Content = SeqFrame;
        }
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
                if (dc.Children.Contains(DataText[i]))
                {
                    TaxaText.Remove(DataText[i]);
                }
            }

            (dc.Parent as StackPanel).Children.Remove(dc);
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
            m3.Left = 20;
            m3.Bottom = 20;
            tbTaxa.FontSize = 14;
            tbTaxa.Foreground = new SolidColorBrush(Colors.Orange);
            tbTaxa.Background = new SolidColorBrush(Colors.Black);
            // tbTaxa.Padding = 15;
            TaxaText.Add(tbTaxa);


            ///////////////////////////padding
            TextBox tbData = new TextBox();
            tbData.AcceptsReturn = true;
            Thickness m2 = tbData.Margin;
            m2.Left = 10;
            tbData.Margin = m2;
            Thickness m4 = tbData.Padding;
            m4.Left = 20;
            m4.Bottom = 50;
            tbData.Width = 300;
            tbData.Height = 150;
            tbData.FontSize = 14;
            tbData.Background = new SolidColorBrush(Colors.LightGray);
            tbData.TextWrapping = TextWrapping.Wrap;
            DataText.Add(tbData);

            Button btnRemove = new Button();
            btnRemove.Content = "Remove";
            btnRemove.IsEnabled = true;
            btnRemove.HorizontalAlignment = HorizontalAlignment.Right;
            btnRemove.Click += btnRemove_Click;
            //       < Button x: Name = "button" Content = "Next" IsEnabled = "True" HorizontalAlignment = "Right" Click = "btnValidate_Click" />


            StackPanel s = new StackPanel();
            s.Orientation = Orientation.Horizontal;
            s.HorizontalAlignment = HorizontalAlignment.Center;
            s.Children.Add(tbTaxa);
            s.Children.Add(tbData);
            s.Children.Add(btnRemove);
            // s.Children.Add();
            //need to add remove button for each row then add button at the bottim
            SeqFrame.Children.Add(s);

            ScrollSeq.Content = SeqFrame;
            //add textboxes to list above so they can be validated
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CharactersPage), App.f.C);
        }
        private void GetEmptyTextBoxes()
        {
            foreach (var x in TaxaText)
            {
                if (string.IsNullOrEmpty(x.Text))
                {
                    taxaErrors.Add(x);
                    stringErrors.Add("Empty input value.");
                    x.Background = new SolidColorBrush(Colors.LightSalmon);
                }
            }
            foreach (var matrixBox in DataText)
            {

                if (string.IsNullOrEmpty(matrixBox.Text))
                {
                    taxaErrors.Add(matrixBox);
                    stringErrors.Add("Empty input value.");
                    matrixBox.Background = new SolidColorBrush(Colors.LightSalmon);
                }
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
        //    MatrixLostFocusEvent(sender, e);
        //    TaxaLostFocusEvent(sender, e);
            GetEmptyTextBoxes();
            if (stringErrors.Count > 0)
            {
                ErrorText.Visibility = Visibility.Visible;
                ErrorText.Text = "The following " + stringErrors.Count + " errors must be fixed before you can continue: " + System.Environment.NewLine;


                for (int i = 0; i < stringErrors.Count; i++)
                {
                    ErrorText.Text += stringErrors[i] + System.Environment.NewLine;
                } 
            }
            else
            {
                List<String> TaxaStrings = new List<String>();
                List<String> DataStrings = new List<String>();

                App.f.C.sequences = new List<Sequence>();
                foreach (TextBox s in TaxaText)
                {
                    TaxaStrings.Add(s.Text);
                }
                foreach (TextBox s in DataText)
                {
                    DataStrings.Add(s.Text);
                }
                for (int i = 0; i < numOfTaxaPanels; i++)
                {
                    App.f.C.sequences.Add(new Sequence(TaxaStrings[i], DataStrings[i]));
                }
            }
        }
        
        private void TaxaLostFocusEvent(object sender, RoutedEventArgs e)
        {
            TextBox TaxaBox = (TextBox)sender;
            TaxaBox.Background = new SolidColorBrush(Colors.Black);
            if(tbErrors.Contains(TaxaBox))
            {
                tbErrors.Remove(TaxaBox);
                if(TaxaBox.Name.Equals("error0"))
                {
                    stringErrors.Remove("Empty input value.");
                    TaxaBox.Name = "";
                }
            }


                if (string.IsNullOrEmpty(TaxaBox.Text))
                {
                // taxaErrors.Add(TaxaBox);
                TaxaBox.Name = "error0";
                    stringErrors.Add("Empty input value.");
                    tbErrors.Add(TaxaBox);
                    TaxaBox.Background = new SolidColorBrush(Colors.LightSalmon);
                }
            
        }
        private void removeOldError()
        {
            bool foundError = false;

            for (int i = 0; i < stringErrors.Count; i++)
            {
                if (stringErrors[i].Substring(0, 6).Equals("Matrix"))
                {
                    stringErrors.Remove(stringErrors[i]);
                }
                else if (stringErrors[i].Substring(0, 2).Equals("DNA"))
                {
                    stringErrors.Remove(stringErrors[i]);

                }
                else if (stringErrors[i].Substring(0, 2).Equals("RNA"))
                {
                    stringErrors.Remove(stringErrors[i]);
                }
                else if (stringErrors[i].Substring(0, 2).Equals("Pro"))
                {
                    stringErrors.Remove(stringErrors[i]);
                }
                else if (stringErrors[i].Substring(0, 2).Equals("Mor"))
                {
                    stringErrors.Remove(stringErrors[i]);// Morphological
                }
            }
        }



        private void MatrixLostFocusEvent(object sender, RoutedEventArgs e)
        {
                TextBox matrixBox = (TextBox)sender;
            matrixBox.Background = new SolidColorBrush(Colors.LightGray);
            //if error already exsists, remove it
            // removeOldError();
            //stringErrors.Clear();

            if (tbErrors.Contains(matrixBox))
            {
                tbErrors.Remove(matrixBox);
                if (matrixBox.Name.Equals("error1"))
                {
                    for(int i=0; i< stringErrors.Count; i++)
                    {
                        if (stringErrors[i].Substring(0,3).Equals("Mat"))
                        {
                            stringErrors.RemoveAt(i);
                        }
                    }
                    matrixBox.Name = "";
                }
               else if (matrixBox.Name.Equals("error2"))
                {
                    for (int i = 0; i < stringErrors.Count; i++)
                    {
                        if (stringErrors[i].Substring(0, 3).Equals("DNA"))
                        {
                            stringErrors.RemoveAt(i);
                        }
                    }
                    matrixBox.Name = "";
                }
               else if (matrixBox.Name.Equals("error3"))
                {
                    for (int i = 0; i < stringErrors.Count; i++)
                    {
                        if (stringErrors[i].Substring(0, 3).Equals("RNA"))
                        {
                            stringErrors.RemoveAt(i);
                        }
                    }
                    matrixBox.Name = "";
                }
                else if (matrixBox.Name.Equals("error4"))
                {
                    for (int i = 0; i < stringErrors.Count; i++)
                    {
                        if (stringErrors[i].Substring(0, 3).Equals("Pro"))
                        {
                            stringErrors.RemoveAt(i);
                        }
                    }
                    matrixBox.Name = "";
                }
                else if (matrixBox.Name.Equals("error5"))
                {
                    for (int i = 0; i < stringErrors.Count; i++)
                    {
                        if (stringErrors[i].Substring(0, 3).Equals("Mor"))
                        {
                            stringErrors.RemoveAt(i);
                        }
                    }
                    matrixBox.Name = "";
                }
            }
            if (matrixBox.Text.Length != charLength)
                {
                tbErrors.Add(matrixBox);
                matrixBox.Name = "error1";
                    stringErrors.Add("Matrix doesn't have " + charLength + " characters. Needs " + (charLength - matrixBox.Text.Length) + " more characters");
                    matrixBox.Background = new SolidColorBrush(Colors.LightSalmon);


                }
                else if (App.f.C.dataSelection == 1)//DNA
                {
                    List<char> charList = new List<char> { 'G', 'A', 'T', 'C', App.f.C.gapChar, App.f.C.missingChar };
                    string error;
                    //  string seqchars = App.f.C.SequenceChars;//where
                    matrixBox.Text = matrixBox.Text.ToUpper();
                    for (int x = 0; x < matrixBox.Text.Length; x++)
                    {
                        if (!charList.Contains(matrixBox.Text[x]))
                        {
                        tbErrors.Add(matrixBox);
                        matrixBox.Name = "error2";
                        error = "DNA sequence contains obscure characters. Only A,C,T,G";
                            error += "or the chosen gap and missing characters, repectively " + App.f.C.gapChar + " and " + App.f.C.missingChar + ", are permitted.";
                            stringErrors.Add(error);
                            error = "";
                            matrixBox.Background = new SolidColorBrush(Colors.LightSalmon);
                            break;
                        }
                    }

                }
                else if (App.f.C.dataSelection == 2)//RNA
                {
                    string error;
                    List<char> charList = new List<char> { 'G', 'A', 'U', 'C', App.f.C.gapChar, App.f.C.missingChar };
                    matrixBox.Text = matrixBox.Text.ToUpper();
                    for (int x = 0; x < matrixBox.Text.Length; x++)
                    {
                        if (!charList.Contains(matrixBox.Text[x]))
                        {
                        tbErrors.Add(matrixBox);
                        matrixBox.Name = "error3";
                        error = "RNA sequence contains obscure characters. Only A, C, G, U ";
                            error += "or the chosen gap and missing characters, repectively " + App.f.C.gapChar + " and " + App.f.C.missingChar + ", are permitted.";
                            stringErrors.Add(error);
                            error = "";
                            matrixBox.Background = new SolidColorBrush(Colors.LightSalmon);
                            break;
                        }
                    }

                }
                else if (App.f.C.dataSelection == 3)//Protein Data
                {
                    string error;
                    List<char> charList = new List<char> { 'A', 'I', 'P', 'V', 'R', 'E', 'W', 'Z', 'J', 'C', 'L', 'S', 'F', 'Y', 'K', 'B', 'X', App.f.C.gapChar, App.f.C.missingChar };//TODO: UPDATE CHARLIST FOR PROTEIN
                    matrixBox.Text = matrixBox.Text.ToUpper();
                    for (int x = 0; x < matrixBox.Text.Length; x++)
                    {
                        if (!charList.Contains(matrixBox.Text[x]))
                        {
                        tbErrors.Add(matrixBox);
                        matrixBox.Name = "error4";
                        error = "Protein sequence contains obscure characters. Only ";
                            for (int r = 0; r < charList.Count - 2; r++)
                            {
                                error += charList[r] + ", ";
                            }
                            error += "or the chosen gap and missing characters, repectively " + App.f.C.gapChar + " and " + App.f.C.missingChar + ", are permitted.";
                            matrixBox.Background = new SolidColorBrush(Colors.LightSalmon);
                            stringErrors.Add(error);
                            break;
                        }
                    }

                }
                else if (App.f.C.dataSelection == 4)//Morphilogical: 0-9 and symbols
                {
                    List<char> charList = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', App.f.C.gapChar, App.f.C.missingChar };
                    for (int x = 0; x < matrixBox.Text.Length; x++)
                    {
                        if (!charList.Contains(matrixBox.Text[x]))
                        {
                        tbErrors.Add(matrixBox);
                        matrixBox.Name = "error5";
                        stringErrors.Add("Morphological Matrix contains obscure characters. Only 0-9 or the chosen gap and missing characters, repectively " + App.f.C.gapChar + " and " + App.f.C.missingChar + ", are permitted.");
                            matrixBox.Background = new SolidColorBrush(Colors.LightSalmon);
                            break;
                        }
                    }

                }
            
            if (stringErrors.Count > 0)
            {
                ErrorText.Visibility = Visibility.Visible;
                ErrorText.Text = "The following " + stringErrors.Count + " errors must be fixed before you can continue: " + System.Environment.NewLine;


                for (int i = 0; i < stringErrors.Count; i++)
                {
                    ErrorText.Text += stringErrors[i] + System.Environment.NewLine;
                }

            }
            else
            {
                ErrorText.Visibility = Visibility.Collapsed;
                ErrorText.Text = "";
            }
           
        }
    }
}