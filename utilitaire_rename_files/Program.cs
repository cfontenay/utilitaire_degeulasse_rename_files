using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace utilitaire_rename_files
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo info = new DirectoryInfo(Directory.GetCurrentDirectory());
            Console.WriteLine("Décroissant ?\n");
            FileInfo[] fileInfos = info.GetFiles();
            if (Console.ReadLine().ToUpper().Contains("O"))
            {
                fileInfos = fileInfos.OrderByDescending(p => p.CreationTime).ToArray();
            }
            else
            {
                fileInfos = fileInfos.OrderBy(p => p.CreationTime).ToArray();
            }

            var currentName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            int index = 0;
            var id = Guid.NewGuid().ToString();
            foreach (FileInfo fileInfoTempo in fileInfos)
            {
                if (fileInfoTempo.FullName != currentName)
                {
                    File.Move(fileInfoTempo.FullName, fileInfoTempo.Directory.FullName + "\\" + id + index + fileInfoTempo.Extension);
                    index++;
                }
                    
            }
            index = 0;
            foreach (FileInfo fileInfo in fileInfos)
            {
                if (fileInfo.FullName != currentName)
                {                    
                    File.Move(fileInfo.Directory.FullName + "\\" + id + index + fileInfo.Extension, fileInfo.Directory.FullName + "\\" + FindName(index) + fileInfo.Extension);
                    index++;
                }
                
            }
            
        }
        static string FindName(int value)
        {
            int n = (('Z' - 'A') + 1);
            List<int> restes = new List<int>();
            if (value == 0)
            {
                restes.Add(0);
            }
            while (value != 0)
            {
                restes.Add(value % n);
                value /= n;
            }
            while (restes.Count() < 8)
            {
                restes.Add(0);
            }
            restes.Reverse();
            return new string(restes.Select(x => (char)('A' + x)).ToArray());
        }
    }
}
