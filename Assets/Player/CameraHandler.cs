using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CameraHandler : MonoBehaviour
{
    bool isInCameraMode = false;

    List<Picture> pictures = new List<Picture>();

    public Renderer boss;

    public Camera c;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) //Toggles show camera UI
        {
            if (!isInCameraMode)
            {
                ShowLayer();
            }
            else
            {
                HideLayer();
            }
        }

        if (isInCameraMode)
        {
            if (Input.GetKeyDown(KeyCode.T)) //Take picture
            {
                var bossVisible = boss.isVisible;

                SavePicture(bossVisible, false); //NOTE: if you can see the boss in the editor window the value will be set to true

                if (bossVisible) //Took picture of boss. Player now has to leave level
                {

                }
            }
        }
    }

    private void ShowLayer()
    {
        c.cullingMask |= 1 << LayerMask.NameToLayer("Camera");
        isInCameraMode = true;
    }

    private void HideLayer()
    {
        c.cullingMask &= ~(1 << LayerMask.NameToLayer("Camera"));
        isInCameraMode = false;
    }

    public Picture SavePicture(bool isMonsterInView, bool isDeath)
    {
        var t = ScreenCapture.CaptureScreenshotAsTexture();

        Picture p = new Picture();
        p.picture = t;
        p.isMonster = isMonsterInView;
        p.isDeath = isDeath;

        pictures.Add(p);
        return p;
    }

    public Picture GetPictureWithMonster() //Raw Image ui texture is good to picture display on
    {
        return pictures.Where(x => x.isMonster == true).LastOrDefault();
    }

    public Picture GetPictureWithDeath()
    {
        return pictures.Where(x => x.isDeath == true).LastOrDefault();
    }

    public class Picture
    {
        public Texture picture;
        public bool isMonster = false;
        public bool isDeath = false;
    }
}
