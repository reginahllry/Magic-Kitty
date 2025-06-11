using UnityEngine;

public class HealthItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject planeObject;
    private Renderer planeRenderer;
    public float Radius = 1;
    public float spawnTimer = 6f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(SpawnApple), 0f, spawnTimer);
        planeRenderer = planeObject.GetComponent<Renderer>();
    }

    void SpawnApple()
    {
        Bounds bounds = planeRenderer.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        float y = bounds.center.y+0.7f;

        Vector3 spawnPos = new Vector3(x, y, z);
        Instantiate(applePrefab, spawnPos, Quaternion.identity);

        // Vector2 circle2D = Random.insideUnitCircle * Radius;
        // Vector3 randomPos = new Vector3(circle2D.x, 0, circle2D.y) + transform.position;
        // Instantiate(applePrefab, randomPos, Quaternion.LookRotation(Vector3.up, Vector3.up));        
    }

    void OnDrawGizmos()
    {
        // Gizmos.color = Color.purple;
        // Gizmos.DrawWireSphere(this.transform.position, Radius);
    }
}
