using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class VehicleBehavior : MonoBehaviour
{
    [SerializeField] float turnSpeed = 5;
    [SerializeField] float acceleration = 8;

    Quaternion targetRotation;
    Rigidbody vehicle;
    float drag;

    protected Joystick joystick;
    public GameObject joy;
    float joyH;
    float joyV;

    public int levelType;

    //Ground detection
    RaycastHit hit;
    float distance = 10f;
    Vector3 dir = -Vector3.up;
    Vector3 offset = new Vector3(0, 0.3f, 0f);
    bool isOffroad = false;

    //Test
    Vector3 lastPosition;
    float totalDistance; 
    private void Start()
    {
        levelType = VehicleSpawner.levelType;
        joystick = FindObjectOfType<Joystick>();
        vehicle = GetComponent<Rigidbody>();
        joy = GameObject.Find("Fixed Joystick");
        joy.SetActive(true);
        drag = vehicle.drag;

        lastPosition = transform.position;
    }

    //TRACK LENGTHS:
    //13 - ~700 units
    //9 - ~700 units
    //7 - ~260 units
    //4 - ~170 units

    void GetJoystickPosition()
    {
        joyH = joystick.Horizontal;
        joyV = joystick.Vertical;
        //Debug.Log("Horizontal: " + joyH);
        //Debug.Log("Vertical: " + joyV);
    }

    void Update()
    {
        SetRotationPoint();

    }

    void FixedUpdate()
    {
        GetJoystickPosition();
        
        CheckForGround();

        GetAccelerationStrenght();

        SetRotation();

        //For measuring parameters only
        
        //totalDistance += Vector3.Distance(transform.position, lastPosition);
        //lastPosition = transform.position;
        //Debug.Log("Total distance travelled: " + totalDistance);
    }

    public void GetAccelerationStrenght()
    {
        //Vector2 joyPos = new Vector2(joyH, joyV);
        float distance = Mathf.Sqrt(Mathf.Pow(joyH, 2f) + Mathf.Pow(joyV, 2f));
        float accelerationInput = acceleration * distance * 0.02f;
        vehicle.AddRelativeForce(Vector3.forward * accelerationInput);
    }

    private void SetRotationPoint()
    {
        
        if (joyH!=0 || joyV!=0)
        {
            Vector3 target = new Vector3(joyH*1000f, 0, joyV*1000f); //1000f = scaling multiplicator 
            //Debug.Log("Target: " + target);
            Vector3 direction = target - transform.position;
            //Debug.Log("Direction: " + direction);
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            //Debug.Log("RotationAngle: " + rotationAngle);
            targetRotation = Quaternion.Euler(0, rotationAngle, 0);
            //Debug.Log("TargetRotation: " + targetRotation);
        }

    }

    private void SetRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * turnSpeed);
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Checkpoint")
        {
            Debug.Log("Level type: " + levelType);
            if (levelType == 1)
            {
                
                CheckpointLevelLogic.CheckpointLevelBehavior(hit.name);
            }
            else // if (levelType == 2)
            {
                RaceLevelLogic.RaceLevelBehavior(hit.name); //Commented for AI testing purposes

            }
        } 
              
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacle") 
        {
            CheckpointLevelLogic.LevelFailed();
            joy.SetActive(false);
            Debug.Log("DEBUG: HIT OBSTACLE");
        }
    }

    void CheckForGround()
    {
        if (Physics.Raycast(transform.position+offset, dir, out hit, distance))
        {
            if (hit.collider.tag == "Road")
            {
                if (isOffroad)
                {
                    vehicle.drag = drag;
                    isOffroad = false;
                    //Debug.Log("Veh is on road");
                }
            }
            else
            { 
                if (!isOffroad)
                {
                    vehicle.drag *= 2f;
                    isOffroad = true;
                    //Debug.Log("Veh is offroad");
                }
            }
        }
    }

}