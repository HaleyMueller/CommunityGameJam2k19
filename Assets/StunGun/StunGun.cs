using UnityEngine;

public class StunGun : MonoBehaviour
{
    public GameObject lightning;
    public GameObject flashlight;
    public Camera playercam;

    //can be hardcoded but put for fine tuning
    public int range;
    

    void Update()
    {


        if (Input.GetKey(KeyCode.Mouse0))
        {
            lightning.gameObject.SetActive(true);

            StunGuard();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            lightning.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashlight.gameObject.activeSelf)
            {
                flashlight.gameObject.SetActive(false);
            }
            else
            {
                flashlight.gameObject.SetActive(true);
            }
        }

    }

    void StunGuard()
    {
        RaycastHit hit;

        if (Physics.Raycast(playercam.transform.position, playercam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Guard theGuard = hit.transform.GetComponentInParent<Guard>();
            if (theGuard != null)
            {
                theGuard.GetStunned();
            }
        }
    }
}
