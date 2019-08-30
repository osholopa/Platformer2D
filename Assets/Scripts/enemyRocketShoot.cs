using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRocketShoot : MonoBehaviour
{

    public Transform gunTip;
    public GameObject bullet;
    float fireRate = 0.5f;
    float nextFire = 0f;
    bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        facingRight = false;
        cannonAnim = GetComponentInChildren<Animator>();
        nextShootTime = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject theProjectile;
    public GameObject shootFX;
    public float shootTime;
    public int chanceShoot;
    public Transform shootFrom;

    float nextShootTime;
    Animator cannonAnim;

    void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player" && nextShootTime < Time.time) {
            nextShootTime = Time.time + shootTime;
            if (Random.Range(0, 10) >= chanceShoot) {
                fireRocket();
                cannonAnim.SetTrigger("cannonShoot");
            }
        }
    }

    void fireRocket() {
        if (Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            if (facingRight) {
                Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            } else if (!facingRight) {
                Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, -180f)));
            }
        }
    }
}
