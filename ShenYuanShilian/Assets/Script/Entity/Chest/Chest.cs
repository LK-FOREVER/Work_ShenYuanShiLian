using System;
using System.Xml;
using UnityEngine;

public class Chest : Entity
{
    public Animator Anim { get; private set; }

    public ChestStateMachine StateMachine { get; private set; }

    public ChestIdleState IdleState { get; private set; }
    public ChestCoinState CoinState { get; private set; }
    public ChestCardState CardState { get; private set; }

    public bool IsDead
    {
        get
        {
            return isDead;
        }
        set
        {
            isDead = value;
        }
    }

    private bool isDead = false;

    protected override void Awake()
    {
        base.Awake();

        EventManager.Instance.AddListener(EventName.CharacterAttack, UnderAttack);
        EventManager.Instance.AddListener(EventName.PassTile, Disappear);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        EventManager.Instance.RemoveListener(EventName.CharacterAttack, UnderAttack);
        EventManager.Instance.RemoveListener(EventName.PassTile, Disappear);
    }

    public override void Init(int _id, int _level, int _characterId)
    {
        base.Init(_id, _level, _characterId);
        goodId = 0;

        Anim = GetComponent<Animator>();

        InitStateMachine();
    }

    private void InitStateMachine()
    {
        StateMachine = new ChestStateMachine();
        IdleState = new ChestIdleState(this, StateMachine, "Idle");
        CoinState = new ChestCoinState(this, StateMachine, "Coin");
        CardState = new ChestCardState(this, StateMachine, "Card");

        StateMachine.Initialize(IdleState);
    }

    protected override void CheckPlayerPos(object sender, EventArgs e)
    {
        base.CheckPlayerPos(sender, e);

        if (characterPos != id) return;

        this.TriggerEvent(EventName.CharacterMoveEnd, new CharacterMoveEndEventArgs() { id = goodId });
    }

    private void UnderAttack(object sender, EventArgs e)
    {
        CharacterAttackEventArgs args = e as CharacterAttackEventArgs;
        if (StateMachine.CurrentState != IdleState || args.pos != id) return;

        this.TriggerEvent(EventName.PlaySound, new PlaySoundEventArgs() { index = Sound.Chest });

        ChestWeight chestWeight = DataManager.Instance.chestWeightDic[level];
        int[] weights = new int[] { chestWeight.coin, chestWeight.card };
        int index = Utils.GetRandomObjectByWeight(weights);
        if(index == 0)
        {
            CoinInfo coinInfo = DataManager.Instance.coinInfoDic[level];
            int coin = UnityEngine.Random.Range(coinInfo.coinMin, coinInfo.coinMax + 1);
            StateMachine.ChangeState(CoinState);
            this.TriggerEvent(EventName.GetCoin, new GetCoinEventArgs() { coin = coin });
        }
        else if(index == 1)
        {
            CardWeight cardWeight = DataManager.Instance.cardWeightDic[level];
            int[] cardWeights = new int[] { cardWeight.equipment, cardWeight.commonBuff, cardWeight.exclusiveBuff };
            int card0 = Utils.GetRandomObjectByWeight(cardWeights);
            int card1 = Utils.GetRandomObjectByWeight(cardWeights);
            StateMachine.ChangeState(CardState);
            SpawnCard(card0);
            SpawnCard(card1);
        }
    }

    private void SpawnCard(int index)
    {
        if(index == 0)
        {
            EquipmentWeight equipmentWeight = DataManager.Instance.equipmentWeightDic[level];
            int[] equipmentWeights = new int[] {equipmentWeight.equipment0, equipmentWeight.equipment1, equipmentWeight.equipment2,
            equipmentWeight.equipment3,equipmentWeight.equipment4,equipmentWeight.equipment5,equipmentWeight.equipment6,equipmentWeight.equipment7,
            equipmentWeight.equipment8,equipmentWeight.equipment9,equipmentWeight.equipment10,equipmentWeight.equipment11,equipmentWeight.equipment12,
            equipmentWeight.equipment13};
            int equipmentId = Utils.GetRandomObjectByWeight(equipmentWeights);
            this.TriggerEvent(EventName.CreateCardOption, new CardOptionEventArgs() { cardType = CardType.Equipment, id = equipmentId });
        }
        else if(index == 1)
        {
            CommonBuffWeight commonBufWeight = DataManager.Instance.commonBuffWeightDic[level];
            int[] commonBuffWeights = new int[] {commonBufWeight.buff0, commonBufWeight.buff1, commonBufWeight.buff2, commonBufWeight.buff3,
            commonBufWeight.buff4,commonBufWeight.buff5,commonBufWeight.buff6,commonBufWeight.buff7,commonBufWeight.buff8,commonBufWeight.buff9,
            commonBufWeight.buff10,commonBufWeight.buff11,commonBufWeight.buff12};
            int commonBuffId = Utils.GetRandomObjectByWeight(commonBuffWeights);
            this.TriggerEvent(EventName.CreateCardOption, new CardOptionEventArgs() { cardType = CardType.CommonBuff, id = commonBuffId });
        }
        else
        {
            if (characterId == 0)
            {
                SoldierBuffWeight soldierBuffWeight = DataManager.Instance.soldierBuffWeightDic[level];
                int[] soldierBuffWeights = new int[] { soldierBuffWeight.buff0, soldierBuffWeight.buff1, soldierBuffWeight.buff2,
                soldierBuffWeight.buff3,soldierBuffWeight.buff4,soldierBuffWeight.buff5};
                int soldierBuffId = Utils.GetRandomObjectByWeight(soldierBuffWeights);
                this.TriggerEvent(EventName.CreateCardOption, new CardOptionEventArgs() { cardType = CardType.ExclusiveBuff, id = soldierBuffId });
            }
            else if (characterId == 1)
            {
                ArcherBuffWeight archerBuffWeight = DataManager.Instance.archerBuffWeightDic[level];
                int[] archerBuffWeights = new int[] { archerBuffWeight.buff0, archerBuffWeight.buff1, archerBuffWeight.buff2,
                archerBuffWeight.buff3,archerBuffWeight.buff4,archerBuffWeight.buff5};
                int archerBuffId = Utils.GetRandomObjectByWeight(archerBuffWeights);
                this.TriggerEvent(EventName.CreateCardOption, new CardOptionEventArgs() { cardType = CardType.ExclusiveBuff, id = archerBuffId });
            }
            else
            {
                MasterBuffWeight masterBuffWeight = DataManager.Instance.masterBuffWeightDic[level];
                int[] masterBuffWeights = new int[] {
                    masterBuffWeight.buff0,
                    masterBuffWeight.buff1,
                    masterBuffWeight.buff2,
                    masterBuffWeight.buff3
                };
                int masterBuffId = Utils.GetRandomObjectByWeight(masterBuffWeights);
                this.TriggerEvent(EventName.CreateCardOption, new CardOptionEventArgs() { cardType = CardType.ExclusiveBuff, id = masterBuffId });
            }
        }
    }

    public void Disappear(object sender, EventArgs e)
    {
        if (characterPos != id) return;
        Destroy(gameObject);
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();
    }
}
