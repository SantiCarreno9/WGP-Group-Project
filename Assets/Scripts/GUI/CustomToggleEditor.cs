#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;

namespace Custom.Editor
{
    [CustomEditor(typeof(CustomToggle))]
    public class CustomToggleEditor : ToggleEditor
    {
        SerializedProperty OnActivatedProperty;
        SerializedProperty OnDeactivatedProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            OnActivatedProperty = serializedObject.FindProperty("OnActivated");
            OnDeactivatedProperty = serializedObject.FindProperty("OnDeactivated");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(OnActivatedProperty);
            EditorGUILayout.PropertyField(OnDeactivatedProperty);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif