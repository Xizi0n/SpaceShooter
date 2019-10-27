using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{ 
    public float speed = 5f;
    public float rotate_Speed = 50f;

    public bool canShoot;
    public bool canRotate;
    private bool canMove = true;


    public float bound_Y = -6.4f;

    public Transform attack_Point;
    public GameObject enemyBullet;
    public int health = 3;
    public float schedule = 5f;

    private Animator anim;
    private AudioSource explosionSound;

    // Start is called before the first frame update

    void Start() {
        if (canRotate) {
            if (Random.Range(0, 2) > 0) {
                rotate_Speed = Random.Range(rotate_Speed, rotate_Speed + 20f);
                rotate_Speed *= -1;
            }
        }

        if (canShoot) {
            Invoke("startShooting", Random.Range(1f, 3f));
        }
    }
    void Awake()
    {
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        schedule -= Time.deltaTime;
        if (schedule < 0f)
        {
            speed *= 1.05f;
            schedule = 5f;
        }
        Move();
        rotateEnemy();
    }

    void Move() {
        if (canMove) {
            Vector3 temp = transform.position;
            temp.y -= speed * Time.deltaTime;
            transform.position = temp;

            if (temp.y < bound_Y) {
                gameObject.SetActive(false);
            }
        }
    }


    void rotateEnemy() {
        if (canRotate) {
            transform.Rotate(new Vector3(0f, 0f, rotate_Speed * Time.deltaTime), Space.World);
        }
    }

    void startShooting() {
        GameObject bullet = Instantiate(enemyBullet, attack_Point.position, Quaternion.Euler(0f, 0f, 180f));

        bullet.GetComponent<BulletScript>().is_EnemyBullet = true;

        if (canShoot) {
            Invoke("startShooting", Random.Range(1f, 3f));
        }
    }


    void TurnOffGameObject() {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D target) {
        if (target.tag == "Bullet") {
            if (health > 1)
            {
                health -= GameController.gun_power;
            }
            else {
                GameController.score += 100;
                GameController.enemiesKilled++;
                if (GameController.enemiesKilled == 5) {
                    achivementController.FiveEnemiesKilled = true;
                }
                canMove = false;
                if (canShoot)
                {
                    canShoot = false;
                    CancelInvoke("startShooting");
                }

                Invoke("TurnOffGameObject", 0.5f);

                anim.Play("Destroy");
                explosionSound.Play();
            }
            
            
        }
    }

}
