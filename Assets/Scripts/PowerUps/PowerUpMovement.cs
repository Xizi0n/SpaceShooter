using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMovement : MonoBehaviour
{

    public float speed = 5f;
    public float bound_Y = -6.4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {            
       Vector3 temp = transform.position;
       temp.y -= speed * Time.deltaTime;
       transform.position = temp;

       if (temp.y < bound_Y)
          {
            gameObject.SetActive(false);
          }
        
    }
}
