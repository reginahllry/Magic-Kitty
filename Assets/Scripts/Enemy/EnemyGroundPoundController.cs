using Unity.Mathematics;
using UnityEngine;

public class EnemyGroundPoundController : MonoBehaviour
{
    [SerializeField] private ParticleSystem groundPoundPf;
    [SerializeField] private float blastRange = 31f;
    private int damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        damage = GetComponent<AIEnemyController>().damage;
    }

    public void Jump()
    {
        SFXManager.Play("ChloeDash", true, 0.8f);
    }

    public void GroundPound()
    {
        Debug.Log("played ground pound particle");
        SFXManager.Play("GroundPoundImpact", true, 0.8f);
        groundPoundPf.Play();
        Invoke(nameof(Whispers), 0.3f);

        checkPlayerHit();
        // Invoke(nameof(SelfDestruct), 2f);
    }

    private void checkPlayerHit()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRange);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Player")
            {
                collider.gameObject.GetComponent<PlayerCombat>().TakeDamage(damage);
            }
        }
    }

    void Whispers()
    {
        SFXManager.Play("Whispers", false, 2f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, blastRange);
    }
}