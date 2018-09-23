namespace cssocketserver.server.config
{
    using java.util.HashMap;

    public class Config
    {
        private static HashMap<string, Config> config = new HashMap<string, Config>();

        protected Config()
        {
            Config.add("default", new Config());
        }

        public static Config create()
        {
            return new Config();
        }

        public static HashMap<string, Config> get()
        {
            return config;
        }

        public static void set(HashMap<string, Config> config)
        {
            Config.config = config;
        }

        public static void add(string key, Config config)
        {
            get().put(key, config);
        }
    }
}