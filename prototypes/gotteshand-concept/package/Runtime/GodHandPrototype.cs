// PROTOTYPE - NOT FOR PRODUCTION
// Question: Macht die Gotteshand (packen, hochheben, werfen) bei 50 Menschen nach 30 s Spass, und haelt die Bildzeit?
// Date: 2026-10-25
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Allmacht.Prototype.Gotteshand
{
    // Builds the whole test scene in code: island, sea, sun, god camera, 50 people,
    // god hand, target ring and the measurement overlay.
    public class GodHandPrototype : MonoBehaviour
    {
        const int StartPeople = 50;
        const float LiftHeight = 3.5f;
        const float PickRadius = 1.5f;
        const float TargetRadius = 3f;
        const float FleeRadius = 8f;
        const int StressCount = 16;
        const float StressWindow = 5f;

        static readonly Color GodColor = new Color(0.75f, 0.9f, 1f);

        Camera cam;
        Vector3 camPivot;
        float camYaw;
        float camHeight = 22f;

        Transform hand, handShadow, target;
        Person held;
        Person hover;
        Vector3 handTarget;
        readonly List<(float t, Vector3 p)> handHistory = new List<(float, Vector3)>();

        readonly List<Person> people = new List<Person>();
        Material baseMat;
        Material[] skins;
        Material[] cloths;

        // measurement
        readonly float[] frameMs = new float[600];
        int frameIndex, frameCount;
        float avgMs, p99Ms, maxMs, nextStatTime;
        int activeRagdolls, maxActiveRagdolls;
        float playStart;
        float firstGrab = -1f, firstThrow = -1f;
        int throws, resolved, hits;
        readonly List<bool> lastResults = new List<bool>();
        bool stressRunning;
        float stressEnd;
        readonly List<float> stressFrames = new List<float>();
        int stressMaxRagdolls;
        string stressResult = "noch keiner (Taste F)";
        bool showOverlay = true;
        GUIStyle style;

        void Start()
        {
            Random.InitState(1234);
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = -1;

            var tmp = GameObject.CreatePrimitive(PrimitiveType.Cube);
            baseMat = tmp.GetComponent<Renderer>().sharedMaterial;
            Destroy(tmp);

            skins = new[] { Mat(new Color(0.93f, 0.76f, 0.62f)), Mat(new Color(0.76f, 0.55f, 0.4f)), Mat(new Color(0.45f, 0.3f, 0.2f)) };
            cloths = new[] { Mat(new Color(0.72f, 0.55f, 0.25f)), Mat(new Color(0.25f, 0.38f, 0.22f)) };

            BuildWorld();
            for (int i = 0; i < StartPeople; i++) SpawnPerson(i);
            playStart = Time.realtimeSinceStartup;
        }

        Material Mat(Color c)
        {
            var m = new Material(baseMat);
            m.color = c;
            return m;
        }

        void BuildWorld()
        {
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientLight = new Color(0.45f, 0.47f, 0.5f);

            var sun = new GameObject("Sonne").AddComponent<Light>();
            sun.type = LightType.Directional;
            sun.intensity = 1.2f;
            sun.shadows = LightShadows.Soft;
            sun.transform.rotation = Quaternion.Euler(40f, -35f, 0f);

            var ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            ground.name = "Insel";
            ground.transform.position = new Vector3(0f, -1f, 0f);
            ground.transform.localScale = new Vector3(70f, 2f, 70f);
            ground.GetComponent<Renderer>().sharedMaterial = Mat(new Color(0.42f, 0.5f, 0.3f));

            var sea = GameObject.CreatePrimitive(PrimitiveType.Plane);
            sea.name = "Meer";
            sea.transform.position = new Vector3(0f, -0.6f, 0f);
            sea.transform.localScale = new Vector3(40f, 1f, 40f);
            sea.GetComponent<Renderer>().sharedMaterial = Mat(new Color(0.18f, 0.32f, 0.45f));
            Destroy(sea.GetComponent<Collider>());

            cam = new GameObject("Gotteskamera").AddComponent<Camera>();
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = new Color(0.62f, 0.74f, 0.85f);
            cam.farClipPlane = 500f;
            cam.gameObject.tag = "MainCamera";

            hand = MakeMarker("Gotteshand", PrimitiveType.Sphere, Vector3.one * 0.6f, GodColor);
            handShadow = MakeMarker("Handschatten", PrimitiveType.Cylinder, new Vector3(0.8f, 0.01f, 0.8f), new Color(0.15f, 0.17f, 0.2f));
            target = MakeMarker("Zielring", PrimitiveType.Cylinder, new Vector3(TargetRadius * 2f, 0.02f, TargetRadius * 2f), GodColor);
            MoveTarget();
        }

        Transform MakeMarker(string name, PrimitiveType type, Vector3 scale, Color c)
        {
            var go = GameObject.CreatePrimitive(type);
            go.name = name;
            go.transform.localScale = scale;
            go.GetComponent<Renderer>().sharedMaterial = Mat(c);
            Destroy(go.GetComponent<Collider>());
            return go.transform;
        }

        void SpawnPerson(int i)
        {
            bool tribeA = i % 2 == 0;
            Vector3 centre = tribeA ? new Vector3(-10f, 0f, 0f) : new Vector3(10f, 0f, 0f);
            Vector2 r = Random.insideUnitCircle * 8f;
            var p = Person.Create(this, centre + new Vector3(r.x, 0f, r.y), skins[Random.Range(0, skins.Length)], cloths[tribeA ? 0 : 1]);
            people.Add(p);
        }

        void MoveTarget()
        {
            Vector2 d = Random.insideUnitCircle.normalized * Random.Range(8f, 20f);
            target.position = new Vector3(d.x, 0.01f, d.y);
        }

        void Update()
        {
            RecordFrame(Time.unscaledDeltaTime * 1000f);
            var kb = Keyboard.current;
            var mouse = Mouse.current;
            if (kb == null || mouse == null) return;

            UpdateCamera(kb, mouse);
            UpdateHand(mouse);

            if (kb.fKey.wasPressedThisFrame) StartStressTest();
            if (kb.nKey.wasPressedThisFrame) for (int i = 0; i < 25; i++) SpawnPerson(people.Count);
            if (kb.kKey.wasPressedThisFrame) TakeScreenshot();
            if (kb.rKey.wasPressedThisFrame) ResetMeasurement();
            if (kb.hKey.wasPressedThisFrame) showOverlay = !showOverlay;

            activeRagdolls = 0;
            foreach (var p in people)
                if (p.State == PersonState.Ragdoll || p.State == PersonState.Held) activeRagdolls++;
            maxActiveRagdolls = Mathf.Max(maxActiveRagdolls, activeRagdolls);
        }

        void FixedUpdate()
        {
            if (held != null) held.DriveTowards(handTarget);
        }

        void UpdateCamera(Keyboard kb, Mouse mouse)
        {
            float dt = Time.unscaledDeltaTime;
            if (kb.qKey.isPressed) camYaw -= 70f * dt;
            if (kb.eKey.isPressed) camYaw += 70f * dt;
            var yawRot = Quaternion.Euler(0f, camYaw, 0f);
            Vector3 move = Vector3.zero;
            if (kb.wKey.isPressed || kb.upArrowKey.isPressed) move += Vector3.forward;
            if (kb.sKey.isPressed || kb.downArrowKey.isPressed) move += Vector3.back;
            if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) move += Vector3.left;
            if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) move += Vector3.right;
            camPivot += yawRot * move * camHeight * 0.8f * dt;
            camPivot.x = Mathf.Clamp(camPivot.x, -40f, 40f);
            camPivot.z = Mathf.Clamp(camPivot.z, -40f, 40f);

            float scroll = mouse.scroll.ReadValue().y;
            if (scroll != 0f) camHeight = Mathf.Clamp(camHeight * (1f - Mathf.Sign(scroll) * 0.1f), 6f, 45f);

            cam.transform.position = camPivot + yawRot * new Vector3(0f, camHeight, -camHeight * 0.75f);
            cam.transform.LookAt(camPivot);
        }

        static bool RayToHeight(Ray ray, float y, out Vector3 point)
        {
            point = Vector3.zero;
            if (ray.direction.y > -0.001f) return false;
            float t = (y - ray.origin.y) / ray.direction.y;
            point = ray.origin + ray.direction * t;
            return true;
        }

        void UpdateHand(Mouse mouse)
        {
            Ray ray = cam.ScreenPointToRay(mouse.position.ReadValue());
            if (!RayToHeight(ray, 0f, out Vector3 groundPoint)) return;

            if (held != null)
            {
                RayToHeight(ray, LiftHeight, out handTarget);
                hand.position = handTarget + Vector3.up * 0.4f;
                handShadow.position = new Vector3(handTarget.x, 0.02f, handTarget.z);
                float now = Time.time;
                handHistory.Add((now, handTarget));
                while (handHistory.Count > 2 && now - handHistory[0].t > 0.1f) handHistory.RemoveAt(0);

                if (mouse.leftButton.wasReleasedThisFrame) ReleaseHeld();
                return;
            }

            hover = null;
            float best = PickRadius * PickRadius;
            foreach (var p in people)
            {
                if (p.State == PersonState.Held) continue;
                Vector3 d = p.PelvisPosition - groundPoint;
                d.y = 0f;
                if (d.sqrMagnitude < best) { best = d.sqrMagnitude; hover = p; }
            }
            hand.position = groundPoint + Vector3.up * (hover != null ? 2.2f : 0.8f);
            hand.localScale = Vector3.one * (hover != null ? 0.8f : 0.5f);
            handShadow.position = new Vector3(groundPoint.x, 0.02f, groundPoint.z);

            if (mouse.leftButton.wasPressedThisFrame && hover != null)
            {
                held = hover;
                held.Grab();
                RayToHeight(ray, LiftHeight, out handTarget);
                handHistory.Clear();
                if (firstGrab < 0f) firstGrab = Time.realtimeSinceStartup - playStart;
            }
        }

        void ReleaseHeld()
        {
            Vector3 vel = Vector3.zero;
            if (handHistory.Count >= 2)
            {
                var a = handHistory[0];
                var b = handHistory[handHistory.Count - 1];
                if (b.t - a.t > 0.0001f) vel = (b.p - a.p) / (b.t - a.t);
            }
            vel.y += Mathf.Min(vel.magnitude * 0.3f, 6f);
            vel = Vector3.ClampMagnitude(vel, 30f);
            bool thrown = vel.magnitude > 4f;
            held.Release(vel, thrown);
            if (thrown)
            {
                throws++;
                if (firstThrow < 0f) firstThrow = Time.realtimeSinceStartup - playStart;
            }
            held = null;
            hand.localScale = Vector3.one * 0.5f;
        }

        // Called by a thrown person on its first hard impact (or when it falls into the sea).
        public void OnThrowImpact(Person who, Vector3 point, bool countsForAim)
        {
            foreach (var p in people)
            {
                if (p == who) continue;
                Vector3 d = p.transform.position - point;
                d.y = 0f;
                if (d.magnitude < FleeRadius) p.Flee(point, 2.5f);
            }
            if (!countsForAim) return;
            Vector3 toTarget = point - target.position;
            toTarget.y = 0f;
            bool hit = toTarget.magnitude <= TargetRadius;
            resolved++;
            if (hit) { hits++; MoveTarget(); }
            lastResults.Add(hit);
            if (lastResults.Count > 5) lastResults.RemoveAt(0);
        }

        void StartStressTest()
        {
            int n = 0;
            var order = new List<Person>(people);
            for (int i = 0; i < order.Count; i++)
            {
                int j = Random.Range(i, order.Count);
                (order[i], order[j]) = (order[j], order[i]);
            }
            foreach (var p in order)
            {
                if (n >= StressCount) break;
                if (p.State != PersonState.Walking) continue;
                Vector2 dir = Random.insideUnitCircle.normalized * Random.Range(3f, 6f);
                p.EnterRagdoll(new Vector3(dir.x, Random.Range(6f, 10f), dir.y));
                p.Thrown = true;
                p.CountsForAim = false;
                n++;
            }
            stressFrames.Clear();
            stressMaxRagdolls = 0;
            stressRunning = true;
            stressEnd = Time.realtimeSinceStartup + StressWindow;
        }

        void RecordFrame(float ms)
        {
            frameMs[frameIndex] = ms;
            frameIndex = (frameIndex + 1) % frameMs.Length;
            frameCount = Mathf.Min(frameCount + 1, frameMs.Length);

            if (Time.realtimeSinceStartup >= nextStatTime && frameCount > 0)
            {
                nextStatTime = Time.realtimeSinceStartup + 0.5f;
                var sorted = new float[frameCount];
                Array.Copy(frameMs, sorted, frameCount);
                Array.Sort(sorted);
                p99Ms = sorted[(int)(0.99f * (frameCount - 1))];
                maxMs = sorted[frameCount - 1];
                int n = Mathf.Min(120, frameCount);
                float sum = 0f;
                for (int i = 1; i <= n; i++) sum += frameMs[(frameIndex - i + frameMs.Length) % frameMs.Length];
                avgMs = sum / n;
            }

            if (stressRunning)
            {
                stressFrames.Add(ms);
                stressMaxRagdolls = Mathf.Max(stressMaxRagdolls, activeRagdolls);
                if (Time.realtimeSinceStartup >= stressEnd)
                {
                    stressRunning = false;
                    stressFrames.Sort();
                    float sum = 0f;
                    foreach (var f in stressFrames) sum += f;
                    float avg = sum / stressFrames.Count;
                    float p99 = stressFrames[(int)(0.99f * (stressFrames.Count - 1))];
                    float max = stressFrames[stressFrames.Count - 1];
                    stressResult = $"Ø {avg:F1} ms, 99 % {p99:F1} ms, max {max:F1} ms bei bis zu {stressMaxRagdolls} Ragdolls und {people.Count} Menschen";
                    Debug.Log($"[Gotteshand] Stresstest: {stressResult}");
                }
            }
        }

        void ResetMeasurement()
        {
            playStart = Time.realtimeSinceStartup;
            firstGrab = firstThrow = -1f;
            throws = resolved = hits = 0;
            lastResults.Clear();
            maxActiveRagdolls = 0;
            frameCount = 0;
            frameIndex = 0;
        }

        void TakeScreenshot()
        {
            string dir = Path.GetFullPath(Path.Combine(Application.dataPath, "..", "prototypes", "gotteshand-concept", "evidence"));
            Directory.CreateDirectory(dir);
            string file = Path.Combine(dir, $"gotteshand-{DateTime.Now:yyyyMMdd-HHmmss}.png");
            ScreenCapture.CaptureScreenshot(file);
            Debug.Log($"[Gotteshand] Screenshot: {file}");
        }

        void OnGUI()
        {
            if (!showOverlay) return;
            if (style == null)
            {
                style = new GUIStyle(GUI.skin.label) { fontSize = 15 };
                style.normal.textColor = Color.white;
            }
            float play = Time.realtimeSinceStartup - playStart;
            int last5 = 0;
            foreach (var r in lastResults) if (r) last5++;
            string text =
                "Allmacht - Prototyp 2: Gotteshand (Wegwerf-Test)\n" +
                "Linke Maustaste: Mensch packen, ziehen, loslassen = werfen | WASD: Kamera | Mausrad: Zoom | Q/E: drehen\n" +
                "F: Stresstest (16 Ragdolls) | N: +25 Menschen | K: Screenshot | R: Messung zuruecksetzen | H: Anzeige aus\n" +
                $"Bildzeit: Ø {avgMs:F1} ms ({(avgMs > 0f ? 1000f / avgMs : 0f):F0} fps) | 99 % (10 s): {p99Ms:F1} ms | max {maxMs:F1} ms\n" +
                $"Menschen: {people.Count} | aktive Ragdolls: {activeRagdolls} (max {maxActiveRagdolls})\n" +
                $"Spielzeit: {(int)(play / 60f):00}:{(int)(play % 60f):00} | erstes Packen: {Secs(firstGrab)} | erster Wurf: {Secs(firstThrow)}\n" +
                $"Wuerfe: {throws} | Zieltreffer: {hits} von {resolved} (letzte 5: {last5}/{lastResults.Count})\n" +
                $"Letzter Stresstest: {stressResult}";
            GUI.color = new Color(0f, 0f, 0f, 0.55f);
            GUI.DrawTexture(new Rect(8f, 8f, 860f, 178f), Texture2D.whiteTexture);
            GUI.color = Color.white;
            GUI.Label(new Rect(16f, 12f, 850f, 175f), text, style);
        }

        static string Secs(float s) => s < 0f ? "-" : $"{s:F1} s";
    }
}
