using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private  float smoothSpeed = 0.125f;
    [SerializeField] private  Vector3 offset = new Vector3(0, 0, -10f);
    
    private Vector3 targetPosition;
    void Start()
    {
        targetPosition = targetObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = targetObject.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
