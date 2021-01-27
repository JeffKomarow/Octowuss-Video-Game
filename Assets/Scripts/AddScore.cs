using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


public class AddScore : MonoBehaviour
{
    [SerializeField]
    private AudioClip _scoreSoundClip;
    private AudioSource _audioScource;
    private Score _score;


    void Start()
    {
        _score = FindObjectOfType<Score>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _score.AddScore(1);
        _audioScource = GetComponent<AudioSource>();

        if (_audioScource == null)
        {
            Debug.LogError("AudioSource on the player is null!");
        }
        else
        {
            _audioScource.clip = _scoreSoundClip;
            _audioScource.Play();
        }
    }
}
