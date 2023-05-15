using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

// Roles
// - List of IDs
// - Only one ID is assassin (ex. id 3)
// - When creting the game, the list is shuffled
// - Each player on connection gets the first number on the list
// - Then pop the num from the list so it cant repeat again 

public class Roles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
     void Shuffle(List<GameObject> a)
         {
             // Loop array
             for (int i = a.Count - 1; i > 0; i--)
             {
                 // Randomize a number between 0 and i (so that the range decreases each time)
                 int rnd = UnityEngine.Random.Range(0, i);
     
                 // Save the value of the current i, otherwise it'll overwrite when we swap the values
                 GameObject temp = a[i];
     
                 // Swap the new and old values
                 a[i] = a[rnd];
                 a[rnd] = temp;
             }
     
             // Print
             for (int i = 0; i < a.Count; i++)
             {
                 Debug.Log(a[i]);
             }
         }
}
