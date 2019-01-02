using System.IO;
using System.Net.Sockets;

//using System.Net.Sockets;

using sc = cssocketserver.server.core;

namespace cssocketserver.server.core.module
{

    public abstract class SocketModule : sc.Module, sc.SocketConnection
    {
        public const string MODULE_NAME = "socket";

        public SocketModule(ServerSocket serverSocket) : base(serverSocket)
        {          
        }

        public void handleStream()
        {

        }

        public void handleStream(sc.ServerSocket client)
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
                { //try to close gracefully
//                    client.close();
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
