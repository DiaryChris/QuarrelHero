using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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

    public void AfterPlayerAttack()
    {
        GM.PlayEffect(2);
        Transform attackTrans = Instantiate(attackPrefabs[GM.curSpellIndex], transform.position, Quaternion.identity);
        attackTrans.GetComponent<AttackItem>().isFromPlayer = true;
        attackTrans.GetComponent<AttackItem>().distX = GM.enemy.GetComponent<Transform>().position.x;
        attackTrans.GetComponent<AttackItem>().power = GM.skillPower[GM.curSpellIndex];
        GM.OnEnemySpell();
    }

    public void AfterPlayerShield()
    {
        
        GM.OnEnemySpell();
    }

    public void AfterPlayerMiss()
    {

        GM.OnEnemySpell();
    }
}
