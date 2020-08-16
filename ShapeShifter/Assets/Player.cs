using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Sprite playerSprite;
    [Header("Player Sprites")]
    [SerializeField]
    [Tooltip("Array of sprites that the player can be")]
    private Sprite[] playerSprites;

    private Vector2 mousePos;
    private float dX;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = playerSprites[0];
        playerSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        GetNextShape();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Sprite shapeSprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
        Sprite currentSprite = gameObject.GetComponent<SpriteRenderer>().sprite;

        if (shapeSprite == currentSprite)
        {
            Destroy(collision.gameObject);
            print("collision");
            GameManager.GM.AddScore(1);
            ChangeShape();
            GetNextShape();
        }
        else
        {
            PlayerDeath();
        }
    }

    private void ChangeShape()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = playerSprite;
    }

    private void GetNextShape()
    {
        int randomSprite = Random.Range(0, playerSprites.Count());
        playerSprite = playerSprites[randomSprite];
        print(playerSprite);
    }

    private void PlayerDeath()
    {
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        dX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
    }

    private void OnMouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePos.x - dX, transform.position.y);
    }
}
