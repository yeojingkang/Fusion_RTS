using UnityEngine;
using System.Collections;

public class OptionMenu : MonoBehaviour {

	enum OptionsState
    {
        Audio=1,
        Controls=2,
        Closed=3
    }

    OptionsState currentState = OptionsState.Closed;

    public GameObject AudioTab;
    public GameObject ControlsTab;

    public void SwitchOptionsState(int state)
    {
        switch(state)
        {
            case 1:
                currentState = OptionsState.Audio;
                break;
            case 2:
                currentState = OptionsState.Controls;
                break;
            case 3:
                currentState = OptionsState.Closed;
                break;
        }

        switch(currentState)
        {
            case OptionsState.Audio:
                AudioTab.SetActive(true);
                ControlsTab.SetActive(false);
                break;
            case OptionsState.Controls:
                AudioTab.SetActive(false);
                ControlsTab.SetActive(true);
                break;
            case OptionsState.Closed:
                AudioTab.SetActive(true);
                ControlsTab.SetActive(false);
                gameObject.SetActive(false);
                break;
        }
    }
}
