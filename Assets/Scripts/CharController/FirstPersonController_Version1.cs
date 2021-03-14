using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirstPersonController_Version1 : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationX = 0F;
    float rotationY = 0F;
    private Quaternion startRotation;


    void Update()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            //Gets rotational input from the mouse
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;

            //Clamp the rotation average to be within a specific value range
            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            //Get the rotation you will be at next as a Quaternion
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);

            //Rotate
            //Can also apply to Rb.rotation = targetRot;
            transform.localRotation = startRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = startRotation * xQuaternion;
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
            transform.localRotation = startRotation * yQuaternion;
        }
    }

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
            rb.freezeRotation = true;
        startRotation = transform.localRotation;
    }
    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        return Mathf.Clamp(angle, min, max);
    }
}