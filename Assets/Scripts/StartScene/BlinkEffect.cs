using TMPro;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pressKeyText;

    float time;

    // Update is called once per frame
    void Update()
    {
        Blink();
    }

    private void Blink()
    {
        if (time < 0.5f)
        {
            pressKeyText.color = new Color(0, 0, 0, 1 - time);
        }
        else
        {
            pressKeyText.color = new Color(0, 0, 0, time);
            if (time > 1f)
            {
                time = 0;
            }
        }

        time += Time.deltaTime;
    }
}
