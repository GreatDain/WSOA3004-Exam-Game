using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 smooth;
    public float sensitivity = 5f;
    public float smoothing = 2f;

    public GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Basic Rotation of Camera and Player gameobject to look around.

        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        smooth.x = Mathf.Lerp(smooth.x, md.x, 1f / smoothing);
        smooth.y = Mathf.Lerp(smooth.y, md.y, 1f / smoothing);
        mouseLook += smooth;
        //Debug.Log(mouseLook.y);

        //***Clamps rotation of camera look in all axes. 

        if (-mouseLook.y < 45f && -mouseLook.y > -45f)
        {
            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        }

        //if (mouseLook.x <= 110f && mouseLook.x >= -110f)
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

    }
}
