using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager_ : MonoBehaviour
{
    [Header("Quests")]
    [SerializeField] private Quest_[] questDisponibles;

    [Header("Inspector Quests")]
    [SerializeField] private InspectorQuestDescripcion inspectorQuestPrefab;
    [SerializeField] private Transform inspectorQuestContenedor;

    void Start()
    {
        CargarQuestEnInspector();
    }

    private void CargarQuestEnInspector()
    {
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            InspectorQuestDescripcion nuevoQuest = Instantiate(inspectorQuestPrefab, inspectorQuestContenedor);
            nuevoQuest.ConfigurarQuestUI(questDisponibles[i]);
        }
    }
}
