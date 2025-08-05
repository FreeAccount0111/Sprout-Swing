using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private List<GameObject> levels = new List<GameObject>();
    private GameObject currentLevel = null;
    public int indexCurrentLevel;
    
    private void Awake()
    {
        Instance = this;
    }

    public void LoadGame(int indexLevel)
    {
        if(currentLevel != null)
            Destroy(currentLevel);

        indexCurrentLevel = indexLevel;
        currentLevel = Instantiate(levels[indexCurrentLevel]);
        //CircleOutline.Instance.ScaleOut();
    }

    public void ResetLevel()
    {

    }

    public void CheckGame()
    {

    }
}
