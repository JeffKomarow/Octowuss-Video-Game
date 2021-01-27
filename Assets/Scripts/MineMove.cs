using UnityEngine;

public class MineMove : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    public int testValue;

    private GameManager _manager;

    private void Start()
    {
        _manager = FindObjectOfType<GameManager>();
        _manager.AddMine(this);
    }


    public void Move()
    {
        transform.position += Vector3.left * _speed * Time.deltaTime;
    }

    private void OnDestroy()
    {
        _manager.RemoveMine(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
