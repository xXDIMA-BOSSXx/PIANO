using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //[HideInInspector]
    public bool release = true;
    public GameObject target;
   

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Key"))
                {
                    target = hit.transform.gameObject;
                    target.GetComponent<Note>().PlayNote();
                    target.GetComponent<Note>().isHolding = true;

                }
                Debug.Log(hit.transform.name);
            }
        }

        if( Input.GetMouseButtonUp(0))
        {
            release = true;
            if (target != null)
                target.GetComponent<Note>().isHolding = false;



        }
        else if (Input.GetMouseButtonDown(0))
        {
            release = false;
        }
        
        
       
        
        
    }
}