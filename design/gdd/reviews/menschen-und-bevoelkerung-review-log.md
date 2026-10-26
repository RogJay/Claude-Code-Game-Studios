# Review log: menschen-und-bevoelkerung.md

## Review — 2026-10-25 — Verdict: NEEDS REVISION
Review mode: lean · Workflow: standard
Scope signal: L
Specialists: none (lean mode; senior verdict skipped)
Blocking items: 4 | Recommended: 11
Summary: The design is coherent, all registry values match, the non-negotiable boundaries hold, and the formulas check out numerically. Four population-accounting and contract gaps block implementation: age deaths never reduce the Volk, the starting Einzelne are undefined, the timing of orders from other systems conflicts with the Kern's step contracts, and tribe switchers can be frozen outside their Gebiet.
Prior verdict resolved: First review
Findings:
- [BLOCKING] Rules 12/14/15 / States / AC-17/19: death by age never reduces V_t, so a dead Einzelner would rejoin the Masse
- [BLOCKING] Rule 7 / Formula 7 / AC-01/20: number, tribe split, Ort and draw order of the starting Einzelne undefined
- [BLOCKING] Rules 13/14/20 / Interactions / Formula 4: execution and timing of orders from Stämme (Verlust, Abspaltung, Eroberung, Gefangennahme) vs Kern inbox/read rules; rhythm for the inbox and Opferwahl unspecified
- [BLOCKING] Rules 20/21 / Formula 9 / AC-22: an Einzelner outside their tribe's Gebiet after Abspaltung/Eroberung stands still forever
- [RECOMMENDED] Dependencies / Rule 4: stale (Zustimmungsmodell GDD exists; held_steps vs Kern held_closed/t_hold_start and Speichert)
- [RECOMMENDED] Rule 10 / Interactions / Game Feel: Statist grab needs target addressing, Ort, ID return, N_max race, up to one step of latency
- [RECOMMENDED] Formula 8: personal Hingabe decays on the history clock (half-life about 5.6 s real vs 240 s life); no consumer of h_i
- [RECOMMENDED] Formula 6 / Zustimmungsmodell: symbol L_i means Lebensdauer here and Lust there
- [RECOMMENDED] Formula 5 / Edge Cases: name uniqueness across tribes, exhausted-list algorithm, NextInt(0, 0)
- [RECOMMENDED] States / Rule 19: prisoners' behaviour, heal timer field, held/captured switchers, im Kampf on grab
- [RECOMMENDED] States / AC-05: orthogonal flags vs exclusive states; AC-05 is vacuous
- [RECOMMENDED] Formulas 1/2: float vs double (prototype is double), r_E as float floor
- [RECOMMENDED] Acceptance Criteria: AC-20 odd ID dies at 841, AC-01 seeded events, bundled AC-03/12, new ACs for the blockers
- [RECOMMENDED] Formulas 3/9: d_t range wrong, R/P_person is derived not constant
- [RECOMMENDED] Tuning Knobs (advisory): consolidated table missing; Player Fantasy (advisory) could be lifted from Game Feel
Note: the stamp below is the content hash of the documents AS REVIEWED (before the revisions above), so the next review reads them as CHANGED and re-reviews.
Reviewed-Content-Hash: design/gdd/menschen-und-bevoelkerung.md 689543f605416b6c9b85f2b2aefea08c3195cdb6
Reviewed-Content-Hash: design/registry/entities.yaml 991cba1d50a35be68cafa7a17f12366c843f2177
Reviewed-Content-Hash: .claude/skills/design-review/SKILL.md a63d8fba9b7142eeb43791f061e2e1533c429b43
Reviewed-Content-Hash: design/gdd/simulationskern.md fb5ad22f3b0964d4ba9446a5033b51c43231cfdf
Reviewed-Content-Hash: design/gdd/zustimmungsmodell.md a40678f204e76a0842030067ec7723b42fde2f18
Reviewed-Content-Hash: design/gdd/game-concept.md 1928814c8180f283991e0bb1a8e9c6a34ff3dd9b
Reviewed-Content-Hash: prototypes/weltengott-concept/prototype.html e3c8aa99b445fc83ff5a9ce4fde8919dd9ab26eb
Reviewed-Content-Hash: design/gdd/systems-index.md c268cf11144d2ed89e7ce82b34dc564378e656e7

## Review — 2026-10-25 — Verdict: NEEDS REVISION
Review mode: lean · Workflow: standard
Scope signal: L
Specialists: none (lean mode; senior verdict skipped; night run with the developer's blanket permission, no questions asked)
Blocking items: 1 | Recommended: 11 (carried over from round 1)
Summary: All four round-1 blockers are resolved (every death lowers the Volk, start cohort, synchronous orders, switchers walk back). One new gap from the revised Kern: it forwards the results of a hold (Ort, Ergebnis) and the grab of a Statist as orders to Menschen, but Rule 23 lists neither. Round 2 is the last round allowed by the roadmap.
Prior verdict resolved: Yes
Findings:
- [BLOCKING] Rule 23 / Rule 10 / Interactions: return from Gottes Hand (Ort, Ergebnis) and the grab of a Statist (Stamm, Zelle) are not listed as orders, though the Kern now forwards them
- [RECOMMENDED] Dependencies / Rule 4: stale (Zustimmungsmodell GDD exists; held_closed and t_hold_start)
- [RECOMMENDED] Formula 8: personal Hingabe decays on the history clock; no consumer of h_i
- [RECOMMENDED] Formula 5 / Edge Cases: name uniqueness and exhausted-list algorithm
- [RECOMMENDED] States / Rule 19: prisoners, heal timer field, held people as switchers, im Kampf on grab
- [RECOMMENDED] States / AC-05: flags vs states; AC-05 vacuous
- [RECOMMENDED] Formulas 1/2: double for Gnade inputs, r_E as per mille
- [RECOMMENDED] Formulas 3/9: d_t range, R/P_person derived
- [RECOMMENDED] Rule 9: says N_target where Formula 3 uses N_target_eff
- [RECOMMENDED] Game Feel: grab latency up to one step
- [RECOMMENDED] Acceptance Criteria: AC-01 seeded events, AC-24 runnable in EditMode
- [RECOMMENDED] Tuning Knobs (advisory): table missing
Note: the stamp below is the content hash of the documents AS REVIEWED (before the revisions above), so the next review reads them as CHANGED and re-reviews.
Reviewed-Content-Hash: design/gdd/menschen-und-bevoelkerung.md f7a806253fb49f504172a22fdf649d96cef55cc1
Reviewed-Content-Hash: design/registry/entities.yaml 8ffb2113f83aaf40f77d8f5bff6806559e5c373f
Reviewed-Content-Hash: .claude/skills/design-review/SKILL.md a63d8fba9b7142eeb43791f061e2e1533c429b43
Reviewed-Content-Hash: design/gdd/simulationskern.md 65360b9e6fece24faa12f2add83cc022dd39c773
Reviewed-Content-Hash: design/gdd/zustimmungsmodell.md 125b1bb366e693cfc0c981b5688ad3bb387666de
Reviewed-Content-Hash: design/gdd/game-concept.md 168cdc5ff8f56635a08ebbb674fe517b93ded4c6
Reviewed-Content-Hash: prototypes/weltengott-concept/prototype.html e3c8aa99b445fc83ff5a9ce4fde8919dd9ab26eb
Reviewed-Content-Hash: design/gdd/systems-index.md 62096626557e3ec51fec3ceb141c4996cbd1e715

## Approval — 2026-10-26 — Status: Approved
Approved by the developer after 2 review rounds (roadmap cap reached); all blocking findings of the last round were fixed. Open Questions in the GDD stay open and are not part of the approval.
