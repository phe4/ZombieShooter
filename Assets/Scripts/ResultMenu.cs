using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultMenu : MonoBehaviour
{
    public Button mainMenu;
    public Button quitGame;
    public Button restartGame;

    // Start is called before the first frame update
    void Start()
    {
        Button main = mainMenu.GetComponent<Button>();
        main.onClick.AddListener(MainMenu);

        Button quit = quitGame.GetComponent<Button>();
        quit.onClick.AddListener(QuitGame);

        Button option = restartGame.GetComponent<Button>();
        option.onClick.AddListener(RestartGame);
    }

    void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1;
    }
    
    void QuitGame()
    {
        Application.Quit();
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
