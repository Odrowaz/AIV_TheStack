using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float ShakeDuration { get; set; }

    [SerializeField] private float shakeAmount = 0.2f;

    void LateUpdate()
    {
        if (ShakeDuration > 0)
        {
            Camera.main.transform.localPosition += Random.insideUnitSphere * shakeAmount;

            ShakeDuration -= Time.deltaTime;
        }
    }
}
