namespace cssocketserver.server.core
{

    using java.io.IOException;
    using java.net.Socket;

    /**
     * @todo pull up
     */
    public interface SocketConnection : Connection
    {
        string getId();
        void handleStream();
        void handleStream(Socket client);
    }
}