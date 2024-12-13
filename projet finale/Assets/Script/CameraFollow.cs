using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _player;
    public Vector3 _offset;
    public Vector2 _minBounds, _maxBounds;

    void LateUpdate()
    {
        Vector3 desiredPosition = _player.position + _offset;
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, _minBounds.x, _maxBounds.x);
        desiredPosition.z = Mathf.Clamp(desiredPosition.z, _minBounds.y, _maxBounds.y);
        transform.position = desiredPosition;
    }
}
