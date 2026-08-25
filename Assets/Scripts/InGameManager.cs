using Assets.Scripts.Entity;
using Assets.Scripts.State;
using Assets.Scripts.UI;
using Assets.Scripts.ValueObject;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    /// <summary>
    /// バトル一式の合成ルート兼 State マシンのホスト。
    /// Entity・Command・State の生成と配線をここに集約し、以降は現在の State を毎フレーム駆動するだけ。
    /// MonoBehaviour への依存をこのクラスに閉じ込め、下位のレイヤーは素の C# に保つ。
    /// </summary>
    public sealed class InGameManager : MonoBehaviour, IStateController
    {
        [Header("Command Buttons")]
        [SerializeField] private Button AttackCommandButton;
        [SerializeField] private Button SkillCommandButton;
        [SerializeField] private Button ItemCommandButton;

        [Header("Item Sub Buttons")]
        [SerializeField] private GameObject ItemButtonsParent;
        [SerializeField] private Button[] ItemButtons;
        [SerializeField] private Button ItemCancelButton;

        [Header("Player Stats")]
        [SerializeField] private int PlayerMaxHp = 30;
        [SerializeField] private int PlayerMaxSp = 3;
        [SerializeField] private int PlayerNormalAttack = 5;
        [SerializeField] private int PlayerSkillAttack = 8;

        [Header("Enemy Stats")]
        [SerializeField] private int EnemyMaxHp = 30;
        [SerializeField] private int EnemyAttack = 4;

        [Header("Items")]
        [SerializeField] private ItemConfig[] Items = Array.Empty<ItemConfig>();

        [Header("Status GUI")]
        [SerializeField] private BattleStatusView StatusView;

        /// <summary>インスペクタでどうぐを設定するための入れ物。実行時は <see cref="ItemData"/> に変換する。</summary>
        [Serializable]
        private struct ItemConfig
        {
            public string Name;
            public int HealAmount;
        }

        private void Start()
        {
            var player = new PlayerEntity(
                new HealthPoint(PlayerMaxHp, PlayerMaxHp),
                new SkillPoint(PlayerMaxSp, PlayerMaxSp),
                new AttackPoint(PlayerNormalAttack),
                new AttackPoint(PlayerSkillAttack),
                new Inventory(Items.Select(i => new ItemData(i.Name, i.HealAmount))));

            var enemy = new EnemyEntity(
                new HealthPoint(EnemyMaxHp, EnemyMaxHp),
                new AttackPoint(EnemyAttack));

            // 依存の少ない State から順に生成する。
            var battleEndState = new BattleEndState();
            var actionResolveState = new ActionResolveState(this, player, enemy, battleEndState);
            var enemyActionState = new EnemyActionState(this, enemy, player, actionResolveState);
            var itemSelectState = new ItemSelectState(ItemButtonsParent, this, ItemButtons, ItemCancelButton, actionResolveState);
            var playerCommandState = new PlayerCommandState(
                this,
                player,
                enemy,
                AttackCommandButton,
                SkillCommandButton,
                ItemCommandButton,
                itemSelectState,
                actionResolveState,
                enemyActionState);

            // PlayerCommandState とは相互参照になるため、コンストラクタでは渡せない分をここで配線する。
            enemyActionState.Configure(playerCommandState);
            itemSelectState.Configure(playerCommandState, enemyActionState);

            _player = player;
            _enemy = enemy;

            ChangeState(playerCommandState);
            StatusView.Refresh(_player, _enemy);
        }

        private void Update()
        {
            _currentState?.Update();
            StatusView.Refresh(_player, _enemy);
        }

        public void ChangeState(IState next)
        {
            // Init() の中からさらに ChangeState() が呼ばれても取り違えないよう、
            // 現在の State を差し替えてから Init() する。
            _currentState?.Exit();
            _currentState = next;
            _currentState.Init();
        }

        private IState _currentState;
        private PlayerEntity _player;
        private EnemyEntity _enemy;
    }
}
