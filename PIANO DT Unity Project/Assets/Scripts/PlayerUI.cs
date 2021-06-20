using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject PlayerCanvas;
    public Image PlayerProgressBar;
    public Text ProgressText;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerProgressBar.fillAmount = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerProgressBar.fillAmount = StatsManager.technique;
        ProgressText.text = StatsManager.techniqueLVL.ToString();
    }
}
