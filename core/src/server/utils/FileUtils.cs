using System.IO;

namespace cssocketserver.server.utils
{

    /**
     * @author andrzej.salamon@gmail.com
     *
     */

    public class FileUtils
    {
        const char FILE_SEPARATOR = getFileseparator();

        public static char getFileseparator()
        {
            return Path.DirectorySeparatorChar;
        }
    }
}
