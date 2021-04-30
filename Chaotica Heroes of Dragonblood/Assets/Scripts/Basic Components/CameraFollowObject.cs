using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [SerializeField]
    private Transform objectToFollow;
    [SerializeField]
    private Vector3 positionOffset;
    [SerializeField]
    private bool delaySwitch;
    [SerializeField]
    private float speedOffset;

    private Vector3 cameraTarget;

    private void Awake() 
    {
 
    }
    void Update()
    {
        CameraMovement();
    }

    void CameraMovement()
    {
        cameraTarget = new Vector3 (objectToFollow.position.x + positionOffset.x, objectToFollow.position.y + positionOffset.y,-10f);
        if(!delaySwitch)
        {
            transform.position = cameraTarget;
        }
        else
        {
            if(objectToFollow.localScale.x == -1f)
            {
                if(cameraTarget.x - positionOffset.x * 2 < transform.position.x)
                {
                    transform.position += new Vector3(-speedOffset*Time.deltaTime,0f,0f); 
                }
            }
            else if(objectToFollow.localScale.x == 1f)
            {
                if(cameraTarget.x +  positionOffset.x  > transform.position.x)
                {
                    transform.position += new Vector3(speedOffset*Time.deltaTime,0f,0f); 
                }
            }

            transform.position = new Vector3(transform.position.x, cameraTarget.y,transform.position.z);
        }


    }
}
