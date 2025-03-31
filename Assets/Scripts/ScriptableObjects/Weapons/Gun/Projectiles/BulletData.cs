using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Scriptable Objects/BulletData")]
public class BulletData : ScriptableObject
{
    public string bulletName;
    public GameObject bulletPrefab;

    [Header("Base")]
    public float bulletMass = .05f;
    public float bulletLifetime = 1f;

    [Header("Size")]
    public float bulletSize = .2f;
    public bool enableCustomSizeCurve = false;
    public AnimationCurve sizeCurve;

    [Header("Speed")]
    public float bulletSpeed = 50f;
    public bool enableCustomSpeedCurve = false;
    public AnimationCurve speedCurve;
    //public Func<float, float> customSpeedFunction;

    [Header("Oscillation")]
    public bool enableOscillation = false;
    public float oscillationFrequency = 0f;
    public float oscillationAmplitude = 0f;

    [Header("Force")]
    public float bulletForce
    {
        get { return bulletSpeed * bulletMass; }
    }
}
