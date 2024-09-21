using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannon
{
    public class CannonShoot : MonoBehaviour
    {
        private GameObject projectilePrefab;
        private Transform shootingPoint;
        private CannonManager cannonManager;
        private CannonAttributes cannonAttributes;

        private float shootingSpeed;
        private bool canShoot = true;


        private void Awake()
        {
            cannonManager = GetComponent<CannonManager>();
            cannonAttributes = GetComponent<CannonAttributes>();
            projectilePrefab = GetComponent<CannonAttributes>().GetProjectilePrefab();
            shootingPoint = GetComponent<CannonAttributes>().GetShootingPoint();
            shootingSpeed = GetComponent<CannonAttributes>().GetShootingSpeed();
        }

        private void Update()
        {
            GameObject closestEnemy = cannonManager.GetClosestEnemy();
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
                projectileBehavior.SetDamage(cannonAttributes.GetDamage());
                projectileBehavior.SetProjectileSpeed(cannonAttributes.GetProjectileSpeed());
            }
        }
    }
}
