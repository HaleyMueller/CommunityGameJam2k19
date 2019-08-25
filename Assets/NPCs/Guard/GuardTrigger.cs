using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardTrigger : MonoBehaviour
{

    bool spottedObject = false;
    bool spottedPlayer = false;

    public GameObject guard;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            spottedPlayer = true;
            guard.GetComponent<Guard>().doSpottedObject = true;
            guard.GetComponent<Guard>().spottedObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            spottedPlayer = false;
            guard.GetComponent<Guard>().doSpottedObject = false;
            guard.GetComponent<Guard>().spottedObject = null;
        }
    }
}
