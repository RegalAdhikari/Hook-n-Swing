using UnityEngine;

public class WaterScript : MonoBehaviour
{

    [SerializeField] private float yOffset;
    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x<=-1f)
        {
            transform.position = new Vector3(0,transform.position.y,transform.position.z);
        }
        transform.position -= new Vector3(xSpeed,0,0);
    }
}
