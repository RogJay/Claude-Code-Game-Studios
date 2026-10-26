// PROTOTYPE - NOT FOR PRODUCTION
// Question: Macht die Gotteshand (packen, hochheben, werfen) bei 50 Menschen nach 30 s Spass, und haelt die Bildzeit?
// Date: 2026-10-25
using UnityEngine;

namespace Allmacht.Prototype.Gotteshand
{
    // Sits on every body part and reports the first hard impact of a thrown person.
    public class PartContact : MonoBehaviour
    {
        public Person Owner;

        void OnCollisionEnter(Collision c)
        {
            if (Owner == null || !Owner.Thrown) return;
            if (c.relativeVelocity.magnitude < 3f) return;
            if (c.collider.transform.IsChildOf(Owner.transform)) return;
            Owner.OnImpact(c.GetContact(0).point);
        }
    }
}
