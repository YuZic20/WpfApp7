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

        //string SPath = @"C:\Users\W0olf\source\";
        //string SPath = @"C:\Users\" + Environment.UserName + @"\source\repos";
        string SPath;
        int action = 2;
        int LastApkClickedToDelete;




        List<PrgData> AppList = new List<PrgData>();
        public MainWindow()
        {
            InitializeComponent();
            ChangeButtonColor(action);
            SButton.Content = "\uD83D\uDD0D";


        }
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateApps();
        }

        private void UpdateApps()
        {
            SPath = PathInput.Text;
            try
            {
                AppList = new List<PrgData>();
                
                AppList = LoadList.GetExe(SPath);
                PrintApps();
            }
            catch(Exception)
            {
                ErrMsg.Text = "nevalidní cesta pro vyhledání";
            }
            
        }
        private void UpdateAppsWithPrewPath()
        {
            AppList = new List<PrgData>();
            AppList = LoadList.GetExe(SPath);
            PrintApps();
        }

        private void PrintApps()
        {
            sp.Children.Clear();
            for (int i = 0; i < AppList.Count(); i++)
            {
                Button newBtn = new Button();

                newBtn.Content = AppList[i].Name + "   |  Path: " + AppList[i].DirectoryPath;
                newBtn.Name = AppList[i].Name;
                newBtn.Height = 20;
                newBtn.Click += new RoutedEventHandler(Apk_Click);
                newBtn.Tag = i;
                


                sp.Children.Add(newBtn);

            }
           
            scroll.UpdateLayout();
        }


        private void Apk_Click(object sender, RoutedEventArgs e)
        {
            ErrMsg.Text = "";
            Button bt = (Button)sender;

            int Id = Int32.Parse(bt.Tag.ToString());

            PrgData ButtonData = AppList[Id];
            if (action == 1)
            {
                LastApkClickedToDelete = Id;
                PopUp.Visibility = Visibility.Visible;
                //delete
            }else if(action == 2)
            {
                StartApk(ButtonData);
            }
            else if(action == 3)
            {
                CopyApk(ButtonData);
            }
            
        }
        private void CopyApk(PrgData sender)
        {
            try
            {
                System.IO.Directory.CreateDirectory(CopyPathInput.Text);
                System.IO.Directory.Move(sender.DirectoryPath, CopyPathInput.Text + "/" + sender.Name);
            }
            catch (Exception)
            {
                ErrMsg.Text = "nevalidní cesta pro destinaci projektu";
            }
            UpdateAppsWithPrewPath();
        }
        private void StartApk(PrgData sender)
        {
            Process.Start(sender.Path);
        }
        private void DeleteApk(PrgData sender)
        {
            if (System.IO.Directory.Exists(sender.DirectoryPath))
            {
                try
                {
                    string ParentPath = System.IO.Directory.GetParent(sender.DirectoryPath).ToString();
                    System.IO.Directory.Delete(ParentPath , true);
                }

                catch (Exception e)
                {
                    ErrMsg.Text = "Složku se nepodařilo celu smazat. důvod: " + e.Message;
                }
                UpdateAppsWithPrewPath();
            }
        }
        private void ButtonActionDelete_Click(object sender, RoutedEventArgs e)
        {
            action = 1;
            ChangeButtonColor(action);
        }
        private void ButtonActionPlay_Click(object sender, RoutedEventArgs e)
        {
            action = 2;
            ChangeButtonColor(action);
        }
        private void ButtonActionCopy_Click(object sender, RoutedEventArgs e)
        {
            action = 3;
            ChangeButtonColor(action);
        }
        private void ChangeButtonColor(int ButtonToHigligt)
        {

            if (ButtonToHigligt == 1)
            {
                ActionB1.Background = Brushes.LightSkyBlue;
                ActionB2.Background = Brushes.AliceBlue;
                ActionB3.Background = Brushes.AliceBlue;

            }
            else if (ButtonToHigligt == 2)
            {
                ActionB1.Background = Brushes.AliceBlue;
                ActionB2.Background = Brushes.LightSkyBlue;
                ActionB3.Background = Brushes.AliceBlue;

            }
            else if (ButtonToHigligt == 3)
            {
                ActionB1.Background = Brushes.AliceBlue;
                ActionB2.Background = Brushes.AliceBlue;
                ActionB3.Background = Brushes.LightSkyBlue;

            }
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            PopUp.Visibility = Visibility.Hidden;
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
           
            DeleteApk((PrgData)AppList[LastApkClickedToDelete]);
            PopUp.Visibility = Visibility.Hidden;
        }

        private void PathInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
