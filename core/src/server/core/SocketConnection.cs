namespace cssocketserver.server.core
{

    using System.Net.Sockets;

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