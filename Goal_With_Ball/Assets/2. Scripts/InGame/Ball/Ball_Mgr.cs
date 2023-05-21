using System;
using UnityEngine;


public class Ball_Mgr : MonoBehaviour
{
    private void Start()
    {
        InGameSystem.instance.BallDie += PlayerBallDie;
    }

    private void PlayerBallDie()
    {
        this.gameObject.SetActive(false);
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Wall"))
            return;

        InGameSystem.instance.DecreaseLife();
    }

    private void OnDisable()
    {
        InGameSystem.instance.BallDie -= PlayerBallDie;

    }
}
