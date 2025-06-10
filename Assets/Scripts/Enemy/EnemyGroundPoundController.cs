using Unity.Mathematics;
using UnityEngine;

public class EnemyGroundPoundController : MonoBehaviour
{
    [SerializeField] private ParticleSystem groundPoundPf;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
        // Invoke(nameof(SelfDestruct), 2f);
    }

    void Whispers()
    {
        SFXManager.Play("Whispers", false, 2f);
    }
}