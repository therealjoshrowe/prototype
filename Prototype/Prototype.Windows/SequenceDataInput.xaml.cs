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
        private List<String> Taxa;
        private int charLength;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.f.C = new CharactersBlock();
            App.f.C = e.Parameter as CharactersBlock;
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
            for(int i=0; i<TaxaText.Count; i++)
            {
                if(dc.Children.Contains(TaxaText[i]))
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
        private void btnValidate_Click(object sender, RoutedEventArgs e)
        {
            List<TextBox> taxaErrors = new List<TextBox>();
            List<String> stringErrors = new List<String>();
         
                
         
            for (int i=0; i< TaxaText.Count; i++)
            {
                TaxaText[i].Background = new SolidColorBrush(Colors.Black);
                DataText[i].Background = new SolidColorBrush(Colors.LightGray);
                TextBox matrixBox = DataText[i];
                if (string.IsNullOrEmpty(TaxaText[i].Text))
                {
                    taxaErrors.Add(TaxaText[i]);
                    stringErrors.Add("Empty input value.");
                    TaxaText[i].Background = new SolidColorBrush(Colors.LightSalmon);
                }
                if (string.IsNullOrEmpty(matrixBox.Text))
                {
                    taxaErrors.Add(matrixBox);
                    stringErrors.Add("Empty input value.");
                    DataText[i].Background = new SolidColorBrush(Colors.LightSalmon);
                }
                else if (matrixBox.Text.Length != charLength)
                {
                    stringErrors.Add("Matrix doesn't have " + charLength + " characters.");
                    DataText[i].Background = new SolidColorBrush(Colors.LightSalmon);
                
                 }
                //else if (App.f.C.dataSelection == 1)
                //{
                //    // List<char> charList = new List<char> { 'G', 'g', 'A', 'a', 'T', 't', 'C', 'c', App.f.C.gapChar, App.f.C.missingChar };
                //    string error;
                //    string seqchars = App.f.C.SequenceChars;
                //    for (int x = 0; x < matrixBox.Text.Length; x++)
                //    {
                //        if (!seqchars.Contains(matrixBox.Text[x]))
                //        {
                //            error = "Protein Matrix contains obscure characters. Only ";// or the chosen gap and missing characters permitted.");
                //            for(int r=0; r< seqchars.Length; r++)
                //            {
                //                error += seqchars[r] + ", ";
                //            }
                //            error += "or the chosen gap and missing characters permitted.";
                //            stringErrors.Add(error);
                //            error = "";
                //            DataText[i].Background = new SolidColorBrush(Colors.LightSalmon);
                //            break;
                //        }
                //    }

                //}
                //else if (App.f.C.dataSelection == 2)
                //{
                //    string error;
                //    string morphchars = App.f.C.MorphChars;
                //    for (int x = 0; x < matrixBox.Text.Length; x++)
                //    {
                //        if (!morphchars.Contains(matrixBox.Text[x]))
                //        {
                //            error = "Protein Matrix contains obscure characters. Only ";// or the chosen gap and missing characters permitted.");
                //            for (int r = 0; r < morphchars.Length; r++)
                //            {
                //                error += morphchars[r] + ", ";
                //            }
                //            error += "or the chosen gap and missing characters permitted.";
                //            stringErrors.Add(error);
                //            error = "";
                //            DataText[i].Background = new SolidColorBrush(Colors.LightSalmon);
                //            break;
                //        }
                //    }

                //}
                //else if (App.f.C.dataSelection  ==3)
                //{
                //    List<char> charList = new List<char> { 'G', 'g', 'A', 'a', 'T', 't', 'C', 'c', App.f.C.gapChar, App.f.C.missingChar };
                //    for(int x=0; x< matrixBox.Text.Length; x++)
                //    {
                //        if(!charList.Contains(matrixBox.Text[x]))
                //        {
                //            stringErrors.Add("Protein Matrix contains obscure characters. Only G,A,T,C or the chosen gap and missing characters permitted.");
                //            DataText[i].Background = new SolidColorBrush(Colors.LightSalmon);
                //            break;
                //        }
                //    }
                   
                //}
            }
            if (stringErrors.Count > 0)
            {                
                TextBox ErrorText = new TextBox();
                ErrorText.Name = "errors";
                ErrorText.Width = 300;
                ErrorText.Height = 300;
                ErrorText.FontSize = 12;
                ErrorText.TextWrapping = TextWrapping.Wrap;
                ErrorText.Background = new SolidColorBrush(Colors.Gainsboro);
                ErrorText.Text = "The following " + taxaErrors.Count + " errors must be fixed before you can continue: " + System.Environment.NewLine;
                for (int i = 0; i < stringErrors.Count; i++)
                {
                    ErrorText.Text += stringErrors[i] + System.Environment.NewLine;
                }
                StackPanel s = new StackPanel();
                s.Orientation = Orientation.Horizontal;
                s.HorizontalAlignment = HorizontalAlignment.Center;
                s.Children.Add(ErrorText);
                ScrollError.Content = s;
            }
            else
            {
                //add to nexus  //add data to general nexus file and pass tp Page3.xaml
          //  App.f.C = new Seq();
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
                this.Frame.Navigate(typeof(Page3));
            }
          

            //NavigationService nav = NavigationService.GetNavigationService(this);
            //nav.Navigate(new Page3());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}