using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.UI;
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

    public GameObject BGMSlider;
    public GameObject SFXSlider;

    public GameObject ScrollingSense;
    public GameObject EdgeScrollingSense;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void SaveOptions()
    {
        Options loadedOptions = new Options();

        if (File.Exists(Application.persistentDataPath + "/Options.fusion"))
        {
            BinaryFormatter bf2 = new BinaryFormatter();
            FileStream file2 = File.Open(Application.persistentDataPath + "/Options.fusion", FileMode.Open);
            Options OptionData2 = (Options)bf2.Deserialize(file2);
            file2.Close();

            loadedOptions.BGM = OptionData2.BGM;
            loadedOptions.SFX = OptionData2.SFX;

            loadedOptions.EdgeScrollingSense = OptionData2.EdgeScrollingSense;
            loadedOptions.ScrollingSense = OptionData2.ScrollingSense;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Options.fusion");
        Options OptionData = new Options();
        if (AudioTab.activeSelf)
        {
            OptionData.BGM = BGMSlider.GetComponent<Slider>().value;
            OptionData.SFX = SFXSlider.GetComponent<Slider>().value;
            OptionData.EdgeScrollingSense = loadedOptions.EdgeScrollingSense;
            OptionData.ScrollingSense = loadedOptions.ScrollingSense;
        }
        else if(ControlsTab.activeSelf)
        {
            OptionData.ScrollingSense = ScrollingSense.GetComponent<Slider>().value;
            OptionData.EdgeScrollingSense = EdgeScrollingSense.GetComponent<Slider>().value;
            OptionData.SFX = loadedOptions.SFX;
            OptionData.BGM= loadedOptions.BGM;
        }
        bf.Serialize(file, OptionData);
        file.Close();
    }

    public void LoadOptions()
    {
        if (File.Exists(Application.persistentDataPath + "/Options.fusion"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Options.fusion", FileMode.Open);
            Options OptionData = (Options)bf.Deserialize(file);
            file.Close();
            if (AudioTab.activeSelf)
            {
                BGMSlider.GetComponent<Slider>().value = OptionData.BGM;
                SFXSlider.GetComponent<Slider>().value = OptionData.SFX;
            }
            else if(ControlsTab.activeSelf)
            {
                ScrollingSense.GetComponent<Slider>().value=OptionData.ScrollingSense;
                EdgeScrollingSense.GetComponent<Slider>().value = OptionData.EdgeScrollingSense;
            }
        }
    }

    public void ResetOptions()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Options.fusion");
        Options OptionData = new Options();

        if (AudioTab.activeSelf)
        {
            OptionData.BGM = 50.0f;
            OptionData.SFX = 50.0f;

            BGMSlider.GetComponent<Slider>().value = OptionData.BGM;
            SFXSlider.GetComponent<Slider>().value = OptionData.SFX;

            OptionData.EdgeScrollingSense = EdgeScrollingSense.GetComponent<Slider>().value;
            OptionData.ScrollingSense = ScrollingSense.GetComponent<Slider>().value;

        }
        else if (ControlsTab.activeSelf)
        {
            OptionData.EdgeScrollingSense = 50.0f;
            OptionData.ScrollingSense = 50.0f;

            EdgeScrollingSense.GetComponent<Slider>().value = OptionData.EdgeScrollingSense;
            ScrollingSense.GetComponent<Slider>().value = OptionData.ScrollingSense;

            OptionData.BGM = BGMSlider.GetComponent<Slider>().value;
            OptionData.SFX = SFXSlider.GetComponent<Slider>().value;
        }
        bf.Serialize(file, OptionData);
        file.Close();
    }
    
    public void ToggleOnOff()
    {
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}

[Serializable]
class Options
{
    public float BGM;
    public float SFX;
    public float ScrollingSense;
    public float EdgeScrollingSense;
}
