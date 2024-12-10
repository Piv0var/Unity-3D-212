using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    private GameObject content;
    #region soundEffectsSlider
    private Slider soundEffectsSlider;
    private void initSoundEffectsSlider()
    {
        soundEffectsSlider = transform
            .Find("Content/Sound/EffectsSlider")
            .GetComponent<Slider>();

        GameState.effectsVolume = soundEffectsSlider.value;
    }
    #endregion

    #region SoundAmbientSlider
    private Slider SoundAmbientSlider;
    private void initSoundAmbientSlider()
    {
        SoundAmbientSlider = transform
            .Find("Content/Sound/AmbientSlider")
            .GetComponent<Slider>();

        GameState.ambientVolume = SoundAmbientSlider.value;
    }
    #endregion

    #region SoundsMuteToggle
    private Toggle SoundsMuteToggle;
    private void initSoundsMuteToggle()
    {
        SoundsMuteToggle = transform
            .Find("Content/Sound/MuteToggle")
            .GetComponent<Toggle>();

        GameState.isSoundsMuted = SoundsMuteToggle.isOn;
    }
    public void OnSoundsMuteToggle(System.Boolean value)
    {
        GameState.isSoundsMuted = value;
    }
    #endregion

    #region Controls Sensitivity
    private Slider SensXSlider;
    private Slider SensYSlider;
    private Toggle LinkTogle;
    private bool isLinked;
   
    private void initControlsSensitivity()
    {
        SensXSlider = transform
            .Find("Content/Controls/SensXSlider")
            .GetComponent<Slider>();
        SensYSlider = transform
            .Find("Content/Controls/SensYSlider")
            .GetComponent<Slider>();
        LinkTogle = transform
            .Find("Content/Controls/LinkTogle")
            .GetComponent<Toggle>();
        OnLinkToggle(LinkTogle.isOn);
        OnSensXSlider(SensXSlider.value);
        if (!isLinked) OnSensYSlider(SensYSlider.value);
    }
    public void OnSensXSlider(System.Single value)
    {
        float sens = Mathf.Lerp(0.01f, 0.1f, value);
        GameState.lookSensitivityX = sens;

        if (isLinked)
        {
            SensYSlider.value = value;
            GameState.lookSensitivityY = -sens;
        }
    }
    public void OnSensYSlider(System.Single value)
    {
        float sens = Mathf.Lerp(-0.01f, -0.1f, value);
        GameState.lookSensitivityY = sens;
        if (isLinked)
        {
            SensXSlider.value = value;
            GameState.lookSensitivityX = -sens;

        }

    }
    public void OnLinkToggle(System.Boolean value)
    {
        isLinked = value;
    }


    #endregion


    #region Controls Fpv limit
    private Slider fpvSlider;
    private void initControlsFpv()
    {
        fpvSlider = transform
            .Find("Content/Controls/FPVSlider")
            .GetComponent<Slider>();
        OnFpvSlider(fpvSlider.value);
    }
    public void OnFpvSlider(System.Single value)
    {
        GameState.fpvRange = Mathf.Lerp(0.3f, 1.1f, value);
    }
    #endregion

    void Start()
    {
        initSoundEffectsSlider();
        initSoundAmbientSlider();
        initSoundsMuteToggle();
        initControlsSensitivity();
        content = transform
            .Find("Content")
            .gameObject;
        

        if (content.activeInHierarchy)
        {
            Time.timeScale = 0.0f;
        }
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale= content.activeInHierarchy ? 1.0f : 0.0f;
            content.SetActive(!content.activeInHierarchy);
        }
    }
    public void OnSoundsEffectsChanged(System.Single value)
    {
        GameState.effectsVolume = value;
    }
    public void OnSoundsAmbientChanged(System.Single value)
    {
        GameState.ambientVolume = value;
    }
}
