using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private List<InventoryObject> objects = new List<InventoryObject>();

    public GameObject inventoryItemPrefab;

    public GameObject InventoryParent;

    public bool PlayerHasItem(InventoryObject obj)
    {
        var item = objects.Where(x => x == obj).FirstOrDefault();

        if (item != null)
        {
            return true;
        }

        return false;
    }

    public void AddObjectToInventory(InventoryObject inventoryObject)
    {
        var main = GameObject.Instantiate(inventoryItemPrefab, InventoryParent.transform);
        main.transform.Find("Text").GetComponent<TMPro.TextMeshProUGUI>().text = inventoryObject.Name;

        if (inventoryObject.image != null)
        {
            if (main.transform.Find("Image") != null)
                main.transform.Find("Image").GetComponent<UnityEngine.UI.Image>().sprite = inventoryObject.image;
        }

        main.GetComponent<RectTransform>().localPosition = new Vector3(main.GetComponent<RectTransform>().localPosition.x, main.GetComponent<RectTransform>().localPosition.y, main.GetComponent<RectTransform>().localPosition.z);

        Vector3 defaultPostition = new Vector3(main.GetComponent<RectTransform>().localPosition.x, main.GetComponent<RectTransform>().localPosition.y, main.GetComponent<RectTransform>().localPosition.z);

        inventoryObject.obj = main;

        objects.Add(inventoryObject);

        UpdateUI(defaultPostition);
    }

    private void UpdateUI(Vector3 defaultPostition)
    {
        var i = 0;

        foreach (var obj in objects)
        {
            obj.obj.GetComponent<RectTransform>().localPosition = new Vector3(defaultPostition.x - (150 * i), defaultPostition.y, defaultPostition.z);

            i++;
        }
    }
}

public class InventoryObject
{
    public string Name;
    public Sprite image;

    public GameObject obj;
}
