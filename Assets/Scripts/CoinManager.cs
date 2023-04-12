using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    GameManager gameManager;

    GameObject paren;
    List<Transform> liste = new List<Transform>();
        

    // Start is called before the first frame update
    void Start()
    {

        gameManager = FindObjectOfType<GameManager>();

        paren = GameObject.Find("CoinParent");
        liste.AddRange(paren.gameObject.GetComponentsInChildren<Transform>());

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 180f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.scoreCount += 1;
            //Destroy(gameObject);
            gameObject.SetActive(false);
            UIUpdater.updaterInsatnce.ScoreTextter();
            UIUpdater.updaterInsatnce.coinSound.Play();
        }
    }

    public void CoinBearer2()
    {   
        foreach (Transform lis in liste)
        {
            if (lis.gameObject.activeSelf == false)
            {
                print("False");
                lis.gameObject.SetActive(true);
            }
        }
    }

    public void CoinBearer()    // Ýlk hali. Burada sadece ilk coin object geri geliyordu.
    {
        if (gameObject.activeSelf == false)
        {
            print("False");
            gameObject.SetActive(true);
        }
    }
}
