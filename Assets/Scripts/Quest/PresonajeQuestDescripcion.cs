using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PersonajeQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI tareaObjetivo;
    [SerializeField] private TextMeshProUGUI recompensaOro;
    [SerializeField] private TextMeshProUGUI recompensaExp;

    [Header("Item")]
    //[SerializeField] private Image recompensaItemIcono;
    [SerializeField] private TextMeshProUGUI recompensaItemCantidad;

    public override void ConfigurarQuestUI(Quest_ questPorCargar)
    {
        base.ConfigurarQuestUI(questPorCargar);
        recompensaOro.text = questPorCargar.RecompensaOro.ToString();
        recompensaExp.text = questPorCargar.RecompensaExp.ToString();
        tareaObjetivo.text = $"{questPorCargar.CantidadActual}/{questPorCargar.CantidadObjetivo}";

        //receompensaItemIcono.sprtite = questPorCargar.RecompensaItem.Item.Icono;
        recompensaItemCantidad.text = questPorCargar.RecompensaItem.Cantidad.ToString();
    }
}
