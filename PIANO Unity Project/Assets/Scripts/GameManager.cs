using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool freemode = true;
    private Player player;
    private PlayerMovement playerMovement;
    [HideInInspector] public StatsManager statsManager;
    // Start is called before the first frame update
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        playerMovement = player.GetComponent<PlayerMovement>();
        statsManager = GetComponent<StatsManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (freemode)
        {
            player.playerCamera.tag = "MainCamera";
            player.playerCamera.gameObject.SetActive(true);
            playerMovement.enabled = true;
        }
        else
        {
            player.playerCamera.tag = "Untagged";
            player.playerCamera.gameObject.SetActive(false);
            playerMovement.enabled = false;
        }
    }

    
}
