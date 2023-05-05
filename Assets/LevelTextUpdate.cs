using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTextUpdate : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    private void Start()
    {
        levelText = this.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        levelText.SetText("Level " + GameManager.levelNum);
    }
}
