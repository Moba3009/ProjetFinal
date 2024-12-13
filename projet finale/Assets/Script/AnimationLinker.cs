using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationLinker : MonoBehaviour
{
    public void ResetAttack()
    {
        GetComponentInParent<Player>().ResetAttack();
    }
}
