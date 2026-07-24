using Assets.Scripts.Entity;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    // ドメイン層(Entity)の値を読み取って表示するだけのView。逆方向の依存は持たせない。
    internal sealed class BattleStatusView : MonoBehaviour
    {
        [Header("Player HP")]
        [SerializeField] private Slider PlayerHpSlider;
        [SerializeField] private Text PlayerHpText;

        [Header("Player SP")]
        [SerializeField] private Slider PlayerSpSlider;
        [SerializeField] private Text PlayerSpText;

        [Header("Enemy HP")]
        [SerializeField] private Slider EnemyHpSlider;
        [SerializeField] private Text EnemyHpText;

        public void Refresh(PlayerEntity player, EnemyEntity enemy)
        {
            SetGauge(PlayerHpSlider, PlayerHpText, player.Hp.Current, player.Hp.Max);
            SetGauge(PlayerSpSlider, PlayerSpText, player.Sp.Current, player.Sp.Max);
            SetGauge(EnemyHpSlider, EnemyHpText, enemy.Hp.Current, enemy.Hp.Max);
        }

        private static void SetGauge(Slider slider, Text text, int current, int max)
        {
            if (slider != null)
            {
                slider.maxValue = max;
                slider.value = current;
            }

            if (text != null)
                text.text = $"{current}/{max}";
        }
    }
}
