using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 1.5f;
    public float knockbackPower = 5f;

    private Animator animator;
    public bool isAttacking { get; private set; }

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void StartAttack() {
        animator.SetTrigger("AttackStarted");
        isAttacking = true;
    }

    public void StopAttack() {
        animator.ResetTrigger("AttackStarted");
        isAttacking = false;
    }
}
