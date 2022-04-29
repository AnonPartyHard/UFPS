public abstract class BaseWeapon
{
	public abstract void Pick(WeaponViewMono weapon);

	public abstract void Draw(WeaponViewMono weapon);
	
	public abstract void FireHold(WeaponViewMono weapon);

	public abstract void FireDown(WeaponViewMono weapon);

	public abstract void FireRelease(WeaponViewMono weapon);

	public abstract void AltFireHold(WeaponViewMono weapon);
	
	public abstract void AltFireDown(WeaponViewMono weapon);
	
	public abstract void AltFireRelease(WeaponViewMono weapon);

	public abstract void ReloadDown(WeaponViewMono weapon);
	
	public abstract void ReloadHold(WeaponViewMono weapon);
	
	public abstract void ReloadRelease(WeaponViewMono weapon);
	
	public abstract void Drop(WeaponViewMono weapon);

	public abstract void Update(WeaponViewMono weapon);
	public abstract void FixedUpdate(WeaponViewMono weapon);
}