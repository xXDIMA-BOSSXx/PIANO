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
    private GameManager gameManager;
    public int rightNoteIndex;
    public bool checkWrongNote;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        
        PianoGameUI.SetActive(false);
        TechniqueUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.freemode)
        {
            PianoGameUI.SetActive(false);
            TechniqueUI.SetActive(false);
        }
        if(piano != null)
        {
            if(checkWrongNote)
            CheckWrongNote();
        }
        
    }

    public void StartPianoGame(Piano _piano)
    {
        PianoGameUI.SetActive(true);
        TechniqueUI.SetActive(true);
        piano = _piano;
    
    }
    public void CheckWrongNote()
    {
        for (int i = 0; i < piano.Notes.Length; i++)
        {
            if (piano.Notes[i].wasPlayed && i != rightNoteIndex && checkWrongNote)
            {
                checkWrongNote = false;
                Invoke("SetWrongCheck", 1f);
                StopPianoGame();
            }
        }
    }
    private void SetWrongCheck()
    {
        checkWrongNote = true;
    }

    public void LearnTechnique()
    {
        Debug.Log("I learn technique");
        PianoGameUI.SetActive(false);
        TechniqueUI.SetActive(false);
        StartCoroutine(_LearnTechnique());
        
    }
    private IEnumerator _LearnTechnique()
    {
        int index = piano.GetNoteIndex("C5");
        rightNoteIndex = index;
        StartCoroutine(DemandNote(index));
        yield return new WaitUntil(() => piano.Notes[index].wasPlayed);

        // GOING UP
        for (int i = 1; i < 13; i++)
        {
            switch (i)
            {
                case 1:
                    i++;
                    break;
                case 3:
                    i++;
                    break;
                case 6:
                    i++;
                    break;
                case 8:
                    i++;
                    break;
                case 10:
                    i++;
                    break;
            } // MajorScale

            int random = Random.Range(1,3);
            if(random == 1)
            {
                //player plays key
                yield return new WaitForSeconds(.7f);
                StartCoroutine(DemandNote(index + i));
                rightNoteIndex = index + i;
                yield return new WaitUntil(() => piano.Notes[index + i].wasPlayed);
                StatsManager.technique += .25f;
                PlayerPrefs.SetFloat("technique", StatsManager.technique);
            }
            else
            {
                //automatic key playing
                yield return new WaitForSeconds(.7f);
                piano.PlayKey(index + i, .5f);
                StatsManager.technique += .02f;
                PlayerPrefs.SetFloat("technique", StatsManager.technique);
            }
        }
       

    }
    public void FreePlay()
    {
        Debug.Log("I learn free");
        PianoGameUI.SetActive(false);
        TechniqueUI.SetActive(false);
    }
    public void LearnPieces()
    {
        Debug.Log("I learn pieces");
    }

    public void SetDemandKey(Note demandKey)
    {
        if (demandKey.selectedKey.activeSelf)
        {
            demandKey.selectedKey.SetActive(false);
        }
        demandKey.demandingKey.SetActive(true);
    }

    public void SetSelectKey(Note selectKey)
    {
        if (selectKey.demandingKey.activeSelf)
        {
            selectKey.demandingKey.SetActive(false);
        }
        selectKey.selectedKey.SetActive(true);
    }

    public IEnumerator DemandRandomKeys(int amount, int min, int max)
    {
        for (int i = 0; i < amount; i++)
        {
            int x = Random.Range(min, max + 1);
            SetDemandKey(piano.Notes[x]);
            yield return new WaitUntil(() => piano.Notes[x].wasPlayed);
            piano.Notes[x].demandingKey.SetActive(false);
            piano.Notes[x].selectedKey.SetActive(true);
            yield return new WaitForSeconds(.5f);
            piano.Notes[x].selectedKey.SetActive(false);
            
        }
    }
    public IEnumerator DemandNote(Note note)
    {
        SetDemandKey(piano.Notes[note.index]);
        yield return new WaitUntil(() => piano.Notes[note.index].wasPlayed);
        piano.Notes[note.index].demandingKey.SetActive(false);
        piano.Notes[note.index].selectedKey.SetActive(true);
        yield return new WaitForSeconds(.5f);
        piano.Notes[note.index].selectedKey.SetActive(false);
        
    }
    public IEnumerator DemandNote(int noteIndex)
    {
        SetDemandKey(piano.Notes[noteIndex]);
        yield return new WaitUntil(() => piano.Notes[noteIndex].wasPlayed);
        piano.Notes[noteIndex].demandingKey.SetActive(false);
        piano.Notes[noteIndex].selectedKey.SetActive(true);
        yield return new WaitForSeconds(.5f);
        piano.Notes[noteIndex].selectedKey.SetActive(false);

    }

    public void StopPianoGame()
    {
        StopAllCoroutines();
        for (int i = 0; i < piano.Notes.Length; i++)
        {
            piano.Notes[i].demandingKey.SetActive(false);
            piano.Notes[i].selectedKey.SetActive(false);
        }
    }
   


}
