namespace cssocketserver.server.core
{
    /**
     * @author wizard
     */
    interface Connection
    {
        void start();
        void stop();
        void receive();
        void broadcast();
        void broadcast(string data);
    }
}
