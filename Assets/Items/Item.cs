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
    }

    void OnTriggerExit(Collider other)
    {
        playerInTrigger = false;
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
