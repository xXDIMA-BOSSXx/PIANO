using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PianoGameManager : MonoBehaviour
{
    
    [Header("Piano - General")]
    public GameObject PianoGameUI;
    public Piano piano;
    private GameObject[] pianoKeys;
    private GameManager gameManager;
     public int rightNoteIndex;
     public bool checkWrongNote = true;
    [SerializeField]private  float techniqueIncrease = .02f;
    [SerializeField]private  float techniqueIncrease2 = .02f;

    [Header("Technique - Scales")]
    public float autoTime = 1f;
    public int autoProgress = 5;

    [Header("Technique - Chords")]
    public GameObject progressBar;
    public GameObject route;
    public float routeSpeed = 1f;
    public float ChordSpeed = 1f;
    private float moveChord;
    private int rand;
    private int selectIndex;
    private bool updateChords;
    private bool movingSelect;
    private bool enableBar;

    [Header("Technique - Arpeggios")]
    public ArpeggioProgressBar ArpeggioProgressBar;
    public float keyTime = 1f;
    public KeyCode firstKey;
    public KeyCode secondKey;
    public KeyCode thirdKey;
    public Sprite A; // source sprites, Images are in ArpeggioProgressBar
    public Sprite S;
    public Sprite D;



    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        checkWrongNote = true;
        PianoGameUI.SetActive(false);
        progressBar.SetActive(false);
        ArpeggioProgressBar.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        LevelStats(); //Sets the Stats of the Games based on the level of the character
        if (gameManager.freemode)
        {
            PianoGameUI.SetActive(false);
        }
        if(piano != null)
        {
            if(checkWrongNote)
            CheckWrongNote();
        }
        if (updateChords)
        {
            UpdateChords();
        }
       
    }

    public void StartPianoGame(Piano _piano)
    {
        PianoGameUI.SetActive(true);
        piano = _piano;
    
    }
    public void CheckWrongNote() //Restart if wrong note is played
    {
        for (int i = 0; i < piano.Notes.Length; i++)
        {
            if (piano.Notes[i].wasPlayed && i != rightNoteIndex && checkWrongNote)
            {
                checkWrongNote = false;
                Debug.Log("FALSE NOTWE");
                StopPianoGame();
                Invoke("SetWrongCheck", 1f);
            }
        }
    }
    private void SetWrongCheck() 
    {
        checkWrongNote = true;
    } 

    public void LevelStats()
    {
        //techniqueIncrease = 1 / (8 * Mathf.Pow(StatsManager.techniqueLVL, 1.1f)); // I LOVE MATH
        //techniqueIncrease2 = 1 / (8 * Mathf.Pow(StatsManager.techniqueLVL, 1.1f));

        //autoTime = -0.03f * StatsManager.techniqueLVL + 1f; //its linear
        autoTime = 1.0826f * Mathf.Pow(0.9237f, StatsManager.techniqueLVL);
        autoProgress = Mathf.RoundToInt(1.93f * Mathf.Pow(1.167f, StatsManager.techniqueLVL));  

    }

    
    public void LearnScales()
    {
        Debug.Log("I learn technique");
        PianoGameUI.SetActive(false);
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
        yield return new WaitForSeconds(1f); // waiting in case playing again
        int index = piano.GetRandomNoteIndexBetween("G3", "G5"); // from G3 to G5 possible range
        rightNoteIndex = index; //wrong note means fail start again
        StartCoroutine(DemandNote(index)); // play first note
        yield return new WaitUntil(() => piano.Notes[index].wasPlayed);
        StatsManager.technique += techniqueIncrease;
        PlayerPrefs.SetFloat("technique", StatsManager.technique);

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
            int random = Random.Range(1,autoProgress); // choose if player or automatic playing for each note
            if(random == 1)
            {
                //player plays key
                yield return new WaitForSeconds(autoTime);
                rightNoteIndex = index + i;
                StartCoroutine(DemandNote(index + i));
                yield return new WaitUntil(() => piano.Notes[index + i].wasPlayed);
                StatsManager.technique += techniqueIncrease;
                PlayerPrefs.SetFloat("technique", StatsManager.technique);
            }
            else
            {
                //automatic key playing

                yield return new WaitForSeconds(autoTime);
                piano.PlayKey(index + i, autoTime);
                StatsManager.technique += techniqueIncrease2;
                PlayerPrefs.SetFloat("technique", StatsManager.technique);
            }
        }
        LearnScales();

    }
    private IEnumerator MajorScaleDown()
    {
        yield return new WaitForSeconds(autoTime); // waiting in case playing again
        int index = piano.GetRandomNoteIndexBetween("G4", "G6"); // possible range
        rightNoteIndex = index; //wrong note means fail start again
        StartCoroutine(DemandNote(index)); // play first note
        yield return new WaitUntil(() => piano.Notes[index].wasPlayed);
        StatsManager.technique += techniqueIncrease;
        PlayerPrefs.SetFloat("technique", StatsManager.technique);

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
            int random = Random.Range(1, autoProgress); // choose if player or automatic playing for each note
            if (random == 1)
            {
                //player plays key
                yield return new WaitForSeconds(autoTime);
                rightNoteIndex = index - i;
                StartCoroutine(DemandNote(index - i));
                yield return new WaitUntil(() => piano.Notes[index - i].wasPlayed);
                StatsManager.technique += techniqueIncrease;
                PlayerPrefs.SetFloat("technique", StatsManager.technique);
            }
            else
            {
                //automatic key playing
                yield return new WaitForSeconds(autoTime);
                piano.PlayKey(index - i, autoTime);
                StatsManager.technique += techniqueIncrease2;
                PlayerPrefs.SetFloat("technique", StatsManager.technique);
            }
        }
        LearnScales();

    }

    public void LearnChords() // after pressing the button in the menu
    {
        Debug.Log("I learn chords");
        PianoGameUI.SetActive(false); // hiding the menu

        piano.pianoCamAnim.SetBool("Chords", true); //moving the camera

        progressBar.SetActive(true); // activating the bar
        updateChords = true; //green/red bar
        StartCoroutine(Chords()); 


    }
    public IEnumerator Chords()
    {
        yield return new WaitForSeconds(.7f); // waiting in case playing again
        int index = piano.GetRandomNoteIndexBetween("G3", "G5"); // from G3 to G5 possible range
        rightNoteIndex = index; //wrong note means fail start again

        piano.Notes[index].demandingKey.SetActive(true); // demanding 3 notes
        piano.Notes[index + 4].demandingKey.SetActive(true);
        piano.Notes[index + 7].demandingKey.SetActive(true);

        yield return new WaitForSeconds(.7f); // waiting in case playing again

        selectIndex = piano.GetRandomNoteIndexBetween("G3", "G5"); // from G3 to G5 possible range
        if (selectIndex == index)
        {
            selectIndex += 3;
        }// just making sure they are not the same

        piano.Notes[selectIndex].selectedKey.SetActive(true); // selecting 3 other notes
        piano.Notes[selectIndex + 4].selectedKey.SetActive(true);
        piano.Notes[selectIndex + 7].selectedKey.SetActive(true);

        movingSelect = true;
    }
    public void UpdateChords()
    {
        if (enableBar) // updating the bar and UI
        {
            if (Input.GetKeyDown(KeyCode.S)) //presses s 
            {
                Vector3 move = new Vector3(-113f, 0, 0); // -113 is the min x, route is on the left
                route.transform.localPosition = move;

                rand = Random.Range(-80, 80); // vary the speed
            }
            if (Input.GetKey(KeyCode.S))
            {
                route.transform.localPosition += Vector3.right * (routeSpeed + rand) * Time.deltaTime; // route moves right
                if (route.transform.localPosition.x >= 113f)
                {
                    route.transform.localPosition = new Vector3(113, 0, 0); // max x +113
                }
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                if (route.transform.localPosition.x >= 46 && route.transform.localPosition.x <= 86) // if route is in the green area of bar
                {
                    Debug.Log("SWEET");
                    StatsManager.technique += .02f;
                    PlayerPrefs.SetFloat("technique", StatsManager.technique);
                    StopPianoGame();
                    updateChords = true;
                    movingSelect = false;
                    StartCoroutine(Chords());
                }
                else
                {
                    Debug.Log("FAIL");
                    StopPianoGame();
                }
            }
        }
        if (movingSelect) // moving the selected notes
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                moveChord = 0;

                piano.Notes[selectIndex].selectedKey.SetActive(false);
                piano.Notes[selectIndex + 4].selectedKey.SetActive(false);
                piano.Notes[selectIndex + 7].selectedKey.SetActive(false);
                selectIndex--;
                UpdateChords2();

            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                moveChord = 0;

                piano.Notes[selectIndex].selectedKey.SetActive(false);
                piano.Notes[selectIndex + 4].selectedKey.SetActive(false);
                piano.Notes[selectIndex + 7].selectedKey.SetActive(false);
                selectIndex++;
                UpdateChords2();
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveChord += Time.deltaTime * ChordSpeed;
                if (moveChord >= 1)
                {
                    moveChord = 0;
                    piano.Notes[selectIndex].selectedKey.SetActive(false);
                    piano.Notes[selectIndex + 4].selectedKey.SetActive(false);
                    piano.Notes[selectIndex + 7].selectedKey.SetActive(false);
                    selectIndex--;
                    UpdateChords2();
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveChord += Time.deltaTime * ChordSpeed;
                if (moveChord >= 1)
                {
                    moveChord = 0;
                    piano.Notes[selectIndex].selectedKey.SetActive(false);
                    piano.Notes[selectIndex + 4].selectedKey.SetActive(false);
                    piano.Notes[selectIndex + 7].selectedKey.SetActive(false);
                    selectIndex++;
                    UpdateChords2();
                }
            }
         
        }

    }
    public void UpdateChords2() // refreshing indexes and checking for bar condition
    {
        piano.Notes[selectIndex].selectedKey.SetActive(true);
        piano.Notes[selectIndex + 4].selectedKey.SetActive(true);
        piano.Notes[selectIndex + 7].selectedKey.SetActive(true);

        movingSelect = true;
        if (selectIndex == rightNoteIndex)
        {
            enableBar = true;
        }
        else enableBar = false;
    }

    public void LearnArpeggios()
    {
        Debug.Log("I learn arpeggios");
        PianoGameUI.SetActive(false);
        piano.pianoCamAnim.SetBool("Arpeggios", true); //moving the camera
        ArpeggioProgressBar.gameObject.SetActive(true);
        StartCoroutine(Arpeggios());
    }

    public IEnumerator Arpeggios()
    {
        yield return new WaitForSeconds(2f); // waiting in case playing again
        int index = piano.GetRandomNoteIndexBetween("C2", "B2"); // from C2 to B2 possible range
        rightNoteIndex = index; //wrong note means fail start again

        
        int rand = Random.Range(1, 7); // randomizing keys

        switch (rand)
        {
            case 1:
                firstKey = KeyCode.A; secondKey = KeyCode.S; thirdKey = KeyCode.D;
                ArpeggioProgressBar.image1 = A;  ArpeggioProgressBar.image2 = S;  ArpeggioProgressBar.image3 = D;
                break;
            case 2:
                firstKey = KeyCode.A; secondKey = KeyCode.D; thirdKey = KeyCode.S;
                ArpeggioProgressBar.image1 = A; ArpeggioProgressBar.image2 = D; ArpeggioProgressBar.image3 = S;
                break;
            case 3:
                firstKey = KeyCode.S; secondKey = KeyCode.A; thirdKey = KeyCode.D;
                ArpeggioProgressBar.image1 = S; ArpeggioProgressBar.image2 = A; ArpeggioProgressBar.image3 = D;
                break;
            case 4:
                firstKey = KeyCode.S; secondKey = KeyCode.D; thirdKey = KeyCode.A;
                ArpeggioProgressBar.image1 = S; ArpeggioProgressBar.image2 = D; ArpeggioProgressBar.image3 = A;
                break;
            case 5:
                firstKey = KeyCode.D; secondKey = KeyCode.A; thirdKey = KeyCode.S;
                ArpeggioProgressBar.image1 = D; ArpeggioProgressBar.image2 = A; ArpeggioProgressBar.image3 = S;
                break;
            case 6:
                firstKey = KeyCode.D; secondKey = KeyCode.S; thirdKey = KeyCode.A;
                ArpeggioProgressBar.image1 = D; ArpeggioProgressBar.image2 = S; ArpeggioProgressBar.image3 = A;
                break;
            default:
                firstKey = KeyCode.A; secondKey = KeyCode.S; thirdKey = KeyCode.D;
                ArpeggioProgressBar.image1 = A; ArpeggioProgressBar.image2 = S; ArpeggioProgressBar.image3 = D;
                break;
        }
        Debug.Log(firstKey.ToString() + " " + secondKey.ToString() + " " + thirdKey.ToString());

        for (int i = 0; i < 9; i++)
        {
            int x = 0; //adding to index major chords in 5 octaves.
            switch(i)
            {
                case 0:
                    break;
                case 1:
                    x = 12;
                    break;
                case 2:
                    x = 24;
                    break;
                case 3:
                    x = 36;
                    break;
                case 4:
                    x = 48;
                    break;
                case 5:
                    x = 36;
                    break;
                case 6:
                    x = 24;
                    break;
                case 7:
                    x = 12;
                    break;
                case 8:
                    x = 0;
                    break;
                default:
                    break;
            }
            if (i < 4)
            {
                if(i == 0)
                {
                    ArpeggioProgressBar.SpawnImage(1);
                    ArpeggioProgressBar.NextImage();
                    ArpeggioProgressBar.SpawnImage(2);
                }
                yield return new WaitUntil(() => Input.GetKeyDown(firstKey));
                piano.PlayKey(index + x, keyTime);
                piano.Notes[index + x].demandingKey.SetActive(true);
                ArpeggioProgressBar.NextImage();
                ArpeggioProgressBar.SpawnImage(3);

                yield return new WaitUntil(() => Input.GetKeyDown(secondKey));
                piano.PlayKey(index + x + 4, keyTime);
                piano.Notes[index + x + 4].demandingKey.SetActive(true);
                ArpeggioProgressBar.NextImage();
                ArpeggioProgressBar.SpawnImage(1);

                yield return new WaitUntil(() => Input.GetKeyDown(thirdKey));
                piano.PlayKey(index + x + 7, keyTime);
                piano.Notes[index + x + 7].demandingKey.SetActive(true);
                ArpeggioProgressBar.NextImage();
                if(i == 3) ArpeggioProgressBar.SpawnImage(3);
                else ArpeggioProgressBar.SpawnImage(2);
            }
            if (i == 4)
            {
                yield return new WaitUntil(() => Input.GetKeyDown(firstKey));
                piano.PlayKey(index + x, keyTime);
                ArpeggioProgressBar.NextImage();
                ArpeggioProgressBar.SpawnImage(2);
            }
            if(i > 4)
            {
                yield return new WaitUntil(() => Input.GetKeyDown(thirdKey));
                piano.PlayKey(index + x + 7, keyTime);
                piano.Notes[index + x + 7].demandingKey.SetActive(false);
                ArpeggioProgressBar.NextImage();
                ArpeggioProgressBar.SpawnImage(1);

                yield return new WaitUntil(() => Input.GetKeyDown(secondKey));
                piano.PlayKey(index + x + 4, keyTime);
                piano.Notes[index + x + 4].demandingKey.SetActive(false);
                ArpeggioProgressBar.NextImage();
                if(i < 8)ArpeggioProgressBar.SpawnImage(3);

                yield return new WaitUntil(() => Input.GetKeyDown(firstKey));
                piano.PlayKey(index + x , keyTime);
                piano.Notes[index + x].demandingKey.SetActive(false);
                ArpeggioProgressBar.NextImage();
                if(i < 8)ArpeggioProgressBar.SpawnImage(2);


            }
        }
        StartCoroutine(Arpeggios());
    }


    public void FreePlay()
    {
        Debug.Log("I learn free");
        PianoGameUI.SetActive(false);
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
        updateChords = false;

    }
   public void ResetUI()
   {

        piano.pianoCamAnim.SetBool("Chords", false);
        PianoGameUI.GetComponent<PianoGamesUI>().Main();
        progressBar.SetActive(false);
        ArpeggioProgressBar.gameObject.SetActive(false);
    }


}
