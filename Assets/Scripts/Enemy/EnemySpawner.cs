using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    /*private float min_X = -11f;
    private float max_X= 11f;*/

    public GameObject[] asteroid_Prefabs;
    public GameObject[] enemyPrefabs;

    public float timer = 20f;
    public float schedule = 20f;

    float halfHeight = 0f;
    float halfWidth = 0f;



    // Start is called before the first frame update
    void Start()
    {
        
        Invoke("SpawnEnemies", timer);
        Camera camera = Camera.main;
        halfHeight = camera.orthographicSize;
        halfWidth = camera.aspect * halfHeight;
    }

    private void Update()
    {
        schedule -= Time.deltaTime;
        if (schedule < 0f)
        {
            timer *= 0.8f;
            schedule = 5f;
        }
    }

    // Update is called once per frame
    void SpawnEnemies() {
        float pos_X = Random.Range(-halfWidth, halfWidth);
        Debug.Log(pos_X);
        Vector3 temp = transform.position;
        temp.x = pos_X;
        temp.z = -6.24f;
        Debug.Log(temp);

        if (Random.Range(0, 2) > 0)
        {
            Instantiate(asteroid_Prefabs[Random.Range(0, asteroid_Prefabs.Length)], temp, Quaternion.identity);
        }
        else {

            Instantiate(enemyPrefabs[Random.Range(0, asteroid_Prefabs.Length)], temp, Quaternion.Euler(0f, 0f, 0f));
        }

        Invoke("SpawnEnemies", timer);
    }
}
