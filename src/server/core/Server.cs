namespace cssocketserver.server.core
{

    using serverconfig = server.config;
    using server.utils.FileUtils;

    using java.net.InetAddress;
    using java.net.ServerSocket;


    /**
     * @author andrzej.salamon@gmail.com
     */
    public sealed class Server : Thread
    { //lets keep it extend
        private ServerSocket serverSocket = null;
        const string IP = getIp();

        private static serverconfig.ServerConfig config;

        //    private Socket client;
        //	private SocketConnection connection;
        private List<Connection> connections = new List<Connection>();

        public Server()
        {
            try
            {
                Server.setConfig(new ServerConfig("config" + FileUtils.FILE_SEPARATOR + "server.xml"));
                setServerSocket(new ServerSocket(Integer.parseInt(config.get("port"))));
            }
            catch (IOException e)
            {
                Console.Out.WriteLine("failed listening on port: " + config.get("port"));
                System.exit(1);
            }

            addDefaultModule();

            this.start();
        }


        protected void addDefaultModule()
        {

            addModule(new WebSocket(getServerSocket()));
            addModule(new Socket(getServerSocket()));
        }

        public void addModule(Connection socketConnection)
        {
            if (!connections.contains(socketConnection))
                connections.add(socketConnection);
        }

        protected void startModules()
        {
            if (connections.size() <= 0) return;

            foreach (Connection conn in connections)
            {
                conn.start();
            }
        }

        public static ServerConfig getConfig()
        {
            return config;
        }

        public static void setConfig(ServerConfig config)
        {
            Server.config = config;
        }

        public void run()
        {
            Console.Out.WriteLine("Andrew (Web)Socket(s) Server v. 1.1");

            startModules(); /* todo allow rts cli , -restart,-stop,-start */
            
            while (true)
            {
                try
                {//@todo thread pooling
                    sleep(1);
                }
                catch (InterruptedException e)
                {
                    Console.Out.WriteLine("sleep failed");
                }
            }
        }

        private static string getIp()
        {
            try
            {
                InetAddress addr = InetAddress.getLocalHost();
                return addr.getAddress().tostring();
            }
            catch (UnknownHostException e)
            {
                return "";
            }
        }

        public ServerSocket getServerSocket()
        {
            return serverSocket;
        }

        public void setServerSocket(ServerSocket serverSocket)
        {
            this.serverSocket = serverSocket;
        }

        public static void main(string[] args)
        {
            new Server();
        }


    }
}
