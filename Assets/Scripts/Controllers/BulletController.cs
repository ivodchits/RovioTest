using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] GameObject _gameObject;
    [SerializeField] Transform _transform;
    [SerializeField] Rigidbody2D _rigidbody;

    public GameObject GameObject => _gameObject;

    public void Shoot(Vector3 position, Quaternion rotation, float speed)
    {
        _transform.position = position;
        _transform.rotation = rotation;
        _gameObject.SetActive(true);
        _rigidbody.velocity = -_transform.right * speed;
    }

    void OnCollisionEnter2D(Collision2D _)
    {
        _gameObject.SetActive(false);
    }
}