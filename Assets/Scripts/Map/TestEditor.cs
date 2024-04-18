using UnityEngine;
using UnityEditor;

// �� Ŭ������ �����Ϳ����� ���˴ϴ�.
#if UNITY_EDITOR
[CustomEditor(typeof(PotalManager))]
public class TestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PotalManager componentScript = (PotalManager)target;

        if (GUILayout.Button("����Ʈ1 �Ϸ�"))
        {
            componentScript.CompleteFirstQuest();
        }
        if (GUILayout.Button("����Ʈ2 �Ϸ�"))
        {
            componentScript.CompleteSecondQuest();
        }
        if (GUILayout.Button("����Ʈ3 �Ϸ�"))
        {
            componentScript.CompleteThirdQuest();
        } 
        if (GUILayout.Button("����Ʈ4 �Ϸ�"))
        {
            componentScript.CompleteFourthQuest();
        }
        if (GUILayout.Button("����Ʈ5 �Ϸ�"))
        {
            componentScript.CompleteFiveQuest();
        }
        if (GUILayout.Button("����Ʈ6 �Ϸ�"))
        {
            componentScript.CompleteSixQuest();
        }
        if (GUILayout.Button("����Ʈ7 �Ϸ�"))
        {
            componentScript.CompleteSevenQuest();
        }
        if (GUILayout.Button("����Ʈ8 �Ϸ�"))
        {
            componentScript.CompleteEightQuest();
        }
        if (GUILayout.Button("����Ʈ9 �Ϸ�"))
        {
            componentScript.CompleteNineQuest();
        }
        if (GUILayout.Button("����Ʈ10 �Ϸ�"))
        {
            componentScript.CompleteTenQuest();
        }
    }
}
#endif