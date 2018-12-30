namespace cssocketserver.server.utils
{
    public class DateUtils
    {
        private const SimpleDateFormat sf = new SimpleDateFormat("yyyyMMddHHmmss");

        public static string format(Date d)
        {
            return sf.format(d);
        }
    }
}