using UnityEngine;

public class MeshAnimator : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float diameter;
    [SerializeField] private Vector3 rotationAxis;
    [SerializeField] private Transform mesh;
    private float angularSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         angularSpeed = speed * 360 / (Mathf.PI * diameter);
    }

    // Update is called once per frame
    void Update() {
        mesh.Rotate(rotationAxis * angularSpeed * Time.deltaTime, Space.Self);                
    }
}
