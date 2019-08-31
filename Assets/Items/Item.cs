using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string Name;
    //public Sprite image;
    public Sprite WorldSprite;

    public GameObject player;

    bool playerInTrigger = false;

    private void Start()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = WorldSprite;
    }

    void OnTriggerEnter(Collider other)
    {
        playerInTrigger = true;

        transform.Find("Canvas").gameObject.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        playerInTrigger = false;
        transform.Find("Canvas").gameObject.SetActive(false);
    }

    private void Update()
    {
        if (playerInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var inventoryObject = new InventoryObject();
                inventoryObject.image = WorldSprite;
                inventoryObject.Name = Name;
                //inventoryObject.obj = this.gameObject;

                player.GetComponent<Inventory>().AddObjectToInventory(inventoryObject);

                Destroy(this.gameObject);
            }
        }
    }

}
