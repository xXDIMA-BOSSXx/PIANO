using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArpeggioProgressBar : MonoBehaviour
{
    private PianoGameManager pgm;
    public Image progressBarImage;
    public GameObject ImagePrefab;
    public List<GameObject> imagePrefabs = new List<GameObject>();
    [HideInInspector]public Sprite image1;
    [HideInInspector]public Sprite image2;
    [HideInInspector]public Sprite image3;
    private void Start()
    {
        pgm = FindObjectOfType<PianoGameManager>();
    }
    private void Update()
    {

    }
    public void SpawnImage(int i)
    {
        GameObject image = Instantiate(ImagePrefab, transform);
        imagePrefabs.Add(image);
        switch (i)
        {
            case 1:
                image.GetComponent<Image>().sprite = image1;
                break;
            case 2:
                image.GetComponent<Image>().sprite = image2;
                break;
            case 3:
                image.GetComponent<Image>().sprite = image3;
                break;

        }
        Animator imageAnim = image.GetComponent<Animator>();
        imageAnim.SetInteger("stage", 1);
    }
    public void NextImage()
    {
        for (int i = 0; i < imagePrefabs.Count; i++)
        {
            int currentStage = imagePrefabs[i].GetComponent<Animator>().GetInteger("stage");
            int nextStage = currentStage + 1;
            imagePrefabs[i].GetComponent<Animator>().SetInteger(  "stage", nextStage);
        }
        for (int i = 0; i < imagePrefabs.Count; i++)
        {
            if (imagePrefabs[i].GetComponent<Animator>().GetInteger("stage") >= 4)
            {
                Destroy(imagePrefabs[i], 1f);
                imagePrefabs.Remove(imagePrefabs[i]);
            }
        }

    }

}
