using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDescripcion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questNombre;
    [SerializeField] private TextMeshProUGUI questDescripcion;

    public virtual void ConfigurarQuestUI(Quest_ questPorCargar)
    {
        questNombre.text = questPorCargar.Nombre;
        questDescripcion.text = questPorCargar.Descripcion;
    }
}
