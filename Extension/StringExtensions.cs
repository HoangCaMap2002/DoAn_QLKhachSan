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
        public static string ToVND(this double amount)
        {
            return string.Format("{0:N0} VNĐ", amount);
        }
    }
}
