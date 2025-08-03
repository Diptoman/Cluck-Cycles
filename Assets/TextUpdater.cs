using TMPro;
using UnityEngine;

public class TextUpdater : MonoBehaviour
{
    public TextMeshPro moneyText;
    private int lastMoney;

    public TextMeshPro clucksRemainingText;
    private int lastClucksRemaining;

    public TextMeshPro cpuSpeed;
    private int lastCpuSpeed;

    void Update()
    {
        UpdateMoney();
        UpdateClucks();
    }

    private void UpdateMoney()
    {
        if (Global.Money == lastMoney || moneyText == null)
            return;

        lastMoney = Global.Money;
        moneyText.text = "$" + lastMoney;
        var anim = moneyText.GetComponent<QuickAnimation>();
        if (anim != null)
            anim.Trigger();
    }

    private void UpdateClucks()
    {
        if (CluckController.ClucksRemaining == lastClucksRemaining || clucksRemainingText == null)
            return;

        lastClucksRemaining = CluckController.ClucksRemaining;
        clucksRemainingText.text = lastClucksRemaining.ToString();
        var anim = clucksRemainingText.GetComponent<QuickAnimation>();

        if (anim != null)
            anim.Trigger();
    }
}
