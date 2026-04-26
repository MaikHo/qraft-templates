# OpenCode Workflows

## Zweck
Diese Datei zeigt typische Nutzungsablaeufe fuer das Agentensystem.
Sie ergaenzt die Theorie-Doku um praktische Team-Workflows, klare Gates und justierbare Stellschrauben.
Zusaetzlich nutzt dieses Projekt eine `AGENTS.md` als Einstiegspunkt fuer AI-Agenten.
- Die `AGENTS.md` enthaelt eine minimale Uebersicht und verweist auf die eigentlichen Workflows.
- Die Hauptlogik liegt weiterhin unter `.opencode/...`.
- Die Datei bleibt bewusst minimal und dupliziert keinen Context.

## Pfad- und Runtime-Hinweis
Im Template-Repository liegen die Dateien unter `opencode/...`; im Zielprojekt werden sie nach `.opencode/...` uebernommen.
Die Workflow-Beschreibungen referenzieren primaer Runtime-Zielpfade unter `.opencode/...`.

## Quick Start (5 Schritte)
1. `@openagent` starten.
2. Aufgabe praezise formulieren (Scope, Ziel, Grenzen).
3. `Plan` pruefen und offene Punkte in `Open Questions` klaeren.
4. `Approval Status` explizit auf freigegeben setzen.
5. Ergebnis ueber Artefakte bewerten (`Change Summary`, `Validation Report`, `Review Findings`, `Document Output` oder `Document Skip Reason`).

## Initialisierung (Wichtig)
- `/init` ist in diesem Setup nicht erforderlich.
- Der Standardstart erfolgt ueber `@openagent`.
- Kontext und Projektverstaendnis werden kontrolliert ueber Analyse aufgebaut.
- `/init` kann optional fuer Analyse genutzt werden, wird aber nicht als Standard empfohlen.
- Begruendung:
  - Die Struktur unter `.opencode/...` ist bereits vorhanden.
  - Unkontrollierte Generierung soll vermieden werden.
  - Die volle Kontrolle ueber Context und Memory bleibt beim Team.
- Memory initial leer beziehungsweise neutral halten.
- Keine Altlasten aus aelteren Projekten uebernehmen.
- Memory erst nach geprueften Projektentscheidungen und stabilen Fakten befuellen.

## Artefakt-Standard (einheitliche Namen)
Verwende in allen Workflows konsistent diese Artefaktnamen:
- `Context Summary`
- `Plan`
- `Approval Status`
- `Change Summary`
- `Validation Report`
- `Review Findings`
- `Document Output`
- `Document Skip Reason`
- `Open Questions`

## Grundprinzip
- Erst Context
- dann Plan
- dann Approval
- dann Umsetzung
- dann Validate
- dann Review
- dann Document/Skip-Grund

Hinweise zur Steuerung:
- `openagent` ist der Standard-Primaeragent fuer orchestrierte Workflows.
- `opencoder` ist ein coding-first Alternativpfad, umgeht aber keine Governance.
- Auch bei direkter Nutzung von `opencoder` gelten Context, Approval, Validation, Review und Document/Skip-Grund.
- Commands/Skills sind abhaengig vom Teamsetup und dienen als optionale Unterstuetzungs- oder Justierpunkte.

## Workflow-Mapping (Kompakt)
| Workflow | Primaeragent | Subagents | Pflicht-Gates |
|---|---|---|---|
| 1. Projekt verstehen | `openagent` | `context-retriever` (optional) | Context, Plan, Approval |
| 2. README/Doku erstellen | `openagent` | `documentation` (typisch) | Context, Plan, Approval, Document |
| 3. Kleinen Bug fixen | `openagent` oder `opencoder` | `coder-agent`, `tester`, `reviewer` | Context, Plan, Approval, Validation, Review |
| 4. Neues Feature planen | `openagent` | `task-manager` (typisch) | Context, Plan, Approval |
| 5. Feature mit Tests umsetzen | `openagent` oder `opencoder` | `task-manager`, `coder-agent`, `tester`, `reviewer`, `documentation` (je Scope) | Context, Plan, Approval, Validation, Review |
| 6. Code bewerten lassen | `openagent` | `reviewer` (analyse-only) | Context, Plan, Approval |
| 7. Unklare Aufgabe / Exploration | `openagent` | `context-retriever` (optional) | Context, Plan, Approval |
| 8. Gemischte Aufgabe | `openagent` | je Teilauftrag: `coder-agent`/`tester`/`reviewer`/`documentation` | Context, Plan, Approval, Review (bei Code), Document (bei Doku-Relevanz) |
| 9. Testlauf / Systemvalidierung | `openagent` | `tester`, `reviewer` (optional) | Context, Plan, Approval, Validation |

## Workflow 1: Projekt verstehen
### Ziel
Projektstatus, Struktur und relevante Risiken verstehen, ohne etwas zu aendern.

### Wann nutzen?
- Beim Onboarding ins Projekt
- Vor groesseren Aenderungen
- Wenn Scope oder Architektur unklar sind

### Beispiel-Prompt
`@openagent Analysiere dieses Projekt und erstelle eine Projektuebersicht. Noch nichts aendern.`

### Erwarteter Ablauf
1. Context laden
2. Analyse-`Plan` erstellen
3. `Approval Status` fuer Analyse einholen
4. Analyse durchfuehren (ohne Aenderungen)
5. Ergebnisse als Zusammenfassung und Fragen bereitstellen

### Intern beteiligte Agenten/Subagents
- Primaer: `openagent`
- Typisch optional: `context-retriever`
- Nicht erwartet: `coder-agent`, `tester`, `reviewer`, `documentation`

### Relevante Skills/Commands (optional)
- Command: `context`
- Command: `clarify` (wenn Anforderungen unklar sind)

### Erwartete Artefakte
- `Context Summary`
- `Plan`
- `Approval Status`
- `Change Summary` (none)
- `Validation Report` (none)
- `Review Findings` (none)
- `Document Output` (none)
- `Document Skip Reason` (Analyse-only)
- `Open Questions`

### Typische Fehler
- Analyse ohne geladenen Context
- Vermischung mit Implementierung
- Keine klaren `Open Questions`

### Justierpunkte (wenn etwas schief laeuft)
- Agent: `openagent` Fokus auf Analysepfad schaerfen
- Policy/Gate: Approval-Gate fuer "noch nichts aendern" strikt halten
- Context/Schema: `.opencode/context/core/standards/docs.md` (Report-Qualitaet)

## Workflow 2: README oder Doku erstellen
### Ziel
Projekt-Dokumentation erstellen oder aktualisieren, ohne Code zu aendern.

### Wann nutzen?
- README fehlt oder ist veraltet
- Setup/Run/Test-Schritte sind unklar
- Architektur-/Nutzungsdoku soll nachgezogen werden

### Beispiel-Prompt
`@openagent Erstelle oder aktualisiere die README fuer dieses Projekt. Frage nach, wenn Informationen fehlen. Keine Codeaenderungen.`

### Erwarteter Ablauf
1. Doku-Context laden
2. `Plan` fuer Doku-Struktur erstellen
3. `Approval Status` einholen
4. Doku-Aenderung umsetzen
5. `Document Output` liefern

### Intern beteiligte Agenten/Subagents
- Primaer: `openagent`
- Typisch: `documentation`
- Optional: `context-retriever`
- Nicht erwartet: `coder-agent`, `tester`, `reviewer`

### Relevante Skills/Commands (optional)
- Skill: `readme`
- Command: `context`

### Erwartete Artefakte
- `Context Summary`
- `Plan`
- `Approval Status`
- `Change Summary`
- `Validation Report` (none oder "N/A fuer Code")
- `Review Findings` (none)
- `Document Output`
- `Document Skip Reason` (none)
- `Open Questions`

### Typische Fehler
- README schreiben ohne Projektkontext
- Codeaenderungen trotz "Keine Codeaenderungen"
- Fehlende reproduzierbare Befehle

### Justierpunkte (wenn etwas schief laeuft)
- Agent: `documentation` Prompt enger auf README-Struktur setzen
- Policy/Gate: Document-Gate gegen "intern-only" Missbrauch absichern
- Context/Schema: `.opencode/context/core/standards/docs.md`

## Workflow 3: Kleinen Bug fixen
### Ziel
Einen klar abgegrenzten internen Bug beheben.

### Wann nutzen?
- Fehler reproduzierbar und Scope klein
- Keine breite Architekturumstellung noetig
- Doku-Relevanz gering oder nicht vorhanden

### Beispiel-Prompt
`@openagent Fixe den Bug in [DATEI/KLASSE]. Es handelt sich um einen internen Code-Fix ohne Doku-Relevanz.`

### Erwarteter Ablauf
1. Code- und Test-Context laden
2. `Plan` fuer minimalen Fix erstellen
3. `Approval Status` einholen
4. Fix umsetzen
5. Build/Test ausfuehren
6. Review durchfuehren
7. Doku ausgeben oder Skip begruenden

### Intern beteiligte Agenten/Subagents
- Primaer: `openagent` oder `opencoder`
- Typisch: `coder-agent`, `tester`, `reviewer`
- Optional: `documentation`

### Relevante Skills/Commands (optional)
- Command: `test`
- Command: `validate-repo`

### Erwartete Artefakte
- `Context Summary`
- `Plan`
- `Approval Status`
- `Change Summary`
- `Validation Report`
- `Review Findings`
- `Document Output` (optional)
- `Document Skip Reason` (wenn keine Doku-Relevanz)
- `Open Questions`

### Typische Fehler
- Fix ohne explizite Approval-Freigabe
- Validation unvollstaendig
- Review uebersprungen

### Justierpunkte (wenn etwas schief laeuft)
- Agent: bei zu grobem Fix `task-manager` vorschalten
- Policy/Gate: Review-Gate fuer Codeaenderungen strikt machen
- Context/Schema: `.opencode/context/core/standards/code.md`, `.opencode/context/core/standards/tests.md`, `.opencode/context/core/workflows/review.md`

## Workflow 4: Neues Feature planen
### Ziel
Umsetzungsplan fuer ein neues Feature erstellen, ohne Implementierung.

### Wann nutzen?
- Scope und Risiken sind noch unklar
- Team braucht vorab Sequenz und Abhaengigkeiten
- Vor Arbeitsteilung im Team

### Beispiel-Prompt
`@openagent Plane ein neues Feature: [FEATURE]. Erstelle zuerst nur den Plan und aendere noch keine Dateien.`

### Erwarteter Ablauf
1. Context laden
2. `Plan` inklusive Annahmen und Risiken erstellen
3. `Approval Status` fuer Plan einholen
4. Keine Umsetzung starten

### Intern beteiligte Agenten/Subagents
- Primaer: `openagent`
- Typisch: `task-manager`
- Optional: `context-retriever`
- Nicht erwartet: `coder-agent`, `tester`, `reviewer`, `documentation`

### Relevante Skills/Commands (optional)
- Command: `tasks`
- Command: `clarify`

### Erwartete Artefakte
- `Context Summary`
- `Plan`
- `Approval Status`
- `Change Summary` (none)
- `Validation Report` (none)
- `Review Findings` (none)
- `Document Output` (none)
- `Document Skip Reason` (Plan-only)
- `Open Questions`

### Typische Fehler
- Planung direkt in Umsetzung kippt
- Keine Annahmen oder Abhaengigkeiten dokumentiert
- Kein klarer Scope-Schnitt

### Justierpunkte (wenn etwas schief laeuft)
- Agent: `task-manager` fuer atomare Teilaufgaben erzwingen
- Policy/Gate: Approval nur fuer Plan, nicht fuer implizite Umsetzung
- Context/Schema: `.opencode/context/core/workflows/delegation.md`, `.opencode/context/core/system/context-bundle-schema.md`

## Workflow 5: Feature mit Tests umsetzen
### Ziel
Feature inklusive Tests kontrolliert umsetzen.

### Wann nutzen?
- Feature-Scope ist freigegeben
- Mehrere Schritte oder Dateien notwendig
- Verifikation ueber Tests ist Pflicht

### Beispiel-Prompt
`@openagent Implementiere Feature [FEATURE] inklusive Tests. Nutze Task-Breakdown und frage vor jeder Aenderung nach Freigabe.`

### Erwarteter Ablauf
1. Context Bundle aufbauen/laden
2. Subtask-`Plan` erstellen
3. `Approval Status` einholen
4. Umsetzung pro Subtask
5. Validation ausfuehren
6. Review durchfuehren
7. Doku liefern oder Skip begruenden

### Intern beteiligte Agenten/Subagents
- Primaer: `openagent` oder `opencoder`
- Typisch: `task-manager`, `coder-agent`, `tester`, `reviewer`
- Optional: `documentation`

### Relevante Skills/Commands (optional)
- Command: `tasks`
- Command: `test`
- Command: `validate-repo`

### Erwartete Artefakte
- `Context Summary`
- `Plan`
- `Approval Status`
- `Change Summary`
- `Validation Report`
- `Review Findings`
- `Document Output` (je Scope)
- `Document Skip Reason` (wenn intern und ohne Doku-Relevanz)
- `Open Questions`

### Typische Fehler
- Kein Task-Breakdown bei komplexem Scope
- Tests nur teilweise aktualisiert
- Doku-Gate vergessen

### Justierpunkte (wenn etwas schief laeuft)
- Agent: bei Drift `task-manager` erneut fuer Re-Plan einsetzen
- Policy/Gate: Stop-on-failure fuer Validation strikt anwenden
- Context/Schema: `.opencode/context/core/standards/code.md`, `.opencode/context/core/standards/tests.md`, `.opencode/context/core/workflows/review.md`

## Workflow 6: Code bewerten lassen
### Ziel
Bestehenden Code auf Verstaendlichkeit, Wartbarkeit und Fehlerpotenzial bewerten.

### Wann nutzen?
- Vor Refactoring
- Vor Release als Risiko-Check
- Fuer Qualitaets- und Sicherheits-Feedback

### Beispiel-Prompt
`@openagent Pruefe [DATEI/KLASSE] auf Verstaendlichkeit, Wartbarkeit und moegliche Fehler. Noch nichts aendern.`

### Erwarteter Ablauf
1. Context laden
2. Review-`Plan` erstellen
3. `Approval Status` einholen
4. Analyse mit Findings liefern
5. Keine Umsetzung ohne separate Freigabe

### Intern beteiligte Agenten/Subagents
- Primaer: `openagent`
- Typisch: `reviewer`
- Optional: `context-retriever`
- Nicht erwartet: `coder-agent`, `tester`, `documentation`

### Relevante Skills/Commands (optional)
- Command: `context`

### Erwartete Artefakte
- `Context Summary`
- `Plan`
- `Approval Status`
- `Change Summary` (none)
- `Validation Report` (none)
- `Review Findings`
- `Document Output` (none)
- `Document Skip Reason` (Analyse-only)
- `Open Questions`

### Typische Fehler
- Analyse ohne Priorisierung der Findings
- Vermischung von Review und direkter Umsetzung
- Fehlende Sicherheitsbetrachtung

### Justierpunkte (wenn etwas schief laeuft)
- Agent: `reviewer` auf Severity-Format verpflichten
- Policy/Gate: "keine Umsetzung ohne Approval" durchsetzen
- Context/Schema: `.opencode/context/core/workflows/review.md`

## Workflow 7: Unklare Aufgabe / Exploration
### Ziel
Optionen, Risiken und naechste Schritte fuer eine unklare Idee herausarbeiten.

### Wann nutzen?
- Wenn Problemdefinition noch unscharf ist
- Bei Architektur- oder Machbarkeitsfragen
- Vor Priorisierungsentscheidungen

### Beispiel-Prompt
`@openagent Ich moechte pruefen, ob [IDEE] sinnvoll ist. Analysiere Moeglichkeiten, Risiken und naechste Schritte. Noch nichts aendern.`

### Erwarteter Ablauf
1. Context laden
2. Explorations-`Plan` erstellen
3. `Approval Status` einholen
4. Optionen, Risiken und naechste Schritte ausarbeiten

### Intern beteiligte Agenten/Subagents
- Primaer: `openagent`
- Typisch optional: `context-retriever`
- Nicht erwartet: `coder-agent`, `tester`, `reviewer`, `documentation`

### Relevante Skills/Commands (optional)
- Command: `clarify`
- Command: `context`

### Erwartete Artefakte
- `Context Summary`
- `Plan`
- `Approval Status`
- `Change Summary` (none)
- `Validation Report` (none)
- `Review Findings` (none)
- `Document Output` (none)
- `Document Skip Reason` (Exploration-only)
- `Open Questions`

### Typische Fehler
- Zu fruehe Festlegung auf eine Option
- Keine expliziten Risiken
- Fehlende Trennung von Annahmen und Fakten

### Justierpunkte (wenn etwas schief laeuft)
- Agent: `openagent` auf Optionenset statt Loesungspfad ausrichten
- Policy/Gate: keine Dateiaenderung ohne neue Freigabe
- Context/Schema: `.opencode/context/core/system/context-bundle-schema.md`

## Workflow 8: Gemischte Aufgabe
### Ziel
Codeaenderung und ggf. Dokuanpassung in einem kontrollierten Ablauf behandeln.

### Wann nutzen?
- Wenn unklar ist, ob Doku betroffen ist
- Wenn ein Fix/Feature wahrscheinlich Dokumentation beruehrt
- Bei kombinierten Tickets aus Dev + Docs

### Beispiel-Prompt
`@openagent Aendere Feature [X] und passe ggf. die Dokumentation an.`

### Erwarteter Ablauf
1. Context laden
2. `Plan` in Teilspuren (Code/Doku) aufteilen
3. `Approval Status` einholen
4. Codepfad umsetzen und validieren
5. Review bei Codeaenderung
6. Doku liefern oder Skip begruenden

### Intern beteiligte Agenten/Subagents
- Primaer: `openagent`
- Typisch: `coder-agent`, `tester`, `reviewer`
- Optional: `documentation`, `task-manager`

### Relevante Skills/Commands (optional)
- Skill: `readme` (wenn README betroffen)
- Command: `test`
- Command: `validate-repo`

### Erwartete Artefakte
- `Context Summary`
- `Plan`
- `Approval Status`
- `Change Summary`
- `Validation Report`
- `Review Findings` (bei Codeaenderung)
- `Document Output` (bei Doku-Relevanz)
- `Document Skip Reason` (wenn keine Doku-Relevanz)
- `Open Questions`

### Typische Fehler
- Scope-Erweiterung waehrend der Umsetzung
- Doku-Relevanz nicht explizit entschieden
- Review bei Code geparkt

### Justierpunkte (wenn etwas schief laeuft)
- Agent: `task-manager` fuer klare Trennung Code vs Doku aktivieren
- Policy/Gate: Review-Gate und Document-Gate getrennt pruefen
- Context/Schema: `.opencode/context/core/standards/docs.md`, `.opencode/context/core/workflows/review.md`

## Workflow 9: Testlauf / Systemvalidierung
### Ziel
Kontrollierten Testlauf nach definiertem Szenario durchfuehren und Abweichungen klassifizieren.

### Wann nutzen?
- Bei Regression-Checks
- Vor Releases oder groesseren Merges
- Zur Validierung der Gate-Disziplin im Team

### Beispiel-Prompt
`@openagent Fuehre einen kontrollierten Testlauf fuer Szenario [1-4] durch. Halte dich an die Gates und dokumentiere Abweichungen.`

### Erwarteter Ablauf
1. Context laden
2. Test-/Validierungs-`Plan` erstellen
3. `Approval Status` einholen
4. Testlauf durchfuehren
5. Abweichungen P0/P1/P2 klassifizieren
6. Unkontrollierte Aenderungen vermeiden

### Intern beteiligte Agenten/Subagents
- Primaer: `openagent`
- Typisch: `tester`
- Optional: `reviewer`
- Nicht erwartet: `coder-agent` (ausser nach separater Freigabe)

### Relevante Skills/Commands (optional)
- Command: `test`
- Command: `validate-repo`

### Erwartete Artefakte
- `Context Summary`
- `Plan`
- `Approval Status`
- `Change Summary` (none, falls reiner Testlauf)
- `Validation Report`
- `Review Findings` (optional)
- `Document Output` (none)
- `Document Skip Reason` (Testlauf-only)
- `Open Questions`

### Typische Fehler
- Testlauf ohne vorherige Scope-Definition
- Abweichungen nicht priorisiert
- Nebenbei ungeplante Codeaenderungen

### Justierpunkte (wenn etwas schief laeuft)
- Agent: `tester` Prompt auf reproduzierbare Matrix schaerfen
- Policy/Gate: Approval vor jedem ausfuehrenden Schritt
- Context/Schema: `.opencode/context/core/standards/tests.md`

## Typische Anti-Patterns
- Agent aendert ohne Approval
- Agent laedt keinen Context
- Agent ueberspringt Review
- Agent erweitert Scope
- Agent schreibt Doku ohne Projektverstaendnis
- Agent vermischt mehrere Tasks

## Was Agenten nicht tun sollen
- keine Commits
- keine Aenderungen ohne Approval
- keine Scope-Erweiterung
- keine Aenderungen an `opencode/config.json`
- keine Loeschungen ohne explizite Freigabe

## Git-Regel
- Agenten duerfen keine Commits ausfuehren.
- Agenten duerfen Commit-Messages vorschlagen.
- Commits erfolgen manuell durch Entwickler.
- Ein Commit sollte genau einen abgeschlossenen Task enthalten.

## Abschluss
Diese Workflows sind Startpunkte. Bei Abweichungen wird nicht sofort alles angepasst, sondern beobachtet, klassifiziert und gezielt nachgebessert.
Ziel ist ein kontrollierbares Agentensystem, das ueber Artefakte, Gates und klare Justierpunkte im Teambetrieb verbessert werden kann.
