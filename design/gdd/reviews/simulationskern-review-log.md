# Review log: simulationskern.md

## Review — 2026-10-25 — Verdict: NEEDS REVISION
Review mode: lean · Workflow: standard
Scope signal: XL
Specialists: none (lean mode; senior verdict skipped)
Blocking items: 4 | Recommended: 14
Summary: The architecture is sound (fixed steps, two time scales, per-system streams, physics-to-event boundary) and all registry values match. Four contract gaps block implementation: the save sequence for a held person contradicts the step rules, in-step state visibility and event-reaction timing are undefined, Formula 4 runs a step in Pause, and held_steps is ambiguous for a running hold.
Prior verdict resolved: First review
Findings:
- [BLOCKING] Edge Cases / Rule 13 / Rule 16 / AC-25: saving while a person is held needs a step to apply the end event, but saving pauses first; the saved queue would also contain a held person
- [BLOCKING] Core Rules 4.2 and 4.3 / AC-07: direct state reads inside a step and event-reaction timing for periodic systems (P=5) are not defined
- [BLOCKING] Formula 4 / Rule 13 / AC-16: A = min(A − n, 1) leaves A at 1, so the first paused frame after a saturated frame runs 1 step (simulated)
- [BLOCKING] Formula 3 / registry lebensjahr / AC-13 / AC-24: held_steps is undefined while a hold is running, so age keeps rising during the hold
- [RECOMMENDED] Dependencies / Cross-References: Menschen & Bevölkerung and Zustimmungsmodell GDDs exist but are listed as "noch keins"
- [RECOMMENDED] Formula 6 / AC-09: stream construction (four SplitMix64 outputs), NextInt/NextDouble and bounded-integer method undefined; AC-09 lacks reference seed table
- [RECOMMENDED] Rules 15-16 / AC-21 / AC-29: event and intervention ID scheme and counters, dangling cause IDs after eviction, unbounded events vs 0-byte allocation
- [RECOMMENDED] States / Rules: world creation order and stream use at t=0 not specified
- [RECOMMENDED] Rules 9-10 / Edge Cases: invalid Ort of the end event, rejected Beginn (view holds a figure), grab latency
- [RECOMMENDED] Rule 13: Pause locks the god's hand for a technical reason; confirm as design (alternative: queue during Pause)
- [RECOMMENDED] Formula 3 / Rule 2: a 30 s feast is 5 Lebensjahre (12.5 % of a life); confirm intent, measure in MVP-0
- [RECOMMENDED] Rule 4.2 / Formula 5: schedule entries as (system, rhythm) pairs; Menschen person tick runs before Kultur in shared steps
- [RECOMMENDED] Formulas 1, 3, 4, 5: S_year ">= 1" vs 5-10, unreachable P_person > S_year, "five" year systems vs six on five offsets, T_life range assumes R=10, A "up to cap+1"
- [RECOMMENDED] States / Formula 4: no exit from "Ohne Grafik"; v_eff needs a measurement window
- [RECOMMENDED] Rule 7 / AC-01: determinism constraints for the C#/DOTS ADR; concrete banned-symbol list
- [RECOMMENDED] Acceptance Criteria: split AC-16/AC-21, Era for 2^40, tolerance in AC-28, stub-system definitions, new ACs for delivery order, pause bug, running hold
- [RECOMMENDED] Tuning Knobs (advisory): consolidated table missing
- [RECOMMENDED] Registry: register O_year per system and system_id table
Note: the stamp below is the content hash of the documents AS REVIEWED (before the revisions above), so the next review reads them as CHANGED and re-reviews.
Reviewed-Content-Hash: design/gdd/simulationskern.md 45c0b6d371c4072d6e6c67b9906a13726c72c15a
Reviewed-Content-Hash: design/registry/entities.yaml 991cba1d50a35be68cafa7a17f12366c843f2177
Reviewed-Content-Hash: .claude/skills/design-review/SKILL.md a63d8fba9b7142eeb43791f061e2e1533c429b43
Reviewed-Content-Hash: design/gdd/menschen-und-bevoelkerung.md 689543f605416b6c9b85f2b2aefea08c3195cdb6
Reviewed-Content-Hash: design/gdd/zustimmungsmodell.md a40678f204e76a0842030067ec7723b42fde2f18
Reviewed-Content-Hash: design/gdd/game-concept.md 1928814c8180f283991e0bb1a8e9c6a34ff3dd9b
Reviewed-Content-Hash: design/gdd/systems-index.md 40299b3b908b531745ff22c5a999d9daa838079d

## Review — 2026-10-25 — Verdict: NEEDS REVISION
Review mode: lean · Workflow: standard
Scope signal: XL
Specialists: none (lean mode; senior verdict skipped; night run with the developer's blanket permission, no questions asked)
Blocking items: 2 | Recommended: 14 (carried over from round 1)
Summary: All four round-1 blockers are resolved (save flush step, in-step reads and inboxes, pause no longer runs a step, held_steps with a running hold). Two small new gaps: Rule 13 / AC-16 reject every intervention in Pause while the lost-figure edge case queues an end event in Pause, and Step 4.1 never says which system applies an intervention. Round 2 is the last round allowed by the roadmap.
Prior verdict resolved: Yes
Findings:
- [BLOCKING] Rule 13 / AC-16 / Edge Cases: Pause rejects every intervention, but view reconciliation end events must be queued in Pause
- [BLOCKING] Rule 4.1: no rule for which system applies an intervention (dispatch to the owning system)
- [RECOMMENDED] Dependencies / Cross-References: still say no dependent GDD exists
- [RECOMMENDED] Formula 6 / AC-09: RNG construction, NextInt/NextDouble, reference table
- [RECOMMENDED] Rules 15-16 / AC-21 / AC-29: event and intervention IDs, dangling causes, allocation wording
- [RECOMMENDED] States: world creation order; Ohne Grafik exit; v_eff window
- [RECOMMENDED] Rules 9-10 / Edge Cases: invalid Ort of an end event; a flying person is set down at its own cell, not at the hand
- [RECOMMENDED] Formulas 1/3/5: S_year >= 1 vs 5-10, six year systems on five offsets, T_life range at R=20
- [RECOMMENDED] Acceptance Criteria: AC-02 Era for 2^40, AC-28 tolerance, banned symbols in AC-01, delivery order AC
- [RECOMMENDED] Tuning Knobs (advisory): table missing
- [RECOMMENDED] Registry: Menschen system_id and year offset restated in another GDD
Note: the stamp below is the content hash of the documents AS REVIEWED (before the revisions above), so the next review reads them as CHANGED and re-reviews.
Reviewed-Content-Hash: design/gdd/simulationskern.md d1e1f00e967c477b342d26806c77742077af20f2
Reviewed-Content-Hash: design/registry/entities.yaml 03907c2659f0b3ede7c936383c17380ed7775b93
Reviewed-Content-Hash: .claude/skills/design-review/SKILL.md a63d8fba9b7142eeb43791f061e2e1533c429b43
Reviewed-Content-Hash: design/gdd/menschen-und-bevoelkerung.md f7a806253fb49f504172a22fdf649d96cef55cc1
Reviewed-Content-Hash: design/gdd/zustimmungsmodell.md 125b1bb366e693cfc0c981b5688ad3bb387666de
Reviewed-Content-Hash: design/gdd/game-concept.md 1928814c8180f283991e0bb1a8e9c6a34ff3dd9b
Reviewed-Content-Hash: design/gdd/systems-index.md 62096626557e3ec51fec3ceb141c4996cbd1e715
