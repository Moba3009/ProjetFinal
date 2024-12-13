using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    
    [SerializeField] private float _radius;
    [SerializeField] private float _yOffset = 1.5f;
    [SerializeField] private float _speed;
    [SerializeField] private float _explosionDelay = 1.5f;
    [SerializeField] private int _damage;
    [SerializeField] private GameObject _explosionVFX;

    private Transform _target;
    private Rigidbody _rigidbody;
    private bool _hasExploded;

    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = ((_target.position + new Vector3(0, _yOffset, 0)) - transform.position).normalized;

        if (!_hasExploded)
        {
            _rigidbody.velocity = direction * _speed;
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HealthAndDefense>() != null && !_hasExploded)
        {
            Explosion();
            Debug.Log("Explosion DOne");
        }
    }

    private void Explosion()
    {
        transform.localScale = Vector3.one * _radius;
        _explosionVFX.SetActive(true);
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, _radius);
        foreach (Collider collider in hitCollider)
        {
            HealthAndDefense health = collider.GetComponent<HealthAndDefense>();
            if (health != null)
            {
                health.RecieveDamage(_damage);
            }
        }

        _hasExploded = true;
        _rigidbody.velocity = Vector3.zero;
        Destroy (gameObject, _explosionDelay);

    }
}
