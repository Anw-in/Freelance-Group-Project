using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;

public class PlayerBattle : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public Skill[] playerSkills;
    public int currentSkillId = 0;
    public Skill currentSkill;
    public Collider attackCollider;
    public HPHandler hp;
    public Player player;

    void Start()
    {
        playerSkills = GetComponent<SkillsHandler>().availableSkills;
        currentSkill = playerSkills[currentSkillId];
        attackCollider = GetComponent<BoxCollider>();
        hp = GetComponent<HPHandler>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (hp.isAlive) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                choosePrevSkill();
            } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                chooseNextSkill();
            }
            if (Input.GetKeyDown(KeyCode.Space)) {
                Attack(currentSkill.damage);
            }
        }
    }

    void chooseNextSkill() {
        if (currentSkillId == playerSkills.Length - 1) {
            currentSkillId = 0;
            pickSkill(playerSkills[currentSkillId]);
        } else {
            currentSkillId++;
            pickSkill(playerSkills[currentSkillId]);
        }
    }

    void choosePrevSkill() {
        if (currentSkillId == 0) {
            currentSkillId = playerSkills.Length - 1;
            pickSkill(playerSkills[currentSkillId]);
        } else {
            currentSkillId--;
            pickSkill(playerSkills[currentSkillId]);
        }
    }

    public void pickSkill(Skill skill) {
        currentSkill = skill;
        Debug.Log("You picked a skill called " + skill.name + "!");
    }

    public void Attack(float damage) {
        damage = player.AttackBonuses(damage);
        if (enemies.Count > 0) {
            Enemy enemy = enemies[0];
            Debug.Log("Enemy hit with " + currentSkill.name + " and got " + damage + " of damage!");
            enemy.Hit(damage);
            if (!enemy.hp.isAlive) {
                enemies.RemoveAt(0);
            }
        }
    }

    void OnTriggerEnter(Collider collider) {
        Enemy enemy = collider.gameObject.GetComponent<Enemy>();
        if (enemy != null && !enemies.Contains(enemy)) {
            enemies.Add(enemy);
        }
    }

    void OnTriggerExit(Collider collider) {
        Enemy enemy = collider.gameObject.GetComponent<Enemy>();
        if (enemy != null) {
            enemies.Remove(enemy);
        }
    }
}
