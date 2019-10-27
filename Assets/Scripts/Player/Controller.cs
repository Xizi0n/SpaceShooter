using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public float speed = 5f;
    public float min_X, max_X;

    [SerializeField]
    private GameObject player_Bullet;

    [SerializeField]
    private Transform attack_Point;
    public Transform attack_Point2;
    public Transform attack_Point3;
    public GameObject shield;

    public float attack_Timer;
    private float current_Attack_Timer;
    private bool canAttack;

    private AudioSource laserAudio;
    private List<GameObject> bullets;

    private float halfHeight = 0f;
    private float halfWidth = 0f;
    public float fireRate = 0.7f;
    public bool isShielded = false;

    private void Awake()
    {
        laserAudio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        shield.SetActive(false);
        bullets = new List<GameObject>();
        current_Attack_Timer = attack_Timer;
        Camera camera = Camera.main;
        halfHeight = camera.orthographicSize;
        halfWidth = camera.aspect * halfHeight;
        for (int i = 0; i < 100; i++) {
            GameObject temp = Instantiate(player_Bullet, attack_Point.position, Quaternion.identity);
            temp.SetActive(false);
            bullets.Add(temp);
        }
        Invoke("Shoot", fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    void Move() {
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {

            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;

            if (temp.x > halfWidth) {
                temp.x = -halfWidth;
            }

            transform.position = temp;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f) {
            Vector3 temp = transform.position;
            temp.x -= speed * Time.deltaTime;

            if (temp.x < -halfWidth)
            {
                temp.x = halfWidth;
            }

            transform.position = temp;
        }
    }

    void Attack() {
        attack_Timer += Time.deltaTime;
        if (attack_Timer > current_Attack_Timer) {
            canAttack = true;
        }

        /*if (Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
        }*/
    }

    void plusLife() {
        GameController.health++;
    }

    void minusLife() {
        if ( !isShielded ) {
            GameController.health--;
            if (GameController.gun_lvl > 1)
            {
                GameController.gun_lvl--;
            }
            if (GameController.gun_power > 1) {
                GameController.gun_power--;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Enemy" || target.tag == "Enemy_Bullet") {
            this.minusLife();
            target.gameObject.GetComponent<Animator>().Play("Destroy");
            Destroy(target.gameObject);
        }
        if (target.gameObject.tag == "PowerUp_Health") {
            this.plusLife();
            Destroy(target.gameObject);
            GameController.collectedPowerUps++;
            powerUpCheck();
        }
        if (target.gameObject.tag == "PowerUp_FireRate") {
            fireRate *= 0.8f;
            Destroy(target.gameObject);
            GameController.collectedPowerUps++;
            powerUpCheck();
        }
        if (target.gameObject.tag == "PowerUp_Laser")
        {
            GameController.gun_lvl++;
            if (GameController.gun_lvl >= 3 && GameController.gun_power < 3) {
                GameController.gun_power++;
            }
            Destroy(target.gameObject);
            GameController.collectedPowerUps++;
            powerUpCheck();
            Debug.Log(GameController.gun_power);
        }
        if (target.gameObject.tag == "PowerUp_Shield")
        {
            Invoke("destroyShield", 20.0f);
            shield.SetActive(true);
            isShielded = true;
            Destroy(target.gameObject);
            GameController.collectedPowerUps++;
            powerUpCheck();
        }
    }

    void destroyShield() {
        isShielded = false;
        shield.SetActive(false);
    }

    void Shoot() {
        List<GameObject> tmp_bullets = getActiveBullets();
        switch (tmp_bullets.Count) {
            case 1:
                tmp_bullets[0].transform.position = attack_Point.position;
                tmp_bullets[0].GetComponent<Rigidbody2D>().AddForce(new Vector2(0,8) * 3, ForceMode2D.Impulse);
                break;
            case 2:
                tmp_bullets[0].transform.position = attack_Point2.position;
                tmp_bullets[0].transform.Rotate(new Vector3(0, 0, 15));
                tmp_bullets[0].GetComponent<Rigidbody2D>().AddForce(new Vector2(-2, 8) * 3, ForceMode2D.Impulse);
                tmp_bullets[1].transform.position = attack_Point3.position;
                tmp_bullets[1].transform.Rotate(new Vector3(0, 0, -15));
                tmp_bullets[1].GetComponent<Rigidbody2D>().AddForce(new Vector2(2, 8) * 3, ForceMode2D.Impulse);
                break;
            case 3:
                tmp_bullets[0].transform.position = attack_Point.position;
                tmp_bullets[0].GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 8) * 3, ForceMode2D.Impulse);
                tmp_bullets[1].transform.position = attack_Point3.position;
                tmp_bullets[1].transform.Rotate(new Vector3(0, 0, -15));
                tmp_bullets[1].GetComponent<Rigidbody2D>().AddForce(new Vector2(2, 8) * 3, ForceMode2D.Impulse);
                tmp_bullets[2].transform.position = attack_Point2.position;
                tmp_bullets[2].transform.Rotate(new Vector3(0, 0, 15));
                tmp_bullets[2].GetComponent<Rigidbody2D>().AddForce(new Vector2(-2, 8) * 3, ForceMode2D.Impulse);
                break;
        }
        if(!GameController.isDead){
            Invoke("Shoot", fireRate);
        }
    }

    List<GameObject> getActiveBullets() {
        List<GameObject> result = new List<GameObject>();
        int j = 0;
        for (int i = 0; i < 100; i++) {
            if (j == GameController.gun_lvl || j == 3) {
                return result;
            }
            if (!bullets[i].activeSelf) {
                result.Add(bullets[i]);
                result[j].transform.rotation = Quaternion.identity;
                result[j].SetActive(true);
                j++;
            }
            
        }
        return null;
    }

    void powerUpCheck() {
        if (GameController.collectedPowerUps == 5) {
            achivementController.FivePowerUpsPicked = true;
        }
    }
}
