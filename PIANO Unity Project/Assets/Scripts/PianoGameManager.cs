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

        piano.PlayKey("C2", 1f);
        piano.PlayKey("E2", 2f);
        piano.PlayKey("G2", 1f);
        StartCoroutine(DemandRandomKeys(30, 30, 60));


    }
    public void FreePlay()
    {
        Debug.Log("I learn free");
    }
    public void LearnPieces()
    {
        Debug.Log("I learn pieces");
    }
    public void DemandKey(Note demandKey)
    {
        demandKey.demandingKey.SetActive(true);
    }
    public IEnumerator DemandRandomKeys(int amount, int min, int max)
    {
        for (int i = 0; i < amount; i++)
        {
            int x = Random.Range(min, max + 1);
            DemandKey(piano.Notes[x]);
            yield return new WaitUntil(() => piano.Notes[x].wasPlayed);

        }
    }
   


}
