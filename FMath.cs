namespace FoxUtilsLib
{
    /// <summary>
    /// Простейшие математические функции
    /// </summary>
    public static class FMath
    {
        /// <summary>
        ///  Ограничивает значение в определённых границах
        /// </summary>
        ///  <param name="value">Значение для ограничения</param>
        ///  <param name="upper_limit">Верхняя граница для значения</param>
        ///  <param name="lower_limit">Нижняя граница для значения</param> 
        /// <returns>
        ///  <paramref name="value"/>: если между <paramref name="lower_limit"/> и <paramref name="upper_limit"/>.
        ///   <para>
        ///  <paramref name="lower_limit"/>: если меньше чем <paramref name="lower_limit"/>
        ///   </para>
        ///  <paramref name="upper_limit"/>: если больше чем <paramref name="upper_limit"/>
        /// </returns>
        public static T Constrain<T>(T value, T upper_limit, T lower_limit) where T : IComparable<T>
        {
            // Лучше не использовать это для не-числовых типов лол
            return value.CompareTo(upper_limit) > 0 ? upper_limit : (value.CompareTo(upper_limit) < 0 ? lower_limit : value);
        }

        /// <summary>
        /// Переопределяет число с одного промежутка к другому. <para></para> Значение <paramref name="from_low"/> переопределяется к <paramref name="to_low"/>, значение <paramref name="from_high"/> к <paramref name="to_high"/>, значения между ними к значениям между ними и т.д.
        /// </summary>
        /// <param name="value">Значение для переопределения</param>
        /// <param name="from_low">Нижняя граница исходного промежутка</param>
        /// <param name="from_high">Верхняя граница исходного промежутка</param>
        /// <param name="to_low">Нижняя граница нужного промежутка</param>
        /// <param name="to_high">Верхняя граница нужного промежутка</param>
        /// <![CDATA[https://www.arduino.cc/reference/en/language/functions/math/map/]]>
        public static long Map(long value, long from_low, long from_high, long to_low, long to_high)
        {
            return (value - from_low) * (to_high - to_low) / (from_high - from_low) + to_low;
        }
    }
}