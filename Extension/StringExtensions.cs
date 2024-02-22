namespace DoAn_QLKhachSan.Extension
{
    public static class StringExtensions
    {
        public static DateTime ToDateTime(this string str)
        {
            string format = "MM/dd/yyyy";
            DateTime datetime;
            if (DateTime.TryParseExact(str, format, null, System.Globalization.DateTimeStyles.None, out datetime))
            {
                return datetime;
            }
            else
            {
                throw new FormatException("Chuỗi không đúng định dạng ngày.");
            }
        }
    }
}
