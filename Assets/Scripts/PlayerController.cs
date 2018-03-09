using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
    [SerializeField] float _speed = 5f;
    [SerializeField] float _lookSensitivity = 3f;

    private PlayerMotor _motor;

    private void Start() {
        _motor = GetComponent<PlayerMotor>();
    }

    private void Update() {
        // Calculate movement velocity as a 3D Vector
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        // Final movement vector
        Vector3 velocity = (movHorizontal + movVertical).normalized * _speed;

        // Apply movement
        _motor.Move(velocity);

        // Calculate rotation as a 3D Vector (turning only)
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, yRot, 0f) * _lookSensitivity;

        // Apply rotation
        _motor.Rotate(rotation);

        // Calculate camera rotation as a 3D Vector
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 cameraRotation = new Vector3(xRot, 0f, 0f) * _lookSensitivity;

        // Apply rotation
        _motor.RotateCamera(cameraRotation);
    }
}
