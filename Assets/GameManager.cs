using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timer;
    public string levelName;

    public static int levelNum = 0;
    public static int enemiesSpawned = 1;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(nextLevel(timer));
    }

    IEnumerator nextLevel(float timer)
    {
        yield return new WaitForSeconds(timer);
        levelNum++;
        if (levelNum % 5 == 0)
        {
            enemiesSpawned++;
        }
        SceneManager.LoadScene(levelName);
    }

}
