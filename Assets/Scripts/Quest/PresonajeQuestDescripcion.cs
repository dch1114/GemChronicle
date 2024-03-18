using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonajeQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI tareaObjetivo;
    [SerializeField] private TextMeshProUGUI recompensaOro;
    [SerializeField] private TextMeshProUGUI recompensaExp;

    [Header("Item")]
    [SerializeField] private Image recompensaItemIcono;
    [SerializeField] private TextMeshProUGUI recompensaItemCantidad;

    private void Update()
    {
        if (QuestPorCompletar.QuestCompletadoCheck)
        {
            return;
        }

        tareaObjetivo.text = $"{QuestPorCompletar.CantidadActual}/{QuestPorCompletar.CantidadObjetivo}";
    }

    public override void ConfigurarQuestUI(Quest_ questPorCargar)
    {
        base.ConfigurarQuestUI(questPorCargar);
        recompensaOro.text = questPorCargar.RecompensaOro.ToString();
        recompensaExp.text = questPorCargar.RecompensaExp.ToString();
        tareaObjetivo.text = $"{questPorCargar.CantidadActual}/{questPorCargar.CantidadObjetivo}";

        //recompensaItemIcono.sprite = questPorCargar.RecompensaItem.Item.Icono;
        //recompensaItemCantidad.text = questPorCargar.RecompensaItem.Cantidad.ToString();
    }

    private void QuestCompletadoRespuesta(Quest_ questCompletado)
    {
        if(questCompletado.ID == QuestPorCompletar.ID)
        {
            tareaObjetivo.text = $"{QuestPorCompletar.CantidadActual}/{QuestPorCompletar.CantidadObjetivo}";
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (QuestPorCompletar.QuestCompletadoCheck)
        {
            gameObject.SetActive(false);
        }

        Quest_.EventoQuestCompletado += QuestCompletadoRespuesta;
    }

    private void OnDisable()
    {
        Quest_.EventoQuestCompletado -= QuestCompletadoRespuesta;
    }
}
