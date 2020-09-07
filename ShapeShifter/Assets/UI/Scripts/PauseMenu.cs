using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu<PauseMenu>
{
    public void ResumePressed()
    {
        GameManager.Instance.ResumeGame();
        PauseMenu.Close();
    }
}
