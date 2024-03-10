using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntry : MonoInstance<GameEntry>
{
    [SerializeField]
    private string serverIp = "";
    [SerializeField]
    private int serverPort = 0;
    
    public Action<string> OnLog;
    public string GetServerIp()
    {
        return serverIp;
    }
    
    private ClientServe clientServe;
    void Start()
    {
        clientServe = new ClientServe();
    }
    
    
    public void OnStartClientServe(string ip)
    {
        clientServe.ConnectServe(ip, serverPort);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
