using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;
    public Text win;
    public Text Lives;
    private int LivesValue = 3;
    public Text Lose;
    private bool facingRight = true;
    Animator anim;
    private int level = 1;
   
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        win.text = "";
        Lives.text = "Lives: " + LivesValue.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetBool("isMoving", true);
        if (scoreValue==4 && level == 1)
        {
            gameObject.transform.position = new Vector2(67f, 0.15f);
            level++;
            LivesValue = 3;
            Lives.text = "Lives: " + LivesValue.ToString();
        }
        if(scoreValue == 8 && level == 2)
        {
            win.text = "You win! Game by Leonardo Maranhao";
        }
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        if(rd2d.velocity == new Vector2(0.0f, 0.0f)){
            anim.SetBool("isMoving", false);
        }

        if (LivesValue==0)
        {
            Destroy(gameObject);
            Lose.text = "You lose,sorry!";
        }

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }   

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
         {
            Flip();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetBool("isGrounded", true);
        anim.SetBool("isJumping", false);
        if(collision.gameObject.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.gameObject);
        }    
        if(collision.gameObject.tag == "Enemy")
        {
            LivesValue -= 1;
            Lives.text = "Lives: " + LivesValue.ToString();
            Destroy(collision.gameObject);
            
        }    
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        anim.SetBool("isGrounded", true);
        anim.SetBool("isJumping", false);
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isGrounded", false);
                anim.SetBool("isJumping", true);
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }    
        }
    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

}
