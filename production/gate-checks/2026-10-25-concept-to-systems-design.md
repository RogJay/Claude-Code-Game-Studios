# Gate Check: Concept → Systems Design

**Date**: 2026-10-25
**Checked by**: gate-check skill (`workflow: standard`, `review_mode: lean`)

### Required Artifacts: 1/1 present
- [x] `design/gdd/game-concept.md`: exists, ~34 KB, no placeholders

Recommended at `standard`:
- [x] Game pillars: 4 pillars, anti-pillars, non-negotiable limits
- [x] Visual Identity Anchor: one visual rule and 3 supporting principles
- [x] Concept prototypes with PROCEED: `prototypes/weltengott-concept/REPORT.md`, `prototypes/gotteshand-concept/REPORT.md`

### Quality Checks: 4/4 passing
- [x] Concept reviewed: 4 `/design-review` passes (lean), each NEEDS REVISION, never MAJOR REVISION NEEDED; the 4th revision is not re-reviewed
- [x] Core loop described (30 s, 5–15 min, session, long term)
- [x] Target audience identified (the developer, private project)
- [x] Visual Identity Anchor has a one-line rule and at least 2 principles

Artifact check (`artifact-check.sh --phase concept`): engine-setup PRESENT, game-concept PRESENT, art-bible ABSENT, map-systems ABSENT, brainstorm and design-review-concept NO_CHECK. The workflow catalog lists art-bible and map-systems as Concept-phase steps; this gate's definition does not require them at `standard`, so they are not findings here.

### Director Panel (2 of 4, `workflow: standard`)

Technical Director: CONCERNS
- Determinism boundary: non-deterministic PhysX results must enter the simulation only as a fixed event (thrower, target, outcome, injury class), never as raw physics state; otherwise headless fast-forward and save/load break. Settle in the god-hand and culture-memory GDDs.
- Time base: whether 5–10 Hz per agent and 1 Hz per tribe are real or simulated time is undefined; the simulation-core GDD must define the tick in simulated time and what coarsens under fast-forward.
- Performance evidence is narrow (primitives, editor, unrecorded hardware, 1 tester): keep the 8–16 ragdoll budget until re-measured with skinned bodies.
- Simulation evidence comes from the HTML prototype: rerun the 100 worlds × 10 eras measurement on the C# simulation with all 4 powers.
- Save/load and debug tools need their own Foundation-layer systems.
- Minor: Next Steps says three reviews (log has four); 6000.3.27f1 not checked against Unity's release API.

Producer: CONCERNS
- MVP-0 scope grew (3 more powers, passive grace, hand actions as interventions, abstract war) without a cut or re-estimate; the 3–4 month figure dates from 1 power.
- The re-estimate is ordered too late; recommends a rough `/estimate` right after `/map-systems`.
- Systems Design has nothing playable; recommends MVP-0 GDDs only, a 2-round review cap, an end date and small Unity spikes. Producer's own guess: 10–14 systems, ~5–10 weeks at 10–20 h/week.
- Design order: Foundation (sim tick, entities, culture state) → consent/lust → powers and god hand → Chronik/UX.
- Concept Next Steps and roadmap disagreed on the art bible's position.
- No measurable MVP-0 criterion for the combined core hypothesis yet.

Creative Director and Art Director not consulted (`workflow: standard`; `modes.rigor: full` gives the full panel).

### Blockers
None.

### Recommendations
- Fix the "drei Reviews" line in the concept's Next Steps (done in the same session).
- Check editor 6000.3.27f1 against Unity's release API when the engine reference is next refreshed.

### Verdict: CONCERNS

Chain-of-Verification: 5 questions checked (2 by tool: review-log entry count, Visual Identity Anchor and pillars re-read) — verdict unchanged.

### Accepted Risks
Accepted by the user, 2026-10-25, to advance to Systems Design:
- MVP-0 scope grew without a re-estimate
- No time box for the design phase yet (only the review cap and MVP-0 scope rules below)
- Time base and determinism boundary undefined until the simulation-core and god-hand GDDs
- No measurable MVP-0 criterion for the combined core hypothesis yet
- Concept and roadmap disagreed on order (aligned in the same session)

Rules the user set for Systems Design (2026-10-25): GDDs for MVP-0 systems only; at most 2 review rounds per GDD; small throwaway Unity tests allowed alongside. Not adopted: a rough `/estimate` right after `/map-systems` (the estimate stays after architecture, as in `production/roadmap.md`).

Stage written: `project.yaml` `project.stage: Systems Design`, `production/stage.txt` `Systems Design` (set-stage.sh, both read back).
