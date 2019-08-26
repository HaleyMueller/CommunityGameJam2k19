using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open : MonoBehaviour
{
    bool isInTheZone = false, isTheDoorOpen = false;
    public GameObject Door, CanvasE, rotator;
    public Animator anim;


    
    void Update()
    {
        if (isInTheZone)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (!isTheDoorOpen)
                {
                    anim.Play("opening");
                    print(Door.name + ("is open"));
                    isTheDoorOpen = true;
                    CanvasE.GetComponent<Canvas>().enabled = false;
                }
            }



        }
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            print("the player entered the door zone");
            isInTheZone = true;
            
            if (!isTheDoorOpen)
            {
                CanvasE.GetComponent<Canvas>().enabled = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            print("the player left the door zone");
            isInTheZone = false;
            CanvasE.GetComponent<Canvas>().enabled = false;
        }
    }
}
