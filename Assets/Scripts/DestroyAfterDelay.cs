using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager.UI;
using UnityEngine;

class DestroyAfterDelay : MonoBehaviour
{
    [SerializeField] private float DelayInSeconds;

    void Start()
    {
        StartCoroutine(StartDestroyTimer());
    }

    IEnumerator StartDestroyTimer()
    {
        yield return new WaitForSeconds(DelayInSeconds);
        GameObject.Destroy(gameObject);
    }
}
