using UnityEngine;

public abstract class BaseProjectile
{
    public abstract void Fired(ProjectileMono projectile);
    public abstract void FixedUpdate(ProjectileMono projectile);
    public abstract void Collided(ProjectileMono projectile, Collision collision);
    public abstract void Destroyed(ProjectileMono projectile);
}