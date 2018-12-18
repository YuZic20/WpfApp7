using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
using System.Xml;
using System.Xml.Linq;


namespace WpfApp7
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string SPath = @"C:\Users\" + Environment.UserName + @"\source\repos";


        
        List<PrgData> AppList = new List<PrgData>();
        public MainWindow()
        {
            InitializeComponent();
            AppList = LoadList.GetExe(SPath);
            PrintApps();
        }
        
        private void PrintApps()
        {
            
            for (int i = 0; i < AppList.Count(); i++)
            {
                Button newBtn = new Button();

                newBtn.Content = AppList[i].Name;
                newBtn.Name = AppList[i].Name;
                newBtn.Height = 20;
                newBtn.Click += new RoutedEventHandler(Apk_Click);
                newBtn.Tag = AppList[i].Path;
                


                sp.Children.Add(newBtn);
                int a = 5;
            }

        }


        private void Apk_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;



            string path = bt.Tag.ToString();
            
            Process.Start(path);
        }
    }
}
