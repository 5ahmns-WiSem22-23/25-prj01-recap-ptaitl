using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ZoneController))]
public class ZoneControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // The cast target can be used to access the variables and methods of the script.
        ZoneController zoneController = (ZoneController)target;

        // Set dirty, so that changes in the prefab appear as override.
        EditorUtility.SetDirty(zoneController);

        // Make enum visible.
        zoneController.zoneTyp = (ZoneController.ZoneTypes)EditorGUILayout.EnumPopup("Zone Typ", zoneController.zoneTyp);

        // dDpending on which option is selected in the enum, a different ui appears.
        if (zoneController.zoneTyp == ZoneController.ZoneTypes.SlowDown)
        {
            zoneController.slowDownRate = EditorGUILayout.Slider("SlowDown Rate", zoneController.slowDownRate, 0, 5);
        }
        else if (zoneController.zoneTyp == ZoneController.ZoneTypes.SpeedUp)
        {
            zoneController.speedUpRate = EditorGUILayout.Slider("SpeedUp Rate", zoneController.speedUpRate, 0, 5);
        }
        else if (zoneController.zoneTyp == ZoneController.ZoneTypes.Kill)
        {
            zoneController.healthDeduction = EditorGUILayout.IntSlider("Health Deduction", zoneController.healthDeduction, 0, 10);
        }
    }
}
