namespace cssocketserver.server.core.module
{
    using servercore = server.core;

    using java.net.ServerSocket;
    using java.security.MessageDigest;
    using java.util.regex.Matcher;
    using java.util.regex.Pattern;

    public abstract class SocketModule : servercore.Module, servercore.SocketConnection
    {

        public SocketModule(ServerSocket serverSocket)
        {
            super(serverSocket);
        }

    }
}
