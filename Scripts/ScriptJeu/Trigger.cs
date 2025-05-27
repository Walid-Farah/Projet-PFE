using UnityEngine;

public class Trigger : MonoBehaviour
{
    [HideInInspector] public static bool camera;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit hit;
        //if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out hit,Mathf.Infinity))
        //{

        //    Debug.Log(hit.transform.gameObject.name);
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player" && other.tag!="MainCamera")
        {
            camera = false;
            Debug.Log("Entrer Cube: " + camera.ToString());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player" && other.tag!="MainCamera")
        {
            camera = true;
            Debug.Log("Sortir Cube: " + camera.ToString());
        }
    }
}
