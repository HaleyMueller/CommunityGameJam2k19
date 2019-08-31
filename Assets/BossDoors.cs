using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoors : MonoBehaviour
{
	public GameObject doors;

    
    void Start()
    {
		doors.gameObject.SetActive(false);
    }


	private void OnTriggerEnter(Collider other)
	{
	}
}
