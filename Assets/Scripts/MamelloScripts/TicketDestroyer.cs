using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketDestroyer : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Invoke("Destroy", 5f);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
