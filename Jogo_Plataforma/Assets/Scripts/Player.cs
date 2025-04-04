using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator playerAnim;
    private Rigidbody2D rbPlayer;
    public float speed;
    private SpriteRenderer sr;
    public float jumpForce;
    public bool inFloor = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnim = GetComponent<Animator>(); 
        sr = GetComponent<SpriteRenderer>();
        rbPlayer = GetComponent<Rigidbody2D>();
    }
    private void fixedUpdate()
    {
        MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Jump();
    }
    void MovePlayer()
    {
        float horizontalMoviment = Input.GetAxisRaw("Horizontal");
        //Debug.Log(horizontalMoviment);
        //transform.position += new Vector3(horizontalMoviment * Time.deltaTime * speed,0,0);
        rbPlayer.linearVelocity = new Vector2(horizontalMoviment * speed,rbPlayer.linearVelocity.y); 

        if (horizontalMoviment > 0)
        {
            playerAnim.SetBool("Walk", true);
            sr.flipX = false;
        }

        else if (horizontalMoviment < 0)
        {
            playerAnim.SetBool("Walk", true);
            sr.flipX = true;
        }
        else
        {
            playerAnim.SetBool("Walk", false);
        }

    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && inFloor)
        {
            playerAnim.SetBool("Jump", true);
            rbPlayer.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
            inFloor = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            playerAnim.SetBool("Jump", false);
            inFloor = true;

        }
    }
}
