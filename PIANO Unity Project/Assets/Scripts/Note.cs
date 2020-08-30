using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public GameObject selectedKey;
    public GameObject demandingKey;
    [HideInInspector] public string pitch;
    [HideInInspector] public AudioSource note;
    [HideInInspector] public AudioSource tempAudio;
    [HideInInspector] PlayerInput input;
    public bool wasPlayed;

    private void Awake()
    {
        selectedKey.SetActive(false);
        demandingKey.SetActive(false);
        pitch = gameObject.name;
        input = FindObjectOfType<PlayerInput>();
        note = GetComponentInChildren<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (input.release)
        {
            if(GetComponentsInChildren<AudioSource>() != null)
            foreach (AudioSource source in GetComponentsInChildren<AudioSource>())
            {
              source.volume -= Time.deltaTime * 1.65f;
            }
        }
        if(wasPlayed)
        {
            wasPlayed = false;
        }
        
    }

    public void PlayNote()
    {
        //PLay AUDIO
        note.volume = 1f;
        tempAudio = Instantiate (note, transform);
        tempAudio.clip = note.clip;
        tempAudio.Play();
        Destroy(tempAudio.gameObject, tempAudio.clip.length);
        demandingKey.SetActive(false);
        wasPlayed = true;
      
    }
    public IEnumerator PlayTimedNote(float time)
    {
        note.volume = 1f;
        tempAudio = Instantiate(note, transform);
        tempAudio.clip = note.clip;
        tempAudio.Play();
        Destroy(tempAudio.gameObject, tempAudio.clip.length);
        Debug.Log("Before");
        yield return new WaitForEndOfFrame();
        input.release = false;
        yield return new WaitForSeconds(time);
        Debug.Log("after");
        input.release = true;

    }
}
