using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    public GameObject[] Keys = new GameObject[88];
    public EnterZone enterZone;
    public GameObject player;
    public Camera pianoCam;
    public Camera playerCam;
    public bool interactedPiano;
    // Start is called before the first frame update
    void Start()
    {
        playerCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(enterZone.inRange && Input.GetKeyDown(KeyCode.E) && !interactedPiano)
        {
            //Player interacts with Piano
            interactedPiano = true;
            playerCam.tag = "Untagged";
            playerCam.gameObject.SetActive(false);
            
            pianoCam.gameObject.SetActive(true);
            pianoCam.tag = "MainCamera";

            //player.gameObject.SetActive(false);
            Debug.Log("SIT ON PIANO with your fat ass!");
        }

        if (interactedPiano && Input.GetKeyDown(KeyCode.X))
        {
            interactedPiano = false;
            playerCam.gameObject.SetActive(true);
            pianoCam.tag = "Untagged";
            pianoCam.gameObject.SetActive(false);
            playerCam.tag = "MainCamera";
            //player.gameObject.SetActive(true);
            Debug.Log("Player doesnT want to praactice.");
        }
    }

     
}

