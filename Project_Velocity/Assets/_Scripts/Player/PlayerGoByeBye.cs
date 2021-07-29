using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGoByeBye : MonoBehaviour
{
    public Animator playAnim;

    public MenuManager deathScreenStart;

    public GameObject playerHolder;

    public GameObject main_Camera;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DamagePlayer"))
        {
            EnemyDeath.enemyCount = 0;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            playAnim.Play("GlitchEffect");

            PlayerHolderTurnOff();
            OverlayTurnOff();

            deathScreenStart.DeathScreen();

            

            //StartCoroutine(ActivateDeathScreen());
        }
    }


    public void PlayerHolderTurnOff()
    {

        playerHolder.SetActive(false);
    }

    public void OverlayTurnOff()
    {
        main_Camera.SetActive(false);
    }


    IEnumerator ActivateDeathScreen()
    {
        yield return new WaitForSeconds(2f);

        deathScreenStart.DeathScreen();

    }
}
