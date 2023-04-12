using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{

    public Text scoreText;
    public GameObject LevelUpPannel;
    public Text panelText;
    public GameObject pauseMenu;

    public static UIUpdater updaterInsatnce;

    public Animator animBool;
    public AudioSource buttonSound;
    public AudioSource crashSound;
    public AudioSource coinSound;
    public AudioSource levelUpSound;

    int totalScore;

    // Start is called before the first frame update
    void Start()
    {
        if(updaterInsatnce != null) 
        {
            Destroy(this.gameObject);
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                Timer.GetInstance().TimeStart();
            }
            print("destroyyy");
            return;
        }
        updaterInsatnce = this;
        print("Id : " + updaterInsatnce.GetInstanceID());
        DontDestroyOnLoad(this.gameObject);

        //print(GameManager.GetInstnce().GetScore());
          
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int TotalScoreCount()
    {
        return totalScore;
    }

    public void TotalScoreZero()
    {
        totalScore = 0;
    }
    public void ScoreTextter()
    {
        //text.text = scoreCount.ToString();
        scoreText.text = string.Format("Score = {0}", GameManager.GetInstnce().GetScore());
    }
    public void NextLevelCaller()
    {
        panelText.text = "Congratulations!\r\nLevel Complete\r\n\n\nScore : " + GameManager.GetInstnce().GetScore();
        print(panelText.text);

        totalScore += GameManager.GetInstnce().GetScore();
        //LevelUpPannel.SetActive(true);

        //panelText.text = string.Format("Congratulations!\r\nMission Complete\r\nTotal Score : {0}", scoreCount);

        Timer.GetInstance().GetTotalTime();
        print(Timer.GetInstance().GetTotalTime());

        animBool = FindObjectOfType<Animator>();
        //animBool.SetBool("PanelActive", true);
        //print("before invoke");
        //Invoke("NextLevel", 1.5f);
        //print("after invoke");

        levelUpSound.Play();
        StartCoroutine(PanelShow());
        StartCoroutine(NextLvlCouroutine());
    }

    public void NextLevel()
    {
        print("is Invoking : " + IsInvoking("NextLevel"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        animBool.SetBool("PanelActive", false);
        LevelUpPannel.SetActive(false);
        Timer.GetInstance().TimeStart();
    }

    IEnumerator NextLvlCouroutine()
    {
        yield return new WaitForSeconds(1.4f);        
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        Timer.GetInstance().TimeStart();
        print(GameManager.GetInstnce().GetScore());
        GameManager.GetInstnce().ScoreZero();
        ScoreTextter();
    }
    
    IEnumerator PanelShow()
    {
        LevelUpPannel.SetActive(true);
        animBool = FindObjectOfType<Animator>();
        animBool.SetBool("PanelActive", true);
        yield return new WaitForSeconds(1.5f);
        animBool.SetBool("PanelActive", false);
        LevelUpPannel.SetActive(false);
    }

    public void PauseMenuButton()
    {
        buttonSound.Play();
        if (pauseMenu.activeSelf == true && Time.timeScale == 0)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;            
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ConitinueButton()
    {
        buttonSound.Play();
        pauseMenu.SetActive(false);
        Timer.GetInstance().TimeStart();
        Time.timeScale = 1;
    }

    public void MainMenuButton()   //Pause menudeki Main menu butonu için
    {
        buttonSound.Play();
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Timer.GetInstance().TimeStop();
        Timer.GetInstance().TimeZero();
        totalScore= 0;
        SceneManager.LoadScene(0);
    }

}
