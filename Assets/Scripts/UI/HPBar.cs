using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public bool isPlayer;

    private ProgressBar bar;
    private GameManager GM;

    private void Start()
    {
        bar = GetComponent<ProgressBar>();
        GM = GameManager.instance;

    }

    private void Update()
    {
        if (isPlayer)
        {
            bar.SetProgressValue(GM.player.hp / GM.player.fullHp);
        }
        else
        {
            bar.SetProgressValue(GM.enemy.hp / GM.enemy.fullHp);
        }
    }
}
