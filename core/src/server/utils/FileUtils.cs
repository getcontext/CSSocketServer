namespace cssocketserver.server.utils
{

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
