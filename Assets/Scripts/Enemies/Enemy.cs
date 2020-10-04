using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Sprite deadBody;
    public int maxHealth;
    float health;

    EnemyStates es;
    NavMeshAgent nma;
    SpriteRenderer sr;
    GameObject vision;
    DynamicBillboardChange dbc;
    Animator anim;

    private void Start()
    {
        health = maxHealth;
        es = GetComponent<EnemyStates>();
        nma = GetComponent<NavMeshAgent>();
        sr = GetComponent<SpriteRenderer>();
        dbc = GetComponent<DynamicBillboardChange>();
        vision = transform.Find("Vision").gameObject;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            es.enabled = false;
            nma.enabled = false;
            dbc.enabled = false;
            anim.enabled = false;
            anim.Rebind();
            sr.sprite = deadBody;
            vision.SetActive(false);
            StartCoroutine("DeathDuration");
            
        }
    }

    void AddDamage(float damage)
    {
        health -= damage;
    }
    IEnumerator DeathDuration()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(gameObject);
    }
}
