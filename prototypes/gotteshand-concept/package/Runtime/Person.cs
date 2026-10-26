// PROTOTYPE - NOT FOR PRODUCTION
// Question: Macht die Gotteshand (packen, hochheben, werfen) bei 50 Menschen nach 30 s Spass, und haelt die Bildzeit?
// Date: 2026-10-25
using UnityEngine;

namespace Allmacht.Prototype.Gotteshand
{
    public enum PersonState { Walking, Held, Ragdoll, GettingUp }

    // One adult built from primitives: 10 rigidbodies joined by CharacterJoints.
    // Walking = all parts kinematic, posed by code. Held/Ragdoll = physics.
    public class Person : MonoBehaviour
    {
        class Part
        {
            public Transform t;
            public Rigidbody rb;
            public Vector3 bindPos;
            public Quaternion bindRot;
            public Vector3 startPos;
            public Quaternion startRot;
        }

        public GodHandPrototype Game;
        public PersonState State { get; private set; } = PersonState.Walking;
        public bool Thrown;          // flying after a throw, until the first hard impact
        public bool CountsForAim;    // player throw (true) or stress test (false)
        public Vector3 PelvisPosition => pelvis.t.position;

        Part pelvis, chest, head, upperArmL, lowerArmL, upperArmR, lowerArmR, upperLegL, lowerLegL, upperLegR, lowerLegR;
        Part[] parts;

        Vector3 walkTarget;
        float walkSpeed;
        float phase;
        Vector3 walkVelocity;
        Vector3 fleeFrom;
        float fleeTimer;
        float settleTimer;
        float ragdollTime;
        float getUpT;

        const float IslandHalf = 33f;
        const float GetUpDuration = 0.7f;

        static readonly Vector3 HipL = new Vector3(-0.09f, 0.87f, 0f), HipR = new Vector3(0.09f, 0.87f, 0f);
        static readonly Vector3 KneeL = new Vector3(-0.09f, 0.47f, 0f), KneeR = new Vector3(0.09f, 0.47f, 0f);
        static readonly Vector3 ShoulderL = new Vector3(-0.24f, 1.42f, 0f), ShoulderR = new Vector3(0.24f, 1.42f, 0f);
        static readonly Vector3 ElbowL = new Vector3(-0.24f, 1.12f, 0f), ElbowR = new Vector3(0.24f, 1.12f, 0f);

        public static Person Create(GodHandPrototype game, Vector3 pos, Material skin, Material cloth)
        {
            var go = new GameObject("Mensch");
            go.transform.position = pos;
            go.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            var p = go.AddComponent<Person>();
            p.Game = game;
            p.Build(skin, cloth);
            p.PickWalkTarget();
            return p;
        }

        void Build(Material skin, Material cloth)
        {
            pelvis = MakePart("Becken", PrimitiveType.Cube, new Vector3(0f, 0.95f, 0f), new Vector3(0.32f, 0.2f, 0.18f), cloth, 12f);
            chest = MakePart("Brust", PrimitiveType.Cube, new Vector3(0f, 1.24f, 0f), new Vector3(0.36f, 0.38f, 0.2f), cloth, 18f);
            head = MakePart("Kopf", PrimitiveType.Sphere, new Vector3(0f, 1.58f, 0f), Vector3.one * 0.22f, skin, 5f);
            upperArmL = MakePart("OberarmL", PrimitiveType.Capsule, new Vector3(-0.24f, 1.27f, 0f), new Vector3(0.1f, 0.15f, 0.1f), skin, 2.5f);
            lowerArmL = MakePart("UnterarmL", PrimitiveType.Capsule, new Vector3(-0.24f, 0.97f, 0f), new Vector3(0.09f, 0.14f, 0.09f), skin, 1.5f);
            upperArmR = MakePart("OberarmR", PrimitiveType.Capsule, new Vector3(0.24f, 1.27f, 0f), new Vector3(0.1f, 0.15f, 0.1f), skin, 2.5f);
            lowerArmR = MakePart("UnterarmR", PrimitiveType.Capsule, new Vector3(0.24f, 0.97f, 0f), new Vector3(0.09f, 0.14f, 0.09f), skin, 1.5f);
            upperLegL = MakePart("OberschenkelL", PrimitiveType.Capsule, new Vector3(-0.09f, 0.67f, 0f), new Vector3(0.14f, 0.2f, 0.14f), cloth, 8f);
            lowerLegL = MakePart("UnterschenkelL", PrimitiveType.Capsule, new Vector3(-0.09f, 0.25f, 0f), new Vector3(0.11f, 0.22f, 0.11f), skin, 4.5f);
            upperLegR = MakePart("OberschenkelR", PrimitiveType.Capsule, new Vector3(0.09f, 0.67f, 0f), new Vector3(0.14f, 0.2f, 0.14f), cloth, 8f);
            lowerLegR = MakePart("UnterschenkelR", PrimitiveType.Capsule, new Vector3(0.09f, 0.25f, 0f), new Vector3(0.11f, 0.22f, 0.11f), skin, 4.5f);
            parts = new[] { pelvis, chest, head, upperArmL, lowerArmL, upperArmR, lowerArmR, upperLegL, lowerLegL, upperLegR, lowerLegR };

            // Twist runs around local X (forward/back bend). A positive angle swings a
            // hanging limb backwards, so knees bend positive, hips/shoulders/elbows negative.
            var down = new Vector3(0f, -0.5f, 0f);
            var top = new Vector3(0f, 1f, 0f);
            Joint(chest, pelvis, down, -30f, 30f, 20f, 20f);
            Joint(head, chest, down, -40f, 40f, 30f, 45f);
            Joint(upperArmL, chest, top, -150f, 50f, 80f, 30f);
            Joint(upperArmR, chest, top, -150f, 50f, 80f, 30f);
            Joint(lowerArmL, upperArmL, top, -140f, 5f, 10f, 10f);
            Joint(lowerArmR, upperArmR, top, -140f, 5f, 10f, 10f);
            Joint(upperLegL, pelvis, top, -100f, 30f, 30f, 20f);
            Joint(upperLegR, pelvis, top, -100f, 30f, 30f, 20f);
            Joint(lowerLegL, upperLegL, top, -5f, 130f, 5f, 5f);
            Joint(lowerLegR, upperLegR, top, -5f, 130f, 5f, 5f);

            var cols = GetComponentsInChildren<Collider>();
            for (int i = 0; i < cols.Length; i++)
                for (int j = i + 1; j < cols.Length; j++)
                    Physics.IgnoreCollision(cols[i], cols[j], true);
        }

        Part MakePart(string name, PrimitiveType type, Vector3 pos, Vector3 scale, Material mat, float mass)
        {
            var go = GameObject.CreatePrimitive(type);
            go.name = name;
            go.transform.SetParent(transform, false);
            go.transform.localPosition = pos;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = scale;
            go.GetComponent<Renderer>().sharedMaterial = mat;
            var rb = go.AddComponent<Rigidbody>();
            rb.mass = mass;
            rb.isKinematic = true;
            rb.linearDamping = 0.05f;
            rb.angularDamping = 0.6f;
            go.AddComponent<PartContact>().Owner = this;
            return new Part { t = go.transform, rb = rb, bindPos = pos, bindRot = Quaternion.identity };
        }

        static void Joint(Part child, Part parent, Vector3 anchor, float lowTwist, float highTwist, float swing1, float swing2)
        {
            var j = child.t.gameObject.AddComponent<CharacterJoint>();
            j.connectedBody = parent.rb;
            j.anchor = anchor;
            j.axis = Vector3.right;
            j.swingAxis = Vector3.forward;
            j.lowTwistLimit = new SoftJointLimit { limit = lowTwist };
            j.highTwistLimit = new SoftJointLimit { limit = highTwist };
            j.swing1Limit = new SoftJointLimit { limit = swing1 };
            j.swing2Limit = new SoftJointLimit { limit = swing2 };
            j.enableProjection = true;
            j.projectionDistance = 0.05f;
            j.projectionAngle = 30f;
        }

        void Update()
        {
            float dt = Time.deltaTime;
            switch (State)
            {
                case PersonState.Walking: UpdateWalking(dt); break;
                case PersonState.Ragdoll: UpdateRagdoll(dt); break;
                case PersonState.GettingUp: UpdateGettingUp(dt); break;
            }
            if (State != PersonState.Walking && State != PersonState.GettingUp && pelvis.t.position.y < -4f)
            {
                if (Thrown) OnImpact(pelvis.t.position);   // fell into the sea: resolves the throw as a miss
                Respawn();
            }
        }

        void UpdateWalking(float dt)
        {
            Vector3 pos = transform.position;
            Vector3 dir;
            float speed;
            if (fleeTimer > 0f)
            {
                fleeTimer -= dt;
                dir = pos - fleeFrom;
                dir.y = 0f;
                if (dir.sqrMagnitude < 0.01f) dir = transform.forward;
                speed = 3.5f;
            }
            else
            {
                dir = walkTarget - pos;
                dir.y = 0f;
                if (dir.magnitude < 0.5f) { PickWalkTarget(); dir = walkTarget - pos; dir.y = 0f; }
                speed = walkSpeed;
            }
            if (dir.sqrMagnitude < 0.0001f) dir = transform.forward;
            dir.Normalize();
            var targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 240f * dt);
            walkVelocity = transform.forward * speed;
            Vector3 next = pos + walkVelocity * dt;
            next.x = Mathf.Clamp(next.x, -IslandHalf, IslandHalf);
            next.z = Mathf.Clamp(next.z, -IslandHalf, IslandHalf);
            transform.position = next;

            phase += speed * dt * 4.5f;
            ApplyWalkPose(Mathf.Sin(phase) * Mathf.Clamp01(speed / 1.2f));
        }

        void ApplyWalkPose(float s)
        {
            float leg = 25f * s;
            SetLimb(upperLegL, lowerLegL, HipL, KneeL, Quaternion.AngleAxis(-leg, Vector3.right), Quaternion.AngleAxis(Mathf.Max(0f, leg) * 1.2f, Vector3.right));
            SetLimb(upperLegR, lowerLegR, HipR, KneeR, Quaternion.AngleAxis(leg, Vector3.right), Quaternion.AngleAxis(Mathf.Max(0f, -leg) * 1.2f, Vector3.right));
            float arm = 20f * s;
            SetLimb(upperArmL, lowerArmL, ShoulderL, ElbowL, Quaternion.AngleAxis(arm, Vector3.right), Quaternion.AngleAxis(-15f, Vector3.right));
            SetLimb(upperArmR, lowerArmR, ShoulderR, ElbowR, Quaternion.AngleAxis(-arm, Vector3.right), Quaternion.AngleAxis(-15f, Vector3.right));
        }

        static void SetLimb(Part upper, Part lower, Vector3 pivotU, Vector3 pivotL, Quaternion qU, Quaternion qL)
        {
            upper.t.localPosition = pivotU + qU * (upper.bindPos - pivotU);
            upper.t.localRotation = qU * upper.bindRot;
            Vector3 joint = pivotU + qU * (pivotL - pivotU);
            Quaternion q = qU * qL;
            lower.t.localPosition = joint + q * (lower.bindPos - pivotL);
            lower.t.localRotation = q * lower.bindRot;
        }

        void PickWalkTarget()
        {
            walkTarget = new Vector3(Random.Range(-28f, 28f), 0f, Random.Range(-28f, 28f));
            walkSpeed = Random.Range(1.0f, 1.5f);
        }

        public void Flee(Vector3 from, float seconds)
        {
            if (State != PersonState.Walking) return;
            fleeFrom = from;
            fleeTimer = seconds;
        }

        // ---- physics states -------------------------------------------------

        public void EnterRagdoll(Vector3 velocity)
        {
            foreach (var p in parts)
            {
                p.rb.isKinematic = false;
                p.rb.interpolation = RigidbodyInterpolation.Interpolate;
                p.rb.linearVelocity = velocity;
            }
            State = PersonState.Ragdoll;
            settleTimer = 0f;
            ragdollTime = 0f;
        }

        public void Grab()
        {
            if (State == PersonState.Walking || State == PersonState.GettingUp) EnterRagdoll(walkVelocity);
            Thrown = false;
            State = PersonState.Held;
        }

        // Called from FixedUpdate while held: the chest follows the hand, the rest dangles.
        public void DriveTowards(Vector3 target)
        {
            var rb = chest.rb;
            Vector3 desired = Vector3.ClampMagnitude((target - rb.position) * 10f, 25f);
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, desired, 0.6f);
            rb.angularVelocity *= 0.9f;
        }

        public void Release(Vector3 throwVelocity, bool thrown)
        {
            foreach (var p in parts)
            {
                p.rb.linearVelocity = throwVelocity;
                p.rb.angularVelocity = Random.insideUnitSphere * 2f;
            }
            Thrown = thrown;
            CountsForAim = thrown;
            State = PersonState.Ragdoll;
            settleTimer = 0f;
            ragdollTime = 0f;
        }

        public void OnImpact(Vector3 point)
        {
            if (!Thrown) return;
            Thrown = false;
            Game.OnThrowImpact(this, point, CountsForAim);
        }

        void UpdateRagdoll(float dt)
        {
            ragdollTime += dt;
            if (pelvis.rb.linearVelocity.magnitude < 0.35f) settleTimer += dt; else settleTimer = 0f;
            if (settleTimer > 1.5f || ragdollTime > 10f) GetUp();
        }

        void GetUp()
        {
            Vector3 p = pelvis.t.position;
            if (Mathf.Abs(p.x) > IslandHalf || Mathf.Abs(p.z) > IslandHalf) { Respawn(); return; }
            Vector3 fwd = Vector3.ProjectOnPlane(chest.t.forward, Vector3.up);
            if (fwd.sqrMagnitude < 0.01f) fwd = transform.forward;

            var worldPos = new Vector3[parts.Length];
            var worldRot = new Quaternion[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                worldPos[i] = parts[i].t.position;
                worldRot[i] = parts[i].t.rotation;
                parts[i].rb.linearVelocity = Vector3.zero;
                parts[i].rb.angularVelocity = Vector3.zero;
                parts[i].rb.isKinematic = true;
                parts[i].rb.interpolation = RigidbodyInterpolation.None;
            }
            transform.SetPositionAndRotation(new Vector3(p.x, 0f, p.z), Quaternion.LookRotation(fwd.normalized));
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].t.SetPositionAndRotation(worldPos[i], worldRot[i]);
                parts[i].startPos = parts[i].t.localPosition;
                parts[i].startRot = parts[i].t.localRotation;
            }
            Thrown = false;
            getUpT = 0f;
            State = PersonState.GettingUp;
        }

        void UpdateGettingUp(float dt)
        {
            getUpT += dt / GetUpDuration;
            float k = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(getUpT));
            foreach (var p in parts)
            {
                p.t.localPosition = Vector3.Lerp(p.startPos, p.bindPos, k);
                p.t.localRotation = Quaternion.Slerp(p.startRot, p.bindRot, k);
            }
            if (getUpT >= 1f) { State = PersonState.Walking; PickWalkTarget(); }
        }

        void Respawn()
        {
            foreach (var p in parts)
            {
                if (!p.rb.isKinematic)
                {
                    p.rb.linearVelocity = Vector3.zero;
                    p.rb.angularVelocity = Vector3.zero;
                }
                p.rb.isKinematic = true;
                p.rb.interpolation = RigidbodyInterpolation.None;
            }
            transform.SetPositionAndRotation(new Vector3(Random.Range(-25f, 25f), 0f, Random.Range(-25f, 25f)), Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
            foreach (var p in parts)
            {
                p.t.localPosition = p.bindPos;
                p.t.localRotation = p.bindRot;
            }
            Thrown = false;
            fleeTimer = 0f;
            State = PersonState.Walking;
            PickWalkTarget();
        }
    }
}
