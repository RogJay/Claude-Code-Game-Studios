# Review log: zustimmungsmodell.md

## Review — 2026-10-25 — Verdict: NEEDS REVISION
Review mode: lean · Workflow: standard
Scope signal: M
Specialists: none (lean mode; senior verdict skipped)
Blocking items: 5 | Recommended: 8
Summary: The design is strong (pure function, fail-closed, hard locks first, explicit Hingabe ban, Monte Carlo target) and all registry values and worked examples check out. Five gaps between the stated non-negotiable boundaries and their enforcement block implementation: consent without any desire is possible (simulated), hard locks do not end an act in the same step and the reverse direction is undefined, nothing enforces that every act passes the function, the scope of a Ritual and mutual consent is undefined, and acts continue below the consent threshold. Round 2 should run in full mode with security and game-designer specialists.
Prior verdict resolved: First review
Findings:
- [BLOCKING] Edge Cases / Formula 1: zero-desire people still consent (Lust-Sekte: 19 % Zärtlichkeit, 2.4 % Gruppe, 1.5 % Sex, 0.7 % Harem); contradicts the "kein Begehren" edge case; needs a desire floor
- [BLOCKING] Rules 10/14 / Edge Cases: hard locks end acts at the next person tick, not at once; reverse direction (combat/injury vs acts) undefined; Blitz/Auftrag can overlap a scene
- [BLOCKING] Rules 1/2/5/17 / AC-05-07: no mechanism enforces that every act passes Prüfe (result token, per-step re-validation, honest inputs b_ij/k_i)
- [BLOCKING] Rule 7 / Formula 1 / Ritual: what a Ritual contains and whether participants consent to each other is undefined (W_D = 0)
- [BLOCKING] Rules 11/14 / Formula 3: an act continues while w is in [θ − Δ_hold, θ), against "im Zweifel ablehnen"; needs explicit sign-off or change
- [RECOMMENDED] Rules 5/17 / Dependencies: Kultur norm contract needs a rate limit (indirect forcing via Eingriff → Norm)
- [RECOMMENDED] Rule 4 / Menschen Rule 18: "in Gottes Hand" as a lock-only profile field
- [RECOMMENDED] Rule 4: valid (Handlungsart, Ort) pairs and norms to supply
- [RECOMMENDED] Formula 1: variable names D_i (Begehren) vs D_t/h_i (Hingabe) and L_i (Lust) vs L_i (Lebensdauer)
- [RECOMMENDED] Formulas 1-3 / AC-19: data-table validity (exact sum vs tolerance, θ lower bound, Ritual W_D = 0, lock reason order)
- [RECOMMENDED] Formula 4: tie rule not transitive; wording of Hauptgrund
- [RECOMMENDED] Acceptance Criteria: new ACs for the blocking fixes
- [RECOMMENDED] Tuning Knobs (advisory): consolidated table missing
Note: the stamp below is the content hash of the documents AS REVIEWED (before the revisions above), so the next review reads them as CHANGED and re-reviews.
Reviewed-Content-Hash: design/gdd/zustimmungsmodell.md a40678f204e76a0842030067ec7723b42fde2f18
Reviewed-Content-Hash: design/registry/entities.yaml 991cba1d50a35be68cafa7a17f12366c843f2177
Reviewed-Content-Hash: .claude/skills/design-review/SKILL.md a63d8fba9b7142eeb43791f061e2e1533c429b43
Reviewed-Content-Hash: design/gdd/menschen-und-bevoelkerung.md ba63ca0d9489dd44389569bb1c3cdfcc26b23305
Reviewed-Content-Hash: design/gdd/simulationskern.md 8ce34ee793a31403c88222b1793b32a3a335e7f8
Reviewed-Content-Hash: design/gdd/game-concept.md 1928814c8180f283991e0bb1a8e9c6a34ff3dd9b
Reviewed-Content-Hash: design/gdd/systems-index.md 9459ca6648bd384a506af3446f5815badd395ca1
