using System;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.IO;
using sc = cssocketserver.server.config;
using su = cssocketserver.server.utils;
using sm = cssocketserver.server.module;

namespace cssocketserver.server.core
{
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

        private readonly Thread serverThread;

        private static sc.ServerConfig config;

        //map to int or not ? 
        private static Dictionary<string, Socket> sockets = new Dictionary<string, Socket>();
        private static Dictionary<string, IPEndPoint> endPoints = new Dictionary<string, IPEndPoint>();
        private readonly List<Connection> connections = new List<Connection>();

        public Server() : this(ServerType.Socket)
        {
            // Console.Out.WriteLine("failed listening on port: " + config.get("port"));
            //add websocket port, nested config, islands
//            Environment.Exit(1);
        }


        public Server(ServerType type)
        {
            string connectionId;

            connectionId = getConnectionId(type);

            try
            {
                config = new sc.ServerConfig("config" + su.FileUtils.getFileSeparator() + "server.xml");

                //@todo refactor to SocketInformation Factory
                addSocket(connectionId,
                    new ServerSocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));
                //@todo encapsulate into Factory
                addEndpoint(connectionId, new IPEndPoint(IPAddress.Any, int.Parse(config.get("socket.port"))));

                // clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // setServerSocket(new ServerSocket(int.TryParse(config.get("port"))));

                //rf to parent cl thread (not possible - sealed)
                serverThread = new Thread(new ThreadStart(this.run));
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("failed listening on port: " + config.get("port"));
                //add websocket port, nested config, islands
                Environment.Exit(1);
            }

            addDefaultModules();

            serverThread.Start();
        }

        public static string getConnectionId(ServerType type)
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
                    break;
            }

            return connectionId;
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

        private Socket getSocket(string name)
        {
            if (sockets.ContainsKey(name))
                return sockets[name];
            return null; //@todo can not be further
        }

        private void addEndpoint(string name, IPEndPoint item)
        {
            if (!endPoints.ContainsKey(name))
                endPoints.Add(name, item);
        }

        protected IPEndPoint getEndpoint(string name)
        {
            return endPoints.ContainsKey(name) ? endPoints[name] : null;
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
            /*@todo allow for single instances */

            while (true)
            {
                //@todo thread pooling
                Thread.Sleep(300);
            }
        }

        /**
         * https://stackoverflow.com/questions/6803073/get-local-ip-address
         */
        public static string getIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            throw new IOException("No network adapters with an IPv4 address in the system!");
        }

        public ServerSocket getServerSocket()
        {
            //incorrect, change dict type
            //use ServerType
            return (ServerSocket) getSocket(getConnectionId(ServerType.Socket)); 
        }

        public void setServerSocket(ServerSocket serverSocket)
        {
            addSocket(getConnectionId(ServerType.Socket), serverSocket);
        }

        public static void Main(string[] args)
        {
            new Server();
        }
    }
}