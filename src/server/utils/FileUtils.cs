namespace server.utils
{

    using java.io.File;
    using java.text.ParseException;
    using java.text.SimpleDateFormat;
    using java.util.Date;

    /**
     * @author andrzej.salamon@gmail.com
     *
     */

    public class FileUtils
    {
        const string FILE_SEPARATOR = getFileseparator();

        public static string getFileseparator()
        {
            return System.getProperty("file.separator");
        }
    }
}
