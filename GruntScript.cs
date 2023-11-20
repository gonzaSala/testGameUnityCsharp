using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntScript : MonoBehaviour
{
    public GameObject John;
    public AudioClip destroyGrunt;
    private Animator Animator;
    public GameObject BulletPrefab;
    private float LastShoot;
    private int Health = 3;



 void Start()
    {
        Animator = GetComponent<Animator>();
    }    void Update()
    {
        if (!JohnMovents.IsAlive || John == null) return;
        Vector3 direction = John.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(John.transform.position.x - transform.position.x);

        if (distance < 1.0f && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        if (JohnMovents.IsAlive)
        {
            Debug.Log("Shoot");
            Vector3 direction;
            if (transform.localScale.x == 1.0f) direction = Vector2.right;
            else direction = Vector2.left;

            GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
            bullet.GetComponent<BulletScript>().SetDirection(direction);
        }
    }
    public void Hit()
    {
        Health = Health - 1;
        if (Health <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        Animator.SetBool("GrauntDead", Health <= 0);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(destroyGrunt);

    }

    public void destroyEnemie(){
        Destroy(gameObject);
    }

    

}