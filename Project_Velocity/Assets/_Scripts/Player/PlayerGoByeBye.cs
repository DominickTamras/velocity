using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGoByeBye : MonoBehaviour
{
    public Animator playAnim;

    public MenuManager deathScreenStart;

    public GameObject playerHolder;

    public GameObject arm;

    public GameObject main_Camera;

    public GameObject disableUI;

    public MeleeAttack meleeDisable;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DamagePlayer"))
        {
            

            EnemyDeath.enemyCount = 0;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            playAnim.Play("GlitchEffect");

            FindObjectOfType<AudioManager>().PlaySound("Player_Death1");
            FindObjectOfType<AudioManager>().PlaySound("Player_Death2");
            meleeDisable.enabled = false;
            disableUI.SetActive(false); //Crosshair 
            

            PlayerHolderTurnOff();
            OverlayTurnOff();

            deathScreenStart.DeathScreen();

          
            //StartCoroutine(ActivateDeathScreen());
            
           
        }
    }


    public void PlayerHolderTurnOff()
    {
        playerHolder.SetActive(false);
        arm.SetActive(false);
        
    }

    public void OverlayTurnOff()
    {
        main_Camera.SetActive(false);
    }


    IEnumerator ActivateDeathScreen()
    {
        yield return new WaitForSeconds(2f);


    }
}
