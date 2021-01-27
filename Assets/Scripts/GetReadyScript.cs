using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetReadyScript : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(RemoveAfterSeconds(2, gameObject));
    }
    IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
    {
        yield return new WaitForSeconds(2);
        obj.SetActive(false);
    }

}
