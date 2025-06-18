using UnityEngine;

public class BoatEnterExit : MonoBehaviour
{
    public GameObject player;             // player
    public Transform seatPosition;        // posisi kursi duduk di boat
    public GameObject boatCamera;         // kamera khusus boat (opsional)
    public GameObject playerCamera;       // kamera player biasa

    private bool isNearBoat = false;
    private bool isOnBoat = false;

    private PlayerMovement playerMovement; // contoh script movement player

    void Start()
    {
        // coba ambil script movement player, sesuaikan nama script kamu
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (isNearBoat && Input.GetKeyDown(KeyCode.E) && !isOnBoat)
        {
            // Naik ke boat
            EnterBoat();
        }
        else if (isOnBoat && Input.GetKeyDown(KeyCode.F))
        {
            // Turun dari boat
            ExitBoat();
        }
    }

    void EnterBoat()
    {
        isOnBoat = true;

        // Pindahkan player ke kursi duduk
        player.transform.position = seatPosition.position;
        player.transform.rotation = seatPosition.rotation;

        // Matikan movement player supaya gak jalan kemana-mana saat duduk
        if (playerMovement != null)
            playerMovement.enabled = false;

        // Aktifkan kamera boat, matikan kamera player
        if (boatCamera != null)
            boatCamera.SetActive(true);
        if (playerCamera != null)
            playerCamera.SetActive(false);
    }

    void ExitBoat()
    {
        isOnBoat = false;

        // Aktifkan kembali movement player
        if (playerMovement != null)
            playerMovement.enabled = true;

        // Pindahkan player ke samping boat agar tidak nabrak
        Vector3 exitPosition = seatPosition.position + boatCamera.transform.right * 2f;
        player.transform.position = exitPosition;

        // Aktifkan kamera player, matikan kamera boat
        if (playerCamera != null)
            playerCamera.SetActive(true);
        if (boatCamera != null)
            boatCamera.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isNearBoat = true;
            Debug.Log("Player near boat");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isNearBoat = false;
            Debug.Log("Player left boat area");
        }
    }
}
