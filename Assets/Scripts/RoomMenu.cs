using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class RoomMenu : MonoBehaviour {

    public GameObject playerTag;

    public GameObject CubePanel;

    public GameObject Cube2Panel;

    enum Side
    {
        Cube=0,
        Cube2=1
    }

    Side currentSide = Side.Cube;

    void Update()
    {
		if (!playerTag)
			return;

        playerTag.GetComponentInChildren<Text>().text = PlayerPrefs.GetString("Name");
    }

    public void SwitchSides()
    {
        switch(currentSide)
        {
            case Side.Cube:
                currentSide = Side.Cube2;
                playerTag.transform.parent = null;
                playerTag.transform.parent = Cube2Panel.transform;
                break;
            case Side.Cube2:
                currentSide = Side.Cube;
                playerTag.transform.parent = null;
                playerTag.transform.parent = CubePanel.transform;
                break;
        }
    }
}
