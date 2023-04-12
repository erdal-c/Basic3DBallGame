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
}
