﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : WalkRoutine
{
    /// <summary>
    /// Courtine that looks at the player and does a timer
    /// </summary>
    IEnumerator lookAtPlayer = null;

    /// <summary>
    /// Timer for how long player has to stay in view to be noticed
    /// </summary>
    public float noticeTimer = 4f;

    [HideInInspector]
    public bool doSpottedObject = false;

    [HideInInspector]
    public GameObject spottedObject = null;

    void Update()
    {
        if (currentlyLookingAtPlayer && spottedObject == null && playerLeftView == false) //Player left view before timer ended
        {
            playerLeftView = true;
            currentlyLookingAtPlayer = false;

            if (lookAtPlayer != null)
                StopCoroutine(lookAtPlayer);
        }

        if (doSpottedObject)
        {
            if (spottedObject != null)
            {
                playerLeftView = false;

                var vec = new Vector3(spottedObject.transform.position.x, this.transform.position.y, spottedObject.transform.position.z);
                transform.LookAt(vec);

                if (currentlyLookingAtPlayer == false)
                {
                    lookAtPlayer = NoticeTimer(); //Look at player and start timer
                    StartCoroutine(lookAtPlayer);
                }
            }
        }
        else
        {
            PatrolLogic();
        }
    }

    #region PlayerLookAt

    private bool currentlyLookingAtPlayer = false;
    private bool playerLeftView = false;

    IEnumerator NoticeTimer()
    {
        Debug.Log("starting timer");
        currentlyLookingAtPlayer = true;
        yield return new WaitForSeconds(noticeTimer);
        if (spottedObject != null)
        {
            Debug.Log("Successfully found player");
        }
        else
        {
            Debug.Log("It must have been the wind");
        }
        currentlyLookingAtPlayer = false;
    }

    #endregion
}
