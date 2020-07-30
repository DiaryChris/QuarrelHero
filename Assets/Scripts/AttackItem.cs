using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackItem : MonoBehaviour
{
    public bool isFromPlayer;
    public float speed = 10f;
    public float distX = 6f;
    public float power = 10f;
    private GameManager GM;

    private void Start()
    {
        if (!isFromPlayer)
        {
            speed = -speed;
        }
        GM = GameManager.instance;
    }

    private void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        if (isFromPlayer && transform.position.x > distX)
        {
            GM.enemy.hp -= power;
            Destroy(gameObject);
        }
        if (!isFromPlayer && transform.position.x < distX)
        {
            GM.player.animator.Play("PlayerInjured");
            GM.player.hp -= power;
            Destroy(gameObject);
        }
    }
}
