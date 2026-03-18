using UnityEditor;

namespace YTools
{
    [CanEditMultipleObjects, CustomEditor(typeof(PlayerControllerData))]
    public class PlayerControllerDataEditor : Editor
    {
        private SerializedProperty _cameraType;
        private SerializedProperty _speedRotationModel;

        private SerializedProperty _speed;
        private SerializedProperty _minSpeed;
        private SerializedProperty _acceleration;
        private SerializedProperty _jumpHeight;
        
        private SerializedProperty _isEnableAcceleration;
        private SerializedProperty _pressingJump;
        
        private SerializedProperty _sensitivity;
        private SerializedProperty _sensivityRatio;
        private SerializedProperty _rotateMinMax;
        
        private SerializedProperty _ground;
        private SerializedProperty _attachable;
        private SerializedProperty _visibleGizmos;
        private SerializedProperty _radius;
        private SerializedProperty _distance;
        private SerializedProperty _gravity;

        private void OnEnable()
        {
            _cameraType = serializedObject.FindProperty("CameraType");
            _speedRotationModel = serializedObject.FindProperty("SpeedRotationModel");
            _speed = serializedObject.FindProperty("Speed");
            _minSpeed = serializedObject.FindProperty("MinSpeed");
            _acceleration = serializedObject.FindProperty("Acceleration");
            _jumpHeight = serializedObject.FindProperty("JumpHeight");
            _isEnableAcceleration = serializedObject.FindProperty("IsEnableAcceleration");
            _pressingJump = serializedObject.FindProperty("PressingJump");
            _sensitivity = serializedObject.FindProperty("Sensitivity");
            _sensivityRatio = serializedObject.FindProperty("SensivityRatio");
            _rotateMinMax = serializedObject.FindProperty("RotateMinMax");
            _ground = serializedObject.FindProperty("Ground");
            _attachable = serializedObject.FindProperty("Attachable");
            _visibleGizmos = serializedObject.FindProperty("VisibleGizmos");
            _radius = serializedObject.FindProperty("Radius");
            _distance = serializedObject.FindProperty("Distance");
            _gravity = serializedObject.FindProperty("Gravity");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("CameraType", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_cameraType);
            if(_cameraType.enumValueIndex == (int)CameraType.TPS)
                EditorGUILayout.PropertyField(_speedRotationModel);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Stats", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_speed);
            EditorGUILayout.PropertyField(_minSpeed);
            EditorGUILayout.PropertyField(_acceleration);
            EditorGUILayout.PropertyField(_jumpHeight);

            EditorGUILayout.Space(10);
            EditorGUILayout.PropertyField(_isEnableAcceleration);
            EditorGUILayout.PropertyField(_pressingJump);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Mouse", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_sensitivity);
            EditorGUILayout.PropertyField(_sensivityRatio);
            EditorGUILayout.PropertyField(_rotateMinMax);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Physics", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_ground);
            EditorGUILayout.PropertyField(_attachable);
            EditorGUILayout.PropertyField(_visibleGizmos);
            EditorGUILayout.PropertyField(_radius);
            EditorGUILayout.PropertyField(_distance);
            EditorGUILayout.PropertyField(_gravity);

            serializedObject.ApplyModifiedProperties();
        }
    }
}