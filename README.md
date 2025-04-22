# 🧠 Chifoumi-AI

Un projet de jeu interactif basé sur le principe de Pierre-Papier-Ciseaux, combiné à des formes géométriques (cercle, triangle, rectangle) et une intelligence artificielle adaptative. Développé en C# (.NET) avec un front-end React et une base de données PostgreSQL.

---

## 🚀 Fonctionnalités principales

### ✅ MVP
- Jouer contre une intelligence artificielle.
- Choix du nom, nombre de manches, et niveau de difficulté.
- L’IA joue automatiquement après le choix du joueur.
- Résumé final après toutes les manches.

### ✨ MVP+
- Système de score (victoires, défaites, égalités).
- Niveaux de difficulté : Facile, Moyen, Difficile.
- IA adaptative basée sur les 10 dernières parties du joueur.

---

## 🧰 Technologies utilisées
- **Back-end :** ASP.NET Core (C#), Entity Framework
- **Front-end :** React (Vite)
- **Base de données :** PostgreSQL
- **ORM :** Entity Framework Core
- **Langage principal :** C#

---

## 🧱 Architecture orientée objet
- Classe abstraite `Form` avec héritage pour : `Rond`, `Triangle`, `Rectangle`
- Classe `ShapeManager` pour la composition et gestion des formes.
- Utilisation des `enum` pour stocker les formes dans la BDD.
- Utilisation de la POO pour favoriser l’extensibilité et la clarté du code.

---

## 📡 Documentation de l'API

L'API permet d'interagir avec le jeu de manière structurée via des requêtes HTTP.

### 1. 🔄 Démarrer une session de jeu

**Endpoint :** `POST /api/Game/start-session`  
**Description :** Démarre une nouvelle session pour un joueur donné.

#### 🧾 Paramètres JSON :
```json
{
  "PlayerName": "Jean",
  "TotalRounds": 3
}
```

#### ✅ Réponse :
```json
{
  "Status": "Success",
  "Message": "Game session started!",
  "SessionId": 123
}
```

---

### 2. 🎮 Jouer une manche

**Endpoint :** `POST /api/Game/play`  
**Description :** Permet au joueur de choisir une forme et jouer une manche.

#### 🧾 Paramètres JSON :
```json
{
  "PlayerShape": "Triangle",
  "GameSessionId": 123
}
```

#### ✅ Réponse :
```json
{
  "PlayerName": "Jean",
  "PlayerShape": "Triangle",
  "ComputerShape": "Rectangle",
  "Result": "Player wins with Triangle!"
}
```

---

### 3. 📜 Récupérer l'historique des parties

**Endpoint :** `GET /api/Game/history`  
**Description :** Affiche l'historique complet des manches jouées.

#### ✅ Réponse :
```json
[
  {
    "GameSessionId": 123,
    "PlayerId": 1,
    "PlayerShape": "Triangle",
    "ComputerShape": "Rectangle",
    "Result": "Player wins with Triangle!",
    "PlayedAt": "2024-03-21T14:30:00Z"
  }
]
```

---

### ❌ Gestion des erreurs

| Code | Message |
|------|---------|
| 400  | "Invalid input. Please provide a valid player name and number of rounds." |
| 400  | "Game session could not be found." |
| 400  | "You have reached the maximum number of rounds for this session." |
| 400  | "Invalid input. Please provide a valid shape and session ID." |

---

## 📷 Aperçu du projet


---

## 🛠️ Améliorations futures
- Ajouter plus de formes géométriques.
- Ajouter une version mobile (MAUI / React Native).
- Exporter les statistiques du joueur.
- Ajouter un mode multijoueur local.

---

## 📄 Licence

Ce projet est open-source. N'hésitez pas à le forker, l'améliorer et le partager !

