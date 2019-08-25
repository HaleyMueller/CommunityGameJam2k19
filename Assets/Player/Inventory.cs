using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<GameObject> objects = new List<GameObject>();

    public GameObject inventoryItemPrefab;

    public GameObject InventoryParent;

    public void AddObjectToInventory(InventoryObject inventoryObject)
    {
        var main = GameObject.Instantiate(inventoryItemPrefab, InventoryParent.transform);
        main.transform.Find("Text").GetComponent<TMPro.TextMeshProUGUI>().text = inventoryObject.Name;

        main.GetComponent<RectTransform>().position = new Vector3(main.GetComponent<RectTransform>().position.x + 10, main.GetComponent<RectTransform>().position.y, main.GetComponent<RectTransform>().position.z);

        objects.Add(main);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var i = new InventoryObject();
            i.Name = "Ree";
            AddObjectToInventory(i);
        }
    }

    private void UpdateUI()
    {

    }
}

public class InventoryObject
{
    public string Name;
    public Sprite image;
}
