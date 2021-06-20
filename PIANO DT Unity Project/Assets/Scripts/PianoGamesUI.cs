using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoGamesUI : MonoBehaviour
{
    [Header("Technique")]
    public GameObject TechniqueUI;
    public GameObject Technique1;
    public GameObject Technique2;
    public GameObject Technique3;
    [Header("Main")]
    public GameObject Back;
    public GameObject Pieces;
    public GameObject FreePlay;

    public void Technique()
    {
        TechniqueUI.SetActive(false);
        Pieces.SetActive(false);
        FreePlay.SetActive(false);
        Back.SetActive(true);
        Technique1.SetActive(true);
        Technique2.SetActive(true);
        Technique3.SetActive(true);
    }
    public void Main()
    {
        TechniqueUI.SetActive(true);
        Pieces.SetActive(true);
        FreePlay.SetActive(true);
        Back.SetActive(false);
        Technique1.SetActive(false);
        Technique2.SetActive(false);
        Technique3.SetActive(false);
    }

}
