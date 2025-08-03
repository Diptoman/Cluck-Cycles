using TMPro;
using UnityEngine;

public class TooltipBox : MonoBehaviour
{
    public Vector2 offset;
    public Vector2 screenBounds;
    public float onSpeed;
    public float offSpeed;
    public Vector2 animOffset;

    public TextMeshPro text;
    public SpriteRenderer box;
    private Vector2 currentPos;
    private float weight;
    private float targetWeight;

    public void Show(string tooltip, Vector2 position)
    {
        var boxSize = box.size;
        currentPos = position + boxSize * 0.5f + offset;

        if (currentPos.y > screenBounds.y)
            currentPos.y = screenBounds.y;
        else if(currentPos.y < -screenBounds.y)
            currentPos.y = -screenBounds.y;
        
        if (currentPos.x > screenBounds.x)
            currentPos.x = screenBounds.x;
        else if(currentPos.x < -screenBounds.x)
            currentPos.x = -screenBounds.x;

        transform.SetPosXY(currentPos);
        targetWeight = 1;
        text.SetText(tooltip);
    }

    public void Hide(bool isForced = false)
    {
        targetWeight = 0;

        if (isForced)
        {
            weight = 0;
            SetAnimWeight(0);
        }
    }

    void Update()
    {
        float newWeight;

        if (targetWeight == 1)
            newWeight = weight + Time.deltaTime * onSpeed;
        else
            newWeight = weight - Time.deltaTime * offSpeed;

        newWeight = Mathf.Clamp01(newWeight);

        if (weight == newWeight)
            return;

        weight = newWeight;
        SetAnimWeight(weight);
    }

    private void SetAnimWeight(float weight)
    {
        //Alpha
        var alphaSmooth = 1 - (1 - weight) * (1 - weight);
        var col = text.color;
        col.a = alphaSmooth;
        text.color = col;

        col = box.color;
        col.a = Mathf.Min(alphaSmooth * 2f, 1);
        box.color = col;

        // Pos
        var posOffset = (1 - alphaSmooth) * animOffset;
        var targetPos = currentPos + posOffset;
        transform.SetPosXY(targetPos);
    }
}
