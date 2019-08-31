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

    public float killRange = 3f;

    [HideInInspector]
    public bool doSpottedObject = false;

    [HideInInspector]
    public GameObject spottedObject = null;

    [HideInInspector]
    public bool seenPlayer = false;

    public AudioSource taserSFX;

    public GameObject exclamation;
    public GameObject particleTaser;

    public GameObject itemPrefab;

    public bool dropItem = false;
    public InventoryObject itemToDrop;

    [HideInInspector]
    bool rayCastWorked = false;

    public AudioSource vfx;

    public List<AudioClip> searchingClips;
    public List<AudioClip> lostClips;
    public List<AudioClip> foundClips;
    public List<AudioClip> deadClips;

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

        if (currentlyLookingAtPlayer && spottedObject == null && playerLeftView == false && isDead == false) //Player left view before timer ended
        {
            playerLeftView = true;
            currentlyLookingAtPlayer = false;

            int indexp = Random.Range(0, lostClips.Count);
            var shootClipz = lostClips[indexp];
            vfx.clip = shootClipz;
            vfx.Play();

            if (lookAtPlayer != null)
                StopCoroutine(lookAtPlayer);
        }

        if (doSpottedObject && isDead == false)
        {
            if (spottedObject != null) //Spotted object by trigger
            {
                float maxRange = 5;
                RaycastHit hit;

                if (Vector3.Distance(transform.position, spottedObject.transform.position) < maxRange) //Within raycast range
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

                            if (Vector3.Distance(this.transform.position, spottedObject.transform.position) <= killRange && playerDeathBool == true)
                            {
                                Debug.Log("player dead");
                                GameObject.FindGameObjectWithTag("Player").GetComponent<CameraHandler>().SavePicture(false, true); //Save current screenshot to pictures

                                ImageHolder.image = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraHandler>().GetPictureWithDeath().picture;

                                if (ImageHolder.image != null)
                                {
                                    UnityEngine.SceneManagement.SceneManager.LoadScene("Death");
                                }
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
            if (isDead == false)
            {
                PatrolLogic(seenPlayer);
            }

            particleTaser.SetActive(false);

            taserSFX.Stop();

            playerDeathBool = false;
        }
    }

    [HideInInspector]
    public bool isDead = false;

    public void GetStunned()
    {
        if (isDead == false)
        {
            isDead = true;

            animator.SetBool("isStunned", true);
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalking", false);
            GetComponent<Guard>().enabled = false;

            this.GetComponent<Animator>().enabled = true;
            this.GetComponent<Guard>().enabled = false;

            int indexp = Random.Range(0, deadClips.Count);
            var shootClipz = deadClips[indexp];
            vfx.clip = shootClipz;
            vfx.Play();

            if (dropItem) //Drop item
            {
                var s = Instantiate(itemPrefab, this.transform.position, this.transform.rotation);

                s.transform.position = new Vector3(s.transform.position.x, s.transform.position.y + .75f, s.transform.position.z);

                s.GetComponent<Item>().Name = itemToDrop.Name;
                s.GetComponent<Item>().WorldSprite = itemToDrop.image;
            }

            vfx.Stop();
            taserSFX.loop = false;
            taserSFX.Stop();
            particleTaser.SetActive(false);
        }
    }

    #region PlayerLookAt

    private bool currentlyLookingAtPlayer = false;
    private bool playerLeftView = false;

    private bool playerDeathBool = false;

    IEnumerator NoticeTimer()
    {
        currentlyLookingAtPlayer = true;

        //int index = Random.Range(0, searchingClips.Count);
        //var shootClip = searchingClips[index];
        //vfx.clip = shootClip;
        //vfx.Play();

        yield return new WaitForSeconds(noticeTimer);
        if (spottedObject != null)
        {
            Debug.Log("Successfully found player");
            exclamation.GetComponent<SpriteRenderer>().enabled = true;
            exclamation.GetComponent<AlwaysLookAtCamera>().enabled = true;

            seenPlayer = true;

            playerDeathBool = true;

            taserSFX.Play();
            particleTaser.SetActive(true);

            int indexx = Random.Range(0, foundClips.Count);
            var shootClipp = foundClips[indexx];
            vfx.clip = shootClipp;
            vfx.Play();
        }
        else
        {


            Debug.Log("It must have been the wind");
            particleTaser.SetActive(false);
        }

        currentlyLookingAtPlayer = false;
    }

    #endregion
}