using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class GunHandler : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnFireChanged))]

    public bool isFiring { get; set; }

    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] Transform shootPoint;
    [SerializeField] LayerMask layerMask;
    float lastTimeFire;

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData networkInputData))
        {
            if (networkInputData.isFiring)
                Fire(networkInputData.aimLookVector);
        }
    }

    void Fire(Vector3 fireDirection)
    {
        if (Time.time - lastTimeFire < 0.15f)
            return;

        StartCoroutine(FireEffectCO());

        Runner.LagCompensation.Raycast(shootPoint.position, shootPoint.forward, 200, Object.InputAuthority, out var hitInfo, layerMask, HitOptions.IncludePhysX);
        float hitDistance = 100;
        bool isHitOtherplayer = false;

        if (hitInfo.Distance > 0)
            hitDistance = hitInfo.Distance;

        if (hitInfo.Hitbox != null)
        {
            if (Object.HasStateAuthority)
                hitInfo.Hitbox.transform.root.GetComponent<PlayerHealth>().TakeDamage(1);

            isHitOtherplayer = true;
        }
        else if (hitInfo.Collider != null)
        {

        }

        if (isHitOtherplayer)
            Debug.DrawRay(shootPoint.position, shootPoint.forward * hitDistance, Color.red, 1);
        else
            Debug.DrawRay(shootPoint.position, shootPoint.forward * hitDistance, Color.green, 1);

        lastTimeFire = Time.time;
    }

    IEnumerator FireEffectCO()
    {
        isFiring = true;
        particleSystem.Play();
        yield return new WaitForSeconds(0.09f);
        isFiring = false;
    }

    static void OnFireChanged(Changed<GunHandler> changed)
    { 
        bool isFiringCurrent = changed.Behaviour.isFiring;

        changed.LoadOld();

        bool isFiringOld = changed.Behaviour.isFiring;

        if (isFiringCurrent && !isFiringOld)
            changed.Behaviour.OnFireRemoteSide();

    }

    void OnFireRemoteSide()
    {
        if (!Object.HasInputAuthority)
            particleSystem.Play();
    }

}
