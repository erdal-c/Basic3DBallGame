using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager OnlyGameManager;
    PlayerController playerController;
    CoinManager coinManager;

    float respawnDelay = 0.5f;
    bool isGameEnding = false;

    [HideInInspector]
    public int scoreCount;

    //private int _property;
    //public int propertyscore          Propert olarak score yazmak istersek böyle property oluþturulabilir. 
    //{
    //    get
    //    {
    //        return _property;
    //    }
    //    set
    //    {
    //        _property = value;
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        coinManager = FindObjectOfType<CoinManager>();

        if (OnlyGameManager != null)
        {
            Destroy(OnlyGameManager);
            return;
        }
        OnlyGameManager = this;
        //DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameManager GetInstnce()
    {
        return OnlyGameManager;
    }

    public int GetScore()
    {
        return scoreCount;
    }
    public void ScoreZero()
    {
        scoreCount = 0;
    }

    public void PlayerRespawner()
    {
        if(isGameEnding == false)
        {
            isGameEnding = true;
            StartCoroutine(RespawnCouroutine());    //StartCoroutine("RespawnCouroutine"); olarak da kullanýlabilir.
            scoreCount= 0;
            UIUpdater.updaterInsatnce.ScoreTextter();
            coinManager.CoinBearer2();
            Timer.GetInstance().TimeStop();
        }
    }

    IEnumerator RespawnCouroutine()
    {
        playerController.gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnDelay);
        playerController.transform.position = playerController.respawnPoint;
        playerController.gameObject.SetActive(true);
        //coinManager.gameObject.SetActive(true);
        Timer.GetInstance().TimeZero();
        Timer.GetInstance().TimeStart();
        isGameEnding = false;
    }

    //public void ScoreTextter()
    //{
    //    //text.text = scoreCount.ToString();
    //    scoreText.text = string.Format("Puan = {0}",scoreCount);
    //}

    //void CooinBearer()
    //{
    //    if (coinManager.gameObject.activeSelf == false)
    //    {
    //        print("False");
    //        //coinManager.gameObject.SetActive(true);
    //    }
    //}

    //public void NextLevelCaller()
    //{
    //    panelText.text = "Congratulations!\r\nMission Complete\r\n\n\nTotal Score : " + scoreCount;
    //    print(panelText.text);
    //    LevelUpPannel.SetActive(true);
    //    //panelText.text = string.Format("Congratulations!\r\nMission Complete\r\nTotal Score : {0}", scoreCount);
        
    //    Invoke("NextLevel", 1.5f);
    //}

    //public void NextLevel()
    //{
    //    LevelUpPannel.SetActive(false);
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    //}
}
