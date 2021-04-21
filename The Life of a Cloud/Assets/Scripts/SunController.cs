using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rotates the sun across the sky
public class SunController : MonoBehaviour
{
    public float dayLength;
    private float rotationSpeed;
    public GameObject rotatePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationSpeed = Time.deltaTime / dayLength;
        transform.RotateAround(rotatePoint.transform.position, Vector3.back, rotationSpeed);
        Quaternion x = this.transform.rotation; //z rotation is set to 0 to prevent spinning
        x.z = 0;
        this.transform.rotation = x;
    }
}
