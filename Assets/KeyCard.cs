using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour
{
    public GameObject player;

    public GameObject doorRotatorTriggerObject; //Need to enable

    public GameObject openLight;
    public GameObject closeLight;

    bool playerInTrigger = false;

    public string keyCardName;

    private bool activatedDoor = false;

    private void Start()
    {
        doorRotatorTriggerObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (activatedDoor == false)
        {
            playerInTrigger = true;

            transform.Find("Canvas").gameObject.SetActive(true);

            Debug.Log(playerHasCard());

            if (playerHasCard())
            {
                transform.Find("Canvas").Find("Panel").Find("NeedCard").gameObject.SetActive(false);
                transform.Find("Canvas").Find("Panel").Find("HasCard").gameObject.SetActive(true);
            }
            else
            {
                transform.Find("Canvas").Find("Panel").Find("NeedCard").gameObject.SetActive(true);
                transform.Find("Canvas").Find("Panel").Find("HasCard").gameObject.SetActive(false);
            }
        }
    }

    bool playerHasCard()
    {
        return player.GetComponent<Inventory>().PlayerHasItem(keyCardName, false);
    }

    void OnTriggerExit(Collider other)
    {
        playerInTrigger = false;
        transform.Find("Canvas").gameObject.SetActive(false);

        //if (playerHasCard())
        //{
        //    transform.Find("Canvas").Find("Panel").Find("NeedCard").gameObject.SetActive(true);
        //    transform.Find("Canvas").Find("Panel").Find("HasCard").gameObject.SetActive(false);
        //}
    }

    private void Update()
    {
        if (playerInTrigger && activatedDoor == false)
        {
            if (Input.GetKeyDown(KeyCode.E) && playerHasCard())
            {
                openLight.SetActive(true);
                closeLight.SetActive(false);

                player.GetComponent<Inventory>().PlayerHasItem(keyCardName, true);
                doorRotatorTriggerObject.gameObject.SetActive(true);

                transform.Find("Canvas").Find("Panel").Find("NeedCard").gameObject.SetActive(false);
                transform.Find("Canvas").Find("Panel").Find("HasCard").gameObject.SetActive(false);

                activatedDoor = true;
            }
        }
    }
}
