namespace cssocketserver.server.module
{

    using sc = server.core;
     using System.Net.Sockets;

     
    /**
     * @author andrzej.salamon@gmail.com
     *
     * for both modules
     *
     * @todo make it async
     * @todo delegate injection as event
     * @todo dynamic instantiation, make it pooling
     */
    public sealed class Socket : sc.module.SocketModule
    {
        public const string MODULE_NAME = "socket";

        public Socket(sc.ServerSocket serverSocket) : base(serverSocket)
        {
          
        }

        public string getId()
        {
            return MODULE_NAME;
        }

        public void handleStream()
        {

        }

        public void handleStream(Socket client)
        {
            try
            {
                setClient(client);
                OutputStream = new ObjectOutputStream(getClient().getOutputStream());
                InputStream = new ObjectInputStream(getClient().getInputStream());
            }
            catch (IOException e)
            {
                e.printStackTrace();
            }
            finally
            {
                try
                { //try to close gracefully
                    client.close();
                }
                catch (IOException e)
                {
                    e.printStackTrace();
                }
            }
        }


        public void run()
        {
            try
            {
                receive();
                broadcast();
                OutputStream.flush();
                
                try
                {
                    //                out.close();
                    //                in.close();
                    client.close();
                }
                catch (IOException e)
                {
                    // e.printStackTrace();
                }
            }
            catch (Exception e)
            {
            }
        }

        public void start()
        {

        }

        public void stop()
        {

        }

        public void receive()
        {
            //            request = (SerializedSocketObject)in.readObject();
        }

        public void broadcast()
        {

        }

        public void broadcast(string data)
        {
            //            response = process(request);
            //            out.writeObject(response);
        }
    }
}