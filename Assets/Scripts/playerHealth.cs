using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class playerHealth : MonoBehaviour {
    public AudioClip playerHurt;

    public float fullHealth;
    float currentHealth;

    public restartGame theGameManager;

    AudioSource playerAS;

    public GameObject deathFX;
    playerController controlMovement;
    //HUD variables
    public Slider healthSlider;
    public Image damageScreen;
    public Text gameOverScreen;
    public Text winGameScreen;

    bool damaged = false;
    Color damagedColour = new Color(5f, 0f, 0f, 1f);
    float smoothColour = 5f;
    // Use this for initialization
    void Start() {
        currentHealth = fullHealth;
        controlMovement = GetComponent<playerController>();

        //HUD Initialazation
        healthSlider.maxValue = fullHealth;
        healthSlider.value = fullHealth;
        damaged = false;

        playerAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (damaged) {
            damageScreen.color = damagedColour;
        } else {
            damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, smoothColour * Time.deltaTime);
        }
        damaged = false;
    }
    public void addDamage(float damage) {
        if (damage <= 0) return;
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        playerAS.clip = playerHurt;
        playerAS.Play();
        //Debug.Log("ouch");
        playerAS.PlayOneShot(playerHurt);
        damaged = true;

        if (currentHealth <= 0) {
            makeDead();

        }
    }

    public void addHealth(float healthAmount) {
        currentHealth += healthAmount;
        if (currentHealth >= fullHealth) {
            currentHealth = fullHealth;

        }
        healthSlider.value = currentHealth;
    }

    public void makeDead() {
        Instantiate(deathFX, transform.position, transform.rotation);
        Destroy(gameObject);
        damageScreen.color = damagedColour;

        Animator gameOverAnimator = gameOverScreen.GetComponent<Animator>();
        gameOverAnimator.SetTrigger("gameOver");
        theGameManager.restartTheGame();
    }

    public void winGame() {
        Destroy(gameObject);
        theGameManager.restartTheGame();
        Animator winGameAnimator = winGameScreen.GetComponent<Animator>();
        winGameAnimator.SetTrigger("gameOver");
    }

}
