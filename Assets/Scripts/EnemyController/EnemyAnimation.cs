using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Animator walkCycle;
    // Start is called before the first frame update
    void Start()
    {
        walkCycle.SetBool("isEnemyWalk", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
