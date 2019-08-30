using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRocketHit : MonoBehaviour {

    public float weaponDamage;

    projectileController myPC;

    public GameObject explosionEffect;

    // Use this for initialization
    void Awake()
    {
        myPC = GetComponentInParent<projectileController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ShootablePlayer"))
        {
            myPC.removeForce();
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            if (other.tag == "Player")
            {
                playerHealth hurtEnemy = other.gameObject.GetComponent<playerHealth>();
                hurtEnemy.addDamage(weaponDamage);
            }
        }

    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ShootablePlayer"))
        {
            myPC.removeForce();
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            if(other.tag == "Player")
            {
                playerHealth hurtEnemy = other.gameObject.GetComponent<playerHealth>();
                hurtEnemy.addDamage(weaponDamage);
            }
        }

    }

}
