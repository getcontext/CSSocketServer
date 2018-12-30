namespace cssocketserver.server.utils
{

    using java.text.SimpleDateFormat;
    using java.util.Date;

    public class DateUtils
    {
        private const SimpleDateFormat sf = new SimpleDateFormat("yyyyMMddHHmmss");

        public static string format(Date d)
        {
            return sf.format(d);
        }
    }
}