using UnityEngine;

public class StunGun : MonoBehaviour
{
    public GameObject lightning;
    public GameObject flashlight;
    public Camera playercam;

    public AudioSource sfx;

    public AudioSource nonLoopingSFX;

    public AudioClip flashLightOn;
    public AudioClip flashLightOff;

    public bool canTaser = true;

    //can be hardcoded but put for fine tuning
    public int range = 1;

    int noTaserCount = 0;

    void Update()
    {
        if (canTaser)
        {
            noTaserCount = 0;

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
        }
        else
        {
            if (noTaserCount == 0) //First time taser disabled
            {
                lightning.gameObject.SetActive(false);
                sfx.Stop();
            }

            noTaserCount++;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashlight.gameObject.activeSelf)
            {
                nonLoopingSFX.clip = flashLightOff;
                nonLoopingSFX.Play();

                flashlight.GetComponent<Light>().enabled = false;
                flashlight.gameObject.SetActive(false);
            }
            else
            {
                nonLoopingSFX.clip = flashLightOn;
                nonLoopingSFX.Play();

                flashlight.GetComponent<Light>().enabled = true;
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
