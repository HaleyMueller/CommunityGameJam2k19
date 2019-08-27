using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMC : MonoBehaviour
{
    public float Speed;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)){
            float Horizontal = Input.GetAxis("Horizontal");
            float Vertical = Input.GetAxis("Vertical");

            Vector3 playerMovement = new Vector3 (Horizontal, 0f, Vertical) * Speed * 1.5f * Time.deltaTime;   
            transform.Translate(playerMovement, Space.Self); 
        }       
        else
        {
            float Horizontal = Input.GetAxis("Horizontal");
            float Vertical = Input.GetAxis("Vertical");

            Vector3 playerMovement = new Vector3 (Horizontal, 0f, Vertical) * Speed * Time.deltaTime;   
            transform.Translate(playerMovement, Space.Self); 
        }
    }
}
