using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource ambientSource;
    public AudioSource spaceSource;
    public AudioSource sfxSource;
    public AudioClip appearPanelSound;
    public AudioClip disappearPanelSound;
    public AudioClip zoomInSound;
    public AudioClip zoomOutSound;
    public AudioClip discoverSound;
    CameraController cameraController;

    void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    void Update()
    {
        spaceSource.volume = 1 - (Camera.main.fieldOfView / (cameraController.maxFov/3));
        ambientSource.volume = (Camera.main.fieldOfView / (cameraController.maxFov)) /2;
    }

    public void PlayAppearSound()
    {
        sfxSource.volume = 0.25f;
        sfxSource.pitch = 1f;
        sfxSource.PlayOneShot(appearPanelSound);
    }

    public void PlayDisappearSound()
    {
        sfxSource.volume = 0.25f;
        sfxSource.pitch = 1f;
        sfxSource.PlayOneShot(disappearPanelSound);
    }
    public void PlayZoomIn()
    {
        sfxSource.volume = 0.25f;
        sfxSource.pitch = 1f;
        sfxSource.PlayOneShot(zoomInSound);
    }

    public void PlayZoomOut()
    {
        sfxSource.volume = 0.25f;
        sfxSource.pitch = 1f;
        sfxSource.PlayOneShot(zoomOutSound);
    }

    public void PlayDiscoverSound()
    {
        sfxSource.volume = 0.25f;
        sfxSource.pitch = 1f;
        sfxSource.PlayOneShot(discoverSound);        
    }
}
