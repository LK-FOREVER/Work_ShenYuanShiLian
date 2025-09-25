using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : Entity
{
    [SerializeField]
    private List<Sprite> sprites;
    [SerializeField]
    private List<SpriteRenderer> squares;

    [SerializeField]
    private GameObject[] monsters;
    [SerializeField]
    private GameObject chest;
    [SerializeField]
    private GameObject door;

    private bool empty = false;

    public override void Init(int _id, int _level, int _characterId)
    {
        base.Init(_id, _level, _characterId);
        goodId = 30;

        for(int i = 0; i < squares.Count; i++)
        {
            if(level > 20)
            {
                squares[i].sprite = sprites[2];
            } 
            else if(level >10)
            {
                squares[i].sprite = sprites[1];
            }
            else
            {
                squares[i].sprite = sprites[0];
            }
        }

        SpawnEntity();
    }

    private void SpawnEntity()
    {
        // if (id == 0) return;
        // if(level != DataManager.Instance.levelInfoDic.Count && id == 100 )
        // {
        //     GameObject go = ObjectPool.Instance.CreateObject(door, new Vector3(0, -6.2f, transform.position.z + 15), Quaternion.identity);
        //     go.GetComponent<Entity>().Init(id, level, characterId);
        //     return;
        // }

        LevelInfo info = DataManager.Instance.levelInfoDic[level];
        int[] levelWeights = new int[]{ info.monster, info.chest, info.empty };
        int index = Utils.GetRandomObjectByWeight(levelWeights);
        // if(index == 0)
        // {
            MonsterWeight monsterWeight = DataManager.Instance.monsterWeightDic[level];
            int[] monsterWeights = new int[] {monsterWeight.monster0, monsterWeight.monster1, monsterWeight.monster2, monsterWeight.monster3,
            monsterWeight.monster4,monsterWeight.monster5,monsterWeight.monster6,monsterWeight.monster7,monsterWeight.monster8,
            monsterWeight.monster9,monsterWeight.monster10,monsterWeight.monster11,monsterWeight.monster12,monsterWeight.monster13,
            monsterWeight.monster14,monsterWeight.monster15,monsterWeight.monster16,monsterWeight.monster17,monsterWeight.monster18,
            monsterWeight.monster19,monsterWeight.monster20,monsterWeight.monster21,monsterWeight.monster22,monsterWeight.monster23,
            monsterWeight.monster24,monsterWeight.monster25,monsterWeight.monster26,monsterWeight.monster27};
            GameObject monsterPrefab = Utils.GetRandomObjectByWeight(monsterWeights, monsters);
            GameObject monster = ObjectPool.Instance.CreateObject(monsterPrefab, new Vector3(0, -4f, transform.position.z + 12), Quaternion.identity);
            monster.GetComponent<Entity>().Init(id, level, characterId);
        // }
        // else if(index == 1)
        // {
        //     GameObject go = ObjectPool.Instance.CreateObject(chest, new Vector3(0, -4f, transform.position.z + 20), Quaternion.identity);
        //     go.GetComponent<Entity>().Init(id, level, characterId);
        // }
        // else if(index == 2)
        // {
        //     empty = true;
        // }
    }

    // protected override void CheckPlayerPos(object sender, EventArgs e)
    // {
    //     base.CheckPlayerPos(sender, e);
    //     if(id == characterPos && empty)
    //     {
    //         this.TriggerEvent(EventName.CharacterMoveEnd, new CharacterMoveEndEventArgs() { id = goodId });

    //         this.TriggerEvent(EventName.PassTile);
    //     }
    // }
}
