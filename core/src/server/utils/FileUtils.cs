using System.IO;

namespace cssocketserver.server.utils
{

    /**
     * @author andrzej.salamon@gmail.com
     *
     */

    public class FileUtils
    {
//        public const char DIR_DELIM = Path.DirectorySeparatorChar;

        public static string getFileSeparator()
        {
            return $"{Path.DirectorySeparatorChar}";
        }
    }
}
