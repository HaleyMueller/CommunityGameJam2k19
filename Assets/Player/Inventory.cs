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
        main.GetComponent<RectTransform>().position = new Vector3(main.GetComponent<RectTransform>().position.x, main.GetComponent<RectTransform>().position.y, main.GetComponent<RectTransform>().position.z);

        objects.Add(main);

        var i = 1;

        foreach (var obj in objects)
        {
            obj.GetComponent<RectTransform>().localPosition = new Vector3(0, obj.GetComponent<RectTransform>().localPosition.y, obj.GetComponent<RectTransform>().localPosition.z);

            if (objects.Count % 2 == 0) //Even amount
            {
                if (i % 2 == 0)
                {
                    obj.GetComponent<RectTransform>().localPosition = new Vector3(obj.GetComponent<RectTransform>().localPosition.x + (110 * i) - (55 * i), obj.GetComponent<RectTransform>().localPosition.y, obj.GetComponent<RectTransform>().localPosition.z);
                }
                else
                {
                    obj.GetComponent<RectTransform>().localPosition = new Vector3(obj.GetComponent<RectTransform>().localPosition.x + ((110 * i) - (55 * i)) * -1, obj.GetComponent<RectTransform>().localPosition.y, obj.GetComponent<RectTransform>().localPosition.z);
                }
            }
            else
            {
                if (i == Mathf.Ceil(i / objects.Count))
                {
                    obj.GetComponent<RectTransform>().localPosition = new Vector3(0, obj.GetComponent<RectTransform>().localPosition.y, obj.GetComponent<RectTransform>().localPosition.z);
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        obj.GetComponent<RectTransform>().localPosition = new Vector3(obj.GetComponent<RectTransform>().localPosition.x + (110 * i) - (55 * i), obj.GetComponent<RectTransform>().localPosition.y, obj.GetComponent<RectTransform>().localPosition.z);
                    }
                    else
                    {
                        obj.GetComponent<RectTransform>().localPosition = new Vector3(obj.GetComponent<RectTransform>().localPosition.x + ((110 * i) - (55 * i)) * -1, obj.GetComponent<RectTransform>().localPosition.y, obj.GetComponent<RectTransform>().localPosition.z);
                    }
                }
            }
            i++;
        }
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
