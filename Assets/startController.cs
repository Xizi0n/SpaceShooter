using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class startController : MonoBehaviour
{
    public TextMeshProUGUI startText;
    // Start is called before the first frame update
    void Start()
    {
        startText.alpha = 1;
        Invoke("toggleStartText", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("GameScene");
        }


    }

    void toggleStartText()
    {
        if (this.startText.alpha == 1)
        {
            this.startText.alpha = 0;
            Invoke("toggleStartText", 0.5f);
        }
        else
        {
            this.startText.alpha = 1;
            Invoke("toggleStartText", 0.5f);
        }
    }
}
