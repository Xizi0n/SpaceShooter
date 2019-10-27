using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{
    private float min_X = -11f;
    private float max_X = 11f;

    public float timer = 5f;

    public GameObject[] powerUp_Prefabs;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnPowerUp", timer);
    }

    // Update is called once per frame
    void SpawnPowerUp()
    {
        float pos_X = Random.Range(min_X, max_X);
        Debug.Log(pos_X);
        Vector3 temp = transform.position;
        temp.x = pos_X;
        temp.z = -6.24f;
        Debug.Log(temp);

        if (Random.Range(0, 2) > 0)
        {
            Instantiate(powerUp_Prefabs[Random.Range(0, powerUp_Prefabs.Length)], temp, Quaternion.identity);
        }

        Invoke("SpawnPowerUp", timer);
    }
}
