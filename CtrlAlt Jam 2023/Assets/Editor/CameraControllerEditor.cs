using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraController)), CanEditMultipleObjects]
public class CameraControllerEditor : Editor
{
    public SerializedProperty 
        cinemachineVirtualCamera_prop,
        cinemachineConfiner2D_prop,
        cameraType_prop,
        offsetThreshold_Prop,
        offsetRadius_Prop,
        threshold_Prop,
        smoothTime_Prop;

    void OnEnable () 
    {

        // Setup the SerializedProperties
        cinemachineVirtualCamera_prop = serializedObject.FindProperty ("cinemachineVirtualCamera");
        cinemachineConfiner2D_prop = serializedObject.FindProperty ("cinemachineConfiner2D");
        cameraType_prop = serializedObject.FindProperty ("cameraType");
        offsetThreshold_Prop = serializedObject.FindProperty("offsetThreshold");
        offsetRadius_Prop = serializedObject.FindProperty ("offsetRadius");
        threshold_Prop = serializedObject.FindProperty ("threshold");
        smoothTime_Prop = serializedObject.FindProperty ("smoothTime");        
    }

    public override void OnInspectorGUI() 
    {
        serializedObject.Update();
        EditorGUILayout.Space();
        
        EditorGUILayout.PropertyField( cinemachineVirtualCamera_prop, new GUIContent("Cinemachine Reference") );
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField( cinemachineConfiner2D_prop, new GUIContent("Cinemachine Confiner") );
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField( cameraType_prop );
        
        EditorGUILayout.Space();
        CameraController.CameraControllerType ct = (CameraController.CameraControllerType)cameraType_prop.enumValueIndex;
        
        EditorGUILayout.LabelField("Camera Type Settings", EditorStyles.boldLabel);
        
        EditorGUILayout.Space();

        switch( ct ) {
        case CameraController.CameraControllerType.FollowPlayerOffset:            
            EditorGUILayout.PropertyField( offsetThreshold_Prop, new GUIContent("Offset Threshold") );            
            EditorGUILayout.PropertyField ( offsetRadius_Prop, new GUIContent("Offset Radius") );
            EditorGUILayout.PropertyField ( smoothTime_Prop, new GUIContent("Smooth Time") );
            break;

        case CameraController.CameraControllerType.FollowTargetObject:            
            EditorGUILayout.PropertyField ( threshold_Prop, new GUIContent("Threshold") );   
            EditorGUILayout.PropertyField ( smoothTime_Prop, new GUIContent("Smooth Time") );
            break;
        }
        
        
        serializedObject.ApplyModifiedProperties ();
    }
}
