using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WpfApp7
{
    static class LoadList
    {
        public static List<PrgData> GetExe(string SPath)
        {
            List<PrgData> AppList = new List<PrgData>();
            List<string> text = new List<string>();
            List<string> exes = new List<string>();


            text = ApplyAllFiles(SPath); //Directory.GetFiles(SPath, "*.csproj", SearchOption.AllDirectories).ToList();

            foreach (string projpath in text)
            {
                try
                {
                    XDocument docNode = XDocument.Load(projpath);
                    var fnode = docNode.Descendants().First(p => p.Name.LocalName == "OutputPath");
                    var lnode = docNode.Descendants().Last(p => p.Name.LocalName == "OutputPath");

                    string name = System.IO.Path.GetFileNameWithoutExtension(projpath);//.Remove(System.IO.Path.GetFileName(projpath).Count() - 7);

                    string path = projpath.Remove(projpath.Count() - name.Count() - 7);

                    string DirectoryPath = System.IO.Directory.GetParent(path).ToString();

                    DirectoryPath = System.IO.Directory.GetParent(DirectoryPath).ToString();


                    string Fpath = path + fnode.Value;

                    string Lpath = path + lnode.Value;

                    exes = GetListOfExes(Fpath, Lpath);
                    string LatestExePath;
                    if (exes.Count() != 0)
                    {
                        LatestExePath = GetRecentExe(exes);
                        PrgData Prog = new PrgData(name, LatestExePath, DirectoryPath);

                        AppList.Add(Prog);

                    }
                }
                catch
                {
                    // error nodu
                }

               

                







                /*
                int Select = 0;
                int SelectErr = 0;
                DateTime lastModifiedF;
                DateTime lastModifiedL;
                try
                {
                    lastModifiedF = System.IO.File.GetLastWriteTime(Fpath);
                }
                catch (Exception)
                {
                    SelectErr++;
                    Select = 2;
                }
                try
                {
                    lastModifiedL = System.IO.File.GetLastWriteTime(Lpath);
                }
                catch (Exception)
                {
                    SelectErr++;
                    Select = 1;
                }
                */




                

            }
            return AppList;

        }
        static private List<string> GetListOfExes (string Fpath, string Lpath)
        {
            List<string> exeF = new List<string>();
            List<string> exeL = new List<string>();

            Fpath = Fpath.Remove(Fpath.Count() - 1);

            Lpath = Lpath.Remove(Lpath.Count() - 1);

            List<string> exes = new List<string>();

            if (Directory.Exists(Fpath) == true)
            {
                exeF = Directory.GetFiles(Fpath, "*.exe", SearchOption.TopDirectoryOnly).ToList();
            }
            if (Directory.Exists(Lpath) == true)
            {
                exeL = Directory.GetFiles(Lpath, "*.exe", SearchOption.TopDirectoryOnly).ToList();
            }

            exes.AddRange(exeF);
            exes.AddRange(exeL);

            return exes;


        }
        static private string GetRecentExe (List<string> exesPath)
        {
            DateTime lastwrite = System.IO.File.GetLastWriteTime(exesPath[0]);
            string path = exesPath[0];

            foreach (string exePath in exesPath)
            {
                if (System.IO.File.GetLastWriteTime(exePath) > lastwrite)
                {
                    lastwrite = File.GetLastWriteTime(exePath);
                    path = exePath;
                }
            }
            return path;
        }

        static List<string> ApplyAllFiles(string folder)
        {
            List<string> paths = new List<string>();
            List<string> addPaths = new List<string>();
            foreach (string file in Directory.GetFiles(folder, "*.csproj", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    paths.Add(file);
                }
                catch
                {
                    
                    // error na souboru
                }
                
            }
            foreach (string subDir in Directory.GetDirectories(folder))
            {
                try
                {
                    addPaths = ApplyAllFiles(subDir);
                    paths.AddRange(addPaths);
                }
                catch
                {
                    // error na ceste
                }
            }
            return paths;
        }
    }
}
