using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetworks : MonoBehaviour
{
    // Start is called before the first frame update

    

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
        CheckRay(this.transform.forward, this.transform.position, Color.green);
        //Draw ray in behind of the vehicle
        CheckRay(-this.transform.forward, this.transform.position, Color.green);
        
        //Draw ray left to the vehicle
        CheckRay(-this.transform.right, this.transform.position, Color.green);
        //Draw Ray right to the vehicle
        CheckRay(this.transform.right, this.transform.position, Color.green);

        //Vector3 lDir = Quaternion.AngleAxis(transform.rotation.y - 30, transform.forward ) * transform.forward;
        //Vector3 rDir = Quaternion.AngleAxis(transform.rotation.y + 30, transform.forward ) * transform.forward;

        //Draw ray 30 degrees left to the front
        //CheckRay(lDir, this.transform.position);
        //Draw ray 30 degrees right to the front
        //CheckRay(rDir, this.transform.position);

        //TO BE TWEAKED - probably gotta mess with rotation?

        //Draw horizontal ray at front right side of the vehicle
        CheckRay(-this.transform.up, this.transform.position, Color.red);
        //Draw horizontal ray at front left side of the vehicle
        CheckRay(-this.transform.up, this.transform.position, Color.red);
        //Draw horizontal ray at rear right side of the vehicle
        CheckRay(-this.transform.up, this.transform.position, Color.red);
        //Draw horizontal ray at rear right side of the vehicle
        CheckRay(-this.transform.up, this.transform.position, Color.red);
    }

    private void CheckRay(Vector3 direction, Vector3 origin, Color color)
    {
        float distance = 4f; //To be determined - frontal ones will need further distance than horizontals and others
        Debug.DrawLine(origin, origin + (direction * distance), color);
    }
}
