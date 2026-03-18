#if UNITY_EDITOR
using UnityEditor;

namespace YTools
{
    [CanEditMultipleObjects, CustomEditor(typeof(Localization))]
    public class LocalizationEditor : Editor
    {
        private SerializedProperty _ruProperty;
        private SerializedProperty _enProperty;
        private SerializedProperty _isAutoSetProperty;
        private SerializedProperty _updateOfTickProperty;
        private SerializedProperty _isTranslateByKeyProperty;
        private SerializedProperty _keyProperty;
        private SerializedProperty _textProperty;

        private void OnEnable()
        {
            _ruProperty = serializedObject.FindProperty("_ru");
            _enProperty = serializedObject.FindProperty("_en");
            _isAutoSetProperty = serializedObject.FindProperty("_isAutoSet");
            _updateOfTickProperty = serializedObject.FindProperty("_updateOfTick");
            _isTranslateByKeyProperty = serializedObject.FindProperty("_isTranslateByKey");
            _keyProperty = serializedObject.FindProperty("_key");
            _textProperty = serializedObject.FindProperty("_text");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_isTranslateByKeyProperty);

            if (_isTranslateByKeyProperty.boolValue)
                EditorGUILayout.PropertyField(_keyProperty);
            else
            {
                EditorGUILayout.Space(5);
                EditorGUILayout.PropertyField(_ruProperty);
                EditorGUILayout.PropertyField(_enProperty);
            }

            EditorGUILayout.Space(5);
            EditorGUILayout.PropertyField(_isAutoSetProperty);
            EditorGUILayout.PropertyField(_updateOfTickProperty);
            EditorGUILayout.Space(5);
            EditorGUILayout.PropertyField(_textProperty);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif