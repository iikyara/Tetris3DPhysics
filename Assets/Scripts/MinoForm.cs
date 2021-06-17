using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateMinoForm")]
public class MinoForm : ScriptableObject
{
  public string Name = "x - Mino";
  public Vector3Int FormDim;
  /// <summary>
  /// ijk�̈�ԎႢ�Ƃ����xyz�̊�Ƃ��āC���ΓI�ɒ��S��ݒ肷��D
  /// </summary>
  public Vector3 Center;
  public Material Mat;
  [TextArea(19, 19)]
  public string Form =
    "----\n" +
    "----\n" +
    "----\n" +
    "----\n" +
    "\n" +
    "----\n" +
    "----\n" +
    "----\n" +
    "----\n" +
    "\n" +
    "----\n" +
    "----\n" +
    "----\n" +
    "----\n" +
    "\n" +
    "----\n" +
    "----\n" +
    "----\n" +
    "----";

  public bool[,,] GetForm()
  {
    bool[,,] form = new bool[FormDim.x, FormDim.y, FormDim.z];
    var form_lines = Form.Replace("\r\n", "\n").Split(new[] { '\n', '\r' });
    for (int i = 0; i < FormDim.x; i++)
    {
      for (int j = 0; j < FormDim.y; j++)
      {
        for (int k = 0; k < FormDim.z; k++)
        {
          if (form_lines[i * 4 + i + j].Substring(k, 1) == "o")
          {
            form[i, j, k] = true;
          }
          else
          {
            form[i, j, k] = false;
          }
        }
      }
    }
    return form;
  }
}
