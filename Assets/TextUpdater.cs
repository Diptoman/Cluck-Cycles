using TMPro;
using UnityEngine;

public class TextUpdater : MonoBehaviour
{
    public TextMeshPro moneyText;
    private int lastMoney;

    void Update()
    {
        UpdateMoney();
    }

    private void UpdateMoney()
    {
        if (Global.Money == lastMoney || moneyText == null)
            return;

        lastMoney = Global.Money;
        moneyText.text = "$" + lastMoney;
        var anim = moneyText.GetComponent<QuickAnimation>();
        if (anim!= null)
            anim.Trigger();
    }
}
