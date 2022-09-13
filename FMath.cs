namespace FoxUtilsLib
{
    public static class FMath
    {
        /// <summary>
        ///  Constrains a value to be within a range
        /// </summary>
        ///  <param name="value">A value to constraint</param>
        ///  <param name="upper_limit">Maximal limit for the value</param>
        ///  <param name="lower_limit">Minimal limit for the value</param> 
        /// <returns>
        ///  <paramref name="value"/>: if between <paramref name="lower_limit"/> and <paramref name="upper_limit"/>.
        ///   <para>
        ///  <paramref name="lower_limit"/>: if less than <paramref name="lower_limit"/>
        ///   </para>
        ///  <paramref name="upper_limit"/>: if greater than <paramref name="upper_limit"/>
        /// </returns>
        public static T Constrain<T>(T value, T upper_limit, T lower_limit) where T : IComparable<T>
        {
            // that's your problems if you use this for non-numeric types lmao
            return value.CompareTo(upper_limit) > 0 ? upper_limit : (value.CompareTo(upper_limit) < 0 ? lower_limit : value);
        }

        /// <summary>
        /// Re-maps a number from one range to another. <para></para> A value of <paramref name="from_low"/> is mapped to <paramref name="to_low"/>, a value of <paramref name="from_high"/> to <paramref name="to_high"/>, values in-between to values in-between, etc.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="from_low"></param>
        /// <param name="from_high"></param>
        /// <param name="to_low"></param>
        /// <param name="to_high"></param>
        /// <![CDATA[https://www.arduino.cc/reference/en/language/functions/math/map/]]>
        public static long Map(long value, long from_low, long from_high, long to_low, long to_high)
        {
            return (value - from_low) * (to_high - to_low) / (from_high - from_low) + to_low;
        }
    }
}