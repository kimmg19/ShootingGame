using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Game
{
    public struct ShipData
    {
        public int id; public float base_dmg; public string name;
        public string kName;
        public int chr_level;
        public int locked;
        public float dmg;
        public float nextDmg;
        public ShipData(int id, float base_dmg, string name, string kName, int chr_level = 1,
            int locked = 1, float dmg = 1,float nextDmg=1)
        {
            this.id = id;
            this.base_dmg = base_dmg;
            this.name = name;
            this.kName = kName;
            this.chr_level = chr_level;
            this.locked = locked;
            this.dmg = dmg;
            this.nextDmg = nextDmg;
        }
        //11-6
        public string GetImageName()
        {
            return "Character/" + id.ToString() + "/0";
        }
        //chr_level 추가시 이 함수 꼭 실행.-레벨 증가할때
        public void SetDamage()
        {
            this.dmg = chr_level * base_dmg;
            this.nextDmg = (chr_level + 1) * base_dmg;
        }

        public void show()
        {
            Debug.Log("id: " + id + " base_dmg: " + base_dmg + " name: " + name +
                " kName: " + kName+" chr_level: "+chr_level+" locked: "+locked 
                +" dmg: "+dmg);
        }
    }
}