using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxUtilsLib
{
    /// <summary>
    /// Простейшие методы для работы с русским языком
    /// </summary>
    public class FLanguage
    {
        // Пожалуйста, кто-нибудь исправьте этот нонсенс ниже.
        /// <summary>
        /// Функция для склонения слова с числом
        /// </summary>
        /// <param name="number">Исходное число</param>
        /// <param name="nominativ">Именительный падеж слова, например, День</param>
        /// <param name="genetiv">Родительный падеж слова, например, Дня</param>
        /// <param name="plural">Множественное число, например, Дней</param>
        /// <returns>Нужное слово в склонении с указанным числом</returns>
        public static string NumDeclension(int number, string nominativ, string genetiv, string plural)
        {
            string[] titles = new[] { nominativ, genetiv, plural };
            int[] cases = new[] { 2, 0, 1, 1, 1, 2 };
            return $"{number} {titles[number % 100 > 4 && number % 100 < 20 ? 2 : cases[(number % 10 < 5) ? number % 10 : 5]]}";
        }
    }
}
