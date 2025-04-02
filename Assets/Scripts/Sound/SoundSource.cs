using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private AudioSource _audioSource;

    public void Play(AudioClip clip, float soundEffectVolume, float soundEffectPitchVariance)
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        CancelInvoke();
        _audioSource.clip = clip;
        _audioSource.volume = soundEffectVolume;
        _audioSource.Play();
        _audioSource.pitch = Mathf.Clamp(1f + UnityEngine.Random.Range(-soundEffectPitchVariance, soundEffectPitchVariance), 0.5f, 2.0f);

        Invoke(nameof(Disable), clip.length + 1);
    }

    public void Disable()
    {
        _audioSource.Stop();
        Destroy(this.gameObject);
    }

}
