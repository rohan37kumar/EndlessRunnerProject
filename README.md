# Endless Runner Game

A simple endless runner game built in Unity where players collect coins while avoiding obstacles. The game features a coin-based economy system and persistent high scores.

## Core Scripts Overview

### GameManager
Handles the core game logic including:
- Game state management
- Score tracking and high score system
- In-game economy (coins system)
- Player state management

### PlayerController
Controls the player character with features like:
- Lane-based movement system (3 lanes)
- Touch/mouse input handling for lane switching
- Collision detection with obstacles and coins
- Animation state management

### UIManager
Manages all UI elements including:
- Score display
- Game over panel
- Coin balance
- High score tracking
- Scene transitions

### CoinRotator
A simple script that:
- Rotates coin objects in the game
- Handles coin collection when player collides
- Triggers score increment

## Game Features
- Three-lane movement system
- Coin collection and persistent coin balance
- Entry fee (5 coins) for each run
- High score tracking
- Simple and intuitive touch/mouse controls
- Game over system with score display
