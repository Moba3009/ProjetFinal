using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Display : MonoBehaviour
{
    [SerializeField] private Image _skillCoolDown;
    [SerializeField] private FireballSkill _fireballSkill;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _skillCoolDown.fillAmount = _fireballSkill.GetCoolDownRatio();
    }
}
