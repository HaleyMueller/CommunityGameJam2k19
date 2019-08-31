using UnityEngine;

public class StunGun : MonoBehaviour
{
    public GameObject lightning;
    public GameObject flashlight;
    public Camera playercam;

    public AudioSource sfx;

    //can be hardcoded but put for fine tuning
    public int range = 1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            sfx.Play();
        }


        if (Input.GetKey(KeyCode.Mouse0))
        {
            lightning.gameObject.SetActive(true);

            StunGuard();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            lightning.gameObject.SetActive(false);
            sfx.Stop();
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
            Guard theGuard = hit.transform.GetComponentInParent<Guard>();
            if (theGuard != null)
            {
                theGuard.GetStunned();
            }
        }
    }
}
