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
    public bool checkWrongNote = true;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        checkWrongNote = true;
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
    public void CheckWrongNote() //Restart if wrong note is played
    {
        for (int i = 0; i < piano.Notes.Length; i++)
        {
            if (piano.Notes[i].wasPlayed && i != rightNoteIndex && checkWrongNote)
            {
                Debug.Log("FALSE NOTWE");
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
        int rand = Random.Range(1, 3);
        if(rand == 1)
        {
            StartCoroutine(MajorScaleUp());
        }
        else if(rand == 2)
        {
            StartCoroutine(MajorScaleDown());
        }


    }
    private IEnumerator MajorScaleUp()
    {
        yield return new WaitForSeconds(.7f); // waiting in case playing again
        int index = piano.GetRandomNoteIndexBetween("G3", "G5"); // from G3 to G5 possible range
        rightNoteIndex = index; //wrong note means fail start again
        StartCoroutine(DemandNote(index)); // play first note
        yield return new WaitUntil(() => piano.Notes[index].wasPlayed);
        
        for (int i = 1; i < 13; i++) // GOING UP
        {
            // MajorScale
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
            } // skip notes that are not major scale
            int random = Random.Range(1,3); // choose if player or automatic playing for each note
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
        LearnTechnique();

    }
    private IEnumerator MajorScaleDown()
    {
        yield return new WaitForSeconds(.7f); // waiting in case playing again
        int index = piano.GetRandomNoteIndexBetween("G4", "G6"); // possible range
        rightNoteIndex = index; //wrong note means fail start again
        StartCoroutine(DemandNote(index)); // play first note
        yield return new WaitUntil(() => piano.Notes[index].wasPlayed);

        for (int i = 1; i < 13; i++) // GOING DOWN
        {
            // MajorScale
            switch (i)
            {
                case 2:
                    i++;
                    break;
                case 4:
                    i++;
                    break;
                case 6:
                    i++;
                    break;
                case 9:
                    i++;
                    break;
                case 11:
                    i++;
                    break;
            } // skip notes that are not major scale
            int random = Random.Range(1, 3); // choose if player or automatic playing for each note
            if (random == 1)
            {
                //player plays key
                yield return new WaitForSeconds(.7f);
                StartCoroutine(DemandNote(index - i));
                rightNoteIndex = index - i;
                yield return new WaitUntil(() => piano.Notes[index - i].wasPlayed);
                StatsManager.technique += .25f;
                PlayerPrefs.SetFloat("technique", StatsManager.technique);
            }
            else
            {
                //automatic key playing
                yield return new WaitForSeconds(.7f);
                piano.PlayKey(index - i, .5f);
                StatsManager.technique += .02f;
                PlayerPrefs.SetFloat("technique", StatsManager.technique);
            }
        }
        LearnTechnique();

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
    // visual demand or selected key
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

    // demand notes
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
