using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator anim;
    public Weapon currentWeapon;
    public Transform hand;

    public bool isAttacking = false;
    public bool isHolding = false;
    public bool isPicking = false;

    public bool attackingAnimDone = true;
}
