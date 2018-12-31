using System;
using System.IO;
using System.Threading;
//using System.Net.Sockets;
//using System.Net;
//using System.Collections.Generic;
using sc = cssocketserver.server.core;
using scfg = cssocketserver.server.config;
using su = cssocketserver.server.utils;
using sm = cssocketserver.server.module;

namespace cssocketserver.server.module
{
    /**
     * @author andrzej.salamon@gmail.com
     * @todo @see Socket
     */
    public sealed class WebSocket : sc.module.WebSocketModule
    {
        public const string MODULE_NAME = "websocket";

        public WebSocket(sc.ServerSocket serverSocket) : base(serverSocket)
        {
        }

        public override string getId()
        {
            return MODULE_NAME;
        }

        public void run()
        {
            while (!stop)
            {
                try
                {
                    handleStream(serverSocket.accept());
                }
                catch (IOException e)
                {
//                    e.StackTrace;
                }

                request = getRequestAsString();

                if (isGet())
                {
                    //mv, lambda, listener
                    if (isHandshake())
                    {
                        try
                        {
                            sendHandshake();
                        }
//                        catch (NoSuchAlgorithmException e)
//                        {
//                            e.StackTrace;
//                        }
                        catch (IOException e)
                        {
                            //ref ?
//                            e.StackTrace;
                        }
                    }
                    else
                    {
                        try
                        {
                            receive();
                            //                        System.out.println(response);
                        }
                        catch (IOException e)
                        {
//                            e.StackTrace;
                        }
                    }
                }

                if (close)
                {
                    try
                    {
                        broadcast(response); //@todo return resp headers is closed
                        outputStream.flush();
                        outputStream.close();
                        inputStream.close();
                        getClient().close();
                    }
                    catch (IOException e)
                    {
                        Console.Error.WriteLine("unable to close\nFailed processing client request\n Exit code {0}", 1);
                    }
                }

                Thread.Sleep(10);
            }
        }
    }
}