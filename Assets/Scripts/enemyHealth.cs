using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour {

    public float enemyMaxHealth;

    public GameObject enemyDeathFX;

    float currentHealth;

    public Slider enemySlider;

    public bool drops;
    public GameObject theDrop;

//    public AudioClip deathSound;

	// Use this for initialization
	void Start () {
        currentHealth = enemyMaxHealth;
        enemySlider.maxValue = currentHealth;
        enemySlider.value = currentHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addDamage(float damage)
    {
        enemySlider.gameObject.SetActive(true);

        currentHealth -= damage;

        enemySlider.value = currentHealth;
        
        if(currentHealth<=0)
        {
            makeDead();
        }
    }
    void makeDead()
    {
        Destroy(gameObject.transform.parent.gameObject);
//        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Instantiate(enemyDeathFX, transform.position, transform.rotation);
        if (drops) Instantiate(theDrop, transform.position, transform.rotation);
    }
}
