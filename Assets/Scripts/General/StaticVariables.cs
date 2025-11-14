using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StaticVariables : MonoBehaviour
{
    public static StaticVariables i;
    [SerializeField] private LayerMask whatIsPlayer, whatIsEnemy, whatIsDeadzone,
        whatIsCollectable, whatIsUI;
    [SerializeField] private AudioMixerGroup masterMixer, sfxMixer, musicMixer;

    private void Awake() 
    {
        i = this;
    }

    public LayerMask GetDeadzoneLayer() { return whatIsDeadzone; }
    public LayerMask GetPlayerLayer() { return whatIsPlayer; }
    public LayerMask GetEnemyLayer() { return whatIsEnemy; }
    public LayerMask GetCollectableLayer() { return whatIsCollectable; }
    public LayerMask GetUILayer(){ return whatIsUI; }
    public AudioMixerGroup GetMasterMixer(){ return masterMixer; }
    public AudioMixerGroup GetSFXMixer(){ return sfxMixer; }
    public AudioMixerGroup GetMusicMixer(){ return musicMixer; }

}
