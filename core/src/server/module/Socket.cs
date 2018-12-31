using System;
using System.IO;
using System.Net.Sockets; 

using sc = cssocketserver.server.core;
using scfg = cssocketserver.server.config;
using su = cssocketserver.server.utils;
using sm = cssocketserver.server.module;

namespace cssocketserver.server.module
{     
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
//
//        public override void receive()
//        {
//            //            request = (SerializedSocketObject)in.readObject();
//        }
//
//        public void broadcast()
//        {
//
//        }
//
//        public override void broadcast(string data)
//        {
//            //            response = process(request);
//            //            out.writeObject(response);
//        }
    }
}