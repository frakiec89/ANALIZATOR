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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            a.Text = "Ledetect";
            p.Text = "da293d99-95aa-4254-8c7e-c9e9270ef3f7";
            s1.Text = "415";
            s2.Text = "415";


        }

        private async void  b_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var servises = new List< service>
                {
                new service { serviceCode= Convert.ToInt32(s1.Text) },
                new service { serviceCode= Convert.ToInt32(s2.Text) }
                };

                var inAnaliz = new ApiPost { patient = p.Text, services = servises };


                var s = Api.InPostAsync(inAnaliz, a.Text);
                MessageBox.Show(s);
            }
            catch (WebException ex)
            {
                WebResponse response = ex.Response;

                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                      MessageBox.Show( reader.ReadToEnd());
                    }
                }

            }
           
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string content = string.Empty;

                var s = Api.OutGEtAsync( a.Text);

                var p = s.patient;
                if (s != null && s.patient!=null && s.services!=null)
                {
                    foreach (var item in s.services)
                    {
                        content += p + " - " + "CoDe -" + item.code.ToString() + " status - " + item.result + "\n";

                    }
                    MessageBox.Show(content);
                }
            }
            catch (WebException ex)
            {
                WebResponse response = ex.Response;

                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        MessageBox.Show(reader.ReadToEnd());
                    }
                }

            }
        }
    }
}
