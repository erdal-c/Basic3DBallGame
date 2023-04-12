using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private new Rigidbody rigidbody;

    GameManager gameManager;

    [SerializeField]
    private float playerSpeed;
    float movement;
    float verticalMove;
    public float movementSpeed;

    [HideInInspector]
    public Vector3 respawnPoint;



    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
        respawnPoint = this.transform.position;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        //rigidbody.AddForce(0, 0, playerSpeed);
        PlayerFrwMovement();
        rigidbody.velocity = new Vector3(movementSpeed*movement, rigidbody.velocity.y, rigidbody.velocity.z);

    }

    void PlayerFrwMovement()
    {
        //if (verticalMove>=0)
        //{
        //    ;
        //    rigidbody.AddForce(0, 0, playerSpeed*verticalMove);
        //}
        rigidbody.AddForce(0, 0, playerSpeed * verticalMove * Time.fixedDeltaTime);
        
        //Debug.Log(Input.GetAxis("Vertical"));  Vertical ( dikey) hýz -1, 1  arasýnda deðer alýr. Bu satýr ile bu deðeri yazdýrýyoruz.

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Barrier"))
        {
            //this.transform.position = respawnPoint;        Ýlk Hali bu þekilde
            rigidbody.velocity = Vector3.zero;
            gameManager.PlayerRespawner();
            UIUpdater.updaterInsatnce.crashSound.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndTrigger"))
        {
            print("fdf");
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings-1)
            {
                UIUpdater.updaterInsatnce.NextLevelCaller();
            }            
        }
    }
}
