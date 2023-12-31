using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class JohnMovents : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public GameObject BulletPrefab;
    public float Speed;
    public AudioClip deathJohn;
    public float JumpForce;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private Animator Animator;
    private Vector3 initialPosition;
    private bool Grounded;
    private float LastShoot;
    private int Health = 5;
    public static bool IsAlive { get; private set; } = true;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        initialPosition = transform.position;

        

        Debug.Log("Initial Position: " + initialPosition);
        Debug.Log("JohnMovents Start called");
    }

    void Update()
    {
        if (IsAlive)
        {
            Horizontal = Input.GetAxisRaw("Horizontal");

            if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            Animator.SetBool("running", Horizontal != 0.0f);
            Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
            if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
            {
                Grounded = true;
            }
            else Grounded = false;


            if (Input.GetKeyDown(KeyCode.W) && Grounded)
            {
                Jump();
            }

            if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.20f)
            {
                Shoot();
                LastShoot = Time.time;
            }
        }
    }



    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void FixedUpdate()
    {
        if (IsAlive && Time.timeScale > 0f)
        {
            Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
        }
    }

    public void Hit()
    {
        Health = Health - 1;
        Debug.Log("Health: " + Health);
        if (Health <= 0)
        {
            IsAlive = false;
            HandleDeath();
            Camera.main.GetComponent<AudioSource>().PlayOneShot(deathJohn);

             Debug.Log("HandleDeath called");

        }
    }

    private void HandleDeath()
    {
        Animator.SetBool("deathJohn", Health <= 0);
        gameOverScreen.ShowGameOverScreen();
    }

    public void RestartGame()
    {
        Health = 5;
        IsAlive = true;
        transform.position = initialPosition;  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Debug.Log("RestartGame called");
    }

    public void GoToMainMenu()
    {
        Health = 5;
        IsAlive = true;
        transform.position = initialPosition;  
        SceneManager.LoadScene("MainMenu");

        Debug.Log("GoToMainMenu called");
    }
}
