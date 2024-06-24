using UnityEngine;
public class CharacterNavigationController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _stopDistance;
    [SerializeField] private float _rotationSpeed;

    private Vector3 _destination;
    [SerializeField] private string _name;
     public bool _IsReachedDestination;
     private void Start()
     {
         _destination = transform.position;
         
         _IsReachedDestination = true;
     }
    private void Update()
    {
        if (transform.position != _destination)
        {
            Vector3 destinationDirection = _destination - transform.position;
            destinationDirection.y = 0;
        
            float destinationDistance = destinationDirection.magnitude;
        
            if (destinationDistance >= _stopDistance)
            {
                _IsReachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,
                    _rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            }
            else
            {
                _IsReachedDestination = true;
            }
        }
    }

    public void SetDestination(Vector3 pointPosition, string pointName)
    {
        _destination = pointPosition;
        _name = pointName;
        _IsReachedDestination = false;
    }
}
