namespace cssocketserver.server.core.module
{

    using server.core.SocketConnection;

    using javax.xml.bind.DatatypeConverter;
    using java.io.IOException;
    using java.net.ServerSocket;
    using java.security.MessageDigest;
    using java.security.NoSuchAlgorithmException;
    using java.util.regex.Matcher;
    using java.util.regex.Pattern;

    public abstract class SocketModule : server.core.Module, SocketConnection
    {

        public SocketModule(ServerSocket serverSocket)
        {
            super(serverSocket);
        }

    }
}
