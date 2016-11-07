using UnityEngine;
using System.Collections;

public class SFXController : MonoBehaviour {

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

    void Start(){
        audioClips = new AudioClip[] {
            bullet, crash, item1, item2, explosion1, explosion2};
    }

    public void PlaySound(AudioClip clip){
        
        GetComponent<AudioSource>().PlayOneShot(clip);
    }

    public void PlaySound(int index, Vector3 position){

        AudioSource.PlayClipAtPoint(audioClips[index], position);
    }
}
