using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalController : MonoBehaviour
{
    public GameObject enemyFolder;
    public Text enemyCount;
    public Text currentGoal;
    public Text warning;
    public Text resultText;
    public GameObject resultMenu;
    public GameObject Player;
    private int startEnemyCount;
    private bool isInstantiate = false;
    private bool isTransition = false;
    private bool isCounting = false;
    private bool isTransitionComplete = false;
    public GameObject bossPrefab;
    public GameObject Transition;

    // Start is called before the first frame update
    void Start()
    {
        startEnemyCount = enemyFolder.transform.childCount;
        warning.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        int killed = startEnemyCount - enemyFolder.transform.childCount;
        if (!isInstantiate){
            enemyCount.text = killed + "/" + startEnemyCount;
        }
        //enemyFolder.transform.childCount == 0
        if (isTransition)
        {
            Transition.SetActive(true);
            Transform imageTransfrom = Transition.transform.GetChild(1);
            Image image = imageTransfrom.gameObject.GetComponent<Image>();
            Transform textTransform = Transition.transform.GetChild(0);
            
            Text TransitionText = textTransform.gameObject.GetComponent<Text>();
            Color Icolor = image.color;
            Color Tcolor = TransitionText.color;
            if (!isTransitionComplete)
            {
                Icolor.a += 0.01f;
                Tcolor.a += 0.02f;
                image.color = Icolor;
                TransitionText.color = Tcolor;
                if (Icolor.a >= 1.0f)
                {
                    isTransitionComplete = true;
                    Player.transform.position = new Vector3(410f, 17f, 453f);
                    Player.GetComponent<CharacterController>().enabled = true;
                }
            }
            else
            {
                Icolor.a -= 0.01f;
                Tcolor.a -= 0.02f;
                image.color = Icolor;
                TransitionText.color = Tcolor;
                if (Icolor.a <= 0.0f)
                {
                    isTransition = false;
                }
            }
        }
        if (enemyFolder.transform.childCount == 0)
        // if (killed > 0)
        {
            // WinGame();
            Transition.SetActive(true);
            Transform textTransform = Transition.transform.GetChild(0);
            Text TransitionText = textTransform.gameObject.GetComponent<Text>();
            StartCoroutine(StartCountdown(5f, isInstantiate));

       
            // if(isInstantiate == false)
            // {
            //     isTransition = true;
            //     currentGoal.text = "    Kill Boss: "; 
            //     isInstantiate = true;
            //     Player.GetComponent<CharacterController>().enabled = false;
            //     Vector3 pos = new Vector3(438f,-9f,453f);
            //     // Vector3(-510.937134,-227.34436,-529.339355)
            //     GameObject boss = GameObject.Instantiate(bossPrefab, pos, Quaternion.identity);
            //     boss.transform.parent = GameObject.Find("Enemy").transform;
            //     enemyCount.text = 0 + "/" + 1;
            // }
            if(enemyFolder.transform.childCount == 0)
            {
                WinGame();
            }
        }
    }

    private float currCountdownValue;
    public IEnumerator StartCountdown(float countdownValue, bool isInstantiate)
    {
        if(!isCounting){
            isCounting = true;
            warning.gameObject.SetActive(true);
            currCountdownValue = countdownValue;
            while (currCountdownValue >= 0)
            {
                warning.text = "you have " + currCountdownValue + " second to prepare!";
                yield return new WaitForSeconds(1.0f);
                currCountdownValue--;
                if(currCountdownValue <= 0)
                {
                    warning.gameObject.SetActive(false);
                    startTransition();
                }
            }
        }   
    }

    void startTransition(){
        if(isInstantiate == false)
            {
                isTransition = true;
                currentGoal.text = "    Kill Boss: "; 
                isInstantiate = true;
                Player.GetComponent<CharacterController>().enabled = false;
                Vector3 pos = new Vector3(438f,-9f,453f);
                // Vector3(-510.937134,-227.34436,-529.339355)
                GameObject boss = GameObject.Instantiate(bossPrefab, pos, Quaternion.identity);
                boss.transform.parent = GameObject.Find("Enemy").transform;
                enemyCount.text = 0 + "/" + 1;
            }
        if (isTransition)
        {
            Transition.SetActive(true);
            Transform imageTransfrom = Transition.transform.GetChild(1);
            Image image = imageTransfrom.gameObject.GetComponent<Image>();
            Transform textTransform = Transition.transform.GetChild(0);
            
            Text TransitionText = textTransform.gameObject.GetComponent<Text>();
            Color Icolor = image.color;
            Color Tcolor = TransitionText.color;
            if (!isTransitionComplete)
            {
                Icolor.a += 0.01f;
                Tcolor.a += 0.02f;
                image.color = Icolor;
                TransitionText.color = Tcolor;
                if (Icolor.a >= 1.0f)
                {
                    isTransitionComplete = true;
                    Player.transform.position = new Vector3(410f, 17f, 453f);
                    Player.GetComponent<CharacterController>().enabled = true;
                }
            }
            else
            {
                Icolor.a -= 0.01f;
                Tcolor.a -= 0.02f;
                image.color = Icolor;
                TransitionText.color = Tcolor;
                if (Icolor.a <= 0.0f)
                {
                    isTransition = false;
                }
            }
        }

    }

    void WinGame()
    {
        resultText.text = "You Win!";
        resultMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PauseGame();
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }
}

