using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class ConnectClientProxy
{
    private Socket _socket_client;
    public Socket SocketClient
    {
        get { return _socket_client; }
    }

    public ConnectClientProxy(Socket socket) 
    {
        _socket_client = socket;
        //客户端链接成功
        Console.WriteLine("客户端ip:" + _socket_client.RemoteEndPoint.ToString() + "链接成功");
    }

    //接受客户端消息
    public async void ReceiveAsync()
    {
        while (_socket_client.Connected)
        {
            try
            {
                //储存接收到的数据，定义初始容量，一般不超过4096 或 1024
                byte[] buffer = new byte[4096];
                // 把消息存到buffer中，并返回实际接收到的字节数
                int length = await _socket_client.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
                if (length > 0)
                {
                    Console.WriteLine("接收到客户端消息:" + Encoding.UTF8.GetString(buffer, 0, length));
                }
                else 
                {
                    //客户端断开链接
                    Console.WriteLine("客户端断开链接");
                    _socket_client.Close();
                }

            }
            catch (Exception)
            {
                Console.WriteLine("接收客户端消息失败");
                _socket_client.Close();
            }
        }
    }

    public async void SendAsync(byte[] msg)
    {
        try
        {
            await _socket_client.SendAsync(new ArraySegment<byte>(msg), SocketFlags.None);
            Console.WriteLine("发送消息成功,消息内容:" + Encoding.UTF8.GetString(msg));
        }
        catch (Exception)
        {
            Console.WriteLine("发送消息失败");
            _socket_client.Close();
        }
    }
}