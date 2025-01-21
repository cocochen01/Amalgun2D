using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Scriptable Objects/BulletData")]
public class BulletData : ScriptableObject
{
    public string bulletName;
    public GameObject bulletPrefab;

    [Header("Base")]
    public float bulletSpeed;
    public float bulletLifetime;

    [Header("Speed")]
    public bool enableCustomSpeedCurve = false;
    public AnimationCurve speedCurve;
    //public Func<float, float> customSpeedFunction;

    [Header("Oscillation")]
    public bool enableOscillation = false;
    public float oscillationFrequency = 0f;
    public float oscillationAmplitude = 0f;
    public Vector2 oscillationDirection = Vector2.up;

    [Header("Recoil")]
    public float recoilForce = 1f;
    //public float recoilDuration = .4f;
}
