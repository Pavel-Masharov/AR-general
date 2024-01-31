using UnityEngine;
using UnityEngine.UI;

public class CanvasDebuger : MonoBehaviour
{
    [SerializeField] private Text _textDebug;
    public void SetTextDebug(string messageDebug)
    {
        _textDebug.text = messageDebug;
    }
}
