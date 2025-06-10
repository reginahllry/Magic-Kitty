using Cinemachine;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private GameObject enemy;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private Transform spellSpawn;
    private Vector3 mouseWorldPosition;
    private float castSpellCd = 1f;
    private float cooldown = 0f;

    void Update()
    {
        mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        Aim();
        Shoot();
    }

    void Aim()
    {
        if (Input.GetMouseButton(1))
        {
            anim.SetBool("Aiming", true);
            aimVirtualCamera.gameObject.SetActive(true);
        }
        else aimVirtualCamera.gameObject.SetActive(false);
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && cooldown <= 0)
        {
            Debug.Log("SHOOTING");
            Debug.Log("mouseWorldPosition: " + mouseWorldPosition);
            Debug.Log("spellSpawnPosition: " + spellSpawn.position);
            Vector3 aimDir = (mouseWorldPosition - spellSpawn.position).normalized;

            Instantiate(spellPrefab, spellSpawn.position, Quaternion.LookRotation(aimDir, Vector3.up));
            cooldown = castSpellCd;
        }

        else
        {
            cooldown -= Time.deltaTime;
        }
    }
}
