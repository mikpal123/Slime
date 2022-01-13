using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    [SerializeField] private float boundX;
    [SerializeField] private float boundY;


    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        //Check if we're inside box on X axis
        float deltaX = lookAt.position.x - transform.position.x;
        if(transform.position.x > lookAt.position.x)
        {
            if(transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }
        //Check if we're inside box on Y axis
        float deltaY = lookAt.position.y - transform.position.y;
        if (transform.position.y > lookAt.position.y)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
