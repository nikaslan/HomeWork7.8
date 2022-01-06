using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HomeWork7._8
{
    struct Database
    {
        /// <summary>
        /// Массив переменных типа Employee
        /// </summary>
        private Employee[] employees;
        /// <summary>
        /// путь до файла базы
        /// </summary>
        private string databasePath;

        /// <summary>
        /// Создание временной рабочей копии базы данных
        /// </summary>
        /// <param name="filePath"></param>
        public Database(string filePath)
        {
            this.databasePath = filePath;
            this.employees = new Employee[1];
        }
        /// <summary>
        /// считывание данных из базы во временную рабочую копию базы
        /// </summary>
        public void LoadDatabase()
        {
            if(DatabaseExistsCheck())
            {
                /// если файл существует, то мы его считываем. Если он был пуст или был только что создан - то мы должны это проверить
                /// считывая, мы записываем данные во временный стринг
                /// по количеству элементов во временном стринге (когда считали весь файл) мы делаем ресайз массива employees
                /// далее переписываем считанные данные построчно в переменные типа Employee. надо не забыть парсинг (реализован в структуре Employee)
            }
            
        }

        /// <summary>
        /// Проверка наличия файла. Создаем новый файл при необходимости
        /// </summary>
        /// <returns>Возвращает существует ли файл</returns>
        public bool DatabaseExistsCheck()
        {
            bool fileExists = File.Exists(this.databasePath);
            while (true)
            {
                if (!fileExists)
                {
                    Console.WriteLine($"Файла базы по пути {databasePath} не существует. Нажмите Enter для создания файла. Нажмите Esc для отмены.");
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.Enter)
                    {
                        File.Create(databasePath);
                        Console.WriteLine("Файл был создан. Нажмите любую кнопку для продолжения.");
                        fileExists = true;
                        Console.ReadKey(true);
                        break;
                    }
                    else if (key == ConsoleKey.Escape)
                    {
                        break;
                    }
                    Console.Clear();
                    continue;
                }
                break;
            }
            return fileExists;

        }
    }
}
