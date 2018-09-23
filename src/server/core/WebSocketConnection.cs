namespace cssocketserver.server.core
{

    using java.io.IOException;
    using java.security.NoSuchAlgorithmException;

    /**
     * Created by wizard on 6/11/17. 
     */
    public interface WebSocketConnection : SocketConnection
    {
        int MAX_BUFFER = 5000;

        void sendHandshake();
        bool isHandshake();
        bool isHandshake(string data);
        bool isGet();
        bool isGet(string data);
        string getRequestAsstring();
    }
}