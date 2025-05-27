using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float smooth_speed = 0.125f;
    public Vector3 offset;
    public Transform followTarget;
    float rotationX, rotationY;
    float garderValeur;

    GameObject personnage;


    void Start()
    {
        offset = followTarget.position - transform.position;
        garderValeur = offset.z;

        personnage = GameObject.Find("Ch17_nonPBR");
    }
    public void LateUpdate()
    {
        
        if (!(PauseMenu.gamePaused || Input.GetKey(KeyCode.LeftAlt)))
        {
            rotationX += Input.GetAxis("Mouse Y");
            rotationY += Input.GetAxis("Mouse X") * 5f;

            rotationX = Mathf.Clamp(rotationX, -30f, 30f);

            var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

            transform.position = followTarget.position - targetRotation * offset;
            transform.rotation = (targetRotation);


            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, offset.z))
            {
                if (hit.collider.tag != "MainCamera" && hit.collider.tag != "Player" && hit.collider.tag != "Trigger")
                {
                    offset.z -= hit.distance;
                    Debug.Log("objet Toucher :" + hit.collider.name);
                }
            }


            if (Trigger.camera)
            {
                offset.z = garderValeur;
            }
        }
    }



}


