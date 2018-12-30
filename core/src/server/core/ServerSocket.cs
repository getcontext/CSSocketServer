namespace cssocketserver.server.core
{

    using sc = server.config;
    using su = server.utils;

    using System.Threading;
    using System.Net.Sockets;
    using System.Net;
    using System.Collections.Generic;


    /**
     * @author andrzej.salamon@gmail.com
     */
    public sealed class ServerSocket : Socket
    {
        public ServerSocket(SocketInformation socketInformation) : base(socketInformation)
        {
        }

        public ServerSocket(SocketType socketType, ProtocolType protocolType) : base(socketType, protocolType)
        {
        }

        public ServerSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType) : base(addressFamily, socketType, protocolType)
        {
        }
    }
}