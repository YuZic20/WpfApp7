using System;
using System.Collections.Generic;
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
        string SPath = @"D:\Hynek";
        List<PrgData> AppList = new List<PrgData>();
        public MainWindow()
        {
            InitializeComponent();
            GetExe();
        }
        private void GetExe()
        {
            List<string> text = new List<string>(); 
            text = Directory.GetFiles(SPath, "*.csproj", SearchOption.AllDirectories).ToList();

            XDocument doc = new XDocument();
            
            foreach (string projpath in text)
            {
                //doc.Load(projpath);
                //XmlNode node = doc.DocumentElement.SelectSingleNode("/Project/PropertyGroup/OutputPath");
                string a = "";
            }/*
            XDocument doc = XDocument.Load("XMLFile1.xml");
            var authors = doc.Descendants("Author");
            foreach (var author in authors)
            {
                Console.WriteLine(author.Value);
            }*/
        }
    }
}
