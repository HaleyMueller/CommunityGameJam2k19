using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextTyper : MonoBehaviour
{
    public TMPro.TMP_Text textObj;

    public string text;

    int index = 0;

    private void Start()
    {
        StartCoroutine(StartLevel());
    }

    void Update()
    {
        if (wroteLetter == false)
        {
            StartCoroutine(WriteLetter());
        }
    }

    bool wroteLetter = false;

    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(26);

        SceneManager.LoadScene("Level01");
    }

    IEnumerator WriteLetter()
    {
        wroteLetter = true;

        textObj.text += text[index];

        var randomTime = Random.Range(.2f, .7f);

        yield return new WaitForSeconds(randomTime);

        index++;

        wroteLetter = false;
    }

}
