using System.Collections;
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

    public GameObject exclamation;

    bool rayCastWorked = false;


    Animator animator;
    public bool isWalking = false, isIdle = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (isFrozen && !isIdle)
        {
            isWalking = false;
            isIdle = true;
            animator.SetBool("isStunned", false);
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalking", false);
        }
        else if (!isFrozen && !isWalking && doSpottedObject)
        {
            isWalking = false;
            isIdle = true;
            animator.SetBool("isStunned", false);
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalking", false);
        }
        else if (!isFrozen && !isWalking && !doSpottedObject)
        {
            isIdle = false;
            isWalking = true;
            animator.SetBool("isStunned", false);
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalking", true);
        }

        if (currentlyLookingAtPlayer && spottedObject == null && playerLeftView == false) //Player left view before timer ended
        {
            playerLeftView = true;
            currentlyLookingAtPlayer = false;

            if (lookAtPlayer != null)
                StopCoroutine(lookAtPlayer);
        }

        if (doSpottedObject)
        {
            if (spottedObject != null) //Spotted object by trigger
            {
                float maxRange = 5;
                RaycastHit hit;

y                if (Vector3.Distance(transform.position, spottedObject.transform.position) < maxRange) //Within raycast range
                {
                    if (Physics.Raycast(transform.position, (spottedObject.transform.position - transform.position), out hit, maxRange)) //If hit something
                    {
                        if (hit.transform.gameObject == spottedObject) //If hit spotted object directly
                        {
                            rayCastWorked = true;
                            playerLeftView = false;

                            var vec = new Vector3(spottedObject.transform.position.x, this.transform.position.y, spottedObject.transform.position.z);
                            transform.LookAt(vec);

                            if (currentlyLookingAtPlayer == false)
                            {
                                lookAtPlayer = NoticeTimer(); //Look at player and start timer
                                StartCoroutine(lookAtPlayer);
                            }
                        }
                        else
                        {
                            doSpottedObject = false;
                            rayCastWorked = false;
                        }
                    }
                }
            }
        }
        else
        {
            PatrolLogic();
        }
    }
    
    public void GetStunned()
    {
        animator.SetBool("isStunned", true);
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", false);
        GetComponent<Guard>().enabled = false;
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
            exclamation.GetComponent<SpriteRenderer>().enabled = true;
            exclamation.GetComponent<AlwaysLookAtCamera>().enabled = true;
        }
        else
        {
            Debug.Log("It must have been the wind");
        }
        currentlyLookingAtPlayer = false;
    }

    #endregion
}
