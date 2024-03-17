using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspectorQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI questRecompensa;

    public override void ConfigurarQuestUI(Quest_ questPorCargar)
    {
        base.ConfigurarQuestUI (questPorCargar);
        questRecompensa.text = $"-{questPorCargar.RecompensaOro} oro" +
                               $"-{questPorCargar.RecompensaExp} exp";
                               //$"-{questPorCargar.RecompensaItem.Item.Nombre} x{questPorCargar.RecompensaItem.Cantidad}"; ;
    }
}
