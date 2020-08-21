using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{

    public AudioSource note;
    public AudioSource tempAudio;
    PlayerInput input;

    // Start is called before the first frame update
    void Start()
    {
        input = FindObjectOfType<PlayerInput>();
        note = GetComponentInChildren<AudioSource>();
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
    }

    public void PlayNote()
    {
        //PLay AUDIO
        note.volume = 1f;
        tempAudio = Instantiate (note, transform);
        tempAudio.clip = note.clip;
        tempAudio.Play();
        Destroy(tempAudio.gameObject, tempAudio.clip.length);
      
    }
}
