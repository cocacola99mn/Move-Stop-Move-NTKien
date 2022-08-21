using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject stopUI, arrow, enemyHolder;
    public Animation enemyHolderAnim;

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        arrow.SetActive(false);
        stopUI.SetActive(true);
        enemyHolder.SetActive(true);
        enemyHolderAnim.Play();
    }
}
