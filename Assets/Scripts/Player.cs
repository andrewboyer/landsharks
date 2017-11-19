using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float jumpHeight = 4;
    public float timetoJumpApex = 0.4f;
	public float multiplier = 1;
	public float multiplierDuration = 0f;

    public bool isShadow;

    public float moveSpeed = 6;
	public float gravity;
	public float jumpVelocity;

    public float accelerationTimeStopping = 0.1f;
	public float accelerationTimeAirborn = 0.2f;
	public float accelerationTimeGrounded = 0.4f;
    float xSmooth;

    public GameObject shadow;
    public GameObject timerObj;

    private CountdownTimer timer;

    protected float moveThreshold = 0.2f;

    Vector3 velocity;

    Controller2D controller;   

	// Use this for initialization
	void Start () {

        timer = timerObj.GetComponent<CountdownTimer>();

        controller = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timetoJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity * timetoJumpApex);
        Debug.Log("Gravity: " +  gravity + "  JumpSpeed: " + jumpHeight);

        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		if (!isShadow && other.gameObject == shadow) {
			timer.gameOverText.text = "Person Wins!";
			timer.timerEnded ();
		} else if (other.gameObject.tag == "MysteryBox") {
			multiplier = Random.Range (0.5f, 1.5f);
			multiplierDuration = 10f;
		}
    }

    // Update is called once per frame
    void Update () {

        Vector2 input = new Vector2(0, 0);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
        
        if (isShadow)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontalarrow"), Input.GetAxisRaw("Verticalarrow"));
        } else
        {
            input = new Vector2(Input.GetAxisRaw("Horizontalwasd"), Input.GetAxisRaw("Verticalwasd"));
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && controller.collisions.below && isShadow)
        {
            velocity.y = jumpVelocity;
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) && isShadow)
        {
            velocity.y *= 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.W) && controller.collisions.below && !isShadow)
        {
            velocity.y = jumpVelocity;
        }
        else if (Input.GetKeyUp(KeyCode.W) && !isShadow)
        {
            velocity.y *= 0.5f;
        }

		float targetVelocityX = input.x * (moveSpeed * multiplier);

        if (!controller.collisions.below && velocity.y < -15)
        {
            velocity.y = -15;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        if (input.x == 0)
            SetVelo(targetVelocityX, accelerationTimeStopping);
        else if (controller.collisions.below)
            SetVelo(targetVelocityX, accelerationTimeGrounded);
        else
            SetVelo(targetVelocityX, accelerationTimeAirborn);


        controller.Move(velocity * Time.deltaTime);

    }

    void SetVelo(float target, float accel)
    {
        velocity.x = Mathf.SmoothDamp(velocity.x, target, ref xSmooth, accel);
    }
}
