using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    //movement variables
    public float maxSpeed;
    Rigidbody2D myRB;
    Animator myAnim;
    bool facingRight;
    // jumping variables
    bool grounded = false;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;
    //mobile movement variables
    private Vector2 startTouchPosition, endTouchPosition;

    //shooting variables
    public Transform gunTip;
    public GameObject bullet;
    float fireRate = 0.5f;
    float nextFire = 0f;

    // Use this for initialization
    void Start() {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        facingRight = true;
    }

    // Update is called once per frame
    void Update() {

        if (grounded && Input.GetAxis("Jump") > 0) {
            grounded = false;
            myAnim.SetBool("isGrounded", grounded);
            //Jump
            myRB.AddForce(new Vector2(0, jumpHeight));
        }
        //Desktop shoot
        if (SystemInfo.deviceType != DeviceType.Handheld) {
            if (Input.GetAxisRaw("Fire1") > 0) fireRocket();
        }
        //Mobile shoot
        if (Input.touchCount > 0) {
            var touch = Input.touches[0];
            if (touch.position.x > (0.75*Screen.width) && touch.position.y < (0.25*Screen.height)) {
                fireRocket();
            }
        }

    }

    void FixedUpdate() {
        //check if we are grounded - if no, then we are falling
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        myAnim.SetBool("isGrounded", grounded);

        myAnim.SetFloat("verticalSpeed", myRB.velocity.y);

        //Desktop movement
        if (SystemInfo.deviceType == DeviceType.Desktop) {
            float move = Input.GetAxis("Horizontal");
            myAnim.SetFloat("speed", Mathf.Abs(move));
            myRB.velocity = new Vector2(move * maxSpeed, myRB.velocity.y);

            if (move > 0 && !facingRight) {
                flip();
            } else if (move < 0 && facingRight) {
                flip();
            }
        }

        swipeJumpCheck();
        swipeMoveCheck();
    }
    // character flip code
    void flip() {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void fireRocket() {
        if (Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            if (facingRight) {
                Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            } else if (!facingRight) {
                Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 180f)));
            }
        }
    }

    //Mobile movement
    void swipeMoveCheck() {

        if (Input.touchCount == 1) {
            var touch = Input.touches[0];
            if (touch.position.x < Screen.width / 4 && touch.position.y < Screen.height / 2) {

                if (facingRight) {
                    flip();
                }
                myRB.velocity = new Vector2(-1f * maxSpeed, myRB.velocity.y);
                myAnim.SetFloat("speed", Mathf.Abs(1));

            }
            if (touch.position.x > Screen.width / 4 && touch.position.x < 0.5*Screen.width && touch.position.y < Screen.height / 2) {

                if (!facingRight) {
                    flip();
                }
                myRB.velocity = new Vector2(1f * maxSpeed, myRB.velocity.y);
                myAnim.SetFloat("speed", Mathf.Abs(1));

            }
        }
        if (SystemInfo.deviceType == DeviceType.Handheld && Input.touchCount == 0) {

            myAnim.SetFloat("speed", Mathf.Abs(0));
        }
    }

    //Mobile jump
    void swipeJumpCheck() {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            startTouchPosition = Input.GetTouch(0).position;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
            endTouchPosition = Input.GetTouch(0).position;
            if (endTouchPosition.y > startTouchPosition.y) {

                myRB.AddForce(new Vector2(0, jumpHeight * 3));
                myAnim.SetBool("isGrounded", false);
            }
        }
    }

}
