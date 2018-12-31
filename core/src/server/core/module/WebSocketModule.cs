//using System;
using System.IO;
using System.Text;
//using System.Net.Sockets; 

using sc = cssocketserver.server.core;
using scfg = cssocketserver.server.config;
using su = cssocketserver.server.utils;
using sm = cssocketserver.server.module;

namespace cssocketserver.server.core.module
{

    public abstract class WebSocketModule : sc.Module, sc.WebSocketConnection
    {
        public const int MAX_BUFFER = 5000;
        
        protected string secWebSocketKey;

        public WebSocketModule(ServerSocket serverSocket) : base(serverSocket)
        {
        }

        /**
         * @
         * @
         * @todo refactor it to separate Handshake class
         */

        public bool isHandshake()
        {
            Matcher match = Pattern.compile("Sec-WebSocket-Key: (.*)").matcher(request);
            secWebSocketKey = match.group(1);
            return match.find();
        }

        public bool isHandshake(string data)
        {
            return false;
        }

        public bool isGet(string data)
        {
            return false;
        }

        public abstract string getId();

        public void handleStream()
        {
            try
            {
                setClient(serverSocket.accept());
                //reflush the stream
                outputStream = new ObjectOutputStream(getClient().getOutputStream());
                inputStream = new ObjectInputStream(getClient().getInputStream());
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
                    getClient().close();
                }
                catch (IOException e)
                {
//                    e.StackTrace;
                }
            }
        }

        public string getSecWebSocketKey()
        {
            return secWebSocketKey;
        }

        public void setSecWebSocketKey(string secWebSocketKey)
        {
            this.secWebSocketKey = secWebSocketKey;
        }

        public virtual void receive()
        {
            byte[] buffer = new byte[MAX_BUFFER];
            byte length;
            int messageLength, mask, dataStart;

            messageLength = inputStream.read(buffer);
            if (messageLength == -1)
            {
                return;
            }

            requestByte = new byte[messageLength];

            //b[0] is always text in my case so no need to check;
            byte data = buffer[1]; //does it cause a problem ?
            byte op = (byte) 127;
            length = (byte) (data & op);

            mask = 2; //lowest mask
            if (length == (byte) 126) mask = 4; //med
            if (length == (byte) 127) mask = 10; //max mask

            byte[] masks = new byte[4];

            int j = 0, i = mask;
            for (; i < (mask + 4); i++)
            {
                //start at mask, stop at last + 4
                masks[j] = buffer[i]; //problem here
                j++;
            }

            dataStart = mask + 4;

            for (i = dataStart, j = 0; i < messageLength; i++, j++)
            {
                requestByte[j] = (byte) (buffer[i] ^ masks[j % 4]);
            }

            response = new string(requestByte); //why now string copy of byte ?
        }

        public override void broadcast(string data)
        {
            byte[] rawData = Encoding.ASCII.GetBytes(data);
            int rawDataLength = rawData.Length, frameCount;

            frame[0] = (byte) 129;
            /* @TODO: loop it */ //or no, loop is more expensive in dat case
            //is fixed, make it 2 dim static pre-comp,
            //heart of app
            //const logic and byte 255
            //address first frame bytes 
            if (rawDataLength <= 125)
            {
                frame[1] = (byte) rawDataLength;
                frameCount = 2;
            }
            else if (rawDataLength <= 65535)
            {
                frame[1] = (byte) 126;
                frame[2] = (byte) ((rawDataLength >> 8) & (byte) 255);
                frame[3] = (byte) (rawDataLength & (byte) 255);
                frameCount = 4;
            }
            else
            {
                frame[1] = (byte) 127;
                frame[2] = (byte) ((rawDataLength >> 56) & (byte) 255);
                frame[3] = (byte) ((rawDataLength >> 48) & (byte) 255);
                frame[4] = (byte) ((rawDataLength >> 40) & (byte) 255);
                frame[5] = (byte) ((rawDataLength >> 32) & (byte) 255);
                frame[6] = (byte) ((rawDataLength >> 24) & (byte) 255);
                frame[7] = (byte) ((rawDataLength >> 16) & (byte) 255);
                frame[8] = (byte) ((rawDataLength >> 8) & (byte) 255);
                frame[9] = (byte) (rawDataLength & (byte) 255);
                frameCount = 10;
            }

            int responseLength = frameCount + rawDataLength;
            int responseLimit = 0;

            responseByte = new byte[responseLength];

            for (; responseLimit < frameCount; responseLimit++)
            {
                responseByte[responseLimit] = frame[responseLimit];
            }

            foreach (byte dataByte in rawData)
            {
                responseByte[responseLimit++] = dataByte;
            }

            outputStream.write(responseByte);
            outputStream.flush();
        }

        public override void broadcast()
        {
            broadcast("not implemented");
        }

        public string getRequestAsString()
        {
            //mv to field, cache it, reflush the stream
            return new Scanner(inputStream, "UTF-8").useDelimiter("\\r\\n\\r\\n")
                .next(); //bullshit is slow, is immediate release of object
        }

        public void sendHandshake()
        {
            //no text in class plz, mv, to cfg
            responseByte = ("HTTP/1.1 101 Switching Protocols\r\n"
                            + "WebSocketConnection: Upgrade\r\n"
                            + "Upgrade: websocket\r\n"
                            + "Sec-WebSocket-Accept: "
                            + DatatypeConverter
                                .printBase64Binary(
                                    MessageDigest
                                        .getInstance("SHA-1")
                                        .digest((secWebSocketKey + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11")
                                            .getBytes("UTF-8")))
                            + "\r\n\r\n")
                .getBytes("UTF-8");
            outputStream.write(responseByte, 0, responseByte.length);
        }

        public bool isGet()
        {
            Matcher get = Pattern.compile("^GET").matcher(request);
            return get.find();
        }
    }
}