using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Island.Movement;
using Island.Core;
using Island.Control;

namespace Island.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Transform target;
        Health health;


        //Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        //Item currentWeapon = null;


        private void GetRange()
        {

            if (GetComponent<AIController>().InAttackRangeOfPlayer() == true)
            {
                Debug.Log("damage");
            }
        }

        private void Update()
        {
            GetRange();
        }







        //private void Update()
        //{
        //    timeSinceLastAttack += Time.deltaTime;

        //    if (target == null) return;
        //if (target.IsDead()) return;


        //if (!GetIsInRange())
        //{
        //    GetComponent<NativeMover>().MoveTo(target.transform.position);
        //}
        //else
        //{
        //GetComponent<NativeMover>().Cancel();
        //AttackBehavior();
        //}
        //  }

        //private void AttackBehavior()
        //{
        //    transform.LookAt(target.transform);
        //    if (timeSinceLastAttack > timeBetweenAttacks)
        //    {
        //        // This will trigger the Hit() event.
        //        TriggerAttack();
        //        timeSinceLastAttack = Mathf.Infinity;

        //    }
        //}

        //private void TriggerAttack()
        //{
        //    GetComponent<Animator>().ResetTrigger("stopAttack");
        //    GetComponent<Animator>().SetTrigger("attack");
        //}

        ////animation event
        //void Hit()
        //{
        //Debug.Log("damage");

        //            if (target == null) { return; }
        //          else
        //}

        //void Shoot()
        //{
        //    Hit();
        //}


        //public bool CanAttack(GameObject combatTarget)
        //{
        //    if (combatTarget == null) { return false; }
        //    Health targetToTest = combatTarget.GetComponent<Health>();
        //    return targetToTest != null && !targetToTest.IsDead();
        //}

        //public void Attack(GameObject combatTarget)
        //{
        //    GetComponent<ActionScheduler>().StartAction(this);
        //    target = combatTarget.GetComponent<Health>();
        //}

        //public void Cancel()
        //{
        //    StopAttack();
        //    target = null;
        //    GetComponent<NativeMover>().Cancel();
        //}

        //private void StopAttack()
        //{
        //    GetComponent<Animator>().ResetTrigger("attack");
        //    GetComponent<Animator>().SetTrigger("stopAttack");
        //}

        //public void EquipWeapon(Weapon weapon)
        //{
        //    currentWeapon = weapon;
        //    Animator animator = GetComponent<Animator>();
        //    weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        //}

    }
}
