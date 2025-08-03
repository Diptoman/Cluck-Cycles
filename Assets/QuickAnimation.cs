using UnityEngine;

public class QuickAnimation : MonoBehaviour
{
    public Transform target;
    public float weight = 1f;
    private float dynamicWeight = 1f;

    private Vector3 startPos;
    private Vector3 startRot;
    private Vector3 startScale;

    private float randomRot;
    private float enterTimer;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
            target = this.transform;

        startRot = target.localEulerAngles;
        startPos = target.localPosition;
        startScale = target.localScale;
    }

    // Update is called once per frame

    public void Trigger()
    {
        enterTimer = 0f;
        isActive = true;
        dynamicWeight = 1f;

        randomRot = UnityEngine.Random.Range(0.75f, 1.25f);
        if (randomRot > 1f)
            randomRot = -randomRot;
    }

    void Update()
    {
        var config = MousePointAnimation.config;

        if (isActive)
            enterTimer += Time.deltaTime;
        else
            enterTimer = 0f;

        var animTime = enterTimer * config.animSpeed;
        animTime = Mathf.Clamp01(animTime);

        target.localPosition = startPos + config.pos * config.posCurve.Evaluate(animTime) * weight * dynamicWeight;
        target.localEulerAngles = startRot + config.rot * config.rotCurve.Evaluate(animTime) * weight * dynamicWeight * randomRot;
        var scale = Vector3.one + config.scale * config.scaleCurve.Evaluate(animTime) * weight * dynamicWeight;
        target.localScale = Vector3.Scale(scale, startScale);

        if (animTime >= 1f)
        {
            isActive = false;
        }
    }
}
