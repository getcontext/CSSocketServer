namespace cssocketserver.server.core
{

    using server.config.ServerConfig;
    using server.utils.FileUtils;

    using java.io.IOException;
    using java.net.InetAddress;
    using java.net.ServerSocket;
    using java.net.UnknownHostException;
    using java.util.List;
    using java.util.ArrayList;

    // using server.module.*;
    //using server.module.WebSocket;

    /**
     * @author andrzej.salamon@gmail.com
     */
    public sealed class Server : Thread
    { //lets keep it extend
        private ServerSocket serverSocket = null;
        const string IP = getIp();

        private static ServerConfig config;

        //    private Socket client;
        //	private SocketConnection connection;
        private List<Connection> connections = new ArrayList<Connection>();

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
            startModules();
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
