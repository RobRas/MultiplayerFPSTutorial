using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {
    [SerializeField] Camera _camera;
    [SerializeField] float _cameraRotationLimit = 85f;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 _rotation = Vector3.zero;
    private float _cameraRotationX = 0;
    private Vector3 _thrusterForce = Vector3.zero;
    private float _currentCameraRotationX = 0f;

    private Rigidbody _rb;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        PerformMovement();
        PerformRotation();
    }

    public void Move(Vector3 velocity) {
        _velocity = velocity;
    }

    public void Rotate(Vector3 rotation) {
        _rotation = rotation;
    }

    public void RotateCamera(float rotation) {
        _cameraRotationX = rotation;
    }

    public void ApplyThruster(Vector3 thrusterForce) {
        _thrusterForce = thrusterForce;
    }

    private void PerformMovement() {
        if (_velocity != Vector3.zero) {
            _rb.MovePosition(_rb.position + _velocity * Time.fixedDeltaTime);
        }
        if (_thrusterForce != Vector3.zero) {
            _rb.AddForce(_thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    private void PerformRotation() {
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(_rotation));
        if (_camera != null) {
            _currentCameraRotationX -= _cameraRotationX;
            _currentCameraRotationX = Mathf.Clamp(_currentCameraRotationX, -_cameraRotationLimit, _cameraRotationLimit);

            _camera.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0f, 0f);
        }
    }
}
