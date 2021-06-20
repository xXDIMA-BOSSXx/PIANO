using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestGame : MonoBehaviour
{
    public Text testText;


    // Update is called once per frame
    void Update()
    {
        testText.text = "techniqueLVL" + " " + StatsManager.techniqueLVL.ToString() +
                        "\n" + "sightReadingLVL" + " " + StatsManager.sightReadingLVL.ToString() +
                        "\n" + "musicHearingLVL" + " " + StatsManager.musicHearingLVL.ToString() +
                        "\n" + "musicHistoryLVL" + " " + StatsManager.musicHistoryLVL.ToString() +
                        "\n" + "musicTheoryLVL" + " " + StatsManager.musicTheoryLVL.ToString() +
                        "\n" + "improvistationLVL" + " " + StatsManager.improvistationLVL.ToString() +
                        "\n" + "anxietyLVL" + " " + StatsManager.anxietyLVL.ToString();
                       
    }
}
