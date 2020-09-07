using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    //Singleton Pattern
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    //Variables
    [Header("Player")]
    [SerializeField]
    [Tooltip("Player object to spawn")]
    private Player playerObject = null;
    [SerializeField]
    [Tooltip("Player spawn value on X axis")]
    private float spawnX = 0f;
    [SerializeField]
    [Tooltip("Max spawn value on Y axis")]
    private float spawnY = -3.5f;

    [Header("Shapes")]
    [SerializeField]
    [Tooltip("List of shapes to spawn")]
    private List<Shape> shapeList = null;
    [SerializeField]
    [Tooltip("Respawn rate of shapes in seconds")]
    private float maxShapeRespawnRate = 1f;
    [SerializeField]
    [Tooltip("Respawn rate of shapes in seconds")]
    private float minShapeRespawnRate = 1f;

    private bool isPaused = false;
    public bool IsPaused => isPaused;

    //Screen Bounds
    private Vector2 screenBounds;
    public Vector2 ScreenBounds => screenBounds;

    //Player Score
    private int score = 0;
    public int Score => score;

    //Time Elapsed
    private float timeElapsed = 0f;
    public float TimeElapsed => timeElapsed;

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
        timeElapsed += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
            FindObjectOfType<Player>().isPaused = true;
            PauseMenu.Open();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        
        if (FindObjectOfType<Player>() == false)
        {
            InstantiatePlayer();
        }

        isPaused = false;
        FindObjectOfType<Player>().isPaused = false;
    }

    private IEnumerator SpawnShapes()
    {
        while (true)
        {
            SpawnShape();
            AlterRespawnRate();
            yield return new WaitForSeconds(UnityEngine.Random.Range(minShapeRespawnRate, maxShapeRespawnRate));
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
        print("Score: " + score);
        HUDMenu.Instance.UpdateScore(score);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    public void EndGame()
    {
        //Pause the game
        PauseGame();
        //Save the score
        JSONLoader jsonLoader = new JSONLoader();
        jsonLoader.SaveJsonToFile("test", score, timeElapsed);
        //Open the Game Over Menu
        GameOverMenu.Open();
    }

    public void ResetGame()
    {
        score = 0;
        timeElapsed = 0;
        
        Shape[] shapes = FindObjectsOfType<Shape>();
        foreach (Shape shape in shapes)
        {
            Destroy(shape.gameObject);
        }

        if (FindObjectOfType<Player>() == true)
        {
            Destroy(FindObjectOfType<Player>());
        }
    }

    public void SpawnPlayer()
    {
        InstantiatePlayer();
    }

    private void AlterRespawnRate()
    {
        if ((int)timeElapsed % 10 == 0 && (int)timeElapsed >= 10)
        {
            if (minShapeRespawnRate > 0.3f)
            {
                minShapeRespawnRate -= 0.1f;
            }

            if (maxShapeRespawnRate > 0.5f && timeElapsed >= 30)
            {
                maxShapeRespawnRate -= 0.1f;
            }
        }
    }
}
