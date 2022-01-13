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
            this.Heigh = heigh;
            this.BirthDate = bDay;
            this.BirthPlace = bPlace;
        }
        /// <summary>
        /// вывод информации о сотруднике в консоль
        /// </summary>

        #region Объявление автосвойств
        public int Id { get; private set; }
        /// <summary>
        /// Дата создания записи в базе
        /// </summary>
        public DateTime CreationTime { get; private set; }
        /// <summary>
        /// Ф.И.О сотрудника
        /// </summary>
        public string FullName { get; private set; }
        /// <summary>
        /// Возраст сотрудника
        /// </summary>
        public int Age() 
        {
            return Convert.ToInt32(Math.Round(((DateTime.Now - bDay).TotalDays / 365.25), 0));
        }
        /// <summary>
        /// Рост сотрудника
        /// </summary>
        public int Heigh { get; private set; }
        /// <summary>
        /// Дата рождения сотрудника
        /// </summary>
        public DateTime BirthDate { get; private set; }
        /// <summary>
        /// Место рождения сотрудника
        /// </summary>
        public string BirthPlace { get; private set; }
        #endregion

    }
}
