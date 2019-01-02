using System;

namespace cssocketserver.server.utils
{
    public class DateUtils
    {
        private const string DATE_FORMAT = "yyyyMMddHHmmss";

        public static string getDate()
        {
            return DateTime.Now.ToString(DATE_FORMAT);
        }
    }
}