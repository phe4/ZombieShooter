using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//用来处理输入
public class PlayerInputHandler : MonoBehaviour
{
    //方便跨文件调用
    public static PlayerInputHandler Instance;
    public float lookSensitivity = 1f;

    public GameObject bag;
    public GameObject menu;
    public GameObject setting;
    private bool menuOpened = false;
    private bool bagOpened = false;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        bag.SetActive(false);
        menu.SetActive(false);
        setting.SetActive(false);
    }

    public Vector3 GetMoveInput()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));
        move = Vector3.ClampMagnitude(move, 1);
        return move;
    }

    public float GetMouseLookHorizontal()
    {
        return GetMouseLookAxis("Mouse X");
    }

    public float GetMouseLookVertical()
    {
        return GetMouseLookAxis("Mouse Y");
    }

    public float GetMouseLookAxis(string mouseInputName)
    {
        float i = Input.GetAxisRaw(mouseInputName);
        i *= lookSensitivity * 0.01f;

        return i;
    }

    public bool GetFireInputHeld()
    {
        if (bagOpened)
        {
            return false;
        }
        return Input.GetButton("Fire");
    }

    public bool GetReloadInputHeld()
    {
        return Input.GetButton("Reload");
    }

    // Update is called once per frame
    void Update()
    {
        //press esc to quit game
        if (Input.GetKeyDown("escape"))
        {
            if (menuOpened)
            {
                menu.SetActive(false);
                menuOpened = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                ResumeGame();
            }
            else
            {
                menu.SetActive(true);
                menuOpened = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                PauseGame();
            }
        }

        if (Input.GetKey("backspace"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // press tab to open bag
        if (Input.GetKeyDown("tab") && !menuOpened)
        {
            if (bagOpened)
            {
                bag.SetActive(false);
                bagOpened = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                ResumeGame();
            }
            else
            {
                bag.SetActive(true);
                bagOpened = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                PauseGame();
            }
        }
    }
    void PauseGame ()
    {
        Time.timeScale = 0;
    }
    void ResumeGame ()
    {
        Time.timeScale = 1;
    }
}
