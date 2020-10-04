using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    float health;
    public AudioClip hit;
    public FlashScreen flash;
    AudioSource source;
    void Start()
    {
        health = maxHealth;
        source = GetComponent<AudioSource>();
    }

   
    void EnemyHit(float damage)
    {
        source.PlayOneShot(hit);
        health -= damage;
        flash.TookDamage();
    }
}
