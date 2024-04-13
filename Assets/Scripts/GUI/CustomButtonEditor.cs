#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;

namespace Custom.Editor
{
    [CustomEditor(typeof(CustomButton))]
    public class CustomButtonEditor : ButtonEditor
    {
        SerializedProperty OnClickedProperty;
        SerializedProperty OnReleasedProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            OnClickedProperty = serializedObject.FindProperty("OnClicked");
            OnReleasedProperty = serializedObject.FindProperty("OnReleased");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(OnClickedProperty);
            EditorGUILayout.PropertyField(OnReleasedProperty);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif