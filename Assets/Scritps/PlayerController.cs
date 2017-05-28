using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	private float speedX = 0;
	private float speedY = 0;
	public float maxSpeed = 3;
	public float acceleration = 50;
	public float deceleration = 50;
	public float exp = 50;
	public float bulletDamage = 10;
	public float bulletSpeed = 10;
	public float healthRegen = 1;

	private float nextRegenTime = 0.0f;
	private float period = 1.0f;

	public GameObject canvas;
	public GameObject body;
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public GameObject partToRotate;
	public string name;
	// Use this for initialization
	void Start () {
		
	}

	public override void OnStartLocalPlayer() {
		Camera.main.GetComponent<CameraController> ().setTarget (gameObject.transform);
		body.GetComponent<SpriteRenderer> ().color = Color.green;
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > nextRegenTime) {
			nextRegenTime += period;
			Health health = gameObject.GetComponent<Health> ();

			if (health.currentHealth <= health.maxHealth) {
				if (health.maxHealth - health.currentHealth <= healthRegen) {
					health.currentHealth = health.maxHealth;
				} else {
					gameObject.GetComponent<Health> ().currentHealth += healthRegen;
					health.updateHealthBar (health.currentHealth);
				}
			}
		}

		if (!isLocalPlayer) {
			Destroy (canvas);
			return;
		}

		Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);
		Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
		float angle = AngleBetweenTwoPoints(mouseOnScreen, positionOnScreen);
		partToRotate.transform.rotation =  Quaternion.Euler (new Vector3(0f, 0f, angle));

		if (Input.GetKey ("w") && (speedY < maxSpeed)) {
			speedY = speedY + acceleration * Time.deltaTime;
		} else if (Input.GetKey ("s") && (speedY > -maxSpeed)) {
			speedY = speedY - acceleration * Time.deltaTime;
		} else {
			if (speedY > deceleration * Time.deltaTime) {
				speedY = speedY - deceleration * Time.deltaTime;
			} else if (speedY < -deceleration * Time.deltaTime) {
				speedY = speedY + deceleration * Time.deltaTime;
			} else {
				speedY = 0;
			}
		}

		if (Input.GetKey ("a") && (speedX > -maxSpeed)) {
			speedX = speedX - acceleration * Time.deltaTime;
		} else if (Input.GetKey ("d") && (speedX < maxSpeed)) {
			speedX = speedX + acceleration * Time.deltaTime;
		} else {
			if (speedX > deceleration * Time.deltaTime) {
				speedX = speedX - deceleration * Time.deltaTime;
			} else if (speedX < -deceleration * Time.deltaTime) {
				speedX = speedX + deceleration * Time.deltaTime;
			} else {
				speedX = 0;
			}
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			CmdFire();
		}

		if (Input.GetKey ("l")) {
			Debug.Log (gameObject.GetComponent<Experience>().totalXp);
		}

		if (Input.GetKey ("h")) {
			Debug.Log (gameObject.GetComponent<Health> ().currentHealth);
		}

		if (Input.GetKey ("i")) {
			Debug.Log(gameObject.GetComponent<Experience>().lv);
		}

		var posX = transform.position.x + speedX * Time.deltaTime;
		var posY = transform.position.y + speedY * Time.deltaTime;
		transform.position = new Vector3 (posX, posY, 0);

	}

	[Command]
	void CmdFire()
	{
		GameObject bulletGO = (GameObject)Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet> ();
		bullet.damage = bulletDamage;
		bullet.speed = bulletSpeed;
		bullet.owner = this.gameObject;
		NetworkServer.Spawn(bulletGO); 
		Destroy (bulletGO, 2.0f);
	}

	float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
		return Mathf.Atan2 (a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}
}