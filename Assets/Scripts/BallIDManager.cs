using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallIDManager : MonoBehaviour
{
    private static BallIDManager instance;

    private int ballIDCounter = 500;//‚×‚Â‚É500‚Å‚È‚­‚Ä‚à

    public static BallIDManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("BallIDManager").AddComponent<BallIDManager>();
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    public int GetNewBallID()
    {
        ballIDCounter++;
        return ballIDCounter;
    }
}
