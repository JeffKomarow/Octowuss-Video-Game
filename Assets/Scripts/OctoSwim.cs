using UnityEngine;
using Debug = UnityEngine.Debug;

public class OctoSwim : MonoBehaviour
{
    public GameManager gameManager;
    public float velocity = 1;
    private Rigidbody2D rb;

    [SerializeField]
    private AudioClip _scoreBeepSoundClip;
    [SerializeField]
    private AudioClip _bleepSoundClip;
    private AudioSource _audioScource;


    // Start is called before the first frame update
    void Start()
    {
        _audioScource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        if (_audioScource == null)
        {
            Debug.LogError("AudioSource on the player is null!");
        }
        else
        {
            _audioScource.clip = _scoreBeepSoundClip;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            //Jump
            rb.velocity = Vector2.up * velocity;
            _audioScource.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.GameOver();
        _audioScource.Play();
    }
}
