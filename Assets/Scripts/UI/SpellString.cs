using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SpellString : MonoBehaviour
{
    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(GameManager.instance.OnClickEnemySpell);
    }

    //private void OnClick()
    //{

    //    Debug.Log("Clicked Spell String");
    //}
    
}
