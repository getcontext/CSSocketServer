namespace cssocketserver.server.core
{

    using java.io.ObjectInputStream;
    using java.io.ObjectOutputStream;
    using java.net.ServerSocket;
    using java.net.Socket;

    /**
     * @todo add factory
     */
    public abstract class Module : Runnable, Connection
    {
        protected static int counter = 0;
        protected readonly Thread thread;

        private ObjectOutputStream outputStream;

        private ObjectInputStream inputStream;
        protected byte[] requestByte;
        protected byte[] responseByte;
        protected byte[] frame = new byte[10];
        protected string response;
        protected string request;
        protected bool close = false;
        protected int instanceNo; //!imp getMaxInstanceNo
        protected bool stop = false;
        protected java.net.Socket client;
        protected ServerSocket serverSocket;

        protected ObjectOutputStream OutputStream { get => outputStream; set => outputStream = value; }
        protected ObjectInputStream InputStream { get => inputStream; set => inputStream = value; }

        public Module(ServerSocket serverSocket)
        {
            this.serverSocket = serverSocket;
            this.instanceNo = counter++;
            this.thread = new Thread(this, MODULE_NAME + "_" + instanceNo);

        }

        public static int getCounter()
        {
            return counter;
        }

        public static void setCounter(int counter)
        {
            Module.counter = counter;
        }

        public static void incrementCounter(int counter)
        {
            ++Module.counter;
        }

        public Thread getThread()
        {
            return thread;
        }

        protected Socket getClient()
        {
            return client;
        }

        protected void setClient(java.net.Socket client)
        {
            this.client = client;
        }

        public void start()
        {
            getThread().start();
        }

        public void stop()
        {
            getThread().stop();
            stop = true;
        }
    }
}