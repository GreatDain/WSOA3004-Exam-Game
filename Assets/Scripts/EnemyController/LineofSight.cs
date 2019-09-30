using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineofSight : MonoBehaviour
{
    [SerializeField]
    public float lineOfSightRadius;

    [SerializeField]
    [Range(0, 360)]
    public float lineOfSightAngle;

    [SerializeField]
    float variableDelay = 0.2f;

    float senseZoneRadius;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets;


    void Start()
    {
        senseZoneRadius = gameObject.GetComponent<SphereCollider>().radius;

        StartCoroutine("findTargets", variableDelay);
    }

    IEnumerator findTargets(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            findVisibleTargets();
        }
    }

    void findVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInSight = Physics.OverlapSphere(transform.position, senseZoneRadius, targetMask);

        for (int i = 0; i < targetsInSight.Length; i++)
        {
            Transform target = targetsInSight[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < lineOfSightAngle / 2)
            {
                float targetDistance = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, targetDistance, obstacleMask))
                {
                    if (targetDistance <= lineOfSightRadius) {

                        gameObject.GetComponent<EnemyAI>().setSeen(true);

                        visibleTargets.Add(target);

                    }

                    else
                    {
                        gameObject.GetComponent<EnemyAI>().setSeen(false);
                    }

                }

                
            }

            else
            {
                gameObject.GetComponent<EnemyAI>().setSeen(false);
            }
        }
        
    }

    public Vector3 directionAngle(float angleInDegrees, bool globalAngle)
    {

        if (globalAngle == false)
        {

            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

    }

}
