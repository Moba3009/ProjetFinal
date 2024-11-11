using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _stopingdistance = 0.75f;
    [SerializeField] private float _attackCoolDown = 1.5f;
    [SerializeField] private int _damage = 5;

    private Animator _animator;
    private Camera _camera;
    private Rigidbody _rigidbody;

    private
        Vector3 _targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray;
            RaycastHit hit;
            ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                _targetPosition = hit.point;
                transform.LookAt(_targetPosition);
            }
        }

        float distance = (transform.position - _targetPosition).magnitude;

        if (distance > _stopingdistance)
        {
            Vector3 direction = (_targetPosition - transform.position).normalized;
            _rigidbody.velocity = _movementSpeed * direction;
            _animator.SetBool("IsWalking", true);

        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
            _animator.SetBool("IsWalking", false);
        }
        
    }
}
