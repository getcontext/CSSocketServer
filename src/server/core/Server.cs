namespace cssocketserver.server.core
{

    using sc = server.config;
    using su = server.utils;

    using System.Threading;
    using System.Net.Sockets;
    using System.Net;
    using System.Collections.Generic;


    /**
     * @author andrzej.salamon@gmail.com
     * @todo make it async
     */
    public sealed class Server
    {
        public enum ServerType
        {
            Socket,
            WebSocket
        }

        public const string IP = getIp();
        public const string SOCKET_ID = "socket";
        public const string WEBSOCKET_ID = "websocket";
        private Thread serverThread;
        private static serverconfig.ServerConfig config;
        private static Dictionary<string, Socket> sockets = new Dictionary<string, Socket>();

        private SocketConnection connection;
        private static Dictionary<string, IPEndPoint> endPoints = new Dictionary<string, IPEndPoint>();

        private List<Connection> connections = new List<Connection>();

        public Server()
        {

            try
            {
                config = new sc.ServerConfig("config" + FileUtils.FILE_SEPARATOR + "server.xml");

                addSocket(SOCKET_ID, new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));
                addEndpoint(SOCKET_ID, new IPEndPoint(IPAddress.Any, int.TryParse(config.get("socket.port"))));

                // clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // setServerSocket(new ServerSocket(int.TryParse(config.get("port"))));

                serverThread = new Thread(new ThreadStart(this.run)); //@todo rf to parent cl thread
            }
            catch (Exception e)
            {
                // Console.Out.WriteLine("failed listening on port: " + config.get("port"));
                //add websocket port, nested config, islands
                System.exit(1);
            }

            addDefaultModules();

            serverThread.start();
        }


        public Server(ServerType type)
        {

            try
            {

                config = new sc.ServerConfig("config" + FileUtils.FILE_SEPARATOR + "server.xml");

                addSocket(SOCKET_ID, new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));
                addEndpoint(SOCKET_ID, new IPEndPoint(IPAddress.Any, int.TryParse(config.get("socket.port"))));


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

            addDefaultModules();

            serverThread.start();
        }


        private void addDefaultModules()
        {

            addModule(new WebSocket(getServerSocket()));
            addModule(new Socket(getServerSocket()));
        }

        public void addModule(Connection socketConnection)
        {
            if (!connections.contains(socketConnection))
                connections.add(socketConnection);
        }

        private void addSocket(string name, Socket item)
        {
            if (!sockets.ContainsKey(name))
                sockets.Add(name, item);
        }

        protected Socket getSocket(string name)
        {
            if (sockets.ContainsKey(name))
                return sockets[name];
        }

        private void addEndpoint(string name, IPEndPoint item)
        {
            if (!endPoints.ContainsKey(name))
                endPoints.Add(item);
        }

        protected IPEndPoint getEndpoint(string name)
        {
            if (endPoints.ContainsKey(name))
                return endPoints[name];
        }
        private void startModules()
        {
            if (connections.size() <= 0) return;
            foreach (Connection conn in connections)
            {
                conn.start();
            }
        }

        public static sc.ServerConfig gebtConfig()
        {
            return config;
        }

        public static void setConfig(sc.ServerConfig config)
        {
            Server.config = config;
        }

        public void run()
        {
            Console.Out.WriteLine("Andrew (Web)Socket(s) Server v. 1.1");

            startModules(); /* todo allow rts cli , -restart,-stop,-start */
            /* allow for single instances */

            while (true)
            {
                try
                {//@todo thread pooling
                    sleep(300);
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
