using System;
using UnityEngine;

public class FloatAnimation : MonoBehaviour
{
    private Vector3 startRot;
    private Vector3 startPos;
    public Vector3 rotOffset;
    public Vector3 rotSpeed;
    public Vector3 rotAmount;

    public Vector3 posSpeed;
    public Vector3 posAmount;
    
    [NonSerialized]
    public float weight = 1f;
    private float randomStart;

    public bool isActive { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        startRot = transform.localEulerAngles;
        startPos = transform.localPosition;
        randomStart = UnityEngine.Random.Range(0f, 5f);
    }

    public void SetActive(bool active)
    {
        isActive = active;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
            return;

        var t = Time.time + randomStart;
        var x = Mathf.Sin(rotSpeed.x * t + rotOffset.x) * rotAmount.x;
        var y = Mathf.Sin(rotSpeed.y * t + rotOffset.y) * rotAmount.y;
        var z = Mathf.Sin(rotSpeed.z * t + rotOffset.z) * rotAmount.z;

        var finalAngles = startRot + new Vector3(x, y, z) * weight;
        transform.localEulerAngles = finalAngles;

        var posX = Mathf.Sin(posSpeed.x * t) * posAmount.x;
        var posY = Mathf.Sin(posSpeed.y * t) * posAmount.y;
        var posZ = Mathf.Sin(posSpeed.z * t) * posAmount.z;
        transform.localPosition = startPos + new Vector3(posX, posY, posZ) * weight;
    }
}
