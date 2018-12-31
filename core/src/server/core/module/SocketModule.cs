using System.IO;
//using System.Net.Sockets;

namespace cssocketserver.server.core.module
{
    using sc = server.core;

    public abstract class SocketModule : sc.Module, sc.SocketConnection
    {
        public const string MODULE_NAME = "socket";

        public SocketModule(ServerSocket serverSocket) : base(serverSocket)
        {          
        }

        public void handleStream()
        {

        }

        public void handleStream(server.module.Socket client)
        {
            try
            {
                setClient(client);
                OutputStream = new ObjectOutputStream(getClient().getOutputStream());
                InputStream = new ObjectInputStream(getClient().getInputStream());
            }
            catch (IOException e)
            {
//                e.StackTrace;
            }
            finally
            {
                try
                { //try to close gracefully
                    client.close();
                }
                catch (IOException e)
                {
//                    e.StackTrace;
                }
            }
        }

        public override void receive()
        {
            //            request = (SerializedSocketObject)in.readObject();
        }

        public override void broadcast()
        {

        }

        public override void broadcast(string data)
        {
            //            response = process(request);
            //            out.writeObject(response);
        }

        public string getId()
        {
            return MODULE_NAME;
        }
    }
}
