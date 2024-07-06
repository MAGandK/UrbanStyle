using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float ViewRadius;
    [Range(0, 360)] public float viewAngle;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private float _delayTime = 0.2f;
    public List<Transform> VisibleTarget;


    private void Start()
    {
        StartCoroutine("FindTarget", _delayTime);
    }

    IEnumerator FindTarget(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTarget();
        }
    }
    public void FindVisibleTarget()
    {
        List<Transform> newVisibleTargets = new List<Transform>();
        Collider[] targetInRadius = Physics.OverlapSphere(transform.position, ViewRadius, _targetMask);
        
        for (int i = 0; i < targetInRadius.Length; i++)
        {
            Transform target = targetInRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position);
            if (Vector3.Angle(transform.forward, dirToTarget)< viewAngle / 2)
            {
                if (Physics.Raycast(transform.position, dirToTarget, _targetMask))
                {
                    newVisibleTargets.Add(target);
                }
            }
        }
        
        for (int i = VisibleTarget.Count - 1; i >= 0; i--)
        {
            if (!newVisibleTargets.Contains(VisibleTarget[i]))
            {
                VisibleTarget.RemoveAt(i);
            }
        }
        
        foreach (Transform target in newVisibleTargets)
        {
            if (!VisibleTarget.Contains(target))
            {
                VisibleTarget.Add(target);
            }
        }
    }

    public Vector3 DirectionFromAngle(float angleDegrees, bool isAngleGlobal)
    {
        if (!isAngleGlobal)
        {
            angleDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleDegrees * Mathf.Deg2Rad));
    }
}
