using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] PlayerAnimManager anim;
    [SerializeField] Shooting s;
    [SerializeField] float attackCD;
    float attackCDTimer;

    public bool isAttacking;

    Collider col;

    private void Start()
    {
        col = GetComponent<BoxCollider>();
        col.enabled = false;
    }

    private void Update()
    {
        attackManager();
    }

    void attackManager()
    {
        if(attackCDTimer < 0)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                attackStart();
            }
        }

        //Attack CD
        if(attackCDTimer > -1)
        {
            attackCDTimer -= Time.deltaTime;
        }
    }

    void attackStart()
    {
        anim.playPunch();
        col.enabled = true;
        isAttacking = true;
        attackCDTimer = attackCD;
        Invoke("attackEnd", 0.2f);
    }

    void attackEnd()
    {
        col.enabled = false;
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyDeath enemy = other.GetComponent<EnemyDeath>();
        if(enemy != null)
        {
            enemy.Die();
            s.hasBullet = true;
        }
        if(other.CompareTag("ReverseGravity"))
        {
            s.ReverseGravity();
            s.hasBullet = true;
        }
    }
}
