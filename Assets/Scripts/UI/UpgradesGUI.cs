using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradesGUI : MonoBehaviour
    {

    
        [SerializeField] private GameObject buttonPrefab;
        [SerializeField] private GameObject tierPrefab;
    
        [SerializeField] private GameObject contentRoot;
        [SerializeField] private UpgradeData[] dataList;
        private GameObject[] _tiers;
        private int maxTier=5;

        private void BuildTree()
        {
            _tiers = new GameObject[maxTier];
        
            for (int i = 0; i < maxTier; i++)
            {
                var t = Instantiate(tierPrefab, contentRoot.transform, true);
                _tiers[i] = t;
            }
            for (int i = 0; i < dataList.Length; i++)
            {
                var b = Instantiate(buttonPrefab, _tiers[dataList[i].tier-1].transform, true);
                b.name = dataList[i].upgradeName;
                b.GetComponentInChildren<Text>().text = dataList[i].upgradeName;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            BuildTree();
        }


        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
