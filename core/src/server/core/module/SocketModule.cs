namespace cssocketserver.server.core.module
{
    using servercore = server.core;

    public abstract class SocketModule : servercore.Module, servercore.SocketConnection
    {

        public SocketModule(ServerSocket serverSocket)
        {
            super(serverSocket);
        }

    }
}
