using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCC : MonoBehaviour
{
    Vector2 mouseLook, smoothV;
    public float sesitivity = 5f;
    public float smoothing = 2f;
    GameObject character;

    void Start() {
        character = this.transform.parent.gameObject;  
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sesitivity * smoothing, sesitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        if (-mouseLook.y <= 70 && -mouseLook.y >= -100)
        {
            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);

        }
        else if (-mouseLook.y >= 70)
        {
            mouseLook.y = -70;
        }
        else if (-mouseLook.y <= -100)
        {
            mouseLook.y = 100;
        }


        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
    

}
