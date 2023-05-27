using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RandomRatio
{
    FixedRatio = 10,
    Quater = 25,
    Half = 50,
    All = 100
}
public class RandomManager
{
    private float total = 0f;
    private float success = 0f;

    public void createRan(float value)
    {
        float temp = value;
        bool posflag = false;
        bool negflag = false;
        int count = 0;

        for (int i = 0; i < 10000; i++)
        {
            if (total >= 1)
            {
                if ((success / total) < value / 100f)
                {
                    if (!posflag)
                    {
                        temp = value * 2f;
                        posflag = !posflag;
                        negflag = false;
                    }
                }
                else
                {
                    if (!negflag)
                    {
                        temp = value / 2f;
                        negflag = !negflag;
                        posflag = false;
                        count++;
                    }
                }
            }
            if (Random.Range(0f, 100f) < temp)
            {
                success++;
            }
            total++;
        }
        Debug.Log("성공률 " + (success / total));
        Debug.Log("성공수: " + success);
        Debug.Log("표본수: " + total);
        Debug.Log("현재확률: " + temp);
        Debug.Log("count " + count);

    }

    public static int CreateRandomByValue(int value)
    {
        return Random.Range(1, value+1);
    }
    public static float CreateRandomByFloatValue(float value)
    {
        if (value <= 1f)
        {
            return Random.Range(value, 1f);
        }
        else
        {
            return Random.Range(1f, value);
        }
    }

    public static int CreateMonsterSpawnCount(int max)
    {
        int randomValue; //= CreateRandomByValue(100);
        int spawnCount = 1;

        for(int i = 0; i < max-1; i++)
        {
            randomValue = CreateRandomByValue((int) RandomRatio.All);
            if(randomValue <= (int)RandomRatio.Half)
            {
                spawnCount++;
            }
        }

        return spawnCount;
    }

    public static int ApplyFixedRatioValue(int value)
    {
        if (CreateRandomByValue((int) RandomRatio.All) <= (int)RandomRatio.Half) {
            return Random.Range(1, value + 1);
        } else
        {
            return -Random.Range(1, value + 1);
        }
    } 

    public static bool CheckWeight(int value)
    {
        if (value >= CreateRandomByValue((int)RandomRatio.All))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    

    

}
