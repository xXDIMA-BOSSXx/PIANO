using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector]
    public bool release;
   

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.CompareTag("Key"))
                    hit.transform.GetComponent<Note>().PlayNote();
                Debug.Log(hit.transform.name);
            }
        }

        if( Input.GetMouseButtonUp(0))
        {
            release = true;

        }else if (Input.GetMouseButtonDown(0))
        {
            release = false;
        }
        
        
       
        
        
    }
}