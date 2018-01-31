using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayNonNull
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] array = Enumerable.Repeat("paru", 10).ToArray();

            

            // array
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FavIconBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static readonly List<string> s_Domains = new List<string>
                                                              {
                                                                  "google.com",
                                                                  "bing.com",
                                                                  "oreilly.com",
                                                                  "simple-talk.com",
                                                                  "microsoft.com",
                                                                  "facebook.com",
                                                                  "twitter.com",
                                                                  "reddit.com",
                                                                  "baidu.com",
                                                                  "bbc.co.uk"
                                                              };

        private async void clickbutton_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Task<Image>> tasks = s_Domains.Select(GetFavicon);
            Task<Image[]> allTask = Task.WhenAll(tasks);
            Image[] images = await allTask;
            foreach (var eachImage in images)
            {
                AddAFavicon(eachImage);
            }
        }

        private void AddAFavicon(Image imageControl)
       {
             m_WrapPanel.Children.Add(imageControl);
         }

        private async Task<Image> GetFavicon(string domain)
        {
            WebClient webClient = new WebClient();
            byte[] bytes = await webClient.DownloadDataTaskAsync("http://" + domain + "/favicon.ico");        
            return MakeImageControl(bytes);
        }

        private static Image MakeImageControl(byte[] bytes)
        {
            Image imageControl = new Image();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(bytes);
            bitmapImage.EndInit();
            imageControl.Source = bitmapImage;
            imageControl.Width = 16;
            imageControl.Height = 16;
            return imageControl;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("am responsive");
        }
    }
}

<Window x:Class="FavIconBrowser.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="MainWindow" Height="350" Width="525">
    <Grid Height="34" VerticalAlignment="Top">
        <Button Name="clickbutton" Content="click" Click="clickbutton_Click"/>
        <WrapPanel Name="m_WrapPanel" HorizontalAlignment="Left" Height="158" Margin="0,39,0,-163" VerticalAlignment="Top" Width="517"/>
        <Button Content="am responsive" HorizontalAlignment="Left" Height="44" Margin="34,236,0,-246" VerticalAlignment="Top" Width="197" Click="Button_Click"/>

    </Grid>
</Window>

