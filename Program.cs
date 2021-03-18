using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace oc3_windows
{
    class Program
    {
        static void Main(string[] args)
        {
            //foreach (var n in Directory.GetFiles(Directory.GetCurrentDirectory()))
            //{
            //    Console.WriteLine(n);
            //}
            //List<string> authors = new List<string> { "Kevin", "Martin", "Ivan" };
            //List<string> departments = new List<string> { "Biology", "Physics", "Math" };
            //List<int> releaseYears = new List<int> { 2005, 2006, 2007 };
            //List<string> releasedIns = new List<string> { "Some Journal", "Science Conference", "Other magazine" };
            //string filesPath = @$"{Directory.GetCurrentDirectory()}\Library\LibraryFiles";
            //string interfacePath = $@"{Directory.GetCurrentDirectory()}\Library\LibraryInterface";

            Console.WriteLine("1. Удалить одиночный файл по названию\n2. Удалить группу файлов по заданному критерию");
            int key;
            if (int.TryParse(Console.ReadLine(), out key) && key >= 1 && key <= 2)
            {
                switch (key)
                {
                    case 1:
                        var fileList = Directory.GetFiles($"{Directory.GetCurrentDirectory()}\\Library\\LibraryFiles", "file*.txt", SearchOption.AllDirectories);
                        int counter = 1;
                        foreach (var file in fileList)
                        {
                            Console.WriteLine($"{counter++}. {Regex.Match(file, @"[\w]+\.txt$")}");
                        }
                        Console.Write("Номер файла: ");
                        if (int.TryParse(Console.ReadLine(), out key) && key >= 1 && key < counter)
                        {
                            string fileName = Regex.Match(fileList[key - 1], "[a-z0-9]+\\.txt$").ToString();
                            string parentFolder = Directory.GetParent(fileList[key - 1]).FullName;
                            File.Delete($"{Directory.GetCurrentDirectory()}\\Library\\LibraryInterface\\Author\\{File.ReadAllText($"{parentFolder}\\Author.txt")}\\{fileName}");
                            File.Delete($"{Directory.GetCurrentDirectory()}\\Library\\LibraryInterface\\Department\\{File.ReadAllText($"{parentFolder}\\Department.txt")}\\{fileName}");
                            File.Delete($"{Directory.GetCurrentDirectory()}\\Library\\LibraryInterface\\ReleaseYear\\{File.ReadAllText($"{parentFolder}\\ReleaseYear.txt")}\\{fileName}");
                            File.Delete($"{Directory.GetCurrentDirectory()}\\Library\\LibraryInterface\\ReleasedIn\\{File.ReadAllText($"{parentFolder}\\ReleasedIn.txt")}\\{fileName}");
                            Directory.Delete(parentFolder, true);
                            Console.WriteLine("Файл удален");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Удалить по критерию:\n1. Автор\n2. Направление\n3. Год написания\n4. Научный журнал");
                        if (int.TryParse(Console.ReadLine(), out key) && key >= 1 && key <= 4)
                        {
                            string creteria = "";
                            switch (key)
                            {
                                case 1:
                                    creteria = "Author";
                                    break;
                                case 2:
                                    creteria = "Department";
                                    break;
                                case 3:
                                    creteria = "ReleaseYear";
                                    break;
                                case 4:
                                    creteria = "ReleasedIn";
                                    break;
                            }
                            var directoryList = Directory.GetDirectories($"{Directory.GetCurrentDirectory()}\\Library\\LibraryInterface\\{creteria}");
                            counter = 1;
                            foreach (var dir in directoryList)
                            {
                                Console.WriteLine($"{counter++}. {Regex.Match(dir, @"[\w\s]+$")}");
                            }
                            if (int.TryParse(Console.ReadLine(), out key) && key >= 1 && key < counter)
                            {
                                string value = Regex.Match(directoryList[key - 1], @"[\w\s]+$").ToString();
                                foreach (var file in Directory.GetDirectories($"{Directory.GetCurrentDirectory()}\\Library\\LibraryFiles"))
                                {
                                    if (File.ReadAllText($"{file}\\{creteria}.txt") == value)
                                    {
                                        string filePath = Directory.GetFiles(file, $@"file*.txt").First().ToString();
                                        //string fileName = Regex.Match(filePath, $@"[\w]+\.txt$");
                                        string fileName = Regex.Match(filePath, @"[\w]+\.txt$").ToString();
                                        //string fileName = Regex.Match(file, $@"file[0-9]+\.txt$").ToString();
                                        //string parentFolder = Directory.GetParent(file).FullName;
                                        string parentFolder = file;
                                        File.Delete($@"{Directory.GetCurrentDirectory()}\Library\LibraryInterface\Author\{File.ReadAllText($"{parentFolder}\\Author.txt")}\{fileName}");
                                        File.Delete($@"{Directory.GetCurrentDirectory()}\Library\LibraryInterface\Department\{File.ReadAllText($"{parentFolder}\\Department.txt")}\{fileName}");
                                        File.Delete($@"{Directory.GetCurrentDirectory()}\Library\LibraryInterface\ReleaseYear\{File.ReadAllText($"{parentFolder}\\ReleaseYear.txt")}\{fileName}");
                                        File.Delete($@"{Directory.GetCurrentDirectory()}\Library\LibraryInterface\ReleasedIn\{File.ReadAllText($"{parentFolder}\\ReleasedIn.txt")}\{fileName}");
                                        Directory.Delete(parentFolder, true);
                                    }
                                    //        if (File.ReadAllText($"{file}\\{creteria}") == value)
                                    //        {
                                    //            string fileName = Regex.Match(directoryList[key - 1], "[a-z0-9]+\\.txt$").ToString();
                                    //            string parentFolder = Directory.GetParent(fileList[key - 1]).FullName;
                                    //            File.Delete($"{Directory.GetCurrentDirectory()}\\Library\\LibraryInterface\\Author\\{File.ReadAllText($"{parentFolder}\\Author.txt")}\\{fileName}");
                                    //            File.Delete($"{Directory.GetCurrentDirectory()}\\Library\\LibraryInterface\\Department\\{File.ReadAllText($"{parentFolder}\\Department.txt")}\\{fileName}");
                                    //            File.Delete($"{Directory.GetCurrentDirectory()}\\Library\\LibraryInterface\\ReleaseYear\\{File.ReadAllText($"{parentFolder}\\ReleaseYear.txt")}\\{fileName}");
                                    //            File.Delete($"{Directory.GetCurrentDirectory()}\\Library\\LibraryInterface\\ReleasedIn\\{File.ReadAllText($"{parentFolder}\\ReleasedIn.txt")}\\{fileName}");
                                    //            Directory.Delete(parentFolder, true);
                                    //        }
                                }
                            }
                        }
                        //Directory.GetFiles($"{Directory.GetCurrentDirectory()}\\Library\\");
                        break;
                }
            }
            
            Console.ReadKey();
        }
    }
}
