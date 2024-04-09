using UnityEngine;
using UnityEditor;

// 이 클래스는 에디터에서만 사용됩니다.
#if UNITY_EDITOR
[CustomEditor(typeof(PotalManager))]
public class TestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PotalManager componentScript = (PotalManager)target;

        if (GUILayout.Button("퀘스트1 수락"))
        {
            componentScript.AcceptFirstQuest();
        }
        if (GUILayout.Button("퀘스트2 수락"))
        {
            componentScript.AcceptSecondQuest();
        }
        if (GUILayout.Button("퀘스트3 수락"))
        {
            componentScript.AcceptThirdQuest();
        }
        if (GUILayout.Button("퀘스트1 완료"))
        {
            componentScript.CompleteFirstQuest();
        }
        if (GUILayout.Button("퀘스트2 완료"))
        {
            componentScript.CompleteSecondQuest();
        }
        if (GUILayout.Button("퀘스트3 완료"))
        {
            componentScript.CompleteThirdQuest();
        }
    }
}
#endif