namespace cssocketserver.server.core
{

    using serverconfig = server.config;
    using serverutils = server.utils;

<<<<<<< HEAD
    using java.net.InetAddress;
=======
>>>>>>> 22857a18fed0c6dc914117392234b74e3e4db30e
    using System.Threading;
    using System.Net.Sockets;


    /**
     * @author andrzej.salamon@gmail.com
     * @todo make it async
     */
    public sealed class Server
<<<<<<< HEAD
    { //lets keep it extend
=======
    {
        public enum ServerType {
            Socket,
            WebSocket
        }
>>>>>>> 22857a18fed0c6dc914117392234b74e3e4db30e
        public const string IP = getIp();
        private Socket serverSocket;
        private Thread serverThread;
        private static serverconfig.ServerConfig config;

<<<<<<< HEAD
        //  private Socket client;
        //	private SocketConnection connection;
        private List<Connection> connections = new List<Connection>();
        private TcpClient client;
        private TcpListener listener;
        
        public Server()
        {
            serverThread = new Thread(new ThreadStart(this.run)); //@todo rf to parent cl thread
=======
        private Socket clientSocket;
        private SocketConnection connection;
        private IPEndPoint endPointSocket;
        private IPEndPoint endPointWebSocket;

        private List<Connection> connections = new List<Connection>();
        // private TcpClient client;
        // private TcpListener listener;

        public Server()
        {
>>>>>>> 22857a18fed0c6dc914117392234b74e3e4db30e

            try
            {

                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                endPointSocket = new IPEndPoint(IPAddress.Any, int.TryParse(config.get("port")));

                Server.config = new serverconfig.ServerConfig("config" + FileUtils.FILE_SEPARATOR + "server.xml");

                // clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // setServerSocket(new ServerSocket(int.TryParse(config.get("port"))));
                
                serverThread = new Thread(new ThreadStart(this.run)); //@todo rf to parent cl thread
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("failed listening on port: " + config.get("port"));
                //add websocket port, nested config, islands
                System.exit(1);
            }

            addDefaultModule();

            serverThread.start();
<<<<<<< HEAD
=======
        }


        public Server(ServerType type)
        {

            try
            {

                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                endPointSocket = new IPEndPoint(IPAddress.Any, int.TryParse(config.get("port")));

                Server.config = new serverconfig.ServerConfig("config" + FileUtils.FILE_SEPARATOR + "server.xml");

                // clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // setServerSocket(new ServerSocket(int.TryParse(config.get("port"))));
                
                serverThread = new Thread(new ThreadStart(this.run)); //@todo rf to parent cl thread
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("failed listening on port: " + config.get("port"));
                //add websocket port, nested config, islands
                System.exit(1);
            }

            addDefaultModule();

            serverThread.start();
>>>>>>> 22857a18fed0c6dc914117392234b74e3e4db30e
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
