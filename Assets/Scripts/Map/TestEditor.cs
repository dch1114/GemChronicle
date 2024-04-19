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
        if (GUILayout.Button("퀘스트4 완료"))
        {
            componentScript.CompleteFourthQuest();
        }
        if (GUILayout.Button("퀘스트5 완료"))
        {
            componentScript.CompleteFiveQuest();
        }
        if (GUILayout.Button("퀘스트6 완료"))
        {
            componentScript.CompleteSixQuest();
        }
        if (GUILayout.Button("퀘스트7 완료"))
        {
            componentScript.CompleteSevenQuest();
        }
        if (GUILayout.Button("퀘스트8 완료"))
        {
            componentScript.CompleteEightQuest();
        }
        if (GUILayout.Button("퀘스트9 완료"))
        {
            componentScript.CompleteNineQuest();
        }
        if (GUILayout.Button("퀘스트10 완료"))
        {
            componentScript.CompleteTenQuest();
        }
    }
}
#endif