using System.Collections.Generic;
using UnityEngine;

namespace AR.Birds
{
    [CreateAssetMenu(fileName = "DataBirds", menuName = "ScriptableObjects/Entities/DataBirds")]
    public class DataBirds : ScriptableObject
    {
        public List<BirdInfo> _listBirds;
    }

    [System.Serializable]
    public class BirdInfo
    {
        public TypeBirds typeBird;
        public Sprite sprite;
        public int scoreToOpen;
    }
}
