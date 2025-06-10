using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Transform player;

    public GameObject enemyProjectile;
    public Transform spawnPointLeft, spawnPointRight;
    private bool isFacingRight;

    void Start()
    {
        isFacingRight = spriteRenderer.flipX;
    }

    public void ShootAtPlayer()
    {
        SFXManager.Play("Fireball2", true, 0.7f);
        if (isFacingRight)
        {
            Vector3 aimDir = (player.transform.position - spawnPointRight.position).normalized;
            // just thought that the AI will always try to shoot the player anyway, so just make the projectile also towards the player
            Instantiate(enemyProjectile, spawnPointRight.transform.position, Quaternion.LookRotation(aimDir, Vector3.up));
            // cooldown already handled in aienemycontroller script
        }

        else
        {
            Vector3 aimDir = (player.transform.position - spawnPointLeft.position).normalized;
            Instantiate(enemyProjectile, spawnPointLeft.transform.position, Quaternion.LookRotation(aimDir, Vector3.up));
        }
    }   
}
