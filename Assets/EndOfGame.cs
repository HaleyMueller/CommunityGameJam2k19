using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGame : MonoBehaviour
{

    bool playerin = false;

    public GameObject canvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerin = true;

            var play = GameObject.Find("Player");

            if (play.GetComponent<CameraHandler>().GetPictureWithMonster() != null)
            {
                ImageHolder.image = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraHandler>().GetPictureWithMonster().picture;

                SceneManager.LoadScene("Won");
            }
            else
            {
                canvas.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerin = false;

            canvas.SetActive(false);
        }
    }


}
