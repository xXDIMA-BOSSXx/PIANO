using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public GameObject selectedKey;
    public GameObject demandingKey;
    public bool whiteKey;
     public string pitch;
    [HideInInspector] public AudioSource note;
    [HideInInspector] public AudioSource tempAudio;
    [HideInInspector] PlayerInput input;
     public int index;
    /*[HideInInspector]*/ public bool wasPlayed;
    [HideInInspector] public bool isHolding;
    private BoxCollider col;
    private const float wasPlayedTime = .01f;

    private void Awake()
    {
        selectedKey.SetActive(false);
        demandingKey.SetActive(false);
        pitch = gameObject.name;
        input = FindObjectOfType<PlayerInput>();
        note = GetComponentInChildren<AudioSource>();
        col = GetComponent<BoxCollider>();
        if (whiteKey)
        {
            col.center = new Vector3(0f, .4f, 0f); //Setting the collider of the note. Take it how it is.
            col.size = new Vector3(1.1f, 0.5f, 1.1f); 
        } //setting position and scale of collider and colorkeys
        else
        {
            //col.center = new Vector3(-0.022692f, -0.112701f, 1.068068f);
            //col.size = new Vector3(1.045385f, 5.047704f, 0.797697f);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (input.release)
        {
            isHolding = false;
        }
        if (!isHolding)
        {
            if (GetComponentsInChildren<AudioSource>() != null)
                foreach (AudioSource source in GetComponentsInChildren<AudioSource>())
                {
                    source.volume -= Time.deltaTime * 1.65f;
                }
        }
    }

    public void PlayNote()
    {
        //isHoolding is set by PlayerInput.
        //PLay AUDIO
        note.volume = .5f;
        tempAudio = Instantiate (note, transform);
        tempAudio.clip = note.clip;
        tempAudio.Play();
        Destroy(tempAudio.gameObject, tempAudio.clip.length);
        demandingKey.SetActive(false);
        wasPlayed = true;
        Invoke("WasPlayed", wasPlayedTime);
    }
    public IEnumerator PlayTimedNote(float time)
    {
        note.volume = .5f;
        tempAudio = Instantiate(note, transform);
        tempAudio.clip = note.clip;
        tempAudio.Play();
        isHolding = true;
        Destroy(tempAudio.gameObject, tempAudio.clip.length);
        yield return new WaitForEndOfFrame();
        input.release = false;
        selectedKey.SetActive(true);
        yield return new WaitForSeconds(time);
        selectedKey.SetActive(false);
        input.release = true;
        isHolding = false;

    }
    void WasPlayed()
    {
        wasPlayed = false;
    }
}
