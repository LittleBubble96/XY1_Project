using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ClientServe
{
    private Socket clientSocket;
    private IPEndPoint _ipEnd;
    public void ConnectServe(string ip, int port)
    {
        try
        {
            _ipEnd = new IPEndPoint(IPAddress.Parse(ip), port);
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            AsyncCallback callback = ConnectCallback;
            clientSocket.BeginConnect(_ipEnd,callback, port);
            GameEntry.ins.OnLog?.Invoke("Connect to server");
        }
        catch (Exception e)
        {
            GameEntry.ins.OnLog?.Invoke(e.ToString());
            throw;
        }
        
    }
    
    private void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            if (ar.IsCompleted)
            {
                GameEntry.ins.OnLog?.Invoke("ConnectCallback Connect to server success");
            }
            else
            {
                GameEntry.ins.OnLog?.Invoke("ConnectCallback Connect to server fail");
            }
            clientSocket.EndConnect(ar);
        }
        catch (Exception e)
        {
            GameEntry.ins.OnLog?.Invoke(e.ToString());
            throw;
        }
    }
}
