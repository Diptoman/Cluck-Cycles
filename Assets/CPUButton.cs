
using UnityEngine;

public class CPUButton : MonoBehaviour
{
    public string tooltipText;
    public Tooltip tooltip;

    void Awake()
    {
        tooltip.text = tooltipText + Global.CPUPrice;
    }

    void OnMouseDown()
    {
        Debug.Log($"TRY BUY CPU");

        var buyPrice = Global.CPUPrice;
        if (Global.Money >= buyPrice)
        {
            Global.Money -= buyPrice;
            Global.CPUPrice += 5;
            CluckController.ClucksPerRun++;
            tooltip.text = tooltipText + Global.CPUPrice;
            // PUT CPU CYCLES EDITING CODE HERE
        }
        else
            Debug.Log($"Buy failed");
    }
}
