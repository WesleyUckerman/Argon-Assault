using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float DelayTime = 2f ;
    [SerializeField] ParticleSystem Explosion;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{this.name} ** trigered by** {other.gameObject.name}");
        StartCrashSequence();
    }

    private void StartCrashSequence()
    {
        Invoke("ReloadScene", DelayTime);
        GetComponent<PlayerMovement>().enabled = false;
        Explosion.Play();
        GetComponent<MeshRenderer>().enabled = false;
    }

    void ReloadScene()
    {
        int Currentsceneindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(Currentsceneindex);
    }
}
