using Shared_Code;
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
            string x = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();
            int nCharNum;
          // var dataSelect = (App.f.C.Inpj) Enum.Parse(typeof(App.f.C.InputDataType), x);
            try {
                 nCharNum = Int32.Parse(charNum.Text);
            }
            catch(Exception)
            {
                nCharNum = 0;
                stringErrors.Add("Must enter an integer into the number of characters per matrix field.");
            }
            if (GapChar.Text.Length == 0 || GapChar.Text.Length>1)
            {
                stringErrors.Add("Must enter one character into the GAP character field.");
            }
            if (MissingChar.Text.Length == 0 || MissingChar.Text.Length > 1)
            {
                stringErrors.Add("Must enter one character into the GAP character field.");
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
            }
        }
    }
}
