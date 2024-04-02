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

        // 버튼을 생성하고 버튼을 누르면 해당 함수가 실행됩니다.
        if (GUILayout.Button("퀘스트 다 수락하기"))
        {
            componentScript.AcceptAllQuest();
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