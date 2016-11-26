using UnityEngine;
using System.Collections;

// COMP3064 CRN13018 Assignment 1 
// Thursday, November 24, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212 kazuma.sato@georgebrown.ca

public class SFXController : MonoBehaviour {

    // Properties //

    [SerializeField]
    private AudioClip bullet;
    [SerializeField]
    private AudioClip beam;
    [SerializeField]
    private AudioClip crash;
    [SerializeField]
    private AudioClip item1;
    [SerializeField]
    private AudioClip item2;
    [SerializeField]
    private AudioClip explosion1;
    [SerializeField]
    private AudioClip explosion2;

    private AudioClip[] audioClips;

    // Methods //

    void Start(){
        audioClips = new AudioClip[] {
            bullet, beam, crash, item1, item2, explosion1, explosion2};
    }

    public void PlaySound(AudioClip clip){
        
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
    
    public void PlaySound(int index, Vector3 position){

        AudioSource.PlayClipAtPoint(audioClips[index], position);
    }
}
