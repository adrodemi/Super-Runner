
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rbPlayer;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool doubleJumpUsed = false;
    public float doubleJumpForce;
    public bool doubleSpeed = false;
    public GameManager gameManager;
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1f);
            doubleJumpUsed = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !doubleJumpUsed)
        {
            rbPlayer.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            playerAnim.Play("Running_Jump", 3, 0f);
            playerAudio.PlayOneShot(jumpSound, 1f);
            doubleJumpUsed = true;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            doubleSpeed = true;
            playerAnim.SetFloat("Speed_Multiplier", 2);
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && doubleSpeed)
        {
            doubleSpeed = false;
            playerAnim.SetFloat("Speed_Multiplier", 1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            transform.position = new Vector3(transform.position.x - 1, 0f, 0f);
            gameManager.defeatScreen.gameObject.SetActive(true);
        }
    }
}