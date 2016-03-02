using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;
public class Menu : MonoBehaviour {

    public Text playerIP;

    void Start()
    {
        playerIP.text = "IP Address: "+TestIP();
    }

    public void StartGame()
    {
        Application.LoadLevel(1);
    }

    string TestIP()
    {
        return Network.player.ipAddress;
    }


}
