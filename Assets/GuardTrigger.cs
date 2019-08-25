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
            guard.GetComponent<WalkRoutine>().doSpottedObject = true;
            guard.GetComponent<WalkRoutine>().spottedObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            spottedPlayer = false;
            guard.GetComponent<WalkRoutine>().doSpottedObject = false;
            guard.GetComponent<WalkRoutine>().spottedObject = null;
        }
    }
}
