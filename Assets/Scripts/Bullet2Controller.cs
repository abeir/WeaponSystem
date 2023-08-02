using Character;
using UnityEngine;
using WeaponSystem.Controller;

public class Bullet2Controller : BulletController
{
    [SerializeField]
    private float initialSpeed = 5f;
    [SerializeField]
    private float rotationSpeed = 10f;
    [SerializeField]
    private float accelerationTime = 2f;
    

    private Transform _target;
    private TrailRenderer _trail;

    private float _acceleration;
    private float _currentSpeed;
    private Vector3 _currentDirection;
    
    private void Awake()
    {
        _trail = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        var targets = GameObject.FindGameObjectsWithTag("Enemy");
        if (targets != null)
        {
            var index = Random.Range(0, targets.Length);
            _target = targets[index].transform;
        }

        _acceleration = (speed - initialSpeed) / accelerationTime;
        _currentSpeed = initialSpeed;
        _currentDirection = direction;
    }

    private void OnDisable()
    {
        _trail.Clear();
    }


    private void Update()
    {
        var deltaTime = Time.deltaTime;
        if (_target == null || !_target.gameObject.gameObject)
        {
            transform.position += _currentDirection * (_currentSpeed * deltaTime);
            return;
        }
        
        var angleOffset = (_target.position - transform.position).normalized;
        var angle = Vector3.Angle(_currentDirection, angleOffset);
        if (angle < 0.001f)
        {
            _currentDirection = angleOffset;
        }
        else
        {
            var rotateTime = angle / (rotationSpeed * (_currentSpeed / speed));
            _currentDirection = Vector3.Slerp(_currentDirection, angleOffset, deltaTime / rotateTime).normalized;
        }
        
        if (_currentSpeed < speed)
        {
            _currentSpeed += deltaTime * _acceleration;
        }
        transform.position += _currentDirection * (_currentSpeed * deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy"))
        {
            return;
        }

        var enemy = other.GetComponent<BaseCharacter>();
        enemy.OnBulletHit(Weapon);
        
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        
    }
}
