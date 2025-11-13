using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WorldState
{
    isEnemySpelling,
    afterEnemySpell,
    waitForChoose,
    isSpelling,
    afterSpell
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public WorldState defaultState;
    public WorldState worldState;

    public float volume;
    public float timeLimit = 5f;


    public int curSpellIndex;
    public int curCharIndex;

    public int enemySpellIndex;
    public int[] enemyFlow;
    public int enemyFlowCount = 0;
    public AudioClip[] enemySpell;

    public float[] skillPower;
    public int skillCount = 0;

    public Transform playerDialogTrans;
    public Transform enemyDialogTrans;

    public Transform playerResultTrans;
    public Sprite correct;
    public Sprite miss;
    public float resultDelay;

    public Transform pocket;
    //public Sprite pocketSprite;
    //public Sprite pocketOnSprite;

    public SpellManager SM;
    public Player player;
    public Enemy enemy;

    //随机数发生器
    // private Random rand;

    //音乐播放
    public AudioClip BGM;
    public AudioClip[] soundEffects;
    public AudioSource BGMPlayer;
    public AudioSource effectPlayer;
    public AudioSource spellPlayer;
    public float spellPitch = 3;

    private void Awake()
    {
        //singleton pattern
        if (!instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            DestroyImmediate(this);
        }
    }

    private void Start()
    {
        //worldState = defaultState;
        //isSpelling = true;
        OnEnemySpell();
        PlayBGM();
        spellPlayer.pitch = spellPitch;
    }

    public void OnSpellSuccess(int spellIndex)
    {
        //isSpelling = false;
        worldState = WorldState.afterSpell;
        Debug.Log("Spell Success!");

        curCharIndex = 0;
        SM.spellProgressBar.SetProgressValue(0);

        playerResultTrans.GetComponent<SpriteRenderer>().sprite = correct;
        playerDialogTrans.Find("SpellCanvas").gameObject.SetActive(false);
        playerDialogTrans.Find("SpellCanvas/Spell" + curSpellIndex).gameObject.SetActive(false);
        playerResultTrans.gameObject.SetActive(true);

        PlayEffect(0);
        Invoke("DisablePlayerDialog", resultDelay);

        
        switch (spellIndex)
        {
            case 0:
                player.animator.Play("PlayerAttack");
                break;
            case 1:
                player.animator.Play("PlayerAttack");

                break;
            case 2:
                player.animator.Play("PlayerAttack");
                break;
            case 3:
                player.animator.Play("PlayerAttack");
                break;
            
        }

    }



    public void OnSpellTimeOut()
    {
        //isSpelling = false;
        worldState = WorldState.afterSpell;
        Debug.Log("Spell Time out!");

        curCharIndex = 0;
        SM.spellProgressBar.SetProgressValue(0);

        playerResultTrans.GetComponent<SpriteRenderer>().sprite = miss;
        playerDialogTrans.Find("SpellCanvas").gameObject.SetActive(false);
        playerDialogTrans.Find("SpellCanvas/Spell" + curSpellIndex).gameObject.SetActive(false);
        playerResultTrans.gameObject.SetActive(true);
        Invoke("DisablePlayerDialog", resultDelay);

        player.animator.Play("PlayerMiss");
        PlayEffect(1);
    
    }

    public void OnEnemySpell()
    {
        worldState = WorldState.isEnemySpelling;
        Debug.Log("Enemy Spelling");


        enemySpellIndex = enemyFlow[enemyFlowCount % enemyFlow.Length];
        enemyFlowCount++;
        enemyDialogTrans.Find("SpellCanvas/Spell" + enemySpellIndex).gameObject.SetActive(true);
        enemyDialogTrans.gameObject.SetActive(true);

        spellPlayer.clip = enemySpell[enemySpellIndex];
        spellPlayer.Play();


        Invoke("AfterEnemySpell", 2f);
    }

    public void AfterEnemySpell()
    {
        worldState = WorldState.afterEnemySpell;
        Debug.Log("After Enemy Spell");

        enemyDialogTrans.gameObject.SetActive(false);
        enemyDialogTrans.Find("SpellCanvas/Spell" + enemySpellIndex).gameObject.SetActive(false);

        switch (enemySpellIndex)
        {
            case 0:
                enemy.animator.Play("EnemyAttack");
                break;
            case 1:
                enemy.animator.Play("EnemyAttack");
                break;
            case 2:
                enemy.animator.Play("EnemyAttack");

                break;
            case 3:
                enemy.animator.Play("EnemyAttack");
                break;
            
        }

        //Invoke("WaitForChoose", 1f);
    }

    public void WaitForChoose()
    {
        worldState = WorldState.waitForChoose;
        Debug.Log("Wait For Choose Skill");

        //pocket.GetComponent<Image>().sprite = pocketOnSprite;

        if(skillCount == 0)
        {
            Invoke("OnEnemySpell", 1f);
        }
    }

    public void OnSelectSkill()
    {

        //Debug.Log("Select Skill");

        if (worldState != WorldState.waitForChoose)
        {
            return;
        }
        worldState = WorldState.isSpelling;
        Debug.Log("Spelling");

        //pocket.GetComponent<Image>().sprite = pocketSprite;

        playerDialogTrans.Find("SpellCanvas/Spell" + curSpellIndex).gameObject.SetActive(true);
        playerDialogTrans.Find("SpellCanvas/TimeBar/fill").gameObject.SetActive(true);
        playerDialogTrans.gameObject.SetActive(true);
        SM.spellProgressBar = playerDialogTrans.Find("SpellCanvas/Spell" + curSpellIndex + "/fill").GetComponent<ProgressBar>();
    }

    public void OnClickEnemySpell()
    {
        Transform skill = pocket.Find("Skill" + enemySpellIndex);
        if (!skill.gameObject.activeSelf)
        {
            pocket.Find("Skill" + enemySpellIndex).gameObject.SetActive(true);
            skillCount++;
        }
        
        Debug.Log("Clicked Enemy Spell");
    }

    public void DisablePlayerDialog()
    {
        playerDialogTrans.Find("SpellCanvas").gameObject.SetActive(true);
        playerResultTrans.gameObject.SetActive(false);
        playerDialogTrans.gameObject.SetActive(false);
    }


    //Audio Play
    public void PlayBGM()
    {
        //Debug.Log("BGM Play");
        BGMPlayer.clip = BGM;
        BGMPlayer.Play();
    }

    public void PauseBGM()
    {
        BGMPlayer.Pause();
    }
    public void UnPauseBGM()
    {
        BGMPlayer.UnPause();
    }
    public void StopBGM()
    {
        BGMPlayer.Stop();
    }

    public void PlayEffect(int effectId)
    {
        effectPlayer.clip = soundEffects[effectId];
        effectPlayer.Play();
    }


}
