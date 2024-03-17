using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDescripcion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questNombre;
    [SerializeField] private TextMeshProUGUI questDescripcion;

    public Quest_ QuestCargado {  get; set; }

    public virtual void ConfigurarQuestUI(Quest_ quest)
    {
        questNombre.text = quest.Nombre;
        questDescripcion.text = quest.Descripcion;
    }
}
