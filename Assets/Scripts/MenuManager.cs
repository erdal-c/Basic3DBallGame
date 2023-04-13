using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Text EndMenuText;

    public AudioSource buttonSound;
    public AudioSource winSound;
    public AudioSource startSound;

    // Start is called before the first frame update
    void Start()
    {   
        if(EndMenuText != null)
        {
            TextGetter();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "EndMenu")
        {
            
            if (Timer.GetInstance().ActiveCheck() == true)
            {
                Timer.GetInstance().GetTotalTime();
                winSound.Play();
                print(Timer.GetInstance().ActiveCheck());
                
            }           
        }
    }

    public void PlayButton()
    {
        buttonSound.Play();
        if(Timer.GetInstance() != null)
        {
            Timer.GetInstance().TimeStart();
        }
        SceneManager.LoadScene(1);
        UIUpdater.updaterInsatnce.ScoreTextter();
        startSound.Play();
    }

    public void QuitButton() 
    {
        buttonSound.Play();
        Application.Quit();
    }

    public void EndMenuPlayAgain()
    {
        buttonSound.Play();
        if (Timer.GetInstance() != null)
        {
            print("asdf");
            startSound.Play();
            SceneManager.LoadScene(1);
            Timer.GetInstance().TotalTimeCleaner();
            Timer.GetInstance().TimeStart();
            UIUpdater.updaterInsatnce.TotalScoreZero();        
        }    
    }

    public void TextGetter()
    {
        EndMenuText.text = "Total Time : " + Timer.GetInstance().GetTotalTime().ToString("0.00") + 
            "\nTotal Score : " + UIUpdater.updaterInsatnce.TotalScoreCount();
    }
}
