using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class MousePointAnimation : MonoBehaviour
{
    [Serializable]
    public struct Config
    {
        public float animSpeed;
        public AnimationCurve posCurve;
        public AnimationCurve rotCurve;
        public AnimationCurve scaleCurve;
        public Vector3 pos;
        public Vector3 rot;
        public Vector3 scale;
    }

    public static Config globalConfig;
    public Config config => isCustom ? customConfig : globalConfig;

    public bool isCustom;
    [ShowIf("isCustom")]
    public Config customConfig;
    public Transform target;
    public float reduceIdleWeight = 0.25f;
    public FloatAnimation idleAnim;
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

    void OnMouseEnter()
    {
        if (idleAnim != null)
            idleAnim.weight = reduceIdleWeight;

        enterTimer = 0f;
        isActive = true;
        dynamicWeight = 1f;

        randomRot = UnityEngine.Random.Range(0.75f, 1.25f);
        if (randomRot > 1f)
            randomRot = -randomRot;
    }

    void OnMouseExit()
    {
        if (idleAnim != null)
            idleAnim.weight = 1f;

        isActive = false;
    }

    void Update()
    {
        var canReset = isActive && Input.GetKeyDown(KeyCode.Mouse0);
        if (canReset)
        {
            dynamicWeight = 1.5f;
            randomRot = UnityEngine.Random.Range(1f, 1.5f);
            if (randomRot > 1.25f)
                randomRot = -randomRot;
        }

        if (isActive && !canReset)
            enterTimer += Time.deltaTime;
        else
            enterTimer = 0f;

        var animTime = enterTimer * config.animSpeed;
        animTime = Mathf.Clamp01(animTime);

        target.localPosition = startPos + config.pos * config.posCurve.Evaluate(animTime) * weight * dynamicWeight;
        target.localEulerAngles = startRot + config.rot * config.rotCurve.Evaluate(animTime) * weight * dynamicWeight * randomRot;
        var scale = Vector3.one + config.scale * config.scaleCurve.Evaluate(animTime) * weight * dynamicWeight;
        target.localScale = Vector3.Scale(scale, startScale);
    }
}
