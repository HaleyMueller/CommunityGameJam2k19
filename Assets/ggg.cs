using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ggg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<WalkRoutine>().doPatrol = true;
        GetComponent<WalkRoutine>().PatrolLogic(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
