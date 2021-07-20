using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGoByeBye : MonoBehaviour
{
    public Animator playAnim;

    public MenuManager deathScreenStart;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DamagePlayer"))
        {
            EnemyDeath.enemyCount = 0;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            playAnim.Play("GlitchEffect");

            deathScreenStart.DeathScreen();

            //StartCoroutine(ActivateDeathScreen());
        }
    }

    IEnumerator ActivateDeathScreen()
    {
        yield return new WaitForSeconds(0.4f);

        deathScreenStart.DeathScreen();

    }
}
