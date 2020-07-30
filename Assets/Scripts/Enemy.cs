using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp;
    public float fullHp = 100f;
    public Animator animator;
    public Transform[] attackPrefabs;

    private GameManager GM;

    private void Start()
    {
        GM = GameManager.instance;

        hp = fullHp;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    public void AfterEnemyAttack()
    {
        GM.PlayEffect(2);
        Transform attackTrans = Instantiate(attackPrefabs[GM.enemySpellIndex], transform.position, Quaternion.identity);
        attackTrans.GetComponent<AttackItem>().isFromPlayer = false;
        attackTrans.GetComponent<AttackItem>().distX = GM.player.GetComponent<Transform>().position.x;
        attackTrans.GetComponent<AttackItem>().power = GM.skillPower[GM.enemySpellIndex];
        GM.WaitForChoose();
    }
}
