using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public class ObjectSpawn : MonoBehaviour
{
    public float maxTime = 1;
    private float timer = 0;
    public GameObject pipe;
    public GameObject boat;
    public float height;

    public int removeAfterTime = 4;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newpipe = Instantiate(pipe);
        newpipe.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
        StartCoroutine(RemoveAfterTime(removeAfterTime, newpipe));
    }

    // Update is called once per frame
    public void Spawn()
    {
        if (timer > maxTime)
        {
            GameObject newpipe = Instantiate(pipe);
            newpipe.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
            StartCoroutine(RemoveAfterTime(removeAfterTime, newpipe));
            timer = 0;

            GameObject newboat = Instantiate(boat);
            newboat.transform.position = transform.position + new Vector3(-6f, 6.1f, 0);
            StartCoroutine(RemoveAfterTime(removeAfterTime, newboat));
            timer = 0;
        }



        timer += Time.deltaTime;
    }

    public void GameOver()
    {
        StopAllCoroutines();
    }

    IEnumerator RemoveAfterTime(int seconds, GameObject gm)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gm);
    } 
}

