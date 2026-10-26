# Review log: welt-und-insel.md

## Review — 2026-10-25 — Verdict: NEEDS REVISION
Review mode: full · Workflow: standard
Scope signal: M
Specialists: systems-designer, game-designer, qa-lead, world-builder, creative-director (senior); night run with the developer's blanket permission, no questions asked; the reviewed draft was itself written in the night run
Blocking items: 11 | Recommended: 18
Summary: The faithful port of prototype 1 is the right base, but the draft is not faithful to the code it copies (territory every year, full split placement, split and Fest food, extinction at pop < 3) and several ACs fail against their own rules (border water, AC-03 target, AC-14 premise, a V=0 tribe claiming land). Calibration-breaking ideas from the game-designer (burn yield 0 plus ash bonus, regional droughts, 3 start tribes, fertility-weighted territory, lower Gnade ceiling, longer burn effect) wait for the developer and an AC-16 measurement.
Prior verdict resolved: First review
Findings:
- [BLOCKING] Rule 7 / Formula 3: territory is recalculated every year in the prototype, not every 2 years [systems-designer]
- [BLOCKING] Formula 2: splits use landNear with radius+1..radius+6, a score floor and a null result that cancels the split [systems-designer]
- [BLOCKING] Formula 5: split child food = part * 0.5, Fest blessing +pop * 0.6; only start tribes get food = V [systems-designer]
- [BLOCKING] Formula 3 / Menschen Rule 2: prototype extinction at pop < 3; a V=0 tribe still claims cells at r = 2 [systems-designer, qa-lead]
- [BLOCKING] Save list: hunger flag, k, lastFamine missing; δ derivable [systems-designer]
- [BLOCKING] Edge case: a split spot on another tribe's centre yields a tribe with zero cells and NextInt(0,0) in Menschen [systems-designer]
- [BLOCKING] AC-01: border cells are not water by the formula (222/500 islands in the session's re-run) [qa-lead, world-builder, systems-designer]
- [BLOCKING] AC-03: target is not an integer cell [qa-lead]
- [BLOCKING] AC-14: premise impossible [qa-lead]
- [BLOCKING] Formula 7 / Game Feel: scars fade after 20 s though "zwei Ären später" is promised [world-builder, game-designer]
- [BLOCKING] Rule 9: famine event has no cause [game-designer]
- [RECOMMENDED] Formula 4 example contradicts its output range [qa-lead, systems-designer]
- [RECOMMENDED] Rounding: C# Math.Round is banker's rounding [qa-lead]
- [RECOMMENDED] Place list for rituals, feasts, chronicle (Orte promised by the systems index) [world-builder]
- [RECOMMENDED] Player Fantasy: fertility peaks on hills, not in valleys [world-builder]
- [RECOMMENDED] Hauptland, islets, water not walkable [world-builder, systems-designer]
- [RECOMMENDED] Fertility word thresholds [world-builder]
- [RECOMMENDED] Famine reaches the Chronicle only after 6 years (prototype) [systems-designer]
- [RECOMMENDED] f_eff extension point; burned cells with cause to Kultur [world-builder]
- [RECOMMENDED] Drought visuals ease in and out [world-builder]
- [RECOMMENDED] AC rewrites: AC-02, AC-04, AC-06, AC-07, AC-08, AC-09, AC-10, AC-11, AC-12, AC-13, AC-15, AC-16 [qa-lead]
- [RECOMMENDED] Calibration-breaking proposals (burn yield 0 + ash bonus, regional droughts, 3 start tribes, weighted territory, lower Gnade ceiling, cause-tagged consequences) recorded as Open Questions [game-designer; creative-director ruled: wait for the developer and AC-16]
Note: the stamp below is the content hash of the documents AS REVIEWED (before the revisions above), so the next review reads them as CHANGED and re-reviews.
Reviewed-Content-Hash: design/gdd/welt-und-insel.md a3ed92c2d2a7db92c93a9de2bcc1164aa314eeef
Reviewed-Content-Hash: design/registry/entities.yaml c497b38065519f2ee5e87928242c8bd5f45b4b6c
Reviewed-Content-Hash: .claude/skills/design-review/SKILL.md a63d8fba9b7142eeb43791f061e2e1533c429b43
Reviewed-Content-Hash: design/gdd/simulationskern.md b63fb4dcce167d4bcaa019e46ad21d4991d8babe
Reviewed-Content-Hash: design/gdd/menschen-und-bevoelkerung.md beab51b5faca71934fa3ddd44ed4d7f8db4e5248
Reviewed-Content-Hash: design/gdd/game-concept.md 168cdc5ff8f56635a08ebbb674fe517b93ded4c6
Reviewed-Content-Hash: prototypes/weltengott-concept/prototype.html e3c8aa99b445fc83ff5a9ce4fde8919dd9ab26eb
Reviewed-Content-Hash: prototypes/weltengott-concept/REPORT.md e9204c19a61fefb55207be0197cd90bd5576f6e0
Reviewed-Content-Hash: design/gdd/systems-index.md de72e23617dd4f843a4fc7efcbd3cdb749e5fdca

### Revisions after round 1 — 2026-10-25 (night run, no developer sign-off)

All 11 blocking items and the recommended items within the creative director's "fix tonight" list were applied:
territory every year (Rule 9, AC-05, AC-11), general Platzsuche with both draws per attempt and a "no spot" result (Formula 2, Rule 10, AC-14),
food for split children and Fest blessing (Rules 10, 14, Formula 5), extinction under 3 heads (Rule 8; cascade to Menschen Rule 2 logged as an Open Question),
save list extended (hunger flag, k, last famine; δ derived), split cancelled on a foreign centre or zero cells, border always water with no extra draws (third calibration deviation, AC-01/AC-02),
integer targets in AC-03, scars stored permanently (brand_jahr_c, brand_ursache_c), famine cause label and 6-year Chronicle gap (Formula 5, AC-15),
Formula 4 example matched to the output range, floor(x + 0.5) rounding with a midpoint AC, place list {ort_id, zelle, art, stamm, jahr, ursache} (Rule 17, AC-17),
Player Fantasy "green highland, barren coasts", main landmass rule, fertility word thresholds, f_eff extension point, drought fade in the view contract, AC rewrites with exact values and stubs.
Calibration-breaking proposals stay in Open Questions as two batches with metrics. Round 2 is the last round under the roadmap cap.

## Review — 2026-10-26 — Verdict: NEEDS REVISION
Review mode: lean · Workflow: standard
Scope signal: L
Specialists: none (lean); night run, no developer sign-off
Blocking items: 3 | Recommended: 5
Summary: All 11 round-1 blockers are resolved and verified against prototype.html (lines 116–437). Round 2 found an AC-09 premise that the generator does not guarantee ((1, 1) is interior), an open order for the Fest blessing that changes outcomes, and the remaining cross-GDD extinction threshold. All were fixed after the review; this is the last round under the roadmap cap, so the status is In Review and Approved is the developer's call.
Prior verdict resolved: Yes
Findings:
- [BLOCKING] Formula 7 / AC-09: Blitz on (0, 0) claims (1, 1) is border water; it is interior and only usually water
- [BLOCKING] Rule 14: Fest blessing food must use V_t after the gift (prototype order)
- [BLOCKING] Cross-GDD: Menschen Rule 2 extinction at V_t = 0 vs below 3 heads
- [RECOMMENDED] Calibration: list split-on-foreign-centre cancel and start-fallback land check as deviations
- [RECOMMENDED] AC-15: famine gap wording ambiguous
- [RECOMMENDED] AC-12: golden hash valid only on the same build and machine (Kern Rule 7)
- [RECOMMENDED] Formula 5: food range can exceed 3 · V_t after a Fest blessing
- [RECOMMENDED] Tuning Knobs: defaults stay at prototype values until AC-16
Note: welt-und-insel.md and menschen-und-bevoelkerung.md are stamped AS REVIEWED (before the revisions above), so the next review reads them as CHANGED.
Reviewed-Content-Hash: design/gdd/welt-und-insel.md da94ec30ef7066542ef74c6f46aac0fc2ff86ea0
Reviewed-Content-Hash: design/gdd/menschen-und-bevoelkerung.md beab51b5faca71934fa3ddd44ed4d7f8db4e5248
Reviewed-Content-Hash: design/registry/entities.yaml a86bb96a7ff5b0fa9264bd128620b29f469e1617
Reviewed-Content-Hash: .claude/skills/design-review/SKILL.md a63d8fba9b7142eeb43791f061e2e1533c429b43
Reviewed-Content-Hash: design/gdd/simulationskern.md b63fb4dcce167d4bcaa019e46ad21d4991d8babe
Reviewed-Content-Hash: prototypes/weltengott-concept/prototype.html e3c8aa99b445fc83ff5a9ce4fde8919dd9ab26eb

## Approval — 2026-10-26 — Status: Approved
Approved by the developer after 2 review rounds (roadmap cap reached); all blocking findings of the last round were fixed. Open Questions in the GDD stay open and are not part of the approval.
