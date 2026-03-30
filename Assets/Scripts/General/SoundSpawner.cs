using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSpawner : MonoBehaviour
{
    [SerializeField] private AudioClip SoundClip;
    [SerializeField] private float Volume;
    [SerializeField] private Vector2 Pitch;
    [SerializeField] private bool AtPosition = false;

    private void OnEnable()
    {
        if (AtPosition)
        {
            SoundAdder.instance.AddSound(SoundClip, Volume, Pitch, transform.position);
        }
        else
        {
            SoundAdder.instance.AddSound(SoundClip, Volume, Pitch);
        }
    }
}
