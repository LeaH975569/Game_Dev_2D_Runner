using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
//Lea's
public class HeroManager : MonoBehaviour
{
    //public variables
    public GameObject Hero;
    public float movefactor;
    public SpriteRenderer HeroSprite;
    public float jumpforce;
    public float jumpspeed;
    public float forwardforce;
    public float flyforce;

    //private variables
    private Vector2 currentPosition;
    private Animator anim;
    private Rigidbody2D rb2;
    private bool onground = false;

    public string StartScene;
    public string GameOverScene;
    public string GameScene;

    public AudioSource audioSource;
    public AudioClip backgroundMusic;
    public AudioClip FuelcanSound;
    public AudioClip PotionSound;
    public AudioClip DamageSound;
    public AudioClip BulletSound;
    public AudioClip DecoSound;
    public float volume = 0.5f;
    public float soundEffectVolume = 1f;
    public Button muteButton;
    private bool isMuted = false;

    public Button PauseButton;
    public Button PlayButton;
    public Button ResetButton;
    public Button QuitButton;

    private int index;
    public int lives; // decide how many lives to start with 
    public TextMeshProUGUI livestext; // where we reference text
    public TextMeshProUGUI fuelCanText;
    public int fuelCanCount = 0;
    public TextMeshProUGUI distanceText;
    private float totalDistance = 0f;

    public GameObject bullet;
    public float bulletmove;
    private Vector2 bulletposition;
    public float delaycount;
    private float timer;
    public GameObject gunPivot;
    public float gunRotationSpeed = 5f;
    private bool gun1Triggered;
    private bool bulletHitHero = false;
    private float bulletFiredTime = 0f;
    public float bulletTimeout = 2f;
    private Vector3 bulletTarget;


    public float flashtime;
    Color origcolour;
    public int potionLifeIncrease = 1;


    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        livestext.text = lives.ToString();
        fuelCanCount = 0;

        HeroSprite = Hero.GetComponent<SpriteRenderer>();
        anim = Hero.GetComponent<Animator>();
        rb2 = Hero.GetComponent<Rigidbody2D>();
        rb2.freezeRotation = true;
        anim.speed = 0;

        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.Play();
        Button muteBtn = muteButton.GetComponent<Button>();
        muteBtn.onClick.AddListener(ToggleMuteMusic);

        Button Pause = PauseButton.GetComponent<Button>();
        Pause.onClick.AddListener(TaskOnClickPause);
        Button Play = PlayButton.GetComponent<Button>();
        Play.onClick.AddListener(TaskOnClickPlay);
        Button resetbtn = ResetButton.GetComponent<Button>();
        resetbtn.onClick.AddListener(TaskOnClickReset);
        Button Quit = QuitButton.GetComponent<Button>();
        Quit.onClick.AddListener(TaskOnClickQuit);

        origcolour = HeroSprite.material.color;

        gun1Triggered = false;
    }


    void OnCollisionEnter2D(Collision2D hit)
    {
        onground = true;
        Debug.Log("Hero has collided with ground");

        if (hit.gameObject.tag == "Fuel Can")
        {
            Debug.Log("Fuel Can hit");
            flashstartFuel();
            IncreaseFuelCanCount();
            Debug.Log("Fuel can +1");
            hit.gameObject.SetActive(false);
            PlayFuelcanSound();
        }


        if (hit.gameObject.tag == "Deco")
        {
            Debug.Log("Deco hit");
            hit.gameObject.SetActive(false);
            PlayDecoSound();
        }


        if (hit.gameObject.tag == "Potion")
        {
            Debug.Log("Potion hit");
            flashstartPotion();
            IncreaseLives();
            Debug.Log("lives +1");
            Debug.Log("return to flashstart routine");
            hit.gameObject.SetActive(false);
            PlayPotionSound();
        }

        if (hit.gameObject.tag == "Damage")
        {
            Debug.Log("Damage");
            flashstart();
            Debug.Log("return to flashstart routine");
            decreaseLives(); // Decrease lives when hit by a laser
            PlayDamageSound();
        }

    }


    void OnTriggerEnter2D(Collider2D hit)
    {
        onground = true;
        Debug.Log("Hero has collided with ground");

        if (hit.gameObject.tag == "BulletTrigger1")
        {
            hit.gameObject.SetActive(false);
            Debug.Log("Gun Triggered");
            bullet.SetActive(true);
            gun1Triggered = true;
            bulletTarget = transform.position;
            //bullet2Target = transform.position;
        }

        if (hit.gameObject.tag == "Bullet")
        { 
            bullet.SetActive(false);
            Debug.Log("Bullet hit");
            flashstart();
            Debug.Log("return to flashstart routine");
            decreaseLives(); // Decrease lives when hit by a laser
            hit.gameObject.SetActive(false);
            PlayBulletSound();
        }

    }

    void firegun()
    {
        if (delaycount < timer)
        {
            timer = timer + 1;
        }
        else
        {
            Debug.Log("bullet Towards");
            bulletposition = bullet.transform.position;
            bullet.transform.position = Vector3.MoveTowards(bulletposition, bulletTarget, Time.deltaTime * bulletmove);

            // Check if the bullet has hit the hero
            if (!bulletHitHero)
            {
                // Check if 2 seconds have passed since the bullet was fired
                if (Time.time - bulletFiredTime >= bulletTimeout)
                {
                    // Disable the bullet
                    //]bullet.SetActive(false);
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (gun1Triggered == true)
        {
            firegun();
        }
        //FireGun();

        if (Input.GetKey("right"))
        {
            anim.ResetTrigger("Freeze");
            anim.ResetTrigger("Jump");
            anim.SetTrigger("Moving");
            HeroSprite.flipX = false;
            anim.speed = 1;
            Vector2 currentPosition = Hero.transform.position;
            currentPosition.x = currentPosition.x + movefactor;
            Hero.transform.position = currentPosition;

            
            // Rotate the gun pivot to face away from the hero
            Vector3 gunDirection = gunPivot.transform.position - Hero.transform.position;
            float gunAngle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
            Quaternion gunRotation = Quaternion.AngleAxis(gunAngle, Vector3.forward);
            gunPivot.transform.rotation = gunRotation;
             

        }
        else if (Input.GetKey("left"))
        {
            anim.ResetTrigger("Freeze");
            anim.ResetTrigger("Jump");
            anim.SetTrigger("Moving");
            HeroSprite.flipX = true;
            anim.speed = 1;
            Vector2 currentPosition = Hero.transform.position;
            currentPosition.x = currentPosition.x - movefactor;
            Hero.transform.position = currentPosition;

             
            // Rotate the gun pivot to face away from the hero
            Vector3 gunDirection = gunPivot.transform.position - Hero.transform.position;
            float gunAngle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
            Quaternion gunRotation = Quaternion.AngleAxis(gunAngle, Vector3.forward);
            gunPivot.transform.rotation = gunRotation;
             

        }
        else if (!Input.GetKey("left") && !Input.GetKey("right"))
        {
            anim.ResetTrigger("Moving");
            anim.SetTrigger("Freeze");
        }

        if (Input.GetKeyDown("up"))
        {
            anim.ResetTrigger("Freeze");
            anim.SetTrigger("Jump");
            Debug.Log("Up key pressed");
            rb2.AddForce(new Vector2(forwardforce, jumpforce), ForceMode2D.Impulse);
            anim.ResetTrigger("Jump");
        }

        // Update the totalDistance when moving right or left
        if (Input.GetKey("right") || Input.GetKey("left"))
        {
            totalDistance += movefactor * Time.deltaTime;
        }

        distanceText.text = (totalDistance * 100).ToString("F0");

    }


    //damage to player flash
    void flashstart()
    {
        Debug.Log("change colour");
        HeroSprite.material.color = new Color(1, 0, 0, 1);
        Invoke("flashstop", flashtime);
    }

    void flashstartFuel()
    {
        Debug.Log("change colour");
        HeroSprite.material.color = new Color(0, 1, 0, 1);
        Invoke("flashstop", flashtime);
    }

    void flashstartPotion()
    {
        Debug.Log("change colour");
        HeroSprite.material.color = new Color(0, 0, 1, 1);
        Invoke("flashstop", flashtime);
    }

    void flashstop()
    {

        HeroSprite.material.color = origcolour;

    }


    void TaskOnClickPause()
    {
        Debug.Log("You have clicked Pause Button!");
        Time.timeScale = 0;  // setting time to 0
    }

    void TaskOnClickPlay()
    {
        Debug.Log("You have clicked Play Button!");
        Time.timeScale = 1;  // setting time to 0
    }

    void TaskOnClickQuit()
    {
        Debug.Log("You have clicked Quit Button!");
        SceneManager.LoadScene(GameOverScene);
    }

    void TaskOnClickReset()
    {
        Debug.Log("You have clicked Restart Button!");
        SceneManager.LoadScene(GameScene);
    }

    public void ToggleMuteMusic()
    {
        if (isMuted == false)
        {
            audioSource.mute = true;
            isMuted = true;
        }
        else
        {
            audioSource.mute = false;
            isMuted = false;
        }
    }


    public void PlayFuelcanSound()
    {
        audioSource.PlayOneShot(FuelcanSound, soundEffectVolume);
    }

    public void PlayDamageSound()
    {
        audioSource.PlayOneShot(DamageSound, soundEffectVolume);
    }

    public void PlayPotionSound()
    {
        audioSource.PlayOneShot(PotionSound, soundEffectVolume);
    }

    public void PlayBulletSound()
    {
        audioSource.PlayOneShot(BulletSound, soundEffectVolume);
    }

    public void PlayDecoSound()
    {
        audioSource.PlayOneShot(BulletSound, soundEffectVolume);
    }

    // Take lives off the player
    public void decreaseLives()
    {
        lives = lives - 1;
        livestext.text = lives.ToString();
        flashstart();

        //Game Over - when life hits 0
        if (lives <= 0)
        {
            lives = 0;
            PlayerPrefs.SetFloat("TotalDistance", totalDistance); // Save the score as "FinalScore"
            SceneManager.LoadScene(GameOverScene); // Load the GameOver scene

            PlayerPrefs.SetInt("FuelCanCountKey", fuelCanCount); // Save the score as "FinalScore"
            SceneManager.LoadScene(GameOverScene); // Load the GameOver scene

        }

    }

    void IncreaseLives()
    {
        lives += potionLifeIncrease;
        livestext.text = lives.ToString();
    }

    void IncreaseFuelCanCount()
    {
        fuelCanCount++;
        // Update the TextMeshProUGUI to display the updated fuel can count
        fuelCanText.text = fuelCanCount.ToString();
    }




}



