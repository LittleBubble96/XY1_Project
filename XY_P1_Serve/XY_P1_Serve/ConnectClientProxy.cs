using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class ConnectClientProxy
{
    private Socket _socket;
    public Socket Socket
    {
        get { return _socket; }
    }

    public ConnectClientProxy(Socket socket) 
    { 
        _socket = socket;
        //客户端链接成功
        Console.WriteLine("客户端ip:" + _socket.RemoteEndPoint.ToString() + "链接成功");
    }
}