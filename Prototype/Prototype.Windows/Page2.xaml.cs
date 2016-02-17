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
using Shared_Code;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Prototype
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page2 : Page
    {
        public Page2()
        {
            this.InitializeComponent();
            foreach (String s in App.f.T.taxa)
            {
                StackPanel stack = new StackPanel() { Name = "Stack" + s,
                                                      Orientation = new Orientation(),
                                                      Margin = new Thickness(0,10,0,0)
                                                      };
                stack.Children.Add(new TextBlock() { Name = "textBlock" + s});
                stack.Children.Add(new TextBox() { Name = "textBox" + s});
                panel.Children.Add(stack);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            /*App.f.C = new CharactersBlock();
            //String s = textBox.Text.ToString();
            String[] array = s.Split(new char[] { '\n' });
            foreach (String seq in array)
            {
                String[] array2 = seq.Split(new char[] { ':' });
                App.f.C.sequences.Add(new Sequence(array2[0], array2[1]));
            }*/
            this.Frame.Navigate(typeof(Page3));
        }
    }
}
