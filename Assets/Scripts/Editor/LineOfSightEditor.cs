using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LineofSight))]
public class LineOfSightEditor : Editor
{
     void OnSceneGUI()
    {
        LineofSight los = (LineofSight)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(los.transform.position, Vector3.up, Vector3.forward, 360, los.lineOfSightRadius);

        Vector3 sightAngle1 = los.directionAngle(-los.lineOfSightAngle / 2, false);
        Vector3 sightAngle2 = los.directionAngle(los.lineOfSightAngle / 2, false);

        Handles.DrawLine(los.transform.position, los.transform.position + sightAngle1 * los.lineOfSightRadius);
        Handles.DrawLine(los.transform.position, los.transform.position + sightAngle2 * los.lineOfSightRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in los.visibleTargets) {

            Handles.DrawLine(los.transform.position, visibleTarget.position);

        }
    }
}
