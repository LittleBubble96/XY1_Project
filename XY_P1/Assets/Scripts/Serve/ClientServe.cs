using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ClientServe
{
    private TcpClient _client = new TcpClient();
    public async void ConnectServe(string ip, int port)
    {
        try
        {
            await _client.ConnectAsync(ip, port);
            GameEntry.ins.OnLog?.Invoke("Connect to server");
        }
        catch (Exception e)
        {
            GameEntry.ins.OnLog?.Invoke(e.ToString());
            throw;
        }
        
    }
}
