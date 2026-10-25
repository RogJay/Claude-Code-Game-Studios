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

## Review — 2026-10-25 — Verdict: NEEDS REVISION
Review mode: full · Workflow: standard
Scope signal: M
Specialists: game-designer, systems-designer, qa-lead, unity-specialist, security-engineer, creative-director (senior); night run with the developer's blanket permission, no questions asked
Blocking items: 16 | Recommended: 14
Summary: The core holds (one pure function, fail-closed, multiplicative desire factor, hysteresis), so not MAJOR. But enforcement can be forged (default result, result not bound to act or participants, caller-built requests and tables), lock timing has gaps (wrong slot in the step, view grabs before the simulation knows, held and dead people invisible to PrüfeSperren, loss draws can kill participants), violence works as a lever (killing or capturing a bound partner, conquest swapping norms, slavery as a role, captured harem members), and the ritual row lets piety stand in for desire inside contact. Last round allowed by the roadmap.
Prior verdict resolved: Yes (all five round-1 blockers addressed; new findings come from the full-mode specialists)
Findings:
- [BLOCKING] Rule 18 / AC-25: default(Zustimmungsergebnis) forgeable; result not bound to act type, participants, step or phase [unity-specialist, security-engineer, qa-lead]
- [BLOCKING] Rule 14 / Rule 18: continue path undefined; one owner of running acts needed [unity-specialist]
- [BLOCKING] Rule 14 / Kern Formula 5: lock check slot before Gotteskraft and Chronik; orders after it are seen one step late [unity-specialist, security-engineer]
- [BLOCKING] Rule 20 / AC-21: the view grabs in real time before the simulation applies the intervention; scene and ragdoll can overlap for frames [unity-specialist]
- [BLOCKING] Rule 4 / Rule 14: "in Gottes Hand" and death invisible to Prüfe and PrüfeSperren; AC-21 vs AC-26 reason conflict [security-engineer, qa-lead, game-designer]
- [BLOCKING] Rules 4/19: caller builds Anfrage, profile and Datentabelle; no public constructors and one builder needed [security-engineer]
- [BLOCKING] Rule 8 / Menschen Rule 18: Freiheit only frei/gefangen; slavery as a role would pass [security-engineer]
- [BLOCKING] Rule 16 / concept: capture must end harem membership; return only via a fresh join [security-engineer]
- [BLOCKING] Rule 19 / k_i: killing or capturing the bound partner removes the refusal [game-designer]
- [BLOCKING] Rule 4 norms: conquest swaps a person's norms in one step [game-designer, security-engineer]
- [BLOCKING] Formula 1 Ritual / Rule 6-7: piety substitutes for desire inside contact; ritual is the easiest sexual path; "pairwise" undefined [game-designer, security-engineer, systems-designer]
- [BLOCKING] Formula 4: "kein Begehren" guarantee fails when penalties push w_roh to 0 [systems-designer, qa-lead]
- [BLOCKING] AC-02: default(Norm) looks valid and drops a taboo [qa-lead]
- [BLOCKING] AC-19: negative weights, ranges, NaN, missing rows, default table not tested [qa-lead]
- [BLOCKING] AC: one-sided desire and Blitz have no AC [qa-lead]
- [BLOCKING] Menschen Rule 13: loss draws can kill participants of a running act [security-engineer]
- [RECOMMENDED] Formula 2/3 load rules: θ range, float tolerance, weight normalisation, hold desire floor, personal-weight floors [systems-designer, security-engineer]
- [RECOMMENDED] Formula 4: culture invisible in refusals; computed "kulturell" flag [game-designer]
- [RECOMMENDED] Edge Cases: "erschüttert" lock after release from the hand [game-designer]
- [RECOMMENDED] Dependencies: no repeated identical requests; recheck after loading; desire must not grow from hand placements [game-designer, security-engineer]
- [RECOMMENDED] AC-20 tolerances too loose; AC-06 parse asmdef; AC-09 IL scan; AC-21 frame recorder; AC-22 PNG sequence [systems-designer, unity-specialist, qa-lead]
- [RECOMMENDED] Formulas intro stale; typo; Rule 13 reasons; invalid-table edge case list [main review]
- [RECOMMENDED] AC-03, AC-13, AC-14, AC-16, AC-18, AC-27 rewrites; stratified AC-01 [qa-lead] (left for the developer)
- [RECOMMENDED] Tuning Knobs (advisory): table missing [systems-designer called it blocking; creative-director ruled advisory at workflow standard]
Note: the stamp below is the content hash of the documents AS REVIEWED (before the revisions above), so the next review reads them as CHANGED and re-reviews.
Reviewed-Content-Hash: design/gdd/zustimmungsmodell.md 125b1bb366e693cfc0c981b5688ad3bb387666de
Reviewed-Content-Hash: design/registry/entities.yaml 2051506f2e18b2b923afde0e26e479b34aab23c9
Reviewed-Content-Hash: .claude/skills/design-review/SKILL.md a63d8fba9b7142eeb43791f061e2e1533c429b43
Reviewed-Content-Hash: design/gdd/menschen-und-bevoelkerung.md 98ca581754d417dc0ac1c1af4e855e5727a63e27
Reviewed-Content-Hash: design/gdd/simulationskern.md 65360b9e6fece24faa12f2add83cc022dd39c773
Reviewed-Content-Hash: design/gdd/game-concept.md 168cdc5ff8f56635a08ebbb674fe517b93ded4c6
Reviewed-Content-Hash: design/gdd/systems-index.md 62096626557e3ec51fec3ceb141c4996cbd1e715
