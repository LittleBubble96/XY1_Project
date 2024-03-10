using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class TCPListenerClent
{

    //地址和端口号
    private IPEndPoint _ipEnd;
    //Socket
    private Socket _socket;

    public Action<Socket> ConnectClientAction { get; set; }
    //设置服务器的IP地址和端口号
    public TCPListenerClent()
    {

    }

    //开始链接客户端
    public void StartListener(string ip, int port)
    {
        try
        {
            _ipEnd = new IPEndPoint(IPAddress.Parse(ip), port);
            //第一个参数指定地址族，设置为ipv4，第二个参数指定套接字类型(流式)，第三个参数指定协议(TCP 协议)
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //服务器绑定IP地址和端口号
            _socket.Bind(_ipEnd);
            Console.WriteLine("链接服务器成功");
            _socket.Listen(10);
            //开始接受客户端链接
            SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            e.Completed += new EventHandler<SocketAsyncEventArgs>(Accept_Completed);
            _socket.AcceptAsync(e);
        }
        catch (Exception e)
        {
            Console.WriteLine("链接服务器失败");
            return;
        }
    }

    protected void Accept_Completed(object sender, SocketAsyncEventArgs e)
    {
        if (e.SocketError != SocketError.Success)
        {
            return;
        }
        //获取客户端的Socket
        Socket client = e.AcceptSocket;
        ConnectClientAction?.Invoke(client);
    }
}