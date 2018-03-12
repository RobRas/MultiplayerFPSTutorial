using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
    [SerializeField] float _speed = 5f;
    [SerializeField] float _lookSensitivity = 3f;

    [SerializeField] float _thrusterForce = 1300f;

    [Header("Spring Settings:")]
    [SerializeField] float _jointSpring = 20f;
    [SerializeField] float _jointMaxForce = 40f;

    private PlayerMotor _motor;
    private ConfigurableJoint _joint;

    private void Start() {
        _motor = GetComponent<PlayerMotor>();
        _joint = GetComponent<ConfigurableJoint>();

        SetJointSettings(_jointSpring);
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

        float cameraRotationX = xRot * _lookSensitivity;

        // Apply rotation
        _motor.RotateCamera(cameraRotationX);

        // Apply thruster force
        Vector3 thrusterForce = Vector3.zero;

        if (Input.GetButton("Jump")) {
            thrusterForce = Vector3.up * _thrusterForce;
            SetJointSettings(0f);
        } else {
            SetJointSettings(_jointSpring);
        }

        _motor.ApplyThruster(thrusterForce);
    }

    private void SetJointSettings(float jointSpring) {
        _joint.yDrive = new JointDrive {
            positionSpring = jointSpring,
            maximumForce = _jointMaxForce
        };
    }
}
