using UnityEngine;

public class Utils
{
    /// <summary>
    /// 根据权重随机获取对象
    /// </summary>
    /// <param name="weights">权重数组</param>
    /// <param name="objects">对象数组</param>
    /// <returns></returns>
    public static GameObject GetRandomObjectByWeight(int[] weights, GameObject[] objects)
    {
        if (weights.Length != objects.Length) return null;

        float totalWeight = CalculateTotalWeight(weights);

        float randomValue = UnityEngine.Random.Range(0, totalWeight);

        for (int i = 0; i < weights.Length; i++)
        {
            randomValue -= weights[i];
            if (randomValue <= 0) return objects[i];
        }

        return objects[objects.Length - 1];
    }

    public static int GetRandomObjectByWeight(int[] weights)
    {
        float totalWeight = CalculateTotalWeight(weights);

        float randomValue = UnityEngine.Random.Range(0, totalWeight);

        for (int i = 0; i < weights.Length; i++)
        {
            randomValue -= weights[i];
            if (randomValue <= 0) return i;
        }

        return 0;
    }

    /// <summary>
    /// 计算总权重
    /// </summary>
    /// <param name="weights">权重数组</param>
    /// <returns></returns>
    private static int CalculateTotalWeight(int[] weights)
    {
        int totalWeight = 0;

        foreach (int weight in weights)
        {
            totalWeight += weight;
        }

        return totalWeight;
    }

    /// <summary>
    /// 根据概率生成对象
    /// </summary>
    /// <param name="probability">概率</param>
    /// <param name="go">对象</param>
    /// <returns></returns>
    public static GameObject GetRandomObjectByProbability(int probability, GameObject go)
    {
        float randomValue = UnityEngine.Random.Range(0, 100);

        if (randomValue <= probability)
        {
            return go;
        }

        return null;
    }

    public static void DestroyAllChild(Transform tf)
    {
        int ctr = tf.childCount;
        for (int i = ctr - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(tf.GetChild(i).gameObject);
        }
    }

    public static Vector2 GetMouseScreenPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        return mousePos;
    }

    public static Vector2 ScreenPositionToWorldPosition(Vector2 sPoint)
    {
        return Camera.main.ScreenToWorldPoint(sPoint);
    }

    public static Vector2 ScreenPositionToCanvasPosition(Vector2 sPoint, Canvas canvas)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, sPoint, canvas.worldCamera, out Vector2 position);
        return position;
    }

    public static Vector2 ScreenPositionToLocalPosition(Vector2 sPoint, RectTransform rect)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, sPoint, Camera.main, out Vector2 position);
        return position;
    }

    public static Vector2 WorldPositionToUILocalPosition(Vector2 wPoint, RectTransform rect)
    {
        return rect.InverseTransformPoint(wPoint);
    }

    public static void FadeIn()
    {
        Time.timeScale = 1;
        Animator anim = GameObject.Find("Canvas").transform.Find("Fade").GetComponent<Animator>();
        anim.SetBool("FadeIn", true);
        anim.SetBool("FadeOut", false);
    }

    public static void FadeOut()
    {
        Time.timeScale = 1;
        Animator anim = GameObject.Find("Canvas").transform.Find("Fade").GetComponent<Animator>();
        anim.SetBool("FadeIn", false);
        anim.SetBool("FadeOut", true);
    }
}
