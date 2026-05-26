using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = player.position.x;
        transform.position = newPosition;
    }
}
