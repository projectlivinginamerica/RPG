using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager.UI;
using UnityEngine;

class ScaleOverTime : MonoBehaviour
{
    [SerializeField] private AnimationCurve ScaleCurve;
    [SerializeField] private float ScaleDuration = 1.0f;
    [SerializeField] private float CurveMultiplier = 1.0f;

    private float StartTime;

    void Start()
    {
        StartTime = Time.time;
    }

    void Update()
    {
        float t = (Time.time - StartTime) / ScaleDuration;
        t = Mathf.Clamp01(t);
        float scaleVal = ScaleCurve.Evaluate(t) * CurveMultiplier;
        gameObject.transform.localScale = new Vector3(scaleVal, scaleVal, scaleVal);

    }
}
