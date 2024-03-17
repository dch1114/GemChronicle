using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspectorQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI questRecompensa;

    public override void ConfigurarQuestUI(Quest_ quest)
    {
        base.ConfigurarQuestUI (quest);
        QuestCargado = quest;
        questRecompensa.text = $"-{quest.RecompensaOro} oro" +
                               $"\n-{quest.RecompensaExp} exp";
                               //$"\n-{quest.RecompensaItem.Item.Nombre} x{quest.RecompensaItem.Cantidad}"; ;
    }

    public void AceptarQuest()
    {
        if (QuestCargado == null)
        {
            return;
        }

        QuestManager_.Instance.AnadirQuest(QuestCargado);
        gameObject.SetActive(false);
    }
}
