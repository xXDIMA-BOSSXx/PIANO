using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    //Teacher, Piano
    public static float technique;
    public static int techniqueLVL; //1-30

    public static float sightReading;
    public static int sightReadingLVL;//1-10

    //Library
    public static float musicTheory;
    public static int musicTheoryLVL;//1-5

    public static float musicHistory;
    public static int musicHistoryLVL;//1-5

    //Concerts, Teacher, Piano
    public static float musicHearing;
    public static int musicHearingLVL;//1-15

    public static float improvisation;
    public static int improvistationLVL;//1-3

    public static float anxiety;
    public static int anxietyLVL;//10-0

    public static int motivation; //-3 - 3

    public static int inspiration; //0 - 3

    private void Start()
    {
        GetStats();
    }

    private void Update()
    {

        AddStatsLVL();
        ClampStats();
    }

    public void GetStats()
    {
        technique = PlayerPrefs.GetFloat("technique");
        sightReading = PlayerPrefs.GetFloat("sightReading");
        musicTheory = PlayerPrefs.GetFloat("musicTheory");
        musicHistory = PlayerPrefs.GetFloat("musicHistory");
        musicHearing = PlayerPrefs.GetFloat("musicHearing");
        improvisation = PlayerPrefs.GetFloat("improvisation");
        anxiety = PlayerPrefs.GetFloat("anxiety");

        if (PlayerPrefs.GetInt("techniqueLVL") != 0)
        {
            techniqueLVL = PlayerPrefs.GetInt("techniqueLVL");
        }
        else
        {
            techniqueLVL = 1;
        }

        if (PlayerPrefs.GetInt("sightReadingLVL") != 0)
        {
            sightReadingLVL = PlayerPrefs.GetInt("sightReadingLVL");
        }
        else
        {
            sightReadingLVL = 1;
        }

        if (PlayerPrefs.GetInt("musicTheoryLVL") != 0)
        {
            musicTheoryLVL = PlayerPrefs.GetInt("musicTheoryLVL");
        }
        else
        {
            musicTheoryLVL = 1;
        }

        if (PlayerPrefs.GetInt("musicHistoryLVL") != 0)
        {
            musicHistoryLVL = PlayerPrefs.GetInt("musicHistoryLVL");
        }
        else
        {
            musicHistoryLVL = 1;
        }

        if (PlayerPrefs.GetInt("musicHearingLVL") != 0)
        {
            musicHearingLVL = PlayerPrefs.GetInt("musicHearingLVL");
        }
        else
        {
            musicHearingLVL = 1;
        }

        if (PlayerPrefs.GetInt("improvistationLVL") != 0)
        {
            improvistationLVL = PlayerPrefs.GetInt("improvistationLVL");
        }
        else
        {
            improvistationLVL = 1;
        }

        if (PlayerPrefs.GetInt("anxietyLVL") != 0)
        {
            anxietyLVL = PlayerPrefs.GetInt("anxietyLVL");
        }
        else
        {
            anxietyLVL = 10;
        }
    }
    public void ClampStats()
    {
        technique = Mathf.Clamp(technique, 0, 1);
        techniqueLVL = Mathf.Clamp(techniqueLVL, 1, 30);

        sightReading = Mathf.Clamp(sightReading, 0, 1);
        sightReadingLVL = Mathf.Clamp(sightReadingLVL, 1, 10);

        musicTheory = Mathf.Clamp(musicTheory, 0, 1);
        musicTheoryLVL = Mathf.Clamp(musicTheoryLVL, 1, 5);

        musicHistory = Mathf.Clamp(musicHistory, 0, 1);
        musicHistoryLVL = Mathf.Clamp(musicHistoryLVL, 1, 5);

        musicHearing = Mathf.Clamp(musicHearing, 0, 1);
        musicHearingLVL = Mathf.Clamp(musicHearingLVL, 1, 15);

        improvisation = Mathf.Clamp(improvisation, 0, 1);
        improvistationLVL = Mathf.Clamp(improvistationLVL, 0, 3);

        anxiety = Mathf.Clamp(anxiety, 0, 1);
        anxietyLVL = Mathf.Clamp(anxietyLVL, 0, 10);

        motivation = Mathf.Clamp(motivation, -3, 3);

        inspiration = Mathf.Clamp(inspiration, 0, 3);
    }

    public void AddStatsLVL()
    {
        if (technique >= 1)
        {
            technique = 0;
            techniqueLVL++;
            PlayerPrefs.SetInt("techniqueLVL", techniqueLVL);
            PlayerPrefs.SetFloat("technique", technique);
        }

        if (sightReading >= 1)
        {
            sightReading = 0;
            sightReadingLVL++;
            PlayerPrefs.SetInt("sightReadingLVL", sightReadingLVL);
            PlayerPrefs.SetFloat("sightReading", sightReading);
        }

        if (musicTheory >= 1)
        {
            musicTheory = 0;
            musicTheoryLVL++;
            PlayerPrefs.SetInt("musicTheoryLVL", musicTheoryLVL);
            PlayerPrefs.SetFloat("musicTheory", musicTheory);
        }

        if (musicHistory >= 1)
        {
            musicHistory = 0;
            musicHistoryLVL++;
            PlayerPrefs.SetInt("musicHistoryLVL", musicHistoryLVL);
            PlayerPrefs.SetFloat("musicHistory", musicHistory);
        }

        if (musicHearing >= 1)
        {
            musicHearing = 0;
            musicHearingLVL++;
            PlayerPrefs.SetInt("musicHearingLVL", musicHearingLVL);
            PlayerPrefs.SetFloat("musicHearing", musicHearing);
        }

        if (improvisation >= 1)
        {
            improvisation = 0;
            improvistationLVL++;
            PlayerPrefs.SetInt("improvistationLVL", improvistationLVL);
            PlayerPrefs.SetFloat("improvisation", improvisation);
        }

        if (anxiety >= 1)
        {
            anxiety = 0;
            anxietyLVL--;
            PlayerPrefs.SetInt("anxietyLVL", anxietyLVL);
            PlayerPrefs.SetFloat("anxiety", anxiety);
        }
    }
}
