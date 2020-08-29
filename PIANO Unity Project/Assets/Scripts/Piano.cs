using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    public GameObject[] Keys = new GameObject[88];
    [HideInInspector] public Note[] Notes = new Note[88];
    public EnterZone enterZone;
    public GameObject player;
    public Camera pianoCam;
    [HideInInspector] public Camera playerCam;
    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public PianoGameManager pianoGameManager;
    public bool interactedPiano;
    public bool gameStarted;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        pianoGameManager = gameManager.GetComponent<PianoGameManager>();
        playerCam = Camera.main;
        
        for (int i = 0; i < Keys.Length; i++)
        {
            Notes[i] = Keys[i].GetComponent<Note>();
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
            playerCam.tag = "Untagged";
            playerCam.gameObject.SetActive(false);

            pianoCam.gameObject.SetActive(true);
            pianoCam.tag = "MainCamera";
            
            //player.gameObject.SetActive(false);
            Debug.Log("SIT ON PIANO with your fat ass!");
        }

        if (interactedPiano && !gameStarted)
        {
            gameStarted = true;
            pianoGameManager.StartPianoGame(this);
            
        }
        if (interactedPiano && Input.GetKeyDown(KeyCode.X))
        {
            interactedPiano = false;
            gameManager.freemode = true;

            playerCam.gameObject.SetActive(true);
            pianoCam.tag = "Untagged";
            pianoCam.gameObject.SetActive(false);
            playerCam.tag = "MainCamera";
            //player.gameObject.SetActive(true);
            Debug.Log("Player doesnT want to praactice.");
        }
    }

    public void PlayKey(string note)
    {
        for (int i = 0; i < Notes.Length; i++)
        {
            if(Notes[i].pitch == note)
            {
                Notes[i].StartCoroutine("PlayTimedNote", .5f);
                Debug.Log("LLLLL");

            }
        }
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

}

