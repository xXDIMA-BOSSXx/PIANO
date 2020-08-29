using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PianoGameManager : MonoBehaviour
{
    public GameObject PianoGameUI;
    public GameObject TechniqueUI;
    private Piano piano;
    private GameObject[] pianoKeys;

    private void Start()
    {
        PianoGameUI.SetActive(false);
        TechniqueUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void StartPianoGame(Piano _piano)
    {
        PianoGameUI.SetActive(true);
        TechniqueUI.SetActive(true);
        piano = _piano;
    
    }

    public void LearnTechnique()
    {
        Debug.Log("I learn technique");
        PianoGameUI.SetActive(false);
        TechniqueUI.SetActive(false);
        piano.PlayKey("C2", 5f);
        piano.PlayKey("E2", 5f);
        piano.PlayKey("G2", 5f);


    }
    public void FreePlay()
    {
        Debug.Log("I learn free");
    }
    public void LearnPieces()
    {
        Debug.Log("I learn pieces");
    }

    
}
