#if UNITY_EDITOR
using UnityEditor;

namespace YTools
{
    [CanEditMultipleObjects, CustomEditor(typeof(SceneChanger))]
    public class SceneChangerEditor : Editor
    {
        private SerializedProperty _type;
        private SerializedProperty _index;
        private SerializedProperty _name;
        private SerializedProperty _button;

        private void OnEnable()
        {
            _type = serializedObject.FindProperty("_type");
            _index = serializedObject.FindProperty("_index");
            _name = serializedObject.FindProperty("_name");
            _button = serializedObject.FindProperty("_button");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_type);

            if (_type.enumValueIndex == (int)SceneChangerType.Index)
                EditorGUILayout.PropertyField(_index);
            else if (_type.enumValueIndex == (int)SceneChangerType.Name)
                EditorGUILayout.PropertyField(_name);

            if (_type.enumValueIndex == (int)SceneChangerType.Index || _type.enumValueIndex == (int)SceneChangerType.Name)
                EditorGUILayout.Space(5);

            EditorGUILayout.PropertyField(_button);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif