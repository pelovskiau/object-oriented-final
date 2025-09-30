using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuUI : MonoBehaviour
{
    [Header("Main Menu Buttons")]
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button OptionsButton;
    [SerializeField] private Button ExitButton;
    [SerializeField] private Button CreditsButton;

    [Header("Back Buttons")]
    [SerializeField] private Button OptionsBackButton;
    [SerializeField] private Button DifficultyBackButton;

    [Header("Difficulty Buttons")]
    [SerializeField] private Button Easy;
    [SerializeField] private Button Medium;
    [SerializeField] private Button Hard;

    [Header("Input & Display")]
    [SerializeField] private Button SaveButton;
    [SerializeField] private TMP_InputField Name;
    [SerializeField] private TMP_Text PlayerName;
    private string newName;
    [SerializeField] private Slider volumeSlider;
    private float volumeSliderValue;
    [SerializeField] private AudioSource MenuAudio;

    [Header("Panels")]
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private GameObject DifficultyPanel;
    [SerializeField] private GameObject CreditsPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        PlayButton = transform.Find("MainOptions/Play").GetComponent<Button>();
        OptionsButton = transform.Find("MainOptions/Options").GetComponent<Button>();
        CreditsButton = transform.Find("MainOptions/Credits").GetComponent<Button>();
        ExitButton = transform.Find("MainOptions/Exit").GetComponent<Button>();

        OptionsBackButton = transform.Find("Options/Back").GetComponent<Button>();
        SaveButton = transform.Find("Options/Save").GetComponent<Button>();
        Name = transform.Find("Options/InputName").GetComponent<TMP_InputField>();
        volumeSlider = transform.Find("Options/Volume Slider").GetComponent<Slider>();

        Easy = transform.Find("PlayOptions/Easy").GetComponent<Button>();
        Medium = transform.Find("PlayOptions/Medium").GetComponent<Button>();
        Hard = transform.Find("PlayOptions/Hard").GetComponent<Button>();
        DifficultyBackButton = transform.Find("PlayOptions/Back").GetComponent<Button>();

        MainPanel = transform.Find("MainOptions").gameObject;
        OptionsPanel = transform.Find("Options").gameObject;
        DifficultyPanel = transform.Find("PlayOptions").gameObject;
        MenuAudio = GetComponent<AudioSource>();
        //finds all the components so I can unserialize.
    }
    public void Start()
    {
        MenuAudio.volume = DataManager.Instance.VolumeSetting; //sets the volume.
        volumeSliderValue = DataManager.Instance.VolumeSetting;
        volumeSlider.value = volumeSliderValue;
    }


    public void MainMenu()
    { 
        OptionsPanel.SetActive(false); //sets the back buttons
        DifficultyPanel.SetActive(false);
        MainPanel.SetActive(true);
    }
    public void Options()
    { 
        OptionsPanel.SetActive(true );
        MainPanel.SetActive(false);//swaps to options.
    }

    public void Difficulties()
    { 
        DifficultyPanel.SetActive(true);
        MainPanel.SetActive(false); //swaps to difficulty selection
    }

    public void SetVolume()
    { 
        volumeSliderValue= volumeSlider.value;
        MenuAudio.volume= volumeSliderValue;
        DataManager.Instance.VolumeSetting = volumeSliderValue;
        //fingers crossed this changes volume.
    }

    public void SaveButtonPressed()
    { 
        DataManager.Instance.Save();
        //save the data.
    }

    public void InputName()
    {
        if (Name == null)
        {
            Debug.LogWarning("Name input field is null.");
            return;
        }

        if (PlayerName == null)
        {
            Debug.LogWarning("PlayerName text field is null.");
            return;
        }

        if (DataManager.Instance == null)
        {
            Debug.LogWarning("DataManager.Instance is null.");
            return;
        }
        newName = Name.text;
        PlayerName.text = newName;
        DataManager.Instance.PlayerName = newName;
        Debug.Log($"Name updated to: {newName}");
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        // Update is called once per frame
    }
}
