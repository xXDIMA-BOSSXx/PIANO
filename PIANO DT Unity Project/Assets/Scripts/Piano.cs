using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    public GameObject[] Keys = new GameObject[88];
    [HideInInspector] public Note[] Notes = new Note[88];
    public EnterZone enterZone;
    public Player player;
    [Header("Piano Camera")]
    public Camera pianoCam;
    public Animator pianoCamAnim;
    public Transform scalesPosition;
    public Transform chordsPosition;
    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public PianoGameManager pianoGameManager;
    public bool interactedPiano;
    public bool pianoGameStarted;

    // Start is called before the first frame update
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        pianoGameManager = gameManager.GetComponent<PianoGameManager>();
        
    }
    void Start()
    {
        for (int i = 0; i < Keys.Length; i++)
        {
            Notes[i] = Keys[i].GetComponent<Note>();
            Notes[i].index = i;
            //Debug.Log(Notes[i].pitch + " " + Notes[i].index + " " + i);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        EnterZoneInput();
        
    }

    public void EnterZoneInput()
    {
        if (enterZone.inRange && Input.GetKeyDown(KeyCode.E) && !interactedPiano)
        {
            //Player interacts with Piano
            interactedPiano = true;
            gameManager.freemode = false;

            pianoCam.transform.position = scalesPosition.position;
            pianoCam.transform.rotation = scalesPosition.rotation;

            pianoCam.gameObject.SetActive(true);
            pianoCam.tag = "MainCamera";
            
            //player.gameObject.SetActive(false);
            //SIT ON PIANO with your fat ass!
        }

        if (interactedPiano && !pianoGameStarted)
        {
            pianoGameStarted = true;
            pianoGameManager.StartPianoGame(this);
            
        }
        if (interactedPiano && Input.GetKeyDown(KeyCode.X))
        {
            interactedPiano = false;
            gameManager.freemode = true;

            pianoGameManager.StopPianoGame();
            pianoGameManager.ResetUI();
            pianoCam.tag = "Untagged";
            pianoCam.gameObject.SetActive(false);
            pianoGameStarted = false;
            //player.gameObject.SetActive(true);
            //Player doesnT want to praactice
        }
    }

   
    public void PlayKey(int noteIndex, float time)
    {
         Notes[noteIndex].StartCoroutine("PlayTimedNote", time);
    }
    public void PlayKey(string note, float time)
    {
        for (int i = 0; i < Notes.Length; i++)
        {
            if (Notes[i].pitch == note)
            {
                Notes[i].StartCoroutine("PlayTimedNote", time);
                Debug.Log("LLLLL");

            }
        }
    }

    

    public Note GetNote(string note)
    {
        for (int i = 0; i < Notes.Length; i++)
        {
            if (Notes[i].pitch == note)
            {
                return Notes[i];

            }
        }
        Debug.Log("CouldnT find Note :("); return null;
    }
    public int GetNoteIndex(string note)
    {
        for (int i = 0; i < Notes.Length; i++)
        {
            if (Notes[i].pitch == note)
            {
                return i;

            }
        }
        Debug.Log("CouldnT find NoteIndex :(");
        return 0;
    }

    public int GetRandomNoteIndexBetween(string note1, string note2)
    {
        int i = Random.Range(GetNoteIndex(note1), GetNoteIndex(note2) + 1);
        return i;
    }

}

