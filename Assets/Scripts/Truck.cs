using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class Truck : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent navigator;

    [SerializeField]
    List<Waypoint> patrolPath;

    [SerializeField]
    float moveSpeed = 30f;

    [SerializeField]
    float waitTime;

    bool changing = false;

    public int currentPath = 0;
    // Start is called before the first frame update
    void Start()
    {
        navigator.speed = moveSpeed;

        navigator.Warp(gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        navigator.SetDestination(patrolPath[currentPath].transform.position);

        if (navigator.remainingDistance == 0 && changing == false) {

            StartCoroutine(callDelay());

        }
    }

    IEnumerator callDelay()// coroutine for change the next waypoiint in patrol
    {

        changing = true;

        if (changing == true)
        {
            if (currentPath == 0) {

                yield return new WaitForSeconds(waitTime);

            }

            currentPath = (currentPath + 1) % patrolPath.Count;
            yield return new WaitForSeconds(3f);
        }

        changing = false;
    }
}
