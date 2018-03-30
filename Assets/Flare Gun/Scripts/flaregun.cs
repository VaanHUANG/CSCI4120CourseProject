using UnityEngine;
using System.Collections;

public class flaregun : MonoBehaviour {
	
	public Rigidbody flareBullet;
	public Rigidbody VaporBullet;
	public Rigidbody EarthBullet;
	public Transform barrelEnd;
	public GameObject muzzleParticles;
	public AudioClip flareShotSound;
	public AudioClip noAmmoSound;	
	public AudioClip reloadSound;	
	public int bulletSpeed = 2000;
	public int maxSpareRounds = 5;
	public int spareRounds = 3;
	public int currentRound = 0;
	public int bulletType=1;
	
	


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if(Input.GetButtonDown("Fire1") && !GetComponent<Animation>().isPlaying)
		{
			if(currentRound > 0){
				Shoot();
			}else{
				GetComponent<Animation>().Play("noAmmo");
				GetComponent<AudioSource>().PlayOneShot(noAmmoSound);
			}
		}
		if(Input.GetKeyDown(KeyCode.R) && !GetComponent<Animation>().isPlaying)
		{
			Reload();
			
		}

		//switch weapon
		if (Input.GetKeyDown (KeyCode.Alpha1)&& !GetComponent<Animation>().isPlaying) {
			Reload();
			bulletType = 1;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)&& !GetComponent<Animation>().isPlaying) {
			bulletType = 2;
			Reload();
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)&& !GetComponent<Animation>().isPlaying) {
			bulletType = 3;
			Reload();
		}

	}
	
	void Shoot()
	{
			currentRound--;
		if(currentRound <= 0){
			currentRound = 0;
		}
		
		
		
			GetComponent<Animation>().CrossFade("Shoot");
			GetComponent<AudioSource>().PlayOneShot(flareShotSound);
		
			
			Rigidbody bulletInstance;			

		if(bulletType==1){
			bulletInstance = Instantiate(flareBullet,barrelEnd.position,barrelEnd.rotation) as Rigidbody; //INSTANTIATING THE FLARE PROJECTILE
			
			
			bulletInstance.AddForce(barrelEnd.forward * bulletSpeed); //ADDING FORWARD FORCE TO THE FLARE PROJECTILE
			
			Instantiate(muzzleParticles, barrelEnd.position,barrelEnd.rotation);	//INSTANTIATING THE GUN'S MUZZLE SPARKS	
		}

		if(bulletType==2){
			bulletInstance = Instantiate(VaporBullet,barrelEnd.position,barrelEnd.rotation) as Rigidbody; //INSTANTIATING THE FLARE PROJECTILE


			bulletInstance.AddForce(barrelEnd.forward * bulletSpeed); //ADDING FORWARD FORCE TO THE FLARE PROJECTILE

			Instantiate(muzzleParticles, barrelEnd.position,barrelEnd.rotation);	//INSTANTIATING THE GUN'S MUZZLE SPARKS	
		}

		if(bulletType==3){
			bulletInstance = Instantiate(EarthBullet,barrelEnd.position,barrelEnd.rotation) as Rigidbody; //INSTANTIATING THE FLARE PROJECTILE


			bulletInstance.AddForce(barrelEnd.forward * bulletSpeed); //ADDING FORWARD FORCE TO THE FLARE PROJECTILE

			Instantiate(muzzleParticles, barrelEnd.position,barrelEnd.rotation);	//INSTANTIATING THE GUN'S MUZZLE SPARKS	
		}
	}
	
	void Reload()
	{
		if(spareRounds >= 1){
			GetComponent<AudioSource>().PlayOneShot(reloadSound);			
			spareRounds--;
			currentRound=5;
			GetComponent<Animation>().CrossFade("Reload");
		}
		
	}
}
