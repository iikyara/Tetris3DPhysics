using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateMinoForm")]
public class MinoForm : ScriptableObject
{
  public string Name = "x - Mino";
  public Vector3Int FormDim;
  /// <summary>
  /// ijkの一番若いところをxyzの基準として，相対的に中心を設定する．
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
