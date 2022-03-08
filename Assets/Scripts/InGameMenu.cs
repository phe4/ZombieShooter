using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    public Button mainMenu;
    public Button options;
    public Button quitGame;
    public Button settingsApply;
    public Button settingsCancel;
    public Button InstructionsConfirm;

    public Button instructions;
    public GameObject settings;
    public GameObject InstructionPage;
    private bool settingsOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        Button main = mainMenu.GetComponent<Button>();
		main.onClick.AddListener(MainMenu);

        Button option = options.GetComponent<Button>();
		option.onClick.AddListener(Options);

        Button quit = quitGame.GetComponent<Button>();
		quit.onClick.AddListener(QuitGame);

        Button cancel = settingsCancel.GetComponent<Button>();
		cancel.onClick.AddListener(Cancel);

        Button apply = settingsApply.GetComponent<Button>();
		apply.onClick.AddListener(Apply);

        Button instruction = instructions.GetComponent<Button>();
        instruction.onClick.AddListener(Instructions);

        Button instructionsConfirm = InstructionsConfirm.GetComponent<Button>();
        instructionsConfirm.onClick.AddListener(instructionConfirm);

        settings.SetActive(false);
    }

    void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1;
    }

    void Instructions()
    {
        InstructionPage.SetActive(true);
    }

    void instructionConfirm()
    {
        InstructionPage.SetActive(false);
    }

    void Options()
    {
        settings.SetActive(true);
    }
    void Apply()
    {

    }
    void Cancel()
    {
        settings.SetActive(false);
    }
    void QuitGame()
    { 
        Application.Quit();  
    }
    
}
