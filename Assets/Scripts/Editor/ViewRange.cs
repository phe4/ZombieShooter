using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyController))]
// [CustomEditor(typeof(ViewRange))]
public class ViewRangeEditor : Editor {
    public void OnSceneGUI() 
    {
        EnemyController vr = (EnemyController)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(vr.transform.position, Vector3.up, Vector3.forward, 360, vr.sightRange);

        Vector3 angle01 = DirectionFromAngle(vr.transform.eulerAngles.y, -vr.angle / 2);
        Vector3 angle02 = DirectionFromAngle(vr.transform.eulerAngles.y, vr.angle / 2);

        Handles.color = Color.black;
        Handles.DrawLine(vr.transform.position, vr.transform.position + angle01 * vr.sightRange);
        Handles.DrawLine(vr.transform.position, vr.transform.position + angle02 * vr.sightRange);

        if (vr.inSight)
        {
            Handles.color = Color.green;
            Handles.DrawLine(vr.transform.position, vr.player.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
