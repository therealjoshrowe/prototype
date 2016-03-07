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
    public sealed partial class Page3 : Page
    {
        public Page3()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            NexusWriter w = new NexusWriter(App.f);
            w.WriteToFile();
        }

        private void appBarButton1_Click(object sender, object e)
        {

        }

        private void appBarButton2_Click(object sender, object e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void appBarButton3_Click(object sender, object e)
        {
            this.Frame.Navigate(typeof(CharactersPage));
        }

        private void appBarButton4_Click(object sender, object e)
        {
            this.Frame.Navigate(typeof(SequenceDataInput));
        }

        private void appBarButton5_Click(object sender, object e)
        {
            this.Frame.Navigate(typeof(Page3));
        }
    }
}
