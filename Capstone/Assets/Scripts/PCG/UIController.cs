using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("References to Main Script")]
    [SerializeField] private Main mainScript;

    [Header("Width UI")]
    [SerializeField] private Slider sliderWidth;
    [SerializeField] private TMP_InputField inputWidth;

    [Header("Height UI")]
    [SerializeField] private Slider sliderHeight;
    [SerializeField] private TMP_InputField inputHeight;

    [Header("Room Percentage UI")]
    [SerializeField] private Slider sliderRoomPercentage;
    [SerializeField] private TMP_InputField inputRoomPercentage;

    [Header("Generations UI")]
    [SerializeField] private Slider sliderGenerations;
    [SerializeField] private TMP_InputField inputGenerations;

    [Header("Birth Limit UI")]
    [SerializeField] private Slider sliderBirthLimit;
    [SerializeField] private TMP_InputField inputBirthLimit;

    [Header("Death Limit UI")]
    [SerializeField] private Slider sliderDeathLimit;
    [SerializeField] private TMP_InputField inputDeathLimit;

    [Header("Room Threshold UI")]
    [SerializeField] private Slider sliderRoomThreshold;
    [SerializeField] private TMP_InputField inputRoomThreshold;

    private void Awake()
    {
        // Set up slider events
        sliderWidth.onValueChanged.AddListener(OnWidthSliderChanged);
        sliderHeight.onValueChanged.AddListener(OnHeightSliderChanged);
        sliderRoomPercentage.onValueChanged.AddListener(OnRoomPercentageSliderChanged);
        sliderGenerations.onValueChanged.AddListener(OnGenerationsSliderChanged);
        sliderBirthLimit.onValueChanged.AddListener(OnBirthLimitSliderChanged);
        sliderDeathLimit.onValueChanged.AddListener(OnDeathLimitSliderChanged);
        sliderRoomThreshold.onValueChanged.AddListener(OnRoomThresholdSliderChanged);

        // Set up input field events
        inputWidth.onEndEdit.AddListener(OnWidthInputChanged);
        inputHeight.onEndEdit.AddListener(OnHeightInputChanged);
        inputRoomPercentage.onEndEdit.AddListener(OnRoomPercentageInputChanged);
        inputGenerations.onEndEdit.AddListener(OnGenerationsInputChanged);
        inputBirthLimit.onEndEdit.AddListener(OnBirthLimitInputChanged);
        inputDeathLimit.onEndEdit.AddListener(OnDeathLimitInputChanged);
        inputRoomThreshold.onEndEdit.AddListener(OnRoomThresholdInputChanged);
    }

    private void Start()
    {
        // Initialize the UI with the current values in mainScript
        sliderWidth.value = mainScript.width;
        inputWidth.text = mainScript.width.ToString();

        sliderHeight.value = mainScript.height;
        inputHeight.text = mainScript.height.ToString();

        sliderRoomPercentage.value = mainScript.roomPercentage;
        inputRoomPercentage.text = mainScript.roomPercentage.ToString("F2");

        sliderGenerations.value = mainScript.generations;
        inputGenerations.text = mainScript.generations.ToString();

        sliderBirthLimit.value = mainScript.birthLimit;
        inputBirthLimit.text = mainScript.birthLimit.ToString();

        sliderDeathLimit.value = mainScript.deathLimit;
        inputDeathLimit.text = mainScript.deathLimit.ToString();

        sliderRoomThreshold.value = mainScript.roomThreshold;
        inputRoomThreshold.text = mainScript.roomThreshold.ToString();
    }

    // ------ Slider to Main ------
    private void OnWidthSliderChanged(float newValue)
    {
        mainScript.width = (int)newValue;
        inputWidth.text = mainScript.width.ToString();
    }

    private void OnHeightSliderChanged(float newValue)
    {
        mainScript.height = (int)newValue;
        inputHeight.text = mainScript.height.ToString();
    }

    private void OnRoomPercentageSliderChanged(float newValue)
    {
        mainScript.roomPercentage = newValue;
        inputRoomPercentage.text = mainScript.roomPercentage.ToString("F2");
    }

    private void OnGenerationsSliderChanged(float newValue)
    {
        mainScript.generations = (int)newValue;
        inputGenerations.text = mainScript.generations.ToString();
    }

    private void OnBirthLimitSliderChanged(float newValue)
    {
        mainScript.birthLimit = (int)newValue;
        inputBirthLimit.text = mainScript.birthLimit.ToString();
    }

    private void OnDeathLimitSliderChanged(float newValue)
    {
        mainScript.deathLimit = (int)newValue;
        inputDeathLimit.text = mainScript.deathLimit.ToString();
    }

    private void OnRoomThresholdSliderChanged(float newValue)
    {
        mainScript.roomThreshold = (int)newValue;
        inputRoomThreshold.text = mainScript.roomThreshold.ToString();
    }

    // ------ Input to Main ------
    private void OnWidthInputChanged(string newText)
    {
        if (int.TryParse(newText, out int newWidth))
        {
            newWidth = Mathf.Clamp(newWidth, 10, 200);
            mainScript.width = newWidth;
            sliderWidth.value = newWidth;
        }
        else
        {
            inputWidth.text = mainScript.width.ToString();
        }
    }

    private void OnHeightInputChanged(string newText)
    {
        if (int.TryParse(newText, out int newHeight))
        {
            newHeight = Mathf.Clamp(newHeight, 10, 200);
            mainScript.height = newHeight;
            sliderHeight.value = newHeight;
        }
        else
        {
            inputHeight.text = mainScript.height.ToString();
        }
    }

    private void OnRoomPercentageInputChanged(string newText)
    {
        if (float.TryParse(newText, out float newPercentage))
        {
            newPercentage = Mathf.Clamp(newPercentage, 0f, 1f);
            mainScript.roomPercentage = newPercentage;
            sliderRoomPercentage.value = newPercentage;
        }
        else
        {
            inputRoomPercentage.text = mainScript.roomPercentage.ToString("F2");
        }
    }

    private void OnGenerationsInputChanged(string newText)
    {
        if (int.TryParse(newText, out int newGenerations))
        {
            newGenerations = Mathf.Clamp(newGenerations, 1, 20);
            mainScript.generations = newGenerations;
            sliderGenerations.value = newGenerations;
        }
        else
        {
            inputGenerations.text = mainScript.generations.ToString();
        }
    }

    private void OnBirthLimitInputChanged(string newText)
    {
        if (int.TryParse(newText, out int newBirthLimit))
        {
            newBirthLimit = Mathf.Clamp(newBirthLimit, 0, 8);
            mainScript.birthLimit = newBirthLimit;
            sliderBirthLimit.value = newBirthLimit;
        }
        else
        {
            inputBirthLimit.text = mainScript.birthLimit.ToString();
        }
    }

    private void OnDeathLimitInputChanged(string newText)
    {
        if (int.TryParse(newText, out int newDeathLimit))
        {
            newDeathLimit = Mathf.Clamp(newDeathLimit, 0, 8);
            mainScript.deathLimit = newDeathLimit;
            sliderDeathLimit.value = newDeathLimit;
        }
        else
        {
            inputDeathLimit.text = mainScript.deathLimit.ToString();
        }
    }

    private void OnRoomThresholdInputChanged(string newText)
    {
        if (int.TryParse(newText, out int newRoomThreshold))
        {
            newRoomThreshold = Mathf.Clamp(newRoomThreshold, 0, 100);
            mainScript.roomThreshold = newRoomThreshold;
            sliderRoomThreshold.value = newRoomThreshold;
        }
        else
        {
            inputRoomThreshold.text = mainScript.roomThreshold.ToString();
        }
    }
}
