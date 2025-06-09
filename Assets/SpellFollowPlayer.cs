using UnityEngine;

public class SpellFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = new Vector3(0f, 1f, 2f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
