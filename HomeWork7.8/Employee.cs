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
        public Employee (int id, string fullName, int heigh, DateTime bDay, string bPlace)
        {
            this.Id = id;
            this.FullName = fullName;
            this.Heigh = heigh;
            this.BirthPlace = bPlace;
            this.BirthDate = bDay;
            this.Age = Convert.ToInt32(Math.Round(((DateTime.Now - bDay).TotalDays / 365.25), 0));
            this.CreationTime = DateTime.Now;
        }

        #region Объявление автосвойств
        private int Id { get; set; }
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
