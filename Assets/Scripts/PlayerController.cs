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


    void Start()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
        respawnPoint = this.transform.position;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        PlayerFrwMovement();
        rigidbody.velocity = new Vector3(movementSpeed*movement, rigidbody.velocity.y, rigidbody.velocity.z);
    }

    void PlayerFrwMovement()
    {
        rigidbody.AddForce(0, 0, playerSpeed * verticalMove * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Barrier"))
        {
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
