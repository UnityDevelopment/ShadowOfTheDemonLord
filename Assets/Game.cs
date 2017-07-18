using UnityEngine;

public class Game : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // Dice usage example

        //int[] results = Dice.Roll(Dice.DieType.D4, 5, Dice.SortOrder.Descending);

        //for (int i = 0; i < results.Length; i++)
        //{
        //    Debug.Log(results[i].ToString());
        //}


        // Explained mechanic usage example

        // player setup (nimble, strong, willful, stupid! :D)
        AttributeCollection playerAttributes = new AttributeCollection();

        playerAttributes.Add(new Attribute(Attribute.AttributeType.Agility, 9));    // -1 modifier
        playerAttributes.Add(new Attribute(Attribute.AttributeType.Intellect, 3));  // -7 modifier
        playerAttributes.Add(new Attribute(Attribute.AttributeType.Strength, 12));  // +2 modifier
        playerAttributes.Add(new Attribute(Attribute.AttributeType.Will, 7));       // -3 modifier

        // trigger an action
        Action action = new Action(playerAttributes, 5, 3);                         // 5 boons / 3 banes

        // fight a beasty
        action.Attack(4);                                                           // defense value 4

        // action.Challenge(10);                                                    // for  Challenge rather than Attack

        // output the action's outcome
        Debug.Log(action.Outcome.ToString());

        // output the dice rolls for the boons/banes (desired feature request)
        for (int i = 0; i < action.SituationalConcernResults.Length; i++)
        {
            Debug.Log(action.SituationalConcernResults[i].ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Dice usuage example

        // Debug.Log(Dice.RollDie(Dice.DieType.D12));
    }
}
