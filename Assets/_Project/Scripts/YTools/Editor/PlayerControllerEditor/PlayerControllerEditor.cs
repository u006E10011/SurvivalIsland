using UnityEditor;

namespace YTools
{
    [CanEditMultipleObjects, CustomEditor(typeof(PlayerController))]
    public class PlayerControllerEditor : Editor
    {
        private SerializedProperty _camera;
        private SerializedProperty _model;
        private SerializedProperty _dataInstance;
        private SerializedProperty _cameraType;

        private void OnEnable()
        {
            _camera = serializedObject.FindProperty("_camera");
            _model = serializedObject.FindProperty("_model");
            _dataInstance = serializedObject.FindProperty("_dataInstance");
            _cameraType = serializedObject.FindProperty("CameraType");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_dataInstance);
            EditorGUILayout.PropertyField(_camera);

            if (_cameraType?.enumValueIndex == (int)CameraType.TPS)
                EditorGUILayout.PropertyField(_model);

            serializedObject.ApplyModifiedProperties();
        }

    }
}