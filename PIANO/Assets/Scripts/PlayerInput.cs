using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool playNote;
    public bool release;
   

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
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