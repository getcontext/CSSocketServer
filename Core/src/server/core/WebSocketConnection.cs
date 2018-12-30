namespace cssocketserver.server.core
{
    /**
     * Created by wizard on 6/11/17. 
     */
    public interface WebSocketConnection : SocketConnection
    {
        public const int MAX_BUFFER = 5000;

        void sendHandshake();
        bool isHandshake();
        bool isHandshake(string data);
        bool isGet();
        bool isGet(string data);
        string getRequestAsstring();
    }
}