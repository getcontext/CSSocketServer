namespace cssocketserver.server.core
{

    using java.io.IOException;

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
