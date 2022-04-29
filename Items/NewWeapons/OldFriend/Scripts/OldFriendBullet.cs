using UnityEngine;

public class OldFriendBullet : BaseProjectile
{
	private Rigidbody rb;
	private float _pushForce;

	public override void Fired(ProjectileMono projectile)
	{
		rb = projectile.GetComponent<Rigidbody>();
		_pushForce = projectile.CustomFields.GetFloat("BulletPushForce");
		rb.AddForce(projectile.transform.up * _pushForce * Time.fixedDeltaTime, ForceMode.Impulse);
	}

	public override void Collided(ProjectileMono projectile, Collision collision)
	{
		rb.velocity = Vector3.zero;
		ObjectsPool.instance.ReturnObject(projectile.gameObject);
	}

	public override void Destroyed(ProjectileMono projectile)
	{
	}

	public override void FixedUpdate(ProjectileMono projectile)
	{
	}
}