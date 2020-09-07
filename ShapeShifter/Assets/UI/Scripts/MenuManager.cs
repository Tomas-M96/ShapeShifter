using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private static MenuManager instance;
    public static MenuManager Instance => instance;

    [SerializeField]
    private HUDMenu hudMenu = null;
    [SerializeField]
    private MainMenu mainMenu = null;
    [SerializeField]
    private HighScoresMenu highScoresMenu = null;
    [SerializeField]
    private GameOverMenu gameOverMenu = null;
    [SerializeField]
    private PauseMenu pauseMenu = null;

    private Transform menuParent;

    private Stack<Menu> menuStack = new Stack<Menu>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else 
        {
            instance = this;

            InitializeMenus();
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void InitializeMenus()
    {
        if (menuParent == null)
        {
            GameObject menuParentObject = new GameObject("Menus");
            menuParent = menuParentObject.transform;
        }

        Menu[] menuPrefabs = { mainMenu, hudMenu, highScoresMenu, gameOverMenu, pauseMenu };

        foreach (Menu prefab in menuPrefabs)
        {
            if (prefab != null)
            {
                Menu menuInstance = Instantiate(prefab, menuParent);
                if (prefab != mainMenu)
                {
                    menuInstance.gameObject.SetActive(false);
                }
                else 
                {
                    OpenMenu(menuInstance);
                }
            }
        }
    }

    public void OpenMenu(Menu menuInstance)
    {
        if (menuInstance == null)
        {
            Debug.LogWarning("Invalid Menu");
            return;
        }

        if (menuStack.Count > 0)
        {
            foreach (Menu menu in menuStack)
            {
                menu.gameObject.SetActive(false);
            }
        }

        menuInstance.gameObject.SetActive(true);
        menuStack.Push(menuInstance);
    }

    public void CloseMenu()
    {
        if (menuStack.Count == 0)
        {
            Debug.LogWarning("No menus in stack");
            return;
        }

        Menu topMenu = menuStack.Pop();
        topMenu.gameObject.SetActive(false);

        if (menuStack.Count > 0)
        {
            Menu nextMenu = menuStack.Peek();
            nextMenu.gameObject.SetActive(true);
        }
    }

    public void ReturnToMainMenu()
    {
        

        if (menuStack.Count > 0)
        {
            foreach (Menu menu in menuStack)
            {
                print(menuStack.Peek());
                if (menuStack.Peek() != mainMenu)
                {
                    menu.gameObject.SetActive(false);
                }
            }
            menuStack.Clear();
        }
        MainMenu.Open();
    }
}
