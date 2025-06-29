# 🕹️ Contribuer au projet de la Game Jam

Merci de contribuer ! Voici quelques règles simples pour garder le code propre et éviter les galères pendant la jam.

---

## 🧠 Branches

Ne travaille **jamais directement sur `main`**.

### Convention de nommage :
- `feature/nom-de-la-feature` : ajout d'une nouvelle fonctionnalité  
- `fix/nom-du-bug` : correction d’un bug  
- `refactor/nom-du-code` : amélioration sans ajout de fonctionnalité  
- `hotfix/urgent-truc` : fix critique à merger rapidement  

Exemples :
```fix
feature/systeme-vie
fix/menu-clignote
refactor/input-system
```
---

## ✅ Pull Requests (PR)

### Pour chaque contribution :
- Crée une branche propre avec un nom explicite
- Ouvre une Pull Request vers `Dev`
- Ajoute un titre et une description clairs
- Mentionne dans la PR si tu veux une **review rapide** ou si c'est **encore en WIP**
- Pas de merge tant qu’un autre membre n’a pas **approuvé**

> 🛑 AUCUN `git push` direct sur `main` ! (protégé)

---

## 💬 Messages de commit

Les commits doivent être **clairs** et **concernés** :

✅ Correct :
```
fix: empêche la vie de descendre sous 0
feature: ajoute barre de mana
refactor: simplifie le code de déplacement
```
❌ Pas correct :
```
update
test
changement
```
---

## 📁 Fichiers à ne pas committer

Merci de ne **pas pousser ces fichiers** :
- Tous les fichiers dans `Library/`, `Temp/`, `Build/`, `.vs/`
- Les `.db` ou fichiers de sauvegarde
- Les fichiers temporaires personnels

> ✅ Le `.gitignore` est déjà configuré pour ça. Ne le modifie pas sans en parler.
