using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

    public class ClanPanelController : MonoBehaviour
    {
        public Button rankingButton;
        public Button clanButton;
        public ClanRankingPanel clanRanking;
        public MyClanPanel myClan;

        private void SetNewContent(ContentType contentType)
        {
            switch (contentType)
            {
                case ContentType.Ranking:
                    {
                        clanRanking.gameObject.SetActive(true);
                        myClan.gameObject.SetActive(false);
                        break;
                    }
                case ContentType.MyClan:
                    {
                        if (MenuManager.AccountInfo.Clan == null)
                            return;
                        clanRanking.gameObject.SetActive( false);
                        myClan.gameObject.SetActive(true);
                        break;
                    }
            }
        }

        void Start()
        {
            rankingButton.onClick.AddListener(OnRankingButtonClick);
            clanButton.onClick.AddListener(OnMyClanButtonClick);
            SetNewContent(ContentType.Ranking);
        }

        public void OnRankingButtonClick()
        {
            SetNewContent(ContentType.Ranking);
        }
        public void OnMyClanButtonClick()
        {
            SetNewContent(ContentType.MyClan);
        }

        public enum ContentType { Ranking, MyClan }
}
