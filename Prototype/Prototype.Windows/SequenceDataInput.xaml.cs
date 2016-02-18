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


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Taxa = e.Parameter as List<String>;
            DynamicText(Taxa);
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

                StackPanel s = new StackPanel();
                s.Name = "SequencePanel" + i;
                s.Orientation = Orientation.Horizontal;
                s.HorizontalAlignment = HorizontalAlignment.Center;
                s.Children.Add(tbTaxa);
                s.Children.Add(tbData);
                // s.Children.Add();
                SeqFrame.Children.Add(s);
            }
            ScrollSeq.Content = SeqFrame;
            var count = SeqFrame;
        }
        private void btnValidate_Click(object sender, RoutedEventArgs e)
        {
            List<TextBox> taxaErrors = new List<TextBox>();
            List<String> stringErrors = new List<String>();
            for(int i=0; i< TaxaText.Count; i++)
            {
                TaxaText[i].Background = new SolidColorBrush(Colors.Black);
                DataText[i].Background = new SolidColorBrush(Colors.LightGray);
                if (string.IsNullOrEmpty(TaxaText[i].Text))
                {
                    taxaErrors.Add(TaxaText[i]);
                    stringErrors.Add("Empty input value.");
                    TaxaText[i].Background = new SolidColorBrush(Colors.LightSalmon);
                }
                if (string.IsNullOrEmpty(DataText[i].Text))
                {
                    taxaErrors.Add(DataText[i]);
                    stringErrors.Add("Empty input value.");
                    DataText[i].Background = new SolidColorBrush(Colors.LightSalmon);
                }
            }
            if (taxaErrors.Count > 0)
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
                App.f.C = new CharactersBlock();
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
    }
}