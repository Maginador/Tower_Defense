using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuildUI : MonoBehaviour
    {

        [SerializeField] private Text[] towers;
        [SerializeField] private Text[] costs;

        public void BuildTexts(PlayerPersistentData persistentData)
        {
            for(int i =0; i<towers.Length; i++)
            {
                towers[i].text = persistentData.GetTower(i).textName;
                costs[i].text = persistentData.GetTower(i).initialCost.ToString();
            
            }
        
        }
    }
}
