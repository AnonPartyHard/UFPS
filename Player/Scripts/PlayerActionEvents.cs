using UnityEngine;

//TODO: provide events through Scriptable object later, delete this class

public class PlayerActionEvents : MonoBehaviour
{
    public delegate void WeaponAction(Weapon weapon);

    public WeaponAction onWeaponChosed;
    public WeaponAction onWeaponEquiped;
    public WeaponAction onWeaponDropped;
    public WeaponAction onWeaponShot;
    public WeaponAction onReloadStart;
    public WeaponAction onReloadDone;

    public void ChoseWeapon(Weapon weapon)
    {
        onWeaponChosed?.Invoke(weapon);
    }

    public void EquipWeapon(Weapon weapon)
    {
        onWeaponEquiped?.Invoke(weapon);
    }

    public void DropWeapon(Weapon weapon)
    {
        onWeaponDropped?.Invoke(weapon);
    }

    public void WeaponShot(Weapon weapon)
    {
        onWeaponShot?.Invoke(weapon);
    }

    public void ReloadStart(Weapon weapon)
    {
        onReloadStart?.Invoke(weapon);
    }
    
    public void ReloadDone(Weapon weapon)
    {
        onReloadDone?.Invoke(weapon);
    }
}