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
        public Socket(sc.ServerSocket serverSocket) : base(serverSocket)
        {
          
        }

        public override void run()
        {
            try
            {
                receive();
                broadcast();
                networkStream.Flush();
                
                try
                {
                    //                out.close();
                    //                in.close();
                    client.Close();
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
    }
}