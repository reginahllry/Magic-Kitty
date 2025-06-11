using System.Collections.Generic;
using UnityEngine;

public class HealthHeartBar : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerCombat playerHealth;
    private List<HealthHeart> hearts = new List<HealthHeart>();

    private void OnEnable()
    {
        PlayerCombat.OnPlayerDamaged += DrawHearts;
        PlayerCombat.OnPlayerHealed += DrawHearts;
        Invoke(nameof(DrawHearts), 0.2f);
    }
    
    private void OnDisable()
    {
        PlayerCombat.OnPlayerDamaged -= DrawHearts;
        PlayerCombat.OnPlayerHealed -= DrawHearts;
    }

    public void DrawHearts()
    {
        if (playerHealth == null)
        {
            Debug.LogWarning("HHB: playerHealth not assigned");
            return;
        }

        ClearHearts();

        // a quarter heart = 10 hp
        // float maxHealthRemainder = playerHealth.maxHealth % 2; 
        // int heartsToMake = (int)(playerHealth.maxHealth / 40 + maxHealthRemainder);
        int heartsToMake = Mathf.CeilToInt(playerHealth.maxHealth/40);

        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(playerHealth.currentHealth/10 - (i * 4), 0, 4);
            print("CurrentHealth: "+ playerHealth.currentHealth +"ASSIGNED HEART STATUS:" + heartStatusRemainder);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform, false);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }

    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        hearts = new List<HealthHeart>();
    }
}
