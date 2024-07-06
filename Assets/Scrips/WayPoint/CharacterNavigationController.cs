using UnityEngine;
using UnityEngine.AI;

public class CharacterNavigationController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _stopDistance;
    [SerializeField] private float _rotationSpeed;

    private Vector3 _destination;
    [SerializeField] private string _name;

    //private CharacterController _characterController;
    private NavMeshAgent _agent;
    
     public bool _IsReachedDestination;
     private void Start()
     {
         _destination = transform.position;

        // _characterController = GetComponent<CharacterController>();
        _agent = GetComponent<NavMeshAgent>();
        
        _agent.speed = _speed;
        _agent.stoppingDistance = _stopDistance;
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
                 // Vector3 movement = destinationDirection.normalized * (_speed * Time.deltaTime);
                 // movement.y = -1f;
                 // _characterController.Move(movement);
                 _agent.SetDestination(_destination);
             }
             else
             {
                 _IsReachedDestination = true;
                 _agent.ResetPath();
             }
         }
     }


     public void SetDestination(Vector3 pointPosition, string pointName)
    {
        _destination = new Vector3(pointPosition.x, transform.position.y, pointPosition.z);
        _name = pointName;
        _IsReachedDestination = false;
    }
}
