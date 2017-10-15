using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : Raycast {

    public LayerMask passengerMask;
    public Vector3 move;
    public bool isFerrisWheel;
    public bool isScreamer;
    float timeCounter;
    float vcircle;
    float w;
    public float rcircle;
    public float cx;
    public float cy;
    public float rotTime;
    public float off_set;

    public float y1;
    public float y2;
    public Vector3 v1;
    public Vector3 v2;
    public bool screamerDirUp;

    public PlatformPos platformpos;
    List<PassengerMovement> passengerMovement;
    Dictionary<Transform, Controller2D> passengerDictionary = new Dictionary<Transform, Controller2D>();

	// Use this for initialization
	public override void Start () {
        base.Start();
        vcircle = 2 * Mathf.PI * rcircle / rotTime;
        w = vcircle / rcircle;
        screamerDirUp = true;

    }
	
	// Update is called once per frame
	void Update () {
        timeCounter += Time.deltaTime;
        UpdateRaycastOrigins();
        platformpos.Reset();
        if (isFerrisWheel)
        {

            Vector3 pos = new Vector3(cx + Mathf.Cos(timeCounter * (vcircle/rcircle)+ (off_set * Mathf.PI)) * rcircle,cy + Mathf.Sin(timeCounter * (vcircle / rcircle)+ (off_set * Mathf.PI)) * rcircle, 0);
            
            platformpos.Pos = pos;

            //Vector3 velocity = new Vector3((platformpos.Pos.x-platformpos.oldPos.x)/Time.deltaTime, (platformpos.Pos.y - platformpos.oldPos.y) / Time.deltaTime, 0);
            Vector3 velocity = new Vector3(-vcircle * Mathf.Sin(w* timeCounter+ (off_set * Mathf.PI)), vcircle * Mathf.Cos(w * timeCounter+ (off_set * Mathf.PI)), 0);
            
            CalculatePassengerMovement(velocity * Time.deltaTime);
            MovePassengers(true);
            transform.position = pos;
            //transform.Translate((velocity * Time.deltaTime), Space.World);
            MovePassengers(false);
        } else if(isScreamer)
        {
            Vector3 velocity;
            if (transform.position.y < y1 && !screamerDirUp)
            {
                screamerDirUp = true;

            } else if (transform.position.y > y2 && screamerDirUp)
            {
                screamerDirUp = false;
            }
            if (screamerDirUp)
            {
                velocity = v1 * Time.deltaTime;
            } else
            {
                velocity = v2 * Time.deltaTime;
            }
            CalculatePassengerMovement(velocity);
            MovePassengers(true);
            transform.Translate(velocity);
            MovePassengers(false);
        }
        else
        {
            Vector3 velocity = move * Time.deltaTime;
            CalculatePassengerMovement(velocity);
            MovePassengers(true);
            transform.Translate(velocity);
            MovePassengers(false);
        }
	}

    void MovePassengers(bool beforeMovementPlatform)
    {
        foreach (PassengerMovement passenger in passengerMovement)
        {
            if (!passengerDictionary.ContainsKey(passenger.transform))
            {
                passengerDictionary.Add(passenger.transform, passenger.transform.GetComponent<Controller2D>());
            }
            if(passenger.moveBeforePlatform == beforeMovementPlatform)
            {
                passengerDictionary[passenger.transform].Move(passenger.velocity,passenger.passenger);
            }
        }
    }

    void CalculatePassengerMovement(Vector3 velocity)
    {
        HashSet<Transform> movedPassengers = new HashSet<Transform>();
        passengerMovement = new List<PassengerMovement>();

        float directionX = Mathf.Sign(velocity.x);
        float directionY = Mathf.Sign(velocity.y);

        //vertical moving platform

        if (velocity.y != 0)
        {
            float rayLength = Mathf.Abs(velocity.y) + skinwidth;

            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
                rayOrigin += Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);

                if (hit)
                {
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        movedPassengers.Add(hit.transform);
                        float pushX = (directionY == 1) ? velocity.x : 0;
                        float pushY = velocity.y - (hit.distance - skinwidth) * directionY;

                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), directionY == 1, true));
;                    }
                }
            }
        }
        if (velocity.x != 0)
        {
            float rayLength = Mathf.Abs(velocity.x) + skinwidth;

            for (int i = 0; i < horizontalRayCount; i++)
            {
                Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask);
                if (hit)
                {
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        movedPassengers.Add(hit.transform);
                        float pushX = velocity.x - (hit.distance - skinwidth) * directionX;
                        float pushY = -skinwidth;

                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), false, true));
                    }
                }
            }
        }
        if(directionY==-1||velocity.y==0 && velocity.x != 0)
        {
                float rayLength = 2* skinwidth;

                for (int i = 0; i < verticalRayCount; i++)
                {
                    Vector2 rayOrigin = raycastOrigins.topLeft;
                    rayOrigin += Vector2.right * (verticalRaySpacing * i);
                    RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);

                    if (hit)
                    {
                        if (!movedPassengers.Contains(hit.transform))
                        {
                            movedPassengers.Add(hit.transform);
                            float pushX = velocity.x;
                            float pushY;
                            if (velocity.y > -15 && isScreamer)
                            {
                                pushY = 0;
                            }
                            else
                            {
                                pushY = velocity.y;
                            }



                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), true, false));
                    }
                    }
                }
        }
    }
    struct PassengerMovement
    {
        public Transform transform;
        public Vector3 velocity;
        public bool passenger;
        public bool moveBeforePlatform;

        public PassengerMovement(Transform _transform, Vector3 _velocity, bool _passenger, bool _moveBeforePlatform)
        {
            transform = _transform;
            velocity = _velocity;
            passenger = _passenger;
            moveBeforePlatform = _moveBeforePlatform;
        }
    }
    public struct PlatformPos
    {
        public Vector3 oldPos;
        public Vector3 Pos;

        public void Reset()
        {
            oldPos = Pos;
            Pos = Vector3.zero;
        }

    }
}
