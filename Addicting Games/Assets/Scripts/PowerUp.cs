 using System.Collections;
 using UnityEngine;

public class PowerUp : MonoBehaviour
 {
     public float speedIncrease = 2.0f;
     public int maxSpeedUpgrades = 10;

    private bool collected = false;
     private int timesCollected = 0;

     void OnTriggerEnter2D(Collider2D other)
     {
         if (other.CompareTag("Player") && !collected)
         {
             PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
             {
                 // Increase the player's speed and update color
                 IncreasePlayerSpeed(player);
                 ChangePlayerColor(player);

                 // Mark the power-up as collected
                 collected = true;
                 timesCollected++;

                //If the player collected the power-up 10 times, change the color to blueish
                 if (timesCollected >= maxSpeedUpgrades)
                 {
                     ChangeToBlueishColor(player);
                 }
             }
         }
     }

     void IncreasePlayerSpeed(PlayerController player)
     {
          //Calculate the speed increase based on the number of times collected
         player.ApplySpeedIncrease(speedIncrease);
     }

     void ChangePlayerColor(PlayerController player)
     {
          //Change player's color to yellow
         player.ChangeColor(Color.yellow);
     }

     void ChangeToBlueishColor(PlayerController player)
    {
          //Change player's color to blueish
         player.ChangeColor(new Color(0.5f, 0.5f, 1.0f));
     }
 }
