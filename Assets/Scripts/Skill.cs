using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Skill : MonoBehaviour
{
    public int skillIndex;
    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnSelectSkill);
    }

    private void OnSelectSkill()
    {
        GameManager.instance.curSpellIndex = skillIndex;
        GameManager.instance.OnSelectSkill();
    }
}
