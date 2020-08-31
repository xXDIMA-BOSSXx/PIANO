using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool freemode = true;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (freemode)
        {
            player.playerCamera.tag = "MainCamera";
            player.playerCamera.gameObject.SetActive(true);
        }
        else
        {
            player.playerCamera.tag = "Untagged";
            player.playerCamera.gameObject.SetActive(false);
        }
    }

    
}
