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
                TaxaText.Add(tbTaxa);

                TextBox tbData = new TextBox();
                tbData.Name = "data" + i;
                tbData.AcceptsReturn = true;
                Thickness m2 = tbData.Margin;
                m2.Left = 10;
                tbData.Margin = m2;
                tbData.Width = 100;
                tbData.Height = 60;
                DataText.Add(tbData);

                StackPanel s = new StackPanel();
                s.Name = "SequencePanel" + i;
                s.Orientation = Orientation.Horizontal;
                s.Children.Add(tbTaxa);
                s.Children.Add(tbData);
                // s.Children.Add();
                SeqFrame.Children.Add(s);
            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            ////add data to general nexus file and pass tp Page3.xaml
            //App.f.C = new CharactersBlock();
            //List<String> TaxaStrings = new List<String>();
            //List<String> DataStrings = new List<String>();
            //foreach (TextBox s in TaxaText)
            //{
            //    TaxaStrings.Add(s.Text);
            //}
            //foreach (TextBox s in DataText)
            //{
            //    DataStrings.Add(s.Text);
            //}
            //for (int i = 0; i < numOfTaxaPanels; i++)
            //{
            //    App.f.C.sequences.Add(new Sequence(TaxaStrings[i], DataStrings[i]));
            //}

            //NavigationService nav = NavigationService.GetNavigationService(this);
            //nav.Navigate(new Page3());
        }
    }
}