using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEvent : MonoBehaviour
{
    public GameObject stopUI,moveUI, enemyHolder, nextLevelTrigger;
    public GameObject[] enemy;

    public Collider triggerCollider;

    public Transform arrow;

    public Animation enemyHolderAnim,arrowAnim;

    private void Update()
    {
        EndTutorialEvent();
    }

    private void OnTriggerEnter(Collider other)
    {
        arrow.gameObject.SetActive(false);            

        stopUI.SetActive(true);
        
        enemyHolder.SetActive(true);
        enemyHolderAnim.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        triggerCollider.enabled = false;
    }

    public void EndTutorialEvent()
    {
        if(enemy[0] == null && enemy[1] == null && enemy[2] == null)
        {
            arrowAnim.Play(GameConstant.ARROW_ANIM);
            arrow.gameObject.SetActive(true);
            moveUI.SetActive(true);
            stopUI.SetActive(false);
            nextLevelTrigger.SetActive(true);
        }
    }
}
