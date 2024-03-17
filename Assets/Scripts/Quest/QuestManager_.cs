using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager_ : Singleton<QuestManager_>
{
    [Header("Quests")]
    [SerializeField] private Quest_[] questDisponibles;

    [Header("Inspector Quests")]
    [SerializeField] private InspectorQuestDescripcion inspectorQuestPrefab;
    [SerializeField] private Transform inspectorQuestContenedor;

    [Header("Personaje Quests")]
    [SerializeField] private PersonajeQuestDescripcion personajeQuestPrefab;
    [SerializeField] private Transform personajeQuestContenedor;

    public Quest QuestPorReclamar { get; private set; }

    private void Start()
    {
        CargarQuestEnInspector();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            AnadirProgreso("Mata10", 1);
            AnadirProgreso("Mata25", 1);
            AnadirProgreso("Mata50", 1);
        }
    }

    private void CargarQuestEnInspector()
    {
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            InspectorQuestDescripcion nuevoQuest = Instantiate(inspectorQuestPrefab, inspectorQuestContenedor);
            nuevoQuest.ConfigurarQuestUI(questDisponibles[i]);
        }
    }

    private void AnadirQuestPorCompletar(Quest_ questPorCompletar)
    {
        PersonajeQuestDescripcion nuevoQuest = Instantiate(personajeQuestPrefab, personajeQuestContenedor);
        nuevoQuest.ConfigurarQuestUI(questPorCompletar);
    }

    public void AnadirQuest(Quest_ questPorCompletar)
    {
        AnadirQuestPorCompletar(questPorCompletar);
    }

    public void AnadirProgreso(string questID, int cantidad)
    {
        Quest_ questporActualizar = QuestExiste(questID);
        questporActualizar.AnadirProgreso(cantidad);
    }

    private Quest_ QuestExiste(string questID)
    {
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            if (questDisponibles[i].ID == questID)
            {
                return questDisponibles[i];
            }
        }

        return null;
    }
}
