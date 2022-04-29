using UnityEngine;

public class TemplateBullet : BaseProjectile
{
    public override void Fired(ProjectileMono projectile)
    {
    }

    public override void Collided(ProjectileMono projectile, Collision collision)
    {
        GameObject.Destroy(projectile.gameObject);
    }

    public override void Destroyed(ProjectileMono projectile)
    {
    }

    public override void FixedUpdate(ProjectileMono projectile)
    {
        projectile.GetComponent<Rigidbody>().useGravity = false;
    }
}
