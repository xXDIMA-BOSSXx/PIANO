using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static float technique;
    public static int techniqueLVL;

    private void Start()
    {
        technique = PlayerPrefs.GetFloat("technique");
        if(PlayerPrefs.GetInt("techniqueLVL") != 0)
        {
            techniqueLVL = PlayerPrefs.GetInt("techniqueLVL");
        }
        else
        {
            techniqueLVL = 1;
        }
    }

    private void Update()
    {
        if(technique >= 1)
        {
            technique = 0;
            techniqueLVL++;
            PlayerPrefs.SetInt("techniqueLVL", techniqueLVL);
            PlayerPrefs.SetFloat("technique", technique);
            Debug.Log(techniqueLVL);
        }
     
    }
}
