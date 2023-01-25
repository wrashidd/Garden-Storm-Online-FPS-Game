using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SingleFruitShot : Gun
{
 [SerializeField] private Camera cam;
 private PhotonView PV;
 [SerializeField] private GameObject fruitBullet;

 private void Awake()
 {
  PV = GetComponent<PhotonView>();
 }

 public override void Use()
 {
  //Shoot();
 }

 void Shoot()
 {
  Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
  ray.origin = cam.transform.position;
  if(Physics.Raycast(ray, out RaycastHit hit))
  {
   hit.collider.gameObject.GetComponent<IDamageable>()?.TakeFruit(((GunInfo)itemInfo).takeFruit);
   PV.RPC("RPC_Shoot", RpcTarget.All, hit.point, hit.normal);
  }

 
 }
 
 [PunRPC]
 void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
 {
  //Instantiate(bullet, firePoint.position, firePoint.rotation);
  //Debug.Log(hitPosition);
  Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);
  if (colliders.Length != 0)
  {
   GameObject fruitImpactObject = Instantiate(fruitImpactPrefab, hitPosition, Quaternion.LookRotation(hitNormal *0.001f, Vector3.up) * fruitImpactPrefab.transform.rotation);
   Destroy(fruitImpactObject, 2f);
   fruitImpactObject.transform.SetParent(colliders[0].transform);
  }
 }
 
}
