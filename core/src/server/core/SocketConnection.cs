using System;
using System.IO;
using System.Net.Sockets; 

using sc = cssocketserver.server.core;
using scfg = cssocketserver.server.config;
using su = cssocketserver.server.utils;
using sm = cssocketserver.server.module;

namespace cssocketserver.server.core
{
    /**
     * @todo pull up
     */
    public interface SocketConnection : Connection
    {
        string getId();
        void handleStream();
        void handleStream(Socket client);
    }
}