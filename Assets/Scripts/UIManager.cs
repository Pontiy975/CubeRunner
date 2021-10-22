using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

internal class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel, buttonPanel;

    [SerializeField] private Text mainText;
    [SerializeField] private string winText, failText;
    [SerializeField] private Slider manualSlider;

    internal static UIManager Instance { get; private set; }
    
    private int sliderDirection = 1;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsStart) Manual();
        else
        {
            manualSlider.gameObject.SetActive(false);
            mainPanel.SetActive(false);
        }
    }

    internal void ShowPanel(bool isWin, int cubesCount)
    {
        if (isWin) mainText.text = winText + cubesCount.ToString();
        else mainText.text = failText;

        mainPanel.SetActive(true);
        buttonPanel.SetActive(true);
    }

    private void Manual()
    {
        if (manualSlider.value == manualSlider.minValue) sliderDirection = 1;
        if (manualSlider.value == manualSlider.maxValue) sliderDirection = -1;

        manualSlider.value += Time.deltaTime * sliderDirection;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_ANDROID
            Application.Quit();
        #endif
    }
}
