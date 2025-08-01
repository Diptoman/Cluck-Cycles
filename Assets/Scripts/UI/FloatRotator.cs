using UnityEngine;

public class FloatRotator : MonoBehaviour
{
    private float randomStart;
    private Vector3 startRot;
    public Vector3 rotSpeed;
    public Vector3 rotAmount;
    // Start is called before the first frame update
    void Start()
    {
        startRot = transform.localEulerAngles;
        randomStart = Random.Range(0f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        var t = Time.time * Mathf.Deg2Rad + randomStart;
        var x = Mathf.Sin(rotSpeed.x * t) * rotAmount.x;
        var y = Mathf.Sin(rotSpeed.y * t) * rotAmount.y;
        var z = Mathf.Sin(rotSpeed.z * t) * rotAmount.z;

        var finalAngles = startRot + new Vector3(x, y, z);
        transform.localEulerAngles = finalAngles;
    }
}
