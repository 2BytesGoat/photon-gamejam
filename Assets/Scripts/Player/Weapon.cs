using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
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
        isAttacking = false;
    }
}
