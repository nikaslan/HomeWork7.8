using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork7._8
{
    struct Employee
    {
        /// <summary>
        /// Создание нового сотрудника на основе считанной записи из файла базы
        /// </summary>
        /// <param name="databaseEntry">считанная из файла строка</param>
        public Employee (string databaseEntry)
        {
            string[] entryValues = databaseEntry.Split('#');

            this.Id = Convert.ToInt32(entryValues[0]);
            this.CreationTime = Convert.ToDateTime(entryValues[1]);
            this.FullName = entryValues[2];
            this.Age = Convert.ToInt32(entryValues[3]);
            this.Heigh = Convert.ToInt32(entryValues[4]);
            this.BirthDate = Convert.ToDateTime(entryValues[5]);
            this.BirthPlace = entryValues[6];
        }

        /// <summary>
        /// Создание нового сотрудника
        /// </summary>
        /// <param name="id">Порядковый номер в базе. Высчитывается по последней записи на момент создания +1</param>
        /// <param name="fullName">Ф.И.О сотрудника</param>
        /// <param name="heigh">Рост сотрудника</param>
        /// <param name="bDay">День рождения сотрудника</param>
        /// <param name="bPlace">Место рождения сотрудника</param>
        /// <param name="fileString">Строчка в файле базы</param>
        public Employee (int id, string fullName, int heigh, DateTime bDay, string bPlace)
        {
            this.Id = id;
            this.CreationTime = DateTime.Now;
            this.FullName = fullName;
            this.Age = Convert.ToInt32(Math.Round(((DateTime.Now - bDay).TotalDays / 365.25), 0));
            this.Heigh = heigh;
            this.BirthDate = bDay;
            this.BirthPlace = bPlace;
        }
        /// <summary>
        /// вывод информации о сотруднике в консоль
        /// </summary>
        public void PrintEmployee()
        {
            Console.WriteLine($"{this.Id,3}| {this.CreationTime,16:dd.MM.yyyy HH:mm}| {this.FullName,25}| {this.Age,8}| {this.Heigh,3}см| {this.BirthDate,10:dd.MM.yyyy}| {this.BirthPlace,20}");
        }
        /// <summary>
        /// Переводит экземпляр структуры типа Employee в строку, в формате для записи в файл базы 
        /// </summary>
        /// <returns></returns>
        public string ToFile()
        {
            string line = $"{this.Id}#{this.CreationTime:dd.MM.yyyy HH:mm}#{this.FullName}#{this.Age}#{this.Heigh}#{this.BirthDate:dd.MM.yyyy}#{this.BirthPlace}";
            return line;
        }

        #region Объявление автосвойств
        public int Id { get; private set; }
        /// <summary>
        /// Дата создания записи в базе
        /// </summary>
        private DateTime CreationTime { get; set; }
        /// <summary>
        /// Ф.И.О сотрудника
        /// </summary>
        private string FullName { get; set; }
        /// <summary>
        /// Возраст сотрудника
        /// </summary>
        private int Age { get; set; }
        /// <summary>
        /// Рост сотрудника
        /// </summary>
        private int Heigh { get; set; }
        /// <summary>
        /// Дата рождения сотрудника
        /// </summary>
        private DateTime BirthDate { get; set; }
        /// <summary>
        /// Место рождения сотрудника
        /// </summary>
        private string BirthPlace { get; set; }
        #endregion

    }
}
