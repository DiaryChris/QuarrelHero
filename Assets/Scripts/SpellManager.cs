using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpellManager : MonoBehaviour
{

    public int[][] spells;
    public Sprite[] spellSprites;
    
    public ProgressBar spellProgressBar;

    //音量阈值
    public float thresholdLow;
    public float thresholdHigh;
    public float timeSustain;

    private Level curLevel;


    private float timer;

    private GameManager GM;

    private void Start()
    {
        GM = GameManager.instance;

        InitSpells();
        curLevel = Level.None;
        
    }

    private void Update()
    {
        if (GM.worldState == WorldState.isSpelling)
        {
            //Debug.Log(curLevel.ToString());
            int spell = DiscSpell();
            int spellsLength = spells[GM.curSpellIndex].Length;
            //Debug.Log(spell);
            if (spell == spells[GM.curSpellIndex][GM.curCharIndex])
            {
                spellProgressBar.AddProgressValue(1f / spellsLength);
                GM.curCharIndex++;
                if(GM.curCharIndex >= spellsLength)
                {
                    GM.OnSpellSuccess(GM.curSpellIndex);
                }
            }
            
        }
    }

    private void InitSpells()
    {
        spells = new int[][]{
            new int[] { 0,1,1 },
            new int[] { 1,0,1,0 },
            new int[] { 1,1,0,1,1 },
            new int[] { 0,1,1,0,1,0,1 }
            //new int[] { 1,1,0,1,1,0,1 }
        };
    }

    //判别音量返回音节-1或0或1
    private int DiscSpell()
    {
        timer += Time.deltaTime;

        if (GM.volume > thresholdHigh)
        {
            if (curLevel == Level.Low)
            {
                curLevel = Level.High;
                if (timer > timeSustain)
                {
                    timer = 0;
                    return 0;
                }
                timer = 0;
            }
            if (curLevel == Level.None)
            {
                curLevel = Level.High;
                timer = 0;
            }
            return -1;
        }
        else if (GM.volume > thresholdLow)
        {
            if (curLevel == Level.High)
            {
                curLevel = Level.Low;
                if (timer > timeSustain)
                {
                    timer = 0;
                    return 1;
                }
                timer = 0;
            }
            if (curLevel == Level.None)
            {
                curLevel = Level.Low;
                timer = 0;
            }
            return -1;
        }
        else
        {
            if (curLevel == Level.High)
            {
                curLevel = Level.None;
                if (timer > timeSustain)
                {
                    timer = 0;
                    return 1;
                }
                timer = 0;
            }
            if (curLevel == Level.Low)
            {
                curLevel = Level.None;
                if (timer > timeSustain)
                {
                    timer = 0;
                    return 0;
                }
                timer = 0;
            }
            return -1;
        }

    }

}
