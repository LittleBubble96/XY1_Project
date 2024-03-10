using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI log_text;

    [SerializeField] private Button startServeBtn;
    
    [SerializeField] private TMP_InputField serverIpInput;
    // Start is called before the first frame update
    void Start()
    {
        log_text.text = "";
        serverIpInput.text = GameEntry.ins.GetServerIp();
        startServeBtn.onClick.AddListener(() =>
        {
            GameEntry.ins.OnStartClientServe(serverIpInput.text);
        });
        GameEntry.ins.OnLog += (log) =>
        {
            log_text.text += log + "\n";
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
