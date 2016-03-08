using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.UI;
public class OptionMenu : MonoBehaviour {

    //states of the options menu
	enum OptionsState
    {
        Settings=0,
        Audio=1,
        Controls=2,
        Closed=3
    }

    //default state = closed
    OptionsState currentState = OptionsState.Closed;

    //tabs in the options
    public GameObject AudioTab;
    public GameObject ControlsTab;

    //panels 
    public GameObject SettingsPanel;
    public GameObject OptionsPanel;

    //sliders
    public GameObject BGMSlider;
    public GameObject SFXSlider;
    public GameObject ScrollingSense;
    public GameObject EdgeScrollingSense;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    //saves the options of the game when the user request to save
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

    //load the options of the game
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

    //resets options of the game to default
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

    //switch the state in which the option is in
    public void SwitchOptionsState(int state)
    {
        switch (state)
        {
            case 0:
                currentState = OptionsState.Settings;
                break;
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

        switch (currentState)
        {
            case OptionsState.Settings:
                if (SettingsPanel.activeSelf)
                {
                    SwitchOptionsState(3);
                }
                else
                {
                    SettingsPanel.SetActive(true);
                    OptionsPanel.SetActive(false);
                }
                break;
            case OptionsState.Audio:
                SettingsPanel.SetActive(false);
                AudioTab.SetActive(true);
                ControlsTab.SetActive(false);

                BGMSlider.transform.parent.GetChild(2).GetComponent<InputField>().text = 
                    BGMSlider.GetComponent<Slider>().value.ToString();

                SFXSlider.transform.parent.GetChild(2).GetComponent<InputField>().text =
                    SFXSlider.GetComponent<Slider>().value.ToString();

                break;
            case OptionsState.Controls:
                AudioTab.SetActive(false);
                ControlsTab.SetActive(true);

                EdgeScrollingSense.transform.parent.GetChild(2).GetComponent<InputField>().text =
                    EdgeScrollingSense.GetComponent<Slider>().value.ToString();
                SetInputValue(ScrollingSense);
                break;
            case OptionsState.Closed:
                AudioTab.SetActive(true);
                ControlsTab.SetActive(false);
                SettingsPanel.SetActive(false);

                break;
        }
    }

    public void SetSliderZero(GameObject test)
    {
        test.transform.GetChild(1).GetComponent<Slider>().value = 0;
    }

    public void SetSliderValue(GameObject test)
    {
        if (float.Parse(test.transform.GetChild(2).GetComponent<InputField>().text) > 100.0f)
        {
            test.transform.GetChild(2).GetComponent<InputField>().text = (100).ToString();
        }
        else if(float.Parse(test.transform.GetChild(2).GetComponent<InputField>().text) < 0.0f)
        {
            test.transform.GetChild(2).GetComponent<InputField>().text = (0).ToString();
        }
        test.transform.GetChild(1).GetComponent<Slider>().value = float.Parse(test.transform.GetChild(2).GetComponent<InputField>().text);
    }

    public void SetInputValue(GameObject TheSlider)
    {
        TheSlider.transform.parent.GetChild(2).GetComponent<InputField>().text =
                    (Mathf.Round(TheSlider.GetComponent<Slider>().value * 1000.0f) / 1000.0f).ToString();
    }

    public void checkInput(GameObject input)
    {
        if (input.GetComponent<Text>().text.Length == 0)
        {
            input.GetComponent<Text>().text = "0";
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
