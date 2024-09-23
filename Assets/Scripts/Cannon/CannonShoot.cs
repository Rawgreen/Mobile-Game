using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannon
{
    public class CannonShoot : MonoBehaviour
    {
        private GameObject projectilePrefab;
        private Transform shootingPoint;
        private CannonStats cannonStats;

        private float shootingSpeed;
        private bool canShoot = true;

        private void Start()
        {
            cannonStats = CannonManager.Instance.GetCannonStats();
            projectilePrefab = cannonStats.GetProjectilePrefab();
            shootingPoint = transform.Find("ShootingPoint");
            shootingSpeed = cannonStats.GetShootingSpeed();
        }

        private void Update()
        {
            GameObject closestEnemy = CannonManager.Instance.GetClosestEnemy();
            if (closestEnemy != null && canShoot)
            {
                StartCoroutine(AttackDelay());
                canShoot = false;
            }
        }

        private IEnumerator AttackDelay()
        {
            yield return new WaitForSeconds(shootingSpeed);
            canShoot = true;
            ShootProjectile();
        }

        private void ShootProjectile()
        {
            GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
            Projectile.ProjectileBehavior projectileBehavior = projectile.GetComponent<Projectile.ProjectileBehavior>();
            if (projectileBehavior != null)
            {
                projectileBehavior.SetDamage(cannonStats.GetDamage());
                projectileBehavior.SetProjectileSpeed(cannonStats.GetProjectileSpeed());
            }
        }
    }
}
