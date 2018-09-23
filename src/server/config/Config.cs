namespace cssocketserver.server.config
{
    using java.util.Dictionary;

    public class Config
    {
        private static Dictionary<string, Config> config = new Dictionary<string, Config>();

        protected Config()
        {
            Config.add("default", new Config());
        }

        public static Config create()
        {
            return new Config();
        }

        public static Dictionary<string, Config> get()
        {
            return config;
        }

        public static void set(Dictionary<string, Config> config)
        {
            Config.config = config;
        }

        public static void add(string key, Config config)
        {
            get().put(key, config);
        }
    }
}