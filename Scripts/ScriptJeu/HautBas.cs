using UnityEngine;

public class HautBas : MonoBehaviour
{
    private Vector3 _offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 _currentVelocity = Vector3.zero;
    float vertical;
    float mouvementH;



    private void Awake() => _offset = transform.position - target.position;

    private void LateUpdate()
    {

        Vector3 targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);

        vertical -= Input.GetAxis("Mouse Y");
        vertical=Mathf.Clamp(vertical, -45f, 45f);
        transform.rotation= Quaternion.Euler(vertical, 0, 0);

        mouvementH += Input.GetAxis("Mouse X") * 5f;
        transform.rotation = Quaternion.Euler(0f, mouvementH, 0f);


    }
}