using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float jumpHeight = 4;
    public float timetoJumpApex = 0.4f;

    public bool isShadow;

    public float moveSpeed = 6;
	public float gravity;
	public float jumpVelocity;

	public float accelerationTimeAirborn=0.2f;
	public float accelerationTimeGrounded=0.1f;
    float xSmooth;

    public GameObject shadow;
    public GameObject timerObj;

    private CountdownTimer timer;

    Vector3 velocity;

    Controller2D controller;   

	// Use this for initialization
	void Start () {

        timer = timerObj.GetComponent<CountdownTimer>();

        controller = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timetoJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity * timetoJumpApex);
        print("Gravity: " + gravity + "  JumpSpeed: " + jumpHeight);

        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isShadow && other.gameObject == shadow)
        {
            timer.gameOverText.text = "Person Wins!";
            timer.timerEnded();
        }
    }

    // Update is called once per frame
    void Update () {

        Vector2 input;

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

        if (Input.GetKeyDown(KeyCode.UpArrow)&& controller.collisions.below && isShadow)
        {
            velocity.y = jumpVelocity;
        
        } else if (Input.GetKeyDown(KeyCode.W) && controller.collisions.below && !isShadow)
        {
            velocity.y = jumpVelocity;

        }

        float targetVelocityX = input.x * moveSpeed;

        if (!controller.collisions.below && velocity.y < -15)
        {
            velocity.y = -15;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref xSmooth, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborn);
        controller.Move(velocity * Time.deltaTime);

    }
}
