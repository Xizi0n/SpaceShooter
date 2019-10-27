using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float speed = 5f;

    public float deactivate_Timer = 1f;

    [HideInInspector]
    public bool is_EnemyBullet = false;

    // Start is called before the first frame update
    void Start()
    {

        if (is_EnemyBullet) {
            speed *= -1f;
        }

        Invoke("DeactivateGameObject", deactivate_Timer);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move() {
    }

    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }

    void DeactivateGameObject() {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if ( target.tag == "Enemy") {
            gameObject.SetActive(false);
        }
    }

}
