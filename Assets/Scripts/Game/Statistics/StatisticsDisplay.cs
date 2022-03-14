using TMPro;
using UnityEngine;

namespace Statistics
{
    /// <summary>
    /// Displays statistics
    /// </summary>
    public class StatisticsDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _totalScoreTexts;
        [SerializeField] private TMP_Text[] _levelScoreTexts;
        [SerializeField] private TMP_Text[] _tanksKilledTexts;
        //[SerializeField] private GameObject _playerLifePrefab;
        //[SerializeField] private GameObject _playerLifePanel;

        [Space]

        [SerializeField] private GameObject _playerRemainingTanksUIContainer;

        /// <summary>
        /// Updates statistics on UI
        /// </summary>
        /// <param name="dataToSisplay"></param>
        public void UpdateDisplay(StatisticsData dataToSisplay)
        {
            foreach (TMP_Text item in _totalScoreTexts)
            {
                item.text =  dataToSisplay.TotalScore.ToString("000000");
            }

            foreach (TMP_Text item in _levelScoreTexts)
            {
                item.text = dataToSisplay.LevelScore.ToString("000000");
            }

            foreach (TMP_Text item in _tanksKilledTexts)
            {
                item.text = dataToSisplay.TanksKilled.ToString("000000");
            }

            displayPlayerTanksCount(dataToSisplay);
        }

        private void displayPlayerTanksCount(StatisticsData dataToSisplay)
        {
            /*for (int i = 0; i < dataToSisplay.LevelRemainingPlayerTanksCount; i++)
            {
                Instantiate(_playerLifePrefab, _playerLifePanel.transform.position, Quaternion.identity);
            }*/

            foreach (Transform item in _playerRemainingTanksUIContainer.transform)
            {
                item.gameObject.SetActive(false);
            }

            for (int i = 0; i < dataToSisplay.LevelRemainingPlayerTanksCount; i++)
            {
                _playerRemainingTanksUIContainer.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
