using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SoundAdder : MonoBehaviour
{
    public static SoundAdder instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private GameObject SoundPrefab;

    public void AddSound(AudioClip clip) // clip
    {
        CRS.s.StartC(AddSoundHelper(clip, 1f, Vector2.one, null));
    }
    public void AddSound(AudioClip clip, Vector2 PitchRange) // clip, pitch range
    {
        CRS.s.StartC(AddSoundHelper(clip, 1f, PitchRange, null));
    }
    public void AddSound(AudioClip clip, float Volume) // clip, volume
    {
        CRS.s.StartC(AddSoundHelper(clip, Volume, Vector2.one, null));
    }
    public void AddSound(AudioClip clip, float Volume, Vector2 PitchRange) // clip, volume, pitch range
    {
        CRS.s.StartC(AddSoundHelper(clip, Volume, PitchRange, null));
    }
    public void AddSound(AudioClip clip, float Volume, Vector2 PitchRange, Vector3 Pos) // clip, volume, pitch range, position
    {
        CRS.s.StartC(AddSoundHelper(clip, Volume, PitchRange, Pos));
    }

    private IEnumerator AddSoundHelper(AudioClip clip, float Volume, Vector2 PitchRange, Vector3? Pos)
    {
        var SoundClone = Instantiate(SoundPrefab);
        var AudioSource = SoundClone.GetComponent<AudioSource>();

        if (Pos != null)
        {
            SoundClone.transform.position = Pos.Value;
            AudioSource.spatialBlend = 1f; // 3D sound
        }

        float pitch = Random.Range(PitchRange.x, PitchRange.y);
        AudioSource.pitch = pitch;

        AudioSource.volume = Volume;

        AudioSource.clip = clip;
        AudioSource.Play();

        yield return new WaitForSeconds(clip.length);

        Destroy(SoundClone);
    }
}
