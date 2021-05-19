using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimManager : MonoBehaviour
{
    [SerializeField] Animator anim;

    public void playPunch()
    {
        anim.SetTrigger("MeleeAttack");
    }
}
