using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class achivementController : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI achivementText;
    public static bool FiveEnemiesKilled;
    public static bool FivePowerUpsPicked;
    public static bool OutlivedThirtySeconds;


    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (FiveEnemiesKilled) {
            showAchivement("5 enemies killed");
            Invoke("hideAchivement", 3f);
            FiveEnemiesKilled = false;
        }
        if (FivePowerUpsPicked)
        {
            showAchivement("2 powerUps picked up");
            Invoke("hideAchivement", 3f);
            FivePowerUpsPicked = false;
        }
        if (OutlivedThirtySeconds)
        {
            showAchivement("30s outlived");
            Invoke("hideAchivement", 3f);
            OutlivedThirtySeconds = false;
        }
    }

    public void showAchivement(string message) {
        panel.SetActive(true);
        achivementText.text = message;
    }

    public void hideAchivement() {
        panel.SetActive(false);
    }
}
