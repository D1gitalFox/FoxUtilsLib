namespace FoxUtilsLib
{
    /// <summary>
    /// Методы для работы с текстовыми и прочими данными
    /// </summary>
    public static class FData
    {
        /// <summary>
        /// Перевести текст в base64 строку
        /// </summary>
        public static string Base64Encode(string plainText)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Перевести base64 строку в текст
        /// </summary>
        public static string Base64Decode(string base64EncodedData)
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// Ограничивает длину строки определённым количеством символов
        /// </summary>
        /// <param name="baseString"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Truncate(string baseString, int length)
        {
            if(baseString.Length > length)
                return baseString.Substring(0, length);
            return baseString;
        }
    }
}
