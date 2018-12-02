using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AlStPlayerController : MonoBehaviour
{

    private Rigidbody2D rb2d;

    private bool facingRight = true;

    public float speed;
    public float jumpforce;


    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    private int count;

    //This handles an internal timer
    private float timer;
    private int wholetime;

    public Text countText;
    public Text endText;


    public AudioSource pickupSource;



    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        endText.text = "";
        SetCountText();


    }

    void Awake()
    {

        pickupSource = GetComponent<AudioSource>();

    }

    private void Update()
    {

    }


    // Update is called once per frame
    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        Debug.Log(isOnGround);


        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

        timer = timer + Time.deltaTime;
        if (timer >= 10)
        {
            endText.text = "You Lose!";
            StartCoroutine(ByeAfterDelay(2));
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();

        }
        if (other.gameObject.tag == "Pick Up")
        {
            pickupSource.Play();
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            endText.text = "You Win!";
            StartCoroutine(ByeAfterDelay(2));
        }
    }
    IEnumerator ByeAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        GameLoader.gameOn = false;
    }
}


