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
    public sealed partial class PreviewFile : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.f.C = new CharactersBlock();
            App.f.C = e.Parameter as CharactersBlock;
            
        }
        public PreviewFile()
        {
            this.InitializeComponent();
            NexusWriter w = new NexusWriter(App.f);
            List<string> content =w.PreprareNexusString();
            for(int i=0; i< content.Count; i++)
            {
                txtNexus.Text += content[i];
            }
            HelpPopup.TextWrapping = TextWrapping.Wrap;
            HelpPopup.Text = "This page allows you to view what the Nexus files will look like before downloading said file. \n";
            HelpPopup.Text += "\n Click 'Next' when ready to download your Nexus file. \n";


        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SequenceDataInput), App.f.C);
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Page3), App.f.C);
        }
        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            if (!StandardPopup.IsOpen) { StandardPopup.IsOpen = true; }
        }
        private void ClosePopupClicked(object sender, RoutedEventArgs e)
        {
            // if the Popup is open, then close it 

            if (StandardPopup.IsOpen) { StandardPopup.IsOpen = false; }
        }
    }
}
