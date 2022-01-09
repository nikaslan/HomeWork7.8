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
        /// Размер базы данных (количество строчек в файле)
        /// </summary>
        private int databaseSize;

        /// <summary>
        /// Определяем размер базы данных
        /// </summary>
        private int SetDatabaseSize(string filePath)
        {
            int i = 0;
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    sr.ReadLine();
                    i++;
                }
            }
            return i;         
        }

        /// <summary>
        /// Создание временной рабочей копии базы данных
        /// </summary>
        /// <param name="filePath"></param>
        public Database(string filePath)
        {
            this.databasePath = filePath;
            this.databaseSize = 1;
            this.employees = new Employee[1];
        }
            
        /// <summary>
        /// считывание данных из базы во временную рабочую копию базы
        /// </summary>
        public bool LoadDatabase()
        {
            bool databaseLoaded = false;
            if(this.DatabaseExistsCheck())
            {
                /// если файл существует, то мы его считываем. Если он был пуст или был только что создан - то мы должны это проверить
                /// считывая, мы записываем данные во временный стринг
                /// по количеству элементов во временном стринге (когда считали весь файл) мы делаем ресайз массива employees
                /// далее переписываем считанные данные построчно в переменные типа Employee. надо не забыть парсинг (реализован в структуре Employee)
                /// 
                this.databaseSize = SetDatabaseSize(this.databasePath);
                
                // если в файле больше чем одна строчка, то увеличиваем массив репозитория до количества строчек
                if (databaseSize > 1)
                {
                    Array.Resize(ref this.employees, databaseSize);
                }
                // читаем базу построчно и записываем непустые строчки в масси Employee[]
                using (StreamReader sr = new StreamReader(this.databasePath))
                {
                    for (int i = 0; i < this.databaseSize; i++)
                    {
                        string tempLine = sr.ReadLine(); // считываем очередную строчку из файла
                        // если считанная строчка не пустая, то запускаем создание нового Employee и записываем в наш репозиторий
                        if (tempLine != "")
                        {
                            this.employees[i] = new Employee(tempLine);
                        }
                    }
                }

                databaseLoaded = true;
            }

            return databaseLoaded;
        }

        public void PrintDatabaseToConsole()
        {
            Console.WriteLine("Список сотрудников:\n");
            Console.WriteLine("ID | Добавлен        | Имя                      | Возраст | Рост | Дата рож. | Место рождения");

            bool isEmpty = true; // проверяем существуют ли записи в репозитории. Записей не будет если файл был пуст или был только что создан

            foreach (Employee employee in this.employees)
            {
                employee.PrintEmployee();
                isEmpty = false;
            }
            if (isEmpty)
            {
                Console.WriteLine("База сотрудников пуста");
            }

        }

        /// <summary>
        /// Проверка наличия файла. Создаем новый файл при необходимости
        /// </summary>
        /// <returns>Возвращает существует ли файл</returns>
        private bool DatabaseExistsCheck()
        {
            bool fileExists = File.Exists(this.databasePath);
            while (true)
            {
                if (!fileExists)
                {
                    Console.WriteLine($"Файла базы по пути {databasePath} не существует. Нажмите Enter для создания файла. Нажмите Esc для отмены.");
                    var key = Console.ReadKey(true).Key;

                    // Если нажали Enter - будет создан новый файл по указанному пути
                    if (key == ConsoleKey.Enter)
                    {
                        File.Create(databasePath);
                        Console.WriteLine("Файл был создан. Нажмите любую кнопку для продолжения.");
                        fileExists = true;
                        Console.ReadKey(true);
                        break;
                    }
                    // если нажали Esc - выйдем из функции проверки без создания файла
                    else if (key == ConsoleKey.Escape)
                    {
                        break;
                    }
                    Console.Clear();
                    continue; // если нажали что-то кроме Enter или Esc - то будет снова запрошено нажатие
                }
                break;
            }
            return fileExists;

        }

    }
}
