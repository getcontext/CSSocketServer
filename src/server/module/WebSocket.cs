namespace cssocketserver.server.module
{
    using serverconfig = server.config;
    using servercoremodule = server.core.module;
    using serverutils = server.utils;

    using java.io.ObjectInputStream;
    using java.io.ObjectOutputStream;
    using java.net.ServerSocket;
    using java.net.Socket;
    using java.util.Scanner;

    // using server.core.*;

    /**
     * @author andrzej.salamon@gmail.com
     */
    public sealed class WebSocket : servercoremodule.WebSocketModule
    {
        public const string MODULE_NAME = "websocket";


        public WebSocket(ServerSocket serverSocket)
        {
            super(serverSocket);
        }

        public string getId()
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
                    e.printStackTrace();
                }

                request = getRequestAsString();

                if (isGet())
                { //wat dat iz ? mv, lambda, listener
                    if (isHandshake())
                    {
                        try
                        {
                            sendHandshake();
                        }
                        catch (NoSuchAlgorithmException e)
                        {
                            e.printStackTrace();
                        }
                        catch (IOException e)
                        { //ref ?
                            e.printStackTrace();
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
                            e.printStackTrace();
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
                        System.err.println("unable to close");
                        System.err.println("Failed processing client request");
                    }
                }
                try
                {
                    Thread.sleep(1);
                }
                catch (InterruptedException e)
                {
                    e.printStackTrace();
                }
            }
        }

    }
}