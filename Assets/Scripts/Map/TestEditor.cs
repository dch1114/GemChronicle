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

        // ��ư�� �����ϰ� ��ư�� ������ �ش� �Լ��� ����˴ϴ�.
        if (GUILayout.Button("����Ʈ �� �����ϱ�"))
        {
            componentScript.AcceptAllQuest();
        }
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
    }
}
#endif