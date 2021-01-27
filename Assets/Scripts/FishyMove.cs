using UnityEngine;

public class FishyMove : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    public void Move()
    {
        transform.position += Vector3.left * _speed * 1.5f * Time.deltaTime;

        if (transform.position.x < -16f)
        {
            float randomY = Random.Range(-2.2f, 4.6f);
            transform.position = new Vector3(20, randomY, 0);
        }
    }
}
