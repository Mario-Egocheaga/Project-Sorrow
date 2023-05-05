using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timer;
    public string levelName;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(nextLevel(timer));
    }

    IEnumerator nextLevel(float timer)
    {
        yield return new WaitForSeconds(timer);

        SceneManager.LoadScene(levelName);
    }

}
