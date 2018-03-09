using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {
    [SerializeField] Camera _camera;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 _rotation = Vector3.zero;
    private Vector3 _cameraRotation = Vector3.zero;

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

    public void RotateCamera(Vector3 rotation) {
        _cameraRotation = rotation;
    }

    private void PerformMovement() {
        if (_velocity != Vector3.zero) {
            _rb.MovePosition(_rb.position + _velocity * Time.fixedDeltaTime);
        }
    }

    private void PerformRotation() {
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(_rotation));
        if (_camera != null) {
            _camera.transform.Rotate(-_cameraRotation);
        }
    }
}
