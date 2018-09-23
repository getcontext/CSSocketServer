namespace cssocketserver.server.module
{
    using java.io.IOException;
    using java.io.ObjectInputStream;
    using java.io.ObjectOutputStream;
    using java.net.ServerSocket;
    using java.net.Socket;
    using java.security.NoSuchAlgorithmException;
    using java.util.Scanner;

    // using server.core.*;

    /**
     * @author andrzej.salamon@gmail.com
     */
    public sealed class WebSocket : cssocketserver.server.core.module.WebSocketModule
    {
        public const String MODULE_NAME = "websocket";


        public WebSocket(ServerSocket serverSocket)
        {
            super(serverSocket);
        }

        public String getId()
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