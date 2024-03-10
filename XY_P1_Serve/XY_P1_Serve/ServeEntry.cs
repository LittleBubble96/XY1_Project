using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 整体服务器入口
/// </summary>
public class ServeEntry : Class_Instance<ServeEntry>
{
    public TCPListenerClent TCPListenerClent { get; set; }

    private List<ConnectClientProxy> _connectClientProxys = new List<ConnectClientProxy>();

    public ServeEntry()
    {
        TCPListenerClent = new TCPListenerClent();
        TCPListenerClent.ConnectClientAction += ConnectClient;
    }

    public void Start(string ip, int port)
    {
        TCPListenerClent.StartListener(ip, port);
    }

    protected void ConnectClient(Socket socket)
    {
        if (socket == null)
        {
            Console.WriteLine("客户端链接失败");

            return;
        }
        //打印客户端链接成功
        ConnectClientProxy connectClientProxy = new ConnectClientProxy(socket);
        _connectClientProxys.Add(connectClientProxy);
    }
}