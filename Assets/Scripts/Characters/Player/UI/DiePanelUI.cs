using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiePanelUI : MonoBehaviour
{
    [SerializeField] Image diePanel;
    [SerializeField] List<TextMeshProUGUI> textsToFade;

    [SerializeField] float fadeInDuration = 1f;

    private void OnEnable()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {

        List<Color> originalColors = new List<Color>();
        foreach (TextMeshProUGUI text in textsToFade)
        {
            originalColors.Add(text.color);
            text.color = new Color(originalColors[originalColors.Count - 1].r, originalColors[originalColors.Count - 1].g, originalColors[originalColors.Count - 1].b, 0f);
        }

        Color originalColor = diePanel.color;
        originalColor.a = 0f;
        diePanel.color = originalColor;

        yield return new WaitForSeconds(1f);

        float timer = 0f;
        while (timer < fadeInDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeInDuration);

            foreach (TextMeshProUGUI text in textsToFade)
            {
                text.color = new Color(originalColors[textsToFade.IndexOf(text)].r, originalColors[textsToFade.IndexOf(text)].g, originalColors[textsToFade.IndexOf(text)].b, alpha);
            }

            diePanel.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        foreach (TextMeshProUGUI text in textsToFade)
        {
            text.color = new Color(originalColors[textsToFade.IndexOf(text)].r, originalColors[textsToFade.IndexOf(text)].g, originalColors[textsToFade.IndexOf(text)].b, 1f);
        }
        diePanel.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }

    public void ActiveDiePanel()
    {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            ReSpawn();
        }
    }

    private void ReSpawn()
    {
        gameObject.SetActive(false);

        try
        {
            PlayerStateMachine stateMachine = GameManager.Instance.player.GetStateMachine();
            stateMachine.ChangeState(stateMachine.IdleState);

            PotalManager.Instance.MovePotal(5);
            GameManager.Instance.player.Data.StatusData.HealFull();
            UIManager.Instance.alertPanelUI.ShowAlert("마을로 돌아와 체력을 회복했습니다.");
        } catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
