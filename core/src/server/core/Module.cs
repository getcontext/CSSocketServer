using System.IO;
using System.Threading;
using System.Net.Sockets;

namespace cssocketserver.server.core
{
    /**
     * @todo add factory
     */
    public abstract class Module : Connection
    {
        protected static int counter = 0;
        protected readonly Thread thread;

        protected NetworkStream networkStream;

        protected byte[] requestByte;
        protected byte[] responseByte;
        protected byte[] frame = new byte[10];
        protected string response;
        protected string request;
        protected bool close = false;
        protected int instanceNo; //!imp getMaxInstanceNo
        protected bool stopped = false;
        protected Socket client;

        protected ServerSocket serverSocket;
//
//        protected ObjectOutputStream OutputStream
//        {
//            get => outputStream;
//            set => outputStream = value;
//        }
//
//        protected ObjectInputStream InputStream
//        {
//            get => inputStream;
//            set => inputStream = value;
//        }

        public Module(ServerSocket serverSocket)
        {
            this.serverSocket = serverSocket;
            this.instanceNo = counter++;
            // this.thread = new Thread(this, MODULE_NAME + "_" + instanceNo);
            this.thread = new Thread(new ThreadStart(this.run));
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

        protected void setClient(Socket client)
        {
            this.client = client;
        }

        public void start()
        {
            getThread().Start();
        }

        public void stop()
        {
//            getThread().stop();
            stopped = true;
        }

        public void handleStream(Socket client)
        {
            try
            {
                setClient(client);
                networkStream = new NetworkStream(getClient());
            }
            catch (IOException e)
            {
//                e.StackTrace;
            }
            finally
            {
                try
                {
                    //try to close gracefully
//                    client.close();
                }
                catch (IOException e)
                {
//                    e.StackTrace;
                }
            }
        }


        public abstract void run();
        public abstract void receive();
        public abstract void broadcast();
        public abstract void broadcast(string data);
    }
}