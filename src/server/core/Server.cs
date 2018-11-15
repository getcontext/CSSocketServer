namespace cssocketserver.server.core
{

    using serverconfig = server.config;
    using serverutils = server.utils;

    using java.net.InetAddress;
    using System.Threading;
    using System.Net.Sockets;


    /**
     * @author andrzej.salamon@gmail.com
     * @todo make it async
     */
    public sealed class Server
    { //lets keep it extend
        public const string IP = getIp();
        private Socket serverSocket;
        private Thread serverThread;
        private static serverconfig.ServerConfig config;

        //  private Socket client;
        //	private SocketConnection connection;
        private List<Connection> connections = new List<Connection>();
        private TcpClient client;
        private TcpListener listener;
        
        public Server()
        {
            serverThread = new Thread(new ThreadStart(this.run)); //@todo rf to parent cl thread

            try
            {
                Server.setConfig(new serverconfig.ServerConfig("config" + FileUtils.FILE_SEPARATOR + "server.xml"));
                setServerSocket(new ServerSocket(int.TryParse(config.get("port"))));
            }
            catch (IOException e)
            {
                Console.Out.WriteLine("failed listening on port: " + config.get("port"));
                System.exit(1);
            }

            addDefaultModule();

            serverThread.start();
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

        public static serverconfig.ServerConfig getConfig()
        {
            return config;
        }

        public static void setConfig(serverconfig.ServerConfig config)
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
            return ServerSocket;
        }

        public void setServerSocket(ServerSocket serverSocket)
        {
            this.ServerSocket = serverSocket;
        }

        public static void main(string[] args)
        {
            new Server();
        }
    }
}
