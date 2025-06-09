using UnityEngine;
using System;

public class ProjectileAnimation : MonoBehaviour
{
    private int spellType;
    [SerializeField] private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 1 for fire
        // 2 for water
        // 3 for ice
        // 4 for thunder
        var rng = new System.Random();
        spellType = rng.Next(1, 4);

        if (spellType == 1) anim.SetBool("fire_cast", true);
        if (spellType == 2) anim.SetBool("water_cast", true);
        if (spellType == 3) anim.SetBool("ice_cast", true);
        if (spellType == 4) anim.SetBool("thunder_cast", true);
    }
}
