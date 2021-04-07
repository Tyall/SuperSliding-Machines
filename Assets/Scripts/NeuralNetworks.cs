using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetworks : MonoBehaviour
{
    // Start is called before the first frame update

    //Raycast ground detection
    RaycastHit hit;
    Vector3 offset = new Vector3(0f, 0f, 0f);

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DrawRays();
    }

    private void DrawRays()
    {
        Vector3 pos = transform.position;
        //Draw ray in front of the vehicle
        CheckRay(this.transform.forward, this.transform.position, Color.green, 4f);
        //Draw ray in behind of the vehicle
        CheckRay(-this.transform.forward, this.transform.position, Color.green, 3f);
        
        //Draw ray left to the vehicle
        CheckRay(-this.transform.right, this.transform.position, Color.green, 3f);
        //Draw Ray right to the vehicle
        CheckRay(this.transform.right, this.transform.position, Color.green, 3f);

        Vector3 lDir30 = Quaternion.AngleAxis(30f, Vector3.up ) * transform.forward;
        Vector3 rDir30 = Quaternion.AngleAxis(-30f, Vector3.up ) * transform.forward;
        Vector3 lDir60 = Quaternion.AngleAxis(60f, Vector3.up) * transform.forward;
        Vector3 rDir60 = Quaternion.AngleAxis(-60f, Vector3.up) * transform.forward;

        //Draw ray 30 degrees left to the front
        CheckRay(lDir30, this.transform.position, Color.red, 3f);
        //Draw ray 30 degrees right to the front
        CheckRay(rDir30, this.transform.position, Color.red, 3f);
        //Draw ray 60 degrees left to the front
        CheckRay(lDir60, this.transform.position, Color.red, 2f);
        //Draw ray 60 degrees right to the front
        CheckRay(rDir60, this.transform.position, Color.red, 2f);

    }

    private void CheckRay(Vector3 direction, Vector3 origin, Color color, float distance)
    {
        //Debug.DrawLine(origin, origin + (direction * distance), color);

        //Set color depending on what's on the end of ray
        //Draw a vertical rays from the end of existing rays
        //Connect rays in pairs? Get inputs from both ground and 
        //enemy so it'll try to pick an optimal and clean route at once

        Debug.DrawLine(origin, origin + (direction * distance), Color.green);

        if (Physics.Raycast(transform.position + offset, direction, out hit, distance))
        {
            if (hit.collider.tag == "Road") //or enemy
            {
                Debug.DrawLine(origin, origin + (direction * distance), Color.blue);
                //The problem is it always draws Red if something is hit
            }
            else
            {
                Debug.DrawLine(origin, origin + (direction * distance), Color.red);
            }
        }
    }
}
