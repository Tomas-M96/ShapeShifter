using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Sprite playerSprite;
    public Sprite PlayerSprite => playerSprite;

    private Sprite currentSprite; 

    public bool isPaused = false;
    private bool gameOver = false;

    [Header("Player Sprites")]
    [SerializeField]
    [Tooltip("Array of sprites that the player can be")]
    private Sprite[] playerSprites = null;

    [Header("Player Variables")]
    [SerializeField]
    [Tooltip("The delay on the destruction of the player")]
    private float deathDelay = 1f;

    private Vector2 mousePos;
    private float dX;
    private ParticleSystem deathParticle;
    private AudioSource deathSound;

    private void Awake()
    {
        deathParticle = gameObject.GetComponentInChildren<ParticleSystem>();
        currentSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        deathSound = gameObject.GetComponentInChildren<AudioSource>();
    }

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = playerSprites[0];
        playerSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        GetNextShape();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Get shape sprite and player sprite
        Sprite shapeSprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
        currentSprite = gameObject.GetComponent<SpriteRenderer>().sprite;

        //If the sprites match
        if (shapeSprite == currentSprite)
        {
            //Call the destroy function for the shape
            collision.gameObject.GetComponent<Shape>().DestroyShape();
            //Call the add score function
            GameManager.Instance.AddScore(1);
            //Change the player shape and get the next shape
            ChangeShape();
            GetNextShape();
        }
        else
        {
            gameOver = true;
            //Call the coroutine to delete the player
            StartCoroutine(PlayerDeath());
        }
    }

    private void ChangeShape()
    {
        //Set the player sprite to what is stored in the playerSprite object
        gameObject.GetComponent<SpriteRenderer>().sprite = playerSprite;
    }

    private void GetNextShape()
    {
        if (!gameOver)
        {
            int randomSprite = UnityEngine.Random.Range(0, playerSprites.Count());
            playerSprite = playerSprites[randomSprite];
            HUDMenu.Instance.DisplayNextSprite(playerSprite);
        }
    }

    private IEnumerator PlayerDeath()
    {
        deathParticle.textureSheetAnimation.SetSprite(0, currentSprite);
        deathParticle.Play();
        Debug.Log("Particle Played");
        deathSound.Play();
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(deathDelay);
        //Destroy the player
        Destroy(gameObject);
        //Call the endgame method
        GameManager.Instance.EndGame();
    }

    private void OnMouseDown()
    {
        dX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
    }

    private void OnMouseDrag()
    {
        if (!gameOver)
        {
            if (!isPaused)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float clampedPos = Mathf.Clamp((mousePos.x - dX), (float)Math.Truncate(-GameManager.Instance.ScreenBounds.x), (float)Math.Truncate(GameManager.Instance.ScreenBounds.x));
                transform.position = new Vector2(clampedPos, transform.position.y);
            }
        }
    }
}
