using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponManager : MonoBehaviour
{
    
    public Animator rigController;
    public List<Transform> weaponOrder = new List<Transform>();
    public Weapon[] holdingWeapons;
    public List<Transform> weaponSlotsParents = new List<Transform>();
    public List<Transform> weaponSlots = new List<Transform>();
    public List<Transform> weaponPoses = new List<Transform>();
    public int activeWeaponIndex;
    int tempWeapon;
    bool inAction = false;
    bool isSwitching = false;
    float endAttackTime = 3f;
    float timer = 0;
    bool attackPose = false;
    bool a = false;
    public bool isAttacking;
    public AttackColliderController attackCollider;

    private void Start()
    {
        Weapon exixtingWeapon = GetComponentInChildren<Weapon>();
        if (exixtingWeapon) { Equip(exixtingWeapon); }
    }

    private void Update()
    {
        bool c = false;
        if (holdingWeapons[activeWeaponIndex]) { c = rigController.GetCurrentAnimatorStateInfo(0).IsName("AttackPose" + holdingWeapons[activeWeaponIndex].weaponName); }
        if (Input.GetMouseButtonDown(0) && holdingWeapons[activeWeaponIndex] && !inAction && !rigController.GetBool("Holster") && !a)
        {
            InitiateAttack();
        }
        else if (Input.GetKeyDown(KeyCode.X) && holdingWeapons[0] && !inAction && !c)
        {
            ToggleActiveWeapon();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1) && holdingWeapons[0] && !inAction)
        {
            if(activeWeaponIndex != 0) { SetActiveWeapon(0); }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && holdingWeapons[1] && !inAction)
        {
            if (activeWeaponIndex != 1) { SetActiveWeapon(1); }
        }
    }

    public Weapon GetActualWeapon()
    {
        return holdingWeapons[activeWeaponIndex];
    }

    public void InitiateAttack()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        inAction = true;
        var weapon = holdingWeapons[activeWeaponIndex];
        if (weapon)
        {
            isAttacking = true;
            rigController.SetTrigger("Attack");
            yield return new WaitUntil(WaitUntilAttack);
            while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                Debug.Log("IsAttacking");
                yield return new WaitForEndOfFrame();
            }

            isAttacking = false;
            attackPose = true;
            inAction = false;

            StartCoroutine(EndAttackPose());
        }
    }

    IEnumerator EndAttackPose()
    {
        timer = 0f;

        rigController.SetTrigger("AttackPose");

        while (timer < endAttackTime)
        {
            if (isAttacking) { yield break; }
            yield return timer += Time.deltaTime;
        }

        if (attackPose) { StartCoroutine(IdlePosAttack(false, false)); }
    }

    IEnumerator IdlePosAttack(bool holster, bool switchWeapon)
    {
        a = true;
        rigController.SetTrigger("BackToPose");
        yield return new WaitUntil(WaitUntilIdlePosAttack);
        while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            Debug.Log("IsStoppingAttacking");
            yield return new WaitForEndOfFrame();
        }

        a = false;
        attackPose = false;

        if(holster)
        {
            StartCoroutine(HolsterWeapon(activeWeaponIndex));
        }
        else if(switchWeapon)
        {
            if(activeWeaponIndex == 0)
            {
                SetActiveWeapon(1);
            }
            else
            {
                SetActiveWeapon(0);
            }
        }
    }

    bool WaitUntilIdlePosAttack()
    {
        return rigController.GetCurrentAnimatorStateInfo(0).IsName("IdlePose" + holdingWeapons[tempWeapon].weaponName);
    }

    bool WaitUntilAttack()
    {
        return rigController.GetCurrentAnimatorStateInfo(0).IsName("Attack" + holdingWeapons[tempWeapon].weaponName) || rigController.GetCurrentAnimatorStateInfo(0).IsName("Attack" + holdingWeapons[tempWeapon].weaponName + "1");
    }

    public void ToggleActiveWeapon()
    {
        bool isHolstered = rigController.GetBool("Holster");

        if(isHolstered)
        {
            StartCoroutine(EquipWeapon(activeWeaponIndex));
        }
        else
        {
            if(attackPose)
            {
                attackPose = false;
                IdlePosAttack(true, false);
            }
            else
            {
                StartCoroutine(HolsterWeapon(activeWeaponIndex));
            }
        }
    }

    public void Equip(Weapon newWeapon)
    {
        Weapon PW = holdingWeapons[0];  //PW = primary weapon
        Weapon SW = holdingWeapons[1];  //SW = secondary weapon
        int newWeaponSlotIndex;

        if(!PW)
        {
            Debug.Log("Arma Primária");
            newWeaponSlotIndex = 0;
            holdingWeapons[newWeaponSlotIndex] = newWeapon;
            holdingWeapons[newWeaponSlotIndex].weaponOrder = Weapon.WeaponOrder.Primary;
        }
        else if(!SW)
        {
            Debug.Log("Arma Secundária");
            newWeaponSlotIndex = 1;
            holdingWeapons[newWeaponSlotIndex] = newWeapon;
            holdingWeapons[newWeaponSlotIndex].weaponOrder = Weapon.WeaponOrder.Secondary;
        }
        else
        {
            return;
        }

        Weapon weapon = holdingWeapons[newWeaponSlotIndex];

        weapon.ColliderController(false);
        Destroy(weapon.GetComponent<Rigidbody>());
        weapon.transform.parent = weaponOrder[newWeaponSlotIndex];
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        SetSlotPosition(newWeaponSlotIndex);

        SetActiveWeapon(newWeaponSlotIndex);
    }

    void SetActiveWeapon(int weaponSlotIndex)
    {
        int weaponToEquipIndex = weaponSlotIndex;
        int weaponToHolsterIndex = activeWeaponIndex;
        StartCoroutine(SwitchWeapon(weaponToHolsterIndex, weaponToEquipIndex));
    }

    IEnumerator SwitchWeapon(int holsterIndex, int activeIndex)
    {
        isSwitching = true;
        if (!rigController.GetCurrentAnimatorStateInfo(0).IsName("WeaponEmpty")) { yield return StartCoroutine(HolsterWeapon(holsterIndex)); }
        activeWeaponIndex = activeIndex;
        yield return StartCoroutine(EquipWeapon(activeIndex));
    }

    IEnumerator HolsterWeapon(int index)
    {
        inAction = true;
        var weapon = holdingWeapons[index];
        weapon.attackCollider = null;
        if(weapon)
        {
            rigController.SetBool("Holster", true);
            tempWeapon = index;
            yield return new WaitUntil(GetAnimationHolster);
            while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                Debug.Log("1");
                yield return new WaitForEndOfFrame();
            }
            if (!isSwitching) { inAction = false; }
        }
    }

    bool GetAnimationHolster()
    {
        return rigController.GetCurrentAnimatorStateInfo(0).IsName("Holster" + holdingWeapons[tempWeapon].weaponName);
    }

    IEnumerator EquipWeapon(int index)
    {
        inAction = true;
        var weapon = holdingWeapons[index];
        weapon.attackCollider = attackCollider;
        if (weapon)
        {
            rigController.SetBool("Holster", false);
            rigController.Play("Equip" + weapon.weaponName);
            tempWeapon = index;
            yield return new WaitUntil(GetAnimationEquip);
            while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                Debug.Log("2");
                yield return new WaitForEndOfFrame();
            }
            inAction = false;
            isSwitching = false;
        }
    }

    bool GetAnimationEquip()
    {
        return rigController.GetCurrentAnimatorStateInfo(0).IsName("Equip" + holdingWeapons[tempWeapon].weaponName);
    }

    public void SetSlotPosition(int index)
    {
        Weapon weapon = holdingWeapons[index];
        Transform weaponPose = null;

        for (int weaponPoseIndex = 0; weaponPoseIndex < weaponPoses.Count; weaponPoseIndex++)
        {
            string weaponPositionGameObjectName = "Position" + weapon.weaponName;
            
            if (weaponPoses[weaponPoseIndex].gameObject.name == weaponPositionGameObjectName)
            {
                Debug.Log(index);
                Debug.Log(weaponPoses[weaponPoseIndex].gameObject.name);
                weaponPose = weaponPoses[weaponPoseIndex];
                break;
            }
        }

        rigController.Play(weapon.weaponName + weapon.weaponOrder);

        weaponSlots[index].SetParent(weaponPose, false);
        weaponSlots[index].SetParent(weaponSlotsParents[index], true);
    }
}
