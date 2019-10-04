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

        Vector3 shootAngle1 = los.directionAngle(-los.shootAngle / 2, false);
        Vector3 shootAngle2 = los.directionAngle(los.shootAngle/ 2, false);

        Handles.DrawLine(los.transform.position, los.transform.position + sightAngle1 * los.lineOfSightRadius);
        Handles.DrawLine(los.transform.position, los.transform.position + sightAngle2 * los.lineOfSightRadius);

        Handles.color = Color.yellow;
        foreach (Transform visibleTarget in los.visibleTargets) {

            Handles.DrawLine(los.transform.position, visibleTarget.position);

        }

        Handles.color = Color.blue;
        Handles.DrawWireArc(los.transform.position, Vector3.up, Vector3.forward, 360, los.shootRange);

        Handles.DrawLine(los.transform.position, los.transform.position + shootAngle1 * los.shootRange);
        Handles.DrawLine(los.transform.position, los.transform.position + shootAngle2 * los.shootRange);

        Handles.color = Color.red;
        foreach (Transform inRangeTarget in los.inRangeTargets)
        {

            Handles.DrawLine(los.transform.position, inRangeTarget.position);

        }
    }
}
