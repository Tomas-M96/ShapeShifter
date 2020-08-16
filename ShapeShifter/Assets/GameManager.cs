using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    private void Awake()
    {
        if (GM != null)
        {
            GameObject.Destroy(GM);
        }
        else
        {
            GM = this;
        }

        DontDestroyOnLoad(this);
    }

    [Header("Player")]
    [SerializeField]
    [Tooltip("Player object to spawn")]
    private Player playerObject;
    [SerializeField]
    [Tooltip("Player spawn value on X axis")]
    private float spawnX = 0f;
    [SerializeField]
    [Tooltip("Max spawn value on Y axis")]
    private float spawnY = -3.5f;

    [Header("Shapes")]
    [SerializeField]
    [Tooltip("List of shapes to spawn")]
    private List<Shape> shapeList;
    [SerializeField]
    [Tooltip("Respawn rate of shapes in seconds")]
    private float shapeRespawnRate = 1f;

    //Screen Bounds
    private Vector2 screenBounds;

    //Player Score
    private int score = 0;

    //Time Elapsed
    private float timeElapsed = 0f;



    // Start is called before the first frame update
    void Start()
    {
        ScreenBoundsSetup();
        InstantiatePlayer();
        StartCoroutine(SpawnShapes());
    }

    private void InstantiatePlayer()
    {
        Vector2 playerSpawn = new Vector2(spawnX, spawnY);
        Instantiate<Player>(playerObject, playerSpawn, Quaternion.identity);   
    }
    // Update is called once per frame
    void Update()
    {
        //timeElapsed += Time.deltaTime;
        //print(timeElapsed);
    }

    private IEnumerator SpawnShapes()
    {
        while (true)
        {
            SpawnShape();
            yield return new WaitForSeconds(shapeRespawnRate);
        }
    }

    private void SpawnShape()
    {
        int randomSpawn = UnityEngine.Random.Range(0, shapeList.Count);
        Vector2 spawnLocation = new Vector2(UnityEngine.Random.Range((float)Math.Truncate(-screenBounds.x), (float)Math.Truncate(screenBounds.x)), screenBounds.y + 2);
        Instantiate<Shape>(shapeList[randomSpawn], spawnLocation, Quaternion.identity);
    }

    private void ScreenBoundsSetup()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    public void AddScore(int addition)
    {
        score += addition;
        print(score);
    }
}
