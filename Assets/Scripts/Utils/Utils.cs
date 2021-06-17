using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �I���I���֐��Q
/// </summary>
public static class Utils
{
  /// <summary>
  /// ����������
  /// </summary>
  private static System.Random rand = new System.Random();

  /// <summary>
  /// child�̐e��parent�ɐݒ肷��
  /// </summary>
  /// <param name="parent">�e�I�u�W�F�N�g</param>
  /// <param name="child">�q�I�u�W�F�N�g</param>
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
  /// a��b�̒l�����ւ���
  /// </summary>
  /// <typeparam name="Type">�Ȃ�ł�</typeparam>
  /// <param name="a">�ϐ�1</param>
  /// <param name="b">�ϐ�2</param>
  public static void Swap<Type>(ref Type a, ref Type b)
  {
    Type temp = a;
    a = b;
    b = temp;
  }

  /// <summary>
  /// min��max�̊ԂŃ����_���Ȑ��l��Ԃ�
  /// max�͊܂܂Ȃ�
  /// </summary>
  /// <param name="min">�ŏ��l</param>
  /// <param name="max">�ő�l</param>
  /// <returns>�����_���Ȑ�</returns>
  public static float RandomRange(float min, float max)
  {
    return (float)(rand.NextDouble() * (max - min) + min);
  }

  /// <summary>
  /// min��max�̊ԂŃ����_���Ȑ��l��Ԃ�
  /// max�͊܂܂Ȃ�
  /// </summary>
  /// <param name="min">�ŏ��l</param>
  /// <param name="max">�ő�l</param>
  /// <returns>�����_���Ȑ�</returns>
  public static int RandomRange(int min, int max)
  {
    return rand.Next(min, max);
  }

  /// <summary>
  /// min��max�̊ԂŃ����_���Ȑ��l��Ԃ�
  /// max�͊܂܂Ȃ�
  /// �����_���͈̔͂͊e�v�f���Q��
  /// </summary>
  /// <param name="min">�ŏ��l</param>
  /// <param name="max">�ő�l</param>
  /// <returns>�����_���Ȑ��l�̃x�N�g��</returns>
  public static Vector3 RandomRange(Vector3 min, Vector3 max)
  {
    return new Vector3(
      (float)(rand.NextDouble() * (max.x - min.x) + min.x),
      (float)(rand.NextDouble() * (max.y - min.y) + min.y),
      (float)(rand.NextDouble() * (max.z - min.z) + min.z)
    );
  }

  /// <summary>
  /// eps�̊m����true��Ԃ�
  /// </summary>
  /// <param name="eps">true�̊m��</param>
  /// <returns>�m��eps�ł�bool�l</returns>
  public static bool RandomBool(float eps)
  {
    return (float)(rand.NextDouble()) < eps;
  }

  /// <summary>
  /// ���X�g�̒����烉���_���ŗv�f��Ԃ��D
  /// </summary>
  /// <typeparam name="T">�v�f�̌^</typeparam>
  /// <param name="array">���X�g</param>
  /// <returns>�����_���őI�����ꂽ�v�f</returns>
  public static T RandomList<T>(IList<T> array) where T : class
  {
    if (array.Count == 0) return null;
    return array[RandomRange(0, array.Count)];
  }

  /// <summary>
  /// 1�����z��̒��g���o�͂���
  /// </summary>
  /// <typeparam name="T">ToString����`���ꂽ�K���Ȍ^</typeparam>
  /// <param name="array">�^T��1�����z��</param>
  public static void PrintArray<T>(IList<T> array)
  {
    Debug.Log(GetArrayString<T>(array));
  }

  /// <summary>
  /// 1�����z��̒��g�𕶎���ɂ��ĕԂ�
  /// </summary>
  /// <typeparam name="T">ToString����`���ꂽ�K���Ȍ^</typeparam>
  /// <param name="array">�^T��1�����z��</param>
  /// <returns>�z��𕶎���ɂ�������</returns>
  public static string GetArrayString<T>(IList<T> array)
  {
    //null�Ȃ�󕶎����Ԃ�
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

  /// <returns>�z��𕶎���ɂ�������</returns>
  public static string GetArrayStringNonReturn<T>(IList<T> array)
  {
    //null�Ȃ�󕶎����Ԃ�
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
  /// �Ăяo���������o�͂���
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
  /// Enum�𕶎���ɕϊ�����
  /// </summary>
  /// <param name="t"></param>
  /// <param name="enumObject"></param>
  /// <returns></returns>
  public static string EnumToString(System.Type t, object enumObject)
  {
    return System.Enum.GetName(t, enumObject);
  }

  /// <summary>
  /// �������Enum�ɕϊ�����
  /// </summary>
  /// <typeparam name="EnumType">�Ώۂ�Enum</typeparam>
  /// <param name="t">�Ώۂ�Enum</param>
  /// <param name="enumString">������</param>
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
  /// ���Ə�]���v�Z����D
  /// </summary>
  /// <param name="a">�����鐔</param>
  /// <param name="b">���鐔</param>
  /// <returns>Item1 : ��, Item2 : ��]</returns>
  public static System.Tuple<int, int> Div(int a, int b)
  {
    int q = a / b;
    int p = a % b;
    return new System.Tuple<int, int>(q, p);
  }

  /// <summary>
  /// v2��v1��v3�̊Ԃɂ��邩�𔻕ʂ���
  /// </summary>
  /// <typeparam name="T">�^</typeparam>
  /// <param name="v1">�l�P</param>
  /// <param name="v2">�l�Q�i�Ώہj</param>
  /// <param name="v3">�l�R</param>
  /// <returns></returns>
  public static bool CompareBetween<T>(T v1, T v2, T v3) where T : System.IComparable
  {
    return v1.CompareTo(v2) > 0 & v2.CompareTo(v3) > 0;
    //return v1 < v2 & v2 < v3;
  }

  /// <summary>
  /// Vector2Int��Int�z��ɕϊ�����D
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
  /// �x�N�g���̒l���s���łȂ����m�F
  /// </summary>
  /// <param name="vec">�x�N�g��</param>
  /// <returns>true:����Cfalse:�s��</returns>
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
