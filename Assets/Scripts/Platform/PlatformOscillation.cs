using UnityEngine;

public class PlatformOscillation : MonoBehaviour
{

    // Variables

    [SerializeField] private Transform fulcrumPoint;
    [SerializeField] private float frequency = 2.0f;
    [SerializeField] private float maxAmplitude = 30.0f;
    [SerializeField] private bool shouldOscillation = false;

    private float currentAmplitude;

    void Start()
    {
        if (fulcrumPoint == null)
        {
            fulcrumPoint = transform.parent;
        }
    }


    void Update()
    {
        if (shouldOscillation)
        {
            currentAmplitude = Mathf.Clamp(currentAmplitude + 10.0f * Time.deltaTime, 0, maxAmplitude);
            fulcrumPoint.rotation = Quaternion.AngleAxis(Mathf.Sin(Time.time * frequency) * currentAmplitude, Vector3.forward);
        }
        else
        {
            currentAmplitude = Mathf.Clamp(currentAmplitude - 10.0f * Time.deltaTime, 0, maxAmplitude);
            fulcrumPoint.rotation = Quaternion.AngleAxis(Mathf.Sin(Time.time * frequency) * currentAmplitude, Vector3.forward);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shouldOscillation = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shouldOscillation = false;
        }
    }

}
