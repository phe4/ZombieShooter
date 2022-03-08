using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{
    public Button startGame;
    public Button options;
    public Button quitGame;
    public Button InstructionsConfirm;
    public Button instructions;
    public GameObject InstructionPage;
    // Start is called before the first frame update
    void Start()
    {
        Button start = startGame.GetComponent<Button>();
		start.onClick.AddListener(StartGame);

        Button option = options.GetComponent<Button>();
		option.onClick.AddListener(Options);

        Button quit = quitGame.GetComponent<Button>();
		quit.onClick.AddListener(QuitGame);

        Button instruction = instructions.GetComponent<Button>();
        instruction.onClick.AddListener(Instructions);

        Button instructionsConfirm = InstructionsConfirm.GetComponent<Button>();
        instructionsConfirm.onClick.AddListener(instructionConfirm);

    }

    void StartGame()
    {
        SceneManager.LoadScene("Scene_A");
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
        
    }
    void QuitGame()
    { 
        Application.Quit();  
    }
    
}
