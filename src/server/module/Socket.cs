namespace cssocketserver.server.module
{

    using servercore = server.core;
    using servercoremodule = server.core.module;
    /**
     * @author andrzej.salamon@gmail.com
     */
    public sealed class Socket : servercoremodule.SocketModule
    {
        public const String MODULE_NAME = "socket";

        public Socket(ServerSocket serverSocket)
        {
            super(serverSocket);
        }

        public String getId()
        {
            return MODULE_NAME;
        }

        public void handleStream()
        {

        }

        public void handleStream(java.net.Socket client)
        {
            try
            {
                setClient(client);
                outputStream = new ObjectOutputStream(getClient().getOutputStream());
                inputStream = new ObjectInputStream(getClient().getInputStream());
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
                outputStream.flush();
                
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

        public void broadcast(String data)
        {
            //            response = process(request);
            //            out.writeObject(response);
        }
    }
}