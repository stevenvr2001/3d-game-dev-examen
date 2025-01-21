# Design Document

## Pagina 1: Gameplay Mechanics

### Titel

**Escape the Grind**

### Concept Pitch (1-3 zinnen)

Speel als een uitgebluste kantoormedewerker die dagelijks probeert te ontsnappen uit een absurd kantoor vol excentrieke collega’s. Met behulp van stealth, power-ups, en slimme planning baan je een weg naar vrijheid – totdat de volgende dag begint, en je opnieuw moet ontsnappen!

### Genre

3D Adventure / Satirische Stealth Game

### Doelpubliek

Gamers die houden van humor en stealth mechanics in een herkenbare maar absurde setting.

### Game Flow samenvatting

- Ontwijken van obstakels en de boss.
- Verzamel power-ups zoals koffie om energie te herstellen.
- Gebruik stealth om niet gezien te worden.
- Voltooi missies en ontsnaproutes binnen een bepaald tijdslimiet.

### Basic Look en Feel

De game heeft een semi-realistische 3D-stijl die functioneel en herkenbaar is, maar met subtiele humoristische accenten om de absurditeit van de setting te benadrukken:

- **Omgevingen**: Open kantoren met alledaagse meubels zoals bureaus, stoelen, en printers, maar met kleine grappige details, zoals een collega die presentaties geven. De ruimtes voelen bekend aan, maar hebben een licht chaotische indeling die de gameplay ondersteunt.
- **Personages**: Collega's hebben subtiele humor in hun animaties, zoals iemand die gek wordt achter het computerscherm. Ze zijn aanwezig in de wereld, maar bieden geen actieve interactie.
- **Kleuren en texturen**: Een neutraal kleurenpalet dat typisch is voor een kantooromgeving, met een mix van grijstinten, natuurlijke houtkleuren, en marmer voor meubels. Kleuren van objecten zoals power-ups of obstakels vallen net genoeg op om functioneel te zijn zonder te schreeuwerig over te komen.
- **Interface**: Simpel en duidelijk, zoals een energiesbar die langzaam leegloopt. Het design blijft clean, zodat de focus op de gameplay ligt.

Deze aanpak biedt een toegankelijk en functioneel design dat past bij de humor en stealth-mechanics.

### Gameplay

- **Uitdagingen/Puzzelstructuur**: Gebruik stealth en omgevingsbewustzijn om obstakels te vermijden. Vind creatieve routes en verzamel sleutels of power-ups.
- **Doelen**: Ontsnap binnen het tijdslimiet zonder gepakt te worden door de toezichthouders op de werkvloer.
- **Play Flow**: Spelers bewegen door open kantoren, verstoppen zich achter bureaus, en lossen puzzels op om de uitgang te vinden.

### Mechanics

- **Spelregels**: Vermijd detectie door toezichthouder en ontwijk obstakels. Je energie kan uitgeput raken zonder de nodige cafeineboost, dit kan leiden tot een trage walking speed.
- **Physics/Actions**: Gebruik stealthmechanics, sluipbewegingen, en power-ups om je cafeineboost weer op te trekken.
- **Economie**: Verzamel koffie of energiedrankjes om je voortgang te versnellen. Elke power-up heeft een beperkte duur of aantal.

---

## Pagina 2: Verhaal / Immersive Design

### Verhaal/Setting

Je speelt als een uitgebluste kantoormedewerker in een absurde werkplek waar de banaliteit van het kantoorleven is uitvergroot tot een surrealistisch spel. Het kantoor is gevuld met, overenthousiaste managers, en een hinderlijke layout die je tijd verspillen.

- **Setting**: Open kantoren met managers die je proberen tegen te houden, en collegas die proberen door de dag heen te komen.
- **Spelwereld**: Elke level introduceert nieuwe routes, obstakels, en interacties.
- **Personages**:
  - De Protagonist: Jij, een anonieme werknemer.
  - Collega's: Excentrieke karikaturen.

### Game Art/Audio

- **Art Style**: semi-realistische 3D-artstijl, met grappige elementen.
- **Audio Style**: Humoristische sound effects voor de managers en een satirische achtergrondmuziek geïnspireerd op elevator music.
- **Asset List**:
  - Animaties: animaties voor de collegas.
  - Modellen: Kantoorobjecten, power-ups.
  - Dev-Tools: Unity, Blender, Audacity.

### User Interface

Een minimalistische HUD die laat zien:

- Energielevel van de speler.
- Detectie-cones voor stealth.
- Tijdslimiet voor de ontsnapping.

---

## Pagina 3: Level Ideas

### Levels

1. **Monday Blues**:
   - Een klein dim verlicht kantoor met overwerkte collega's die proberen door de dag heen te komen.
   - Obstakels: bureaustoelen.
   - Power-ups: Gebruik power-ups om je energie terug te krijgen.
2. **Meeting Madness**:
   - Overwerkte collega's die proberen door de dag heen te komen.
   - Een doolhof van kantorruimtes met managers die je proberen vast te houden voor een vergadering.
   - Power-ups: Gebruik power-ups om je energie terug te krijgen.
3. **Microdose Gone Wrong**:
   - Een surrealistische, hallucinogene werkdag, waar de receptioniste verandert is in je idool.
   - Obstakels: verschillende managers die zich vragen stellen bij je huidige mentale staat.
   - Power-ups: Gebruik power-ups om je energie terug te krijgen.

# Design Document

## Pagina 4: Tech

### Artificial Intelligence (AI)

- **Tegenstanders/Vijanden:**
  - Managers en toezichthouders hebben een patrol-AI met detectieconen. Als de speler binnen het zichtveld komt, mislukt de ontsnapping.
- **NPC’s:**
  - Collega’s bewegen willekeurig en kunnen obstakels vormen.
  - Zorgen ervoor dat het kantoor populated is.

### Technical

- **Gamefuncties:**
  - Stealth-mechanieken: zichtcones, en een schuilsysteem.
  - Tijdslimiet-systeem.
  - Power-up systeem: tijdelijke boosts voor snelheid.
- **Game-engine:**
  - Unity 2022.3.50f1.
- **Technologie:**
  - AI: NavMesh voor pathfinding.
  - Tools: Blender voor modellen en Audacity voor audio.

---

## Pagina 5-6: Schema

### Backlog

**Feature-list (met prioriteiten):**

1. **Essentiële Features:**
   - Stealth-mechanieken: zichtconen.
   - Energie-/Power-up-systeem.
   - Tijdslimiet en ontsnapping-mechaniek.
   - Basis-AI voor vijanden (patrol).
   - Basismodel voor kantooromgeving.
2. **Nice-to-Have Features:**
   - Verschillende routes en eindes.
   - grappige NPCs.

### Milestones & Production Timeline

**6-wekenplan:**

- **Week 1:**
  - Project opzetten in Unity.
  - Basisomgeving maken en importeren (een simpel kantoormodel).
  - NavMesh instellen en een eenvoudige patrol-AI implementeren.
- **Week 2:**
  - Implementeren van zichtcones.
  - Basis-stealthmechanieken testen.
- **Week 3:**
  - Energie- en power-up-systeem implementeren.
  - Eerste prototype van "Monday Blues" level maken.
  - Testen van basale gameplay loops.
- **Week 4:**
  - AI uitbreiden (trigger-gebeurtenissen).
  - Extra obstakels en gameplay-elementen toevoegen.
  - Level "Meeting Madness" opzetten.
- **Week 5:**
  - Spelervaring polijsten: animaties, audio, en visuele effecten.
  - Surrealistische effecten voor "Microdose Gone Wrong" ontwikkelen.
  - Testbare WebGL-build maken.
- **Week 6:**
  - Final testing en bugfixes.
  - Optimalisaties voor WebGL (textures, shaders).
  - Definitieve build en presentatie voorbereiden.

---
