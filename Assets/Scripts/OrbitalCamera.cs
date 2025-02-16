using UnityEngine;

public class OrbitalCamera : MonoBehaviour
{
    [SerializeField] private float _yawDistance = 10f;
    [SerializeField] private float _pitchDistance = 10f;

    [SerializeField] private float speed;

    private float _yaw = 0f;
    private float _pitch = 1f;

    private Transform _cameraTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float input_x = Input.GetAxis("Horizontal");
        float input_y = Input.GetAxis("Vertical");

        _yaw += input_x * Time.deltaTime * speed;
        _pitch += input_y * Time.deltaTime * speed;

        // Magic Numbers scelti a mano con dei Debug.Log
        _pitch = Mathf.Clamp(_pitch, 0.5f, 1.15f);

        Vector3 newCameraPosition = Vector3.zero;
        newCameraPosition.x = transform.position.x + _yawDistance * Mathf.Cos(_pitch) * Mathf.Cos(_yaw);
        newCameraPosition.y = transform.position.y + _pitchDistance * Mathf.Sin(_pitch);
        newCameraPosition.z = transform.position.z + _yawDistance * Mathf.Cos(_pitch) * Mathf.Sin(_yaw);
        
        _cameraTransform.position = newCameraPosition;
        _cameraTransform.LookAt(transform.position);
    }
    
}
