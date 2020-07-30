using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBar : MonoBehaviour
{
    public ProgressBar bar;
    public float timer;

    private GameManager GM;

    private void Start()
    {
        bar = GetComponent<ProgressBar>();
        GM = GameManager.instance;
    }

    private void OnEnable()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        bar.SetProgressValue(timer / GM.timeLimit);
        if (timer >= GM.timeLimit)
        {
            //调试用后门
            //GM.OnSpellSuccess(1);
            
            GM.OnSpellTimeOut();
            gameObject.SetActive(false);
        }
    }
}
