# Console RPG

A C# console RPG developed as a university project, focused on object-oriented design principles and the use of design patterns. 

The game features procedurally generated maps, containing items, equipment and enemies that the player can fight. Player's action are displayed in a log. It also supports multiplayer gameplay through a client-server architecture.

The project was developed for Object Oriented Design course during March-June 2025 at Warsaw University of Technology

## Features
- Procedurally generated maps
- Items, weapons and consumable elixirs
- Effects on weapons modifying theirs stats
- Combat with enemies
- In-game currency
- Multiplayer client-server architecture

## Design
The project was designed with modularity and extensibility in mind. New requirements were introduced during development, so the architecture of the systems had to accommodate for the possible changes and additions without major changes to existing code.

Several design patterns were used in different parts of the system, including:
- **Decorator** - allowed adding effects to items like weapons without modifying original classes
- **Builder** - procedurally generating the map while allowing to easily modify the map parameters
- **Visitor** - for handling damage calculation in combat
- **Strategy** - for different behaviors of enemies
- **Chain of responsibility** - handling key inputs
- **MVC** - separating game logic from presentation 

## Technologies
- C#
- .NET
- Client-Server architecture

## Running

TODO

## AI disclosure

GitHub Copilot was used for full-line code completion. Code generated in that way was reviewed by me before integration into codebase. The architecture of the system, design pattern usages and implementation approach were developed fully by me, unless they were forced by task description.
