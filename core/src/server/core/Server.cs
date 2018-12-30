using System;

namespace cssocketserver.server.core
{

    using sc = server.config;
    using su = server.utils;
    using sm = server.module;

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

//        public const string IP = "127.0.0.1" ?? getIp();
        public const string SOCKET_ID = "socket";
        public const string WEBSOCKET_ID = "websocket";
        
        private Thread serverThread;
        private static sc.ServerConfig config;
        //map crap to int or not ? 
        private static Dictionary<string, Socket> sockets = new Dictionary<string, Socket>();
        private static Dictionary<string, IPEndPoint> endPoints = new Dictionary<string, IPEndPoint>();
        private readonly List<Connection> connections = new List<Connection>();

        public Server()
        {

            try
            {
                Server(ServerType.Socket);
            }
            catch (Exception e)
            {
                // Console.Out.WriteLine("failed listening on port: " + config.get("port"));
                //add websocket port, nested config, islands
                Environment.Exit(1);
            }
        }


        public Server(ServerType type)
        {
            string connectionId;
            
            switch (type)
            {
                    case ServerType.Socket:
                        connectionId = SOCKET_ID;
                        break;
                    case ServerType.WebSocket:
                        connectionId = WEBSOCKET_ID;
                        break;                        
                    default:
                        connectionId = SOCKET_ID;
            }

            try
            {

                config = new sc.ServerConfig("config" + su.FileUtils.FILE_SEPARATOR + "server.xml");

                //@todo refactor to SocketInformation Factory
                addSocket(connectionId, new ServerSocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));
                //@todo encapsulate into Factory
                addEndpoint(connectionId, new IPEndPoint(IPAddress.Any, int.TryParse(config.get("socket.port"))));

                // clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // setServerSocket(new ServerSocket(int.TryParse(config.get("port"))));

                serverThread = new Thread(new ThreadStart(this.run)); //@todo rf to parent cl thread (not possible - sealed)
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("failed listening on port: " + config.get("port"));
                //add websocket port, nested config, islands
                Environment.Exit(1);
            }

            addDefaultModules();

            serverThread.start();
        }


        private void addDefaultModules()
        {

            addModule(new sm.WebSocket(getServerSocket()));
            addModule(new sm.Socket(getServerSocket()));
        }

        public void addModule(Connection socketConnection)
        {
            if (!connections.Contains(socketConnection))
                connections.Add(socketConnection);
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
                endPoints.Add(name, item);
        }

        protected IPEndPoint getEndpoint(string name)
        {
            if (endPoints.ContainsKey(name))
                return endPoints[name];
        }
        private void startModules()
        {
            if (connections.Count <= 0) return;
            foreach (Connection conn in connections)
            {
                conn.start();
            }
        }

        public static sc.ServerConfig getConfig()
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
