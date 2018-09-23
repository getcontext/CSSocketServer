namespace cssocketserver.server.core.module
{

    using cssocketserver.server.core.SocketConnection;
    using cssocketserver.server.core.Module;

    using javax.xml.bind.DatatypeConverter;
    using java.net.ServerSocket;
    using java.security.MessageDigest;
    using java.util.regex.Matcher;
    using java.util.regex.Pattern;

    public abstract class SocketModule : Module, SocketConnection
    {

        public SocketModule(ServerSocket serverSocket)
        {
            super(serverSocket);
        }

    }
}
