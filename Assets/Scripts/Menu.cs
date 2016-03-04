using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;
public class Menu : MonoBehaviour {

    enum MenuType
    {
        MainMenu=1,
        ConnectionMenu=2,
        RoomMenu=3,
        StartedGame=4
    }

    public Text playerIP;

    public GameObject MainMenu;
    public GameObject ConnectionMenu;
    public GameObject RoomMenu;

    bool isHost = false;
    MenuType CurrentMenu = MenuType.MainMenu;

    void Start()
    {
        playerIP.text = "IP Address: "+TestIP();
    }

    public void SwitchMenu(bool forward)
    {
        if (forward)
        {
            CurrentMenu++;
        }
        else
        {
            CurrentMenu--;
        }
        switch(CurrentMenu)
        {
            case MenuType.MainMenu:
                MainMenu.SetActive(true);
               
                break;
            case MenuType.ConnectionMenu:
                MainMenu.SetActive(false);
                ConnectionMenu.SetActive(true);
                RoomMenu.SetActive(false);
                break;
            case MenuType.RoomMenu:
                ConnectionMenu.SetActive(false);
                RoomMenu.SetActive(true);
                break;
            case MenuType.StartedGame:
                Application.LoadLevel(1);
                break;
        }

        Debug.Log(CurrentMenu);
    }

    

    string TestIP()
    {
        return Network.player.ipAddress;
    }


}
