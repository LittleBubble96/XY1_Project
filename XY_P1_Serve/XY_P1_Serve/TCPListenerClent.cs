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

    private TcpListener tcpListener;

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
            tcpListener = new TcpListener(IPAddress.Parse(ip), port);
            tcpListener.Start();

            Console.WriteLine("链接服务器成功");
            //开始接受客户端链接
            AcceptClientAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("StartListener 链接服务器失败,e=" + e);
            return;
        }
    }

    public async void AcceptClientAsync()
    {
        try
        {
            TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
            
            ConnectClientAction?.Invoke(tcpClient.Client);
            AcceptClientAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("AcceptClientAsync 链接服务器失败,e=" + e);
            tcpListener.Stop();
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