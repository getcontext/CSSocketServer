using System.Net.Sockets;

namespace cssocketserver.server.core.module
{
    using sc = server.core;

    public abstract class SocketModule : sc.Module, sc.SocketConnection
    {

        public SocketModule(ServerSocket serverSocket) : base(serverSocket)
        {          
        }

        public string getId()
        {
            throw new System.NotImplementedException();
        }

        public void handleStream()
        {
            throw new System.NotImplementedException();
        }

        public void handleStream(Socket client)
        {
            throw new System.NotImplementedException();
        }
    }
}
