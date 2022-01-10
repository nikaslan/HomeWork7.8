using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace HomeWork7._8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.GetEncoding("Cyrillic");
            // объявление переменных
            string filepath = "catalogue.txt";  // путь до файла, в котором хранится база сотрудников
            bool dataBaseNeedsReload = true;
            Database repository = new Database(filepath);

            while (true)
            {
                Console.WriteLine("Программа-справочник \"Сотрудники\".");

                if (dataBaseNeedsReload) // считываем базу только вначале программы и по необходимости
                {
                    if (!repository.LoadDatabase())
                    {
                        Console.WriteLine("Невозможно продолжить без файла базы. Нажмите любую кнопку для завершения программы.");
                        Console.ReadKey();
                        break;
                    }
                    dataBaseNeedsReload = false;
                }
                
                Console.WriteLine("Нажмите 1 для вывода существующих записей\nНажмите 2 для поиска определенной записи\nНажмите 3 для добавления записи\nНажмите Esc для выхода из программы.");

                var key = Console.ReadKey(true).Key;
                if (((char)key) == '1')
                {
                    Console.Clear();
                    repository.PrintDatabaseToConsole();
                    Console.WriteLine("\nНажмите любую кнопку для возврата на главный экран.");
                    Console.ReadKey(true);
                    Console.Clear();
                    continue;
                }
                else if (((char)key) == '2')
                {
                    Console.Clear();
                    repository.ViewAndEditEmployee();
                    //Console.WriteLine("\nНажмите любую кнопку для возврата на главный экран.");
                    //Console.ReadKey(true);
                    Console.Clear();
                    continue;
                }
                else if (((char)key) == '3')
                {
                    Console.Clear();
                    repository.AddEmployee();
                    Console.WriteLine("\nНажмите любую кнопку для возврата на главный экран.");
                    Console.ReadKey(true);
                    Console.Clear();
                    continue;
                }
                else if (key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Console.WriteLine("Программа закрывается..");
                    Thread.Sleep(1000);
                    break;
                }
                Console.Clear();
                // если нажали на что-то, кроме 1,2 или Esc, то это не будет принято 
            }

        }
    }
}
