using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オレオレ関数群
/// </summary>
public static class Utils
{
  /// <summary>
  /// 乱数生成器
  /// </summary>
  private static System.Random rand = new System.Random();

  /// <summary>
  /// childの親をparentに設定する
  /// </summary>
  /// <param name="parent">親オブジェクト</param>
  /// <param name="child">子オブジェクト</param>
  public static void SetParent(GameObject parent, GameObject child)
  {
    //Debug.Log(parent + ", " + child);
    if (parent != null) child.transform.parent = parent.transform;
  }

  public static void SetMainCamera(Camera main)
  {
    foreach (var camera in Camera.allCameras)
    {
      camera.enabled = false;
    }
    main.enabled = true;
  }

  public static List<GameObject> GetAllChildren(GameObject obj)
  {
    List<GameObject> all = new List<GameObject>();
    GetChildren(obj, ref all);
    return all;
  }

  public static void GetChildren(GameObject obj, ref List<GameObject> children)
  {
    var cs = obj.GetComponentInChildren<Transform>();
    if (cs.childCount == 0) return;
    foreach (Transform c_o in cs)
    {
      children.Add(c_o.gameObject);
      GetChildren(c_o.gameObject, ref children);
    }
  }

  /// <summary>
  /// aとbの値を入れ替える
  /// </summary>
  /// <typeparam name="Type">なんでも</typeparam>
  /// <param name="a">変数1</param>
  /// <param name="b">変数2</param>
  public static void Swap<Type>(ref Type a, ref Type b)
  {
    Type temp = a;
    a = b;
    b = temp;
  }

  /// <summary>
  /// minとmaxの間でランダムな数値を返す
  /// maxは含まない
  /// </summary>
  /// <param name="min">最小値</param>
  /// <param name="max">最大値</param>
  /// <returns>ランダムな数</returns>
  public static float RandomRange(float min, float max)
  {
    return (float)(rand.NextDouble() * (max - min) + min);
  }

  /// <summary>
  /// minとmaxの間でランダムな数値を返す
  /// maxは含まない
  /// </summary>
  /// <param name="min">最小値</param>
  /// <param name="max">最大値</param>
  /// <returns>ランダムな数</returns>
  public static int RandomRange(int min, int max)
  {
    return rand.Next(min, max);
  }

  /// <summary>
  /// minとmaxの間でランダムな数値を返す
  /// maxは含まない
  /// ランダムの範囲は各要素を参照
  /// </summary>
  /// <param name="min">最小値</param>
  /// <param name="max">最大値</param>
  /// <returns>ランダムな数値のベクトル</returns>
  public static Vector3 RandomRange(Vector3 min, Vector3 max)
  {
    return new Vector3(
      (float)(rand.NextDouble() * (max.x - min.x) + min.x),
      (float)(rand.NextDouble() * (max.y - min.y) + min.y),
      (float)(rand.NextDouble() * (max.z - min.z) + min.z)
    );
  }

  /// <summary>
  /// epsの確率でtrueを返す
  /// </summary>
  /// <param name="eps">trueの確率</param>
  /// <returns>確率epsでのbool値</returns>
  public static bool RandomBool(float eps)
  {
    return (float)(rand.NextDouble()) < eps;
  }

  /// <summary>
  /// リストの中からランダムで要素を返す．
  /// </summary>
  /// <typeparam name="T">要素の型</typeparam>
  /// <param name="array">リスト</param>
  /// <returns>ランダムで選択された要素</returns>
  public static T RandomList<T>(IList<T> array) where T : class
  {
    if (array.Count == 0) return null;
    return array[RandomRange(0, array.Count)];
  }

  /// <summary>
  /// 1次元配列の中身を出力する
  /// </summary>
  /// <typeparam name="T">ToStringが定義された適当な型</typeparam>
  /// <param name="array">型Tの1次元配列</param>
  public static void PrintArray<T>(IList<T> array)
  {
    Debug.Log(GetArrayString<T>(array));
  }

  /// <summary>
  /// 1次元配列の中身を文字列にして返す
  /// </summary>
  /// <typeparam name="T">ToStringが定義された適当な型</typeparam>
  /// <param name="array">型Tの1次元配列</param>
  /// <returns>配列を文字列にしたもの</returns>
  public static string GetArrayString<T>(IList<T> array)
  {
    //nullなら空文字列を返す
    if (array == null)
    {
      return "";
    }

    string result = "";
    foreach (T item in array)
    {
      result += item.ToString() + "\n";
    }
    return result;
  }

  /// <returns>配列を文字列にしたもの</returns>
  public static string GetArrayStringNonReturn<T>(IList<T> array)
  {
    //nullなら空文字列を返す
    if (array == null)
    {
      return "";
    }

    string result = "";
    foreach (T item in array)
    {
      result += item.ToString() + ", ";
    }
    return result;
  }

  public static string GetDictionaryString<T1, T2>(IDictionary<T1, T2> dict)
  {
    if (dict == null) return "";

    string result = "";
    foreach (var item in dict)
    {
      result += $"{item.Key} : {item.Value}\n";
    }
    return result;
  }

  /// <summary>
  /// 呼び出し元情報を出力する
  /// </summary>
  public static void PrintStackTrace()
  {
    Debug.Log(System.Environment.StackTrace);
  }

  public static string GetStackTrace()
  {
    return System.Environment.StackTrace;
  }

  /// <summary>
  /// Enumを文字列に変換する
  /// </summary>
  /// <param name="t"></param>
  /// <param name="enumObject"></param>
  /// <returns></returns>
  public static string EnumToString(System.Type t, object enumObject)
  {
    return System.Enum.GetName(t, enumObject);
  }

  /// <summary>
  /// 文字列をEnumに変換する
  /// </summary>
  /// <typeparam name="EnumType">対象のEnum</typeparam>
  /// <param name="t">対象のEnum</param>
  /// <param name="enumString">文字列</param>
  /// <returns></returns>
  public static (bool, EnumType) StringToEnum<EnumType>(System.Type t, string enumString) where EnumType : System.Enum
  {
    bool success = true;
    EnumType result = (EnumType)(System.Enum.GetValues(t).GetValue(0));
    try
    {
      result = (EnumType)System.Enum.Parse(t, enumString);
    }
    catch (System.Exception)
    {
      success = false;
    }
    return (success, result);

  }

  /// <summary>
  /// 商と剰余を計算する．
  /// </summary>
  /// <param name="a">割られる数</param>
  /// <param name="b">割る数</param>
  /// <returns>Item1 : 商, Item2 : 剰余</returns>
  public static System.Tuple<int, int> Div(int a, int b)
  {
    int q = a / b;
    int p = a % b;
    return new System.Tuple<int, int>(q, p);
  }

  /// <summary>
  /// v2がv1とv3の間にあるかを判別する
  /// </summary>
  /// <typeparam name="T">型</typeparam>
  /// <param name="v1">値１</param>
  /// <param name="v2">値２（対象）</param>
  /// <param name="v3">値３</param>
  /// <returns></returns>
  public static bool CompareBetween<T>(T v1, T v2, T v3) where T : System.IComparable
  {
    return v1.CompareTo(v2) > 0 & v2.CompareTo(v3) > 0;
    //return v1 < v2 & v2 < v3;
  }

  /// <summary>
  /// Vector2IntをInt配列に変換する．
  /// </summary>
  /// <param name="vec"></param>
  /// <returns></returns>
  public static int[] Vector2IntToIntArray(Vector2Int vec)
  {
    return new int[] { vec.x, vec.y };
  }

  public static float[] ColorToFloatArray(Color color)
  {
    return new float[] { color.r, color.g, color.b, color.a };
  }

  /// <summary>
  /// ベクトルの値が不正でないか確認
  /// </summary>
  /// <param name="vec">ベクトル</param>
  /// <returns>true:正常，false:不正</returns>
  public static bool CheckVector3(Vector3 vec)
  {
    return !(
      float.IsNaN(vec.x) |
      float.IsNaN(vec.y) |
      float.IsNaN(vec.z) |
      float.IsInfinity(vec.x) |
      float.IsInfinity(vec.y) |
      float.IsInfinity(vec.z)
    );
  }
}
