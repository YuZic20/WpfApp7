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

            
            foreach (string projpath in text)
            {
                XDocument docNode = XDocument.Load(projpath);
                var fnode = docNode.Descendants().First(p => p.Name.LocalName == "OutputPath");
                var lnode = docNode.Descendants().Last(p => p.Name.LocalName == "OutputPath");

                string name = System.IO.Path.GetFileName(projpath).Remove(System.IO.Path.GetFileName(projpath).Count()-7);

                string path = projpath.Remove(projpath.Count() - name.Count() - 8);

                PrgData Prog = new PrgData(name, path + fnode.Value, path + lnode.Value);

                AppList.Add(Prog);

            }

        }
    }
}
