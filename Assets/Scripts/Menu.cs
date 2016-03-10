using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;
using UnityEngine.SceneManagement;
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
    public GameObject OptionsMenu;
    public InputField Namespace;

    bool isHost = false;
    MenuType CurrentMenu = MenuType.MainMenu;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start()
    {
       
        playerIP.text = "IP Address: "+TestIP();
    }

    public void SwitchMenu(bool forward)
    {
        if (forward)
        {
            if(CurrentMenu == MenuType.MainMenu)
            {
                if(!string.IsNullOrEmpty(Namespace.text))
                {
                    CurrentMenu++;
                }

            }
            else
            {
                CurrentMenu++;
            }
            
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
                RoomMenu.SetActive(false);
				//Application.LoadLevel(1);	// DEPRECATED
				SceneManager.LoadScene(1);	// NEW API
                break;
        }

        Debug.Log(CurrentMenu);
    }

    

    string TestIP()
    {
        return Network.player.ipAddress;
    }

    public void ToggleOnOffOptions()
    {
        OptionsMenu.SetActive(!OptionsMenu.activeSelf);
    }

    
}
