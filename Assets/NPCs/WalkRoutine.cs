using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkRoutine : MonoBehaviour
{
    public List<WalkPoint> walkPoints = new List<WalkPoint>();

    internal int currentWalkPointIndex = 0;

    private bool doPatrol = true;
    public bool doSpottedObject = false;
    public GameObject spottedObject = null;

    /// <summary>
    /// Walk speed
    /// </summary>
    public float speed = 4f;

    /// <summary>
    /// Timer for how long player has to stay in view to be noticed
    /// </summary>
    public float noticeTimer = 4f;

    public bool pickRandomPoint = false;

    /// <summary>
    /// Bool that checks if it needs to run through index backwards
    /// </summary>
    private bool backwardsPoint = false;

    /// <summary>
    /// Used by Rotation and Stand time to prevent moving for a period of time
    /// </summary>
    private bool isFrozen = false;

    /// <summary>
    /// Courtine that looks at the player and does a timer
    /// </summary>
    IEnumerator lookAtPlayer = null;

    internal void PatrolLogic()
    {
        if (currentlyLookingAtPlayer && spottedObject == null && playerLeftView == false) //Player left view before timer ended
        {
            playerLeftView = true;

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
            if (doPatrol)
            {
                WalkToNextPoint();
            }
        }
    }

    #region PlayerLookAt

    private bool currentlyLookingAtPlayer = false;
    private bool playerLeftView = false;

    IEnumerator NoticeTimer()
    {
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

    #region Patrol

    void WalkToNextPoint()
    {
        if (!isFrozen)
        {
            // Move our position a step closer to the target.
            float step = walkPoints[currentWalkPointIndex].Speed * Time.deltaTime; // calculate distance to move

            Vector3 goToPos = new Vector3(walkPoints[currentWalkPointIndex].point.transform.position.x, this.transform.position.y, walkPoints[currentWalkPointIndex].point.transform.position.z);

            transform.LookAt(goToPos, Vector3.up);
            transform.position = Vector3.MoveTowards(transform.position, goToPos, step);

            //At Point
            if (Vector3.Distance(transform.position, goToPos) < 0.001f)
            {
                isFrozen = true;
                StartCoroutine(StandTime(walkPoints[currentWalkPointIndex].StandTime, walkPoints[currentWalkPointIndex]));

                if (pickRandomPoint == false)
                {
                    NextWalkPoint(currentWalkPointIndex);
                }
                else
                {
                    currentWalkPointIndex = RandomWalkPointIndex(currentWalkPointIndex);
                }
            }
        }
    }

    int RandomWalkPointIndex(int currIndex)
    {
        if (currIndex == walkPoints.Count - 1)
        {
            return currIndex - 1;
        }
        else
        {
            if (currIndex == 0)
            {
                return 1;
            }
            else
            {
                var rFloat = UnityEngine.Random.Range(0, 2);

                if (rFloat < 1f)
                {
                    return currIndex - 1;
                }
                else
                {
                    return currIndex + 1;
                }
            }
        }
    }

    IEnumerator StandTime(float sec, WalkPoint point)
    {
        yield return new WaitForSeconds(sec);

        if (point.RotateSpeed > 0)
        {
            StartCoroutine(Rotate(walkPoints[currentWalkPointIndex]));
        }
        else
        {
            isFrozen = false;
        }

    }

    IEnumerator Rotate(WalkPoint pointTo)
    {
        if (pointTo.RotateSpeed > 0)
        {
            while (true)
            {
                Vector3 vec = new Vector3(pointTo.point.transform.position.x, transform.position.y, pointTo.point.transform.position.z);
                Vector3 dir = vec - transform.position;

                Quaternion rot = Quaternion.LookRotation(dir);

                // slerp to the desired rotation over time
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, walkPoints[currentWalkPointIndex].RotateSpeed * Time.deltaTime);

                if (Vector3.Angle(transform.forward, dir) < 1)
                    break;

                yield return null;
            }
        }

        isFrozen = false;
    }



    /// <summary>
    /// Gets the next Walk Point from index
    /// </summary>
    /// <param name="index">Walk Poin index</param>
    /// <returns></returns>
    WalkPoint NextWalkPoint(int index)
    {
        if (!backwardsPoint)
        {
            if (index == walkPoints.Count - 1)
            {
                backwardsPoint = true;
                currentWalkPointIndex = walkPoints.Count - 1;

                return walkPoints[walkPoints.Count - 1];
            }
        }
        else
        {
            if (index <= 1)
            {
                backwardsPoint = false;
                currentWalkPointIndex = 0;

                return walkPoints[0];
            }
        }

        if (!backwardsPoint)
        {
            currentWalkPointIndex++;
        }
        else
        {
            currentWalkPointIndex--;
        }

        return walkPoints[currentWalkPointIndex];
    }

    #endregion
}

[Serializable]
public class WalkPoint
{
    public GameObject point = null;

    public float StandTime = 0f;
    public float Speed = 3f;
    public float RotateSpeed = 3f;

    //Maybe?
    //public bool LookLeftRight = true;
}
