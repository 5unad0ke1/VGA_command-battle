using Assets.Scripts.Entity;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    /// <summary>
    /// ドメイン層(Entity)の値を読み取って表示するだけの View。逆方向の依存は持たせない。
    /// </summary>
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

        /// <summary>
        /// 現在値でゲージを更新する。毎フレーム呼ばれる前提のポーリング方式なので、
        /// 重い処理や毎回の生成はここに置かない。
        /// </summary>
        public void Refresh(PlayerEntity player, EnemyEntity enemy)
        {
            SetGauge(PlayerHpSlider, PlayerHpText, player.Hp.Current, player.Hp.Max);
            SetGauge(PlayerSpSlider, PlayerSpText, player.Sp.Current, player.Sp.Max);
            SetGauge(EnemyHpSlider, EnemyHpText, enemy.Hp.Current, enemy.Hp.Max);
        }

        // 参照が未設定でも動くよう、null は黙って読み飛ばす。
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
