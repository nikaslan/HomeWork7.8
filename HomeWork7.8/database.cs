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
            this.databaseSize = 0;
            this.employees = new Employee[0];
        }

        /// <summary>
        /// считывание данных из базы во временную рабочую копию базы
        /// </summary>
        public bool LoadDatabase()
        {
            bool databaseLoaded = false;
            if (this.DatabaseExistsCheck())
            {
                /// если файл существует, то мы его считываем. Если он был пуст или был только что создан - то мы должны это проверить
                /// считывая, мы записываем данные во временный стринг
                /// по количеству элементов во временном стринге (когда считали весь файл) мы делаем ресайз массива employees
                /// далее переписываем считанные данные построчно в переменные типа Employee. надо не забыть парсинг (реализован в структуре Employee)
                /// 
                this.databaseSize = SetDatabaseSize(this.databasePath);

                // если в файле больше чем одна строчка, то увеличиваем массив репозитория до количества строчек
                if (databaseSize > 0)
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
        /// <summary>
        /// Вывод содержимого репозитория в консоль
        /// </summary>
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
        /// <summary>
        /// Запись нового значения Employee в файл
        /// ЭТОТ МЕТОД Я ХОЧУ ИСПОЛЬЗОВАТЬ ДЛЯ ЗАПИСИ КАК НОВОЙ СТРОКИ В ФАЙЛ, ТАК И ДЛЯ ПЕРЕЗАПИСИ УЖЕ СУЩЕСТВУЮЩЕЙ
        /// </summary>
        /// <param name="newEntry">экземпляр Employee с новыми данными</param>
        private void UpdateDatabaseEntry(Employee newEntry)
        {
            Console.WriteLine("\nВ справочник будет внесена следующая запись:");
            Console.WriteLine("\nID | Добавлен        | Имя                      | Возраст | Рост | Дата рож. | Место рождения");
            newEntry.PrintEmployee();

            Console.WriteLine("\nЕсли хотите подтвердить внесение записи, нажмите Enter. Для отмены - нажмите Esc.");
            while (true)
            {

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Enter)
                { 
                    if (this.employees[databaseSize - 1].Id < newEntry.Id)
                    { 
                        using (StreamWriter sw = new StreamWriter(databasePath, true, Encoding.UTF8))
                        {
                            sw.WriteLine(newEntry.ToFile()); //добавление новой записи в файл
                        }
                        databaseSize++;
                        Array.Resize(ref employees, databaseSize);
                        employees[databaseSize - 1] = newEntry;
                    }

                    Console.WriteLine("\nЗапись внесена");
                    break;
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                } // выходим без добавления изменений
                // если нажали на что-то, кроме Enter или Esc, то это не будет принято 
            }
        }

        private Employee NewEmployee()
        {
            Employee newEmployee;

            // ввод имени
            Console.WriteLine("Введите имя нового сотрудника. *Только первые 25 символов имени будут сохранены:");
            string newName = Console.ReadLine();
            if (newName.Length > 25) newName = newName.Substring(0, 25);

            // ввод роста
            Console.WriteLine("\nВведите рост нового сотрудника в сантимертах:");
            int newHeigh;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out newHeigh))
                {
                    if (newHeigh < 50 || newHeigh > 250)
                    {
                        Console.WriteLine("Введенный рост должен быть между 50 см и 250 см");
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный данные о росте.");
                }

            }

            // ввод дня рождения
            Console.WriteLine("\nВведите дату рождения нового сотрудника в формате дд.мм.гггг:");
            DateTime newBirthDate;
            while (true)
            {

                if (DateTime.TryParse(Console.ReadLine(), out newBirthDate))
                {

                    if (newBirthDate > DateTime.Now)
                    {
                        Console.WriteLine("Дата рождения должна быть в прошлом.");
                        continue;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Некорректный формат времени.");
                }

            }

            // Ввод города рождения
            Console.WriteLine("\nВведите место рождения нового сотрудника:");
            string newBirthPlace = Console.ReadLine();
            if (newBirthPlace.Length > 20) newBirthPlace = newBirthPlace.Substring(0, 20);
            int newID;
            // Если база пустая - у нового элемента ID = 1. Если нет - то ID последнего элемента +1
            if (databaseSize > 0)
            {
                newID = this.employees[databaseSize - 1].Id + 1;
            }
            else { newID = 1; }
            
            newEmployee = new Employee(newID, newName, newHeigh, newBirthDate, newBirthPlace);

            return newEmployee;
        }

        //private Employee EditEmployee(Employee oldEntry)
        //{
        //    Employee newEmployee;




        //    return newEmployee;
        //}
        /// <summary>
        /// Метод добавления нового сотрудника, который можно вызывать извне 
        /// </summary>
        public void AddEmployee()
        {
            UpdateDatabaseEntry(NewEmployee());
        }

        public void ViewAndEditEmployee()
        {
            Employee existingEmployeeEntry = new Employee();
            bool employeeFound = false;
            
            
            while (true)
            {
                Console.WriteLine("Для просмотра сведений о сотруднике введите его ID.");
                if (Int32.TryParse(Console.ReadLine(), out int employeeID)) 
                {
                    foreach (Employee employee in employees) // сравниваем введенный ID со всем ID сотрудников, которых считали в репозиторий
                    {
                        if (employeeID == employee.Id)
                        {
                            existingEmployeeEntry = employee;
                            employeeFound = true; // нашли совпадение
                        }
                    }

                    if (employeeFound) // если мы нашли сотрудника с нужным ID
                    {
                        Console.Clear();
                        Console.WriteLine("ID | Добавлен        | Имя                      | Возраст | Рост | Дата рож. | Место рождения");
                        existingEmployeeEntry.PrintEmployee();
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Пользователя с таким ID нет в базе.");
                    }
                }
                Console.WriteLine("\nВведен некорректный ID.");
                Console.WriteLine("Нажмите Esc для выхода. Нажмите любую клавишу, чтобы ввести другой ID.");
                var keyPressed = Console.ReadKey(true);
                if (keyPressed.Key == ConsoleKey.Escape) { return; } // выходим если пользователь нажал Esc
                Console.Clear();
            }

            Console.WriteLine("\nЕсли хотите внести изменения, нажмите Enter. Для возврата в предыдущее меню - нажмите Esc.");
            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Enter)
                {
                    Console.WriteLine("Функционал пока не реализован. Нажмите любую кнопку для выхода.");
                    //UpdateDatabaseEntry(EditEmployee(existingEmployeeEntry));
                    Console.ReadKey(true);
                    break;
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }

            }
        }
    }
}
