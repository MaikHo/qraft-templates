# OpenCode Benutzerdokumentation (Projektlokal)

Diese Doku beschreibt, wie du die OpenCode-Vorlage aus diesem Repository in **dein eigenes Projekt** uebernimmst und komplett **im Projektordner** konfigurierst.

Stand: 2026-04-26

## 1. Was ist in `opencode/` enthalten?

Der Ordner [`opencode`](../opencode) ist die Template-Quelle. Laut [`opencode/manifest.json`](../opencode/manifest.json) ist das Zielverzeichnis im Projekt `.opencode`.

Die wichtigsten Bausteine:

- `agents/`: eigene Primary- und Subagents (z. B. `openagent`, `opencoder`, `frontend-specialist`)
- `commands/`: Slash-Commands wie `/test`, `/lint`, `/commit`, `/tasks`
- `skills/`: wiederverwendbare Skill-Pakete (`SKILL.md`)
- `context/`: Standards, Workflows, Projektkontext
- `tools/`: Platz fuer Custom Tools
- `utilities/tool-utils/`: .NET Hilfscode (Env-Loader)
- `config.json`: Template-Permissions (als Basis fuer Projektkonfig)

## 2. In dein Projekt kopieren

### Variante A (empfohlen, sauber): Inhalte nach `.opencode` kopieren

Im Zielprojekt (dein echtes Arbeitsrepo) ausfuehren:

```bash
mkdir -p .opencode
cp -R /Users/mhoffmann/github/qraft-templates/opencode/. ./.opencode/
```

Danach liegt alles unter:

- `.opencode/agents/...`
- `.opencode/commands/...`
- `.opencode/skills/...`
- `.opencode/context/...`

## 3. Projekt-Konfigdatei anlegen (wichtig)

Lege im **Projektroot** eine Datei `opencode.json` an. Das ist laut offizieller Doku die zentrale Projektkonfig.

Beispiel (aktuelle Konfiguration, empfohlen):

```json
{
  "$schema": "https://opencode.ai/config.json",
  "provider": {
    "lmstudio": {
      "npm": "@ai-sdk/openai-compatible",
      "name": "lmstudio (local)",
      "options": {
        "baseURL": "http://localhost:1234/v1"
      },
      "models": {
        "qwen/qwen3.5-9b": {}
      }
    }
  },
  "model": "qwen/qwen3.5-9b",
  "permission": {
    "edit": "ask",
    "bash": "ask",
    "write": "ask",
    "read": "allow",
    "grep": "allow",
    "glob": "allow",
    "list": "allow",
    "skill": "allow",
    "todowrite": "allow",
    "todoread": "allow",
    "task": "ask",
    "webfetch": "ask",
    "websearch": "ask",
    "question": "allow"
  }
}
```

Hinweis:

- Die Template-Datei `.opencode/config.json` kannst du als Referenz behalten.
- Fuer aktuelle OpenCode-Konfiguration ist `opencode.json` im Projektroot der robuste Standard.

Wichtig (Verständnis):

- `opencode.json` im Projektroot ist die zentrale Runtime-Konfiguration
- `.opencode/config.json` ist eine Template-Vorlage und wird typischerweise in die `opencode.json` kopiert
- Es existieren NICHT zwei gleichwertige Config-Dateien – die Root-Datei ist entscheidend

Empfohlener Workflow:

```bash
cp .opencode/config.json opencode.json
```

Danach kannst du `opencode.json` projektspezifisch anpassen (z. B. Modell wechseln, Permissions feinjustieren).

## 4. OpenCode installieren und Provider verbinden

### 4.1 OpenCode installieren

Zum Beispiel:

```bash
npm install -g opencode-ai
# oder
brew install anomalyco/tap/opencode
```

### 4.2 API-Key/Provider verbinden

Im OpenCode-TUI:

```text
/connect
```

Danach Modell waehlen (z. B. per `/models`) und in `opencode.json` auf Projektebene festlegen.

## 5. Projekt initialisieren

Im Projektroot:

```bash
opencode
```

Dann im TUI:

```text
/init
```

`/init` erzeugt eine `AGENTS.md` im Projektroot (falls noch nicht vorhanden). Diese Datei solltest du ins Git einchecken.

## 6. Was der User im Projektordner einrichten muss

Alles lokal im Projekt:

1. `.opencode/` Struktur (Agents/Commands/Skills/Context) vorhanden
2. `opencode.json` im Projektroot angelegt
3. optional `tui.json` im Projektroot fuer projektspezifische UI/Keybinds
4. `.env` im Projektroot fuer Tokens/Secrets (z. B. `SUPABASE_ACCESS_TOKEN` fuer Skill `supabase-logs`)

### 6.1 .env fuer Skills/Tools

Beispiel:

```env
SUPABASE_ACCESS_TOKEN=sbp_xxx
```

Der enthaltene EnvLoader sucht standardmaessig nach `.env` und Parent-Ebenen (siehe [`EnvLoader.cs`](../opencode/utilities/tool-utils/env/EnvLoader.cs)).

### 6.2 .NET Utilities vorbereiten

Wenn du die .NET-basierten Utilities/Skills nutzen willst:

```bash
cd .opencode
dotnet restore
cd utilities/tool-utils
dotnet build
dotnet test
```

## 7. Arbeiten mit Agents

### 7.0 Wie dieses Template wirklich funktioniert (wichtig)

Dieses Template nutzt ein orchestriertes Multi-Agent-System.

Primary Agents (z. B. `openagent`, `opencoder`) sind über `0-category.json` mit Subagents und Kontext verbunden.

Das bedeutet:

- Subagents werden automatisch geladen
- Kontext (Standards, Workflows, Patterns) wird automatisch bereitgestellt
- Workflows laufen über Delegation zwischen Agents

Wichtig:

Du musst in der Regel NICHT alle Subagents manuell aufrufen.
Ein Primary Agent (z. B. `@openagent`) übernimmt die Orchestrierung.

In dieser Vorlage sind custom Agents unter `.opencode/agents` definiert.

### 7.1 Primary Agents

- `openagent`: universeller Orchestrator
- `opencoder`: coding-fokussiert
- weitere Spezialisierungen unter `agents/development`

### 7.2 Subagents

Unter `agents/subagents` z. B.:

- `core/task-manager`
- `core/documentation`
- `code/coder-agent`
- `code/tester`
- `code/reviewer`
- `code/build-agent`

### 7.3 Ablauf (typisch)

1. Du startest einen Primary Agent (z. B. `@openagent`)
2. Der Agent lädt automatisch Kontext aus `.opencode/context/...`
3. Der Agent entscheidet anhand der Aufgabe, welche Subagents benötigt werden
4. Subagents werden sequenziell oder kombiniert ausgeführt (z. B. Task → Code → Test → Review)
5. Der Primary Agent aggregiert die Ergebnisse und gibt das finale Resultat zurück

### 7.4 Agent-Auswahl (wichtig für die Nutzung)

- Ohne `@agent` wird der aktuell aktive Primary Agent verwendet
- Mit `@openagent` oder `@opencoder` wählst du den Agent explizit
- Subagents können direkt per `@tester`, `@reviewer` usw. aufgerufen werden

Empfehlung:

- Komplexe Aufgaben: `@openagent`
- Reine Implementierung: `@opencoder`
- Einzelne Prüfungen: Subagents direkt aufrufen

### 7.5 Automatische Orchestrierung (OpenAgent)

Bei Nutzung von `@openagent` läuft typischerweise automatisch:

1. Analyse der Aufgabe
2. Zerlegung in Tasks (task-manager)
3. Implementierung (coder-agent)
4. Test-Erstellung (tester)
5. Code-Review (reviewer)
6. Build-/Validierung (build-agent)

Dieser Ablauf ist durch die Agent-Komposition (`0-category.json`) definiert.

### 7.6 MVP-Kernworkflow (verbindlich)

Fuer den stabilen Kernbetrieb gilt:

`Context -> Plan -> Approval -> Implement -> Validate -> Review -> Document`

Erwartete Outputs pro Gate:

1. Context: Context Summary (relevante Standards, Dateien, Annahmen)
2. Plan: Approval-faehiger Plan (Steps, Risiken, Exit Criteria)
3. Approval: Expliziter Freigabestatus vor Umsetzung
4. Implement: Change Summary (welche Dateien/Verhalten geaendert wurden)
5. Validate: Validation Report (Build/Test/Checks mit Ergebnis)
6. Review: Severity-basierte Findings inkl. Empfehlungen
7. Document: Projektnahe Zusammenfassung + offene Fragen + naechste Schritte

Governance (verbindlich):

- Review ist Pflicht bei:
  - Produktiven Code-Aenderungen
  - Build-/Runtime-relevanten Konfigurationsaenderungen
- Test-Aenderungen: Review empfohlen, je Kontext verpflichtend
- Review ist nicht verpflichtend bei:
  - Reinen Doku-Aenderungen
  - Reiner Analyse ohne Aenderungen
  - Klar gekennzeichneten generierten Artefakten
- Document ist Pflicht, wenn README, Dokumentation, Architektur, Workflow oder oeffentlich sichtbare Projektstruktur betroffen ist
- Document ist optional bei reinen internen Code-Fixes ohne Doku-Relevanz
- Wenn Document uebersprungen wird, muss der Grund im Abschlussbericht stehen

Context-Regel:

- Read-only Discovery (read/list/grep/glob oder nicht-mutierende bash-Discovery) ist ohne vollstaendigen Context erlaubt
- Sobald bash fuer Aenderung, Validierung, Build/Test oder projektbezogene Entscheidung genutzt wird, gilt Context-Pflicht
- write/edit/task bleiben immer contextpflichtig
- Keine Ausfuehrung ohne Approval, ausser reine lesende Discovery

### 7.7 Kontext-System

Der Kontext unter `.opencode/context` wird automatisch geladen.

Beispiele:

- `core/standards/code.md` → Coding-Regeln
- `core/standards/tests.md` → Testkonventionen
- `core/workflows/review.md` → Review-Prozess

Der Agent nutzt diesen Kontext aktiv zur Entscheidungsfindung und Umsetzung.

### 7.8 Context-Bundle-Standard (fuer Delegation)

Wenn `openagent` Kontext an Subagents uebergibt (`.tmp/context/{session-id}/bundle.md`), gilt das kanonische Schema:

- `.opencode/context/core/system/context-bundle-schema.md`

Ziel:

- Konsistenter Kontextfluss zwischen `openagent`, `task-manager`, `coder-agent` und `reviewer`
- Vorhersagbare, reproduzierbare Agent-Antworten

## 8. Arbeiten mit Commands

Die Markdown-Dateien in `.opencode/commands/*.md` erzeugen Slash-Commands:

- `/test`
- `/lint`
- `/commit`
- `/tasks`
- `/clarify`
- usw.

### 8.1 Aufruf

Einfach im Chat:

```text
/test
```

oder mit Argumenten:

```text
/clarify docs/prd-feature.md
```

### 8.2 Prompt-Mechanik in Commands

Unterstuetzt werden u. a.:

- `$ARGUMENTS`
- `$1`, `$2`, ...
- `!\`bash-command\`` (Shell-Output im Prompt)
- `@datei/pfad` (Dateiinhalt in Prompt)

## 9. Arbeiten mit Skills

Skills liegen in `.opencode/skills/<name>/SKILL.md`.

In dieser Vorlage z. B.:

- `frontend-design`
- `readme`
- `supabase-logs`
- `supabase-postgres-best-practices`

Ablauf:

1. Skill-Datei wird von OpenCode entdeckt
2. Agent sieht Skill-Name + Beschreibung
3. Agent laedt Skill-Inhalt on-demand ueber Skill-Tool
4. Agent fuehrt Workflow aus `SKILL.md` aus

Wichtig:

- `SKILL.md` braucht YAML-Frontmatter mit mindestens `name` und `description`
- Skill-Name sollte Verzeichnisnamen entsprechen

## 10. Arbeiten mit Tools und Permissions

OpenCode-Tools (z. B. `read`, `edit`, `write`, `bash`, `task`, `webfetch`, `skill`) werden ueber `permission` gesteuert:

- `allow`: direkt ausfuehren
- `ask`: Rueckfrage/Approval
- `deny`: blockiert

Empfehlung fuer produktive Repos:

- global konservativ (`* = ask`)
- fein granular je Tool/Command
- riskante Bash-Muster (`rm *`, `sudo *`) auf `deny`

## 11. MCP, Provider, weitere Integrationen (optional)

Alles projektlokal in `opencode.json` moeglich:

- `mcp`: MCP-Server einbinden
- `provider`: Provider-spezifische Optionen
- `model`: Standardmodell pro Projekt
- `agent`: Agent-Overrides pro Projekt
- `command`: Command-Definitionen auch per JSON moeglich

## 12. Empfohlener End-to-End Startablauf

Im Zielprojekt:

```bash
# 1) Template kopieren
mkdir -p .opencode
cp -R /Users/mhoffmann/github/qraft-templates/opencode/. ./.opencode/

# 2) Root-Config anlegen
cp .opencode/config.json opencode.json

# 3) OpenCode starten
opencode
# im TUI:
# /connect
# /models
# /init
```

Danach direkt nutzen:

- Architektur/Kontext verstehen: nutze z. B. `/context`
- Feature planen: Plan-Agent oder `openagent`
- Implementieren: `opencoder` bzw. spezialisierte Agents
- Testen/Validieren: `/test`, `/lint`, `/validate-repo`

## 13. Troubleshooting

### OpenCode findet Commands/Agents/Skills nicht

Pruefen:

- liegt alles unter `.opencode/...` im Projekt?
- heissen Skills exakt `SKILL.md`?
- ist `opencode.json` im Projektroot?
- startest du OpenCode im richtigen Projektordner?

### Provider verbunden, aber kein Modell verfuegbar

- `/connect` erneut ausfuehren
- `/models` oeffnen
- `model` in `opencode.json` setzen

### Supabase-Logs-Skill funktioniert nicht

- `SUPABASE_ACCESS_TOKEN` in `.env`
- falls mehrere Projekte: `--project <ref>` mitgeben
- Zeitfenster vergroessern (`--last 2h`, `--last 24h`)

## 14. Denkmodell: Verhalten von Agentensystemen

### Erwartung vs. Realitaet

Agentensysteme verhalten sich nicht deterministisch.
Selbst bei identischem Input kann das System unterschiedliche Entscheidungen treffen.

Das ist keine Fehlfunktion, sondern eine grundlegende Eigenschaft von LLM-basierten Systemen:

- Ergebnisse sind probabilistisch, nicht fest vorgegeben.
- Mehrere Agenten erzeugen emergentes Verhalten.
- Interaktionen zwischen Komponenten fuehren zu Variabilitaet.

### Zieldefinition (wichtig)

Ziel ist nicht:

„Das System funktioniert immer exakt gleich.“

Ziel ist:

„Das System ist kontrollierbar, nachvollziehbar und debugbar.“

### Erfolgsindikatoren

Ein funktionierendes Agentensystem erkennt man daran:

1. Entscheidungen sind erklaerbar.
2. Verhalten ist weitgehend reproduzierbar.
3. Abweichungen sind analysierbar.
4. Eingriffe fuehren zu vorhersehbaren Aenderungen.

### Praktische Konsequenzen

- Regeln (Gates, Policies) sind Leitplanken, keine Garantie.
- Fehler sind erwartbar und Teil des Systems.
- Stabilitaet entsteht durch Iteration, nicht durch initial perfektes Design.
- Kontrolle ist wichtiger als Autonomie.

### Empfohlene Arbeitsweise

1. Kleine Use Cases testen.
2. Abweichungen beobachten.
3. Ursachen analysieren.
4. Genau eine Regel anpassen.
5. Erneut testen.

### Kerngedanke

Ein gutes Agentensystem ist nicht perfekt, sondern verstaendlich, steuerbar und iterativ verbesserbar.

## 15. Quellen

### Repository-intern

- [`opencode/manifest.json`](../opencode/manifest.json)
- [`opencode/config.json`](../opencode/config.json)
- [`opencode/agents`](../opencode/agents)
- [`opencode/commands`](../opencode/commands)
- [`opencode/skills`](../opencode/skills)
- [`opencode/context`](../opencode/context)
- [`EnvLoader.cs`](../opencode/utilities/tool-utils/env/EnvLoader.cs)

### Web-Doku (OpenCode)

- [Intro](https://opencode.ai/en/docs)
- [Config](https://opencode.ai/docs/config/)
- [Agents](https://opencode.ai/docs/agents/)
- [Commands](https://opencode.ai/docs/commands/)
- [Skills](https://opencode.ai/docs/skills)
- [Tools](https://opencode.ai/docs/tools/)
- [Permissions](https://opencode.ai/docs/permissions/)
- [Providers](https://opencode.ai/docs/providers/)
