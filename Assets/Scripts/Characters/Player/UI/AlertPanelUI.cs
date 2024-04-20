using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlertPanelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI alertTxt;

    private void OnEnable()
    {
        StartCoroutine(ShowOffAlert());
    }

    IEnumerator ShowOffAlert()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    public void ShowAlert(string _message)
    {
        alertTxt.text = _message;
        gameObject.SetActive(true);
    }
}
