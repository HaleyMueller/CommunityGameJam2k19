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

    public bool PlayerHasItem(string objName, bool removeItem)
    {
        var item = objects.Where(x => x.Name == objName).FirstOrDefault();

        Debug.Log(objects.Count());

        if (item != null)
        {
            if (removeItem)
            {
                RemoveObjectFromInventory(item);
            }

            return true;
        }

        return false;
    }

    public void RemoveObjectFromInventory(InventoryObject inventoryObject)
    {
        var trans = objects.FirstOrDefault().obj.transform.localPosition;

        Destroy(inventoryObject.obj);

        objects.Remove(inventoryObject);

        UpdateUI(trans);
    }

    public void AddObjectToInventory(InventoryObject inventoryObject)
    {
        var main = GameObject.Instantiate(inventoryItemPrefab);
        //main.transform.SetParent(panefl.transform, false);

        main.transform.Find("Canvas").Find("Panel").Find("Item").Find("Text").GetComponent<TMPro.TextMeshProUGUI>().text = inventoryObject.Name;

        if (inventoryObject.image != null)
        {
            if (main.transform.Find("Canvas").Find("Panel").Find("Item").Find("Image") != null)
                main.transform.Find("Canvas").Find("Panel").Find("Item").Find("Image").GetComponent<UnityEngine.UI.Image>().sprite = inventoryObject.image;
        }

        //TODO Broken below

        //main.GetComponent<RectTransform>().localPosition = new Vector3(main.GetComponent<RectTransform>().localPosition.x, main.GetComponent<RectTransform>().localPosition.y, main.GetComponent<RectTransform>().localPosition.z);

        //Vector3 defaultPostition = new Vector3(main.GetComponent<RectTransform>().localPosition.x, main.GetComponent<RectTransform>().localPosition.y, main.GetComponent<RectTransform>().localPosition.z);

        inventoryObject.obj = main;

        objects.Add(inventoryObject);

        Debug.Log("Added " + inventoryObject.Name);

        //UpdateUI(defaultPostition);
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

[System.Serializable]
public class InventoryObject
{
    public string Name;
    public Sprite image;

    [HideInInspector]
    public GameObject obj;
}
