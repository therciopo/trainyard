# Les Trains

_Read this in other languages: [English](README.en.md)._

## Directives

Vous aurez à fournir une implémentation C# de ITrainsStarter.cs (que vous ne pouvez modifier) qui résout tous les tests d'acceptations (AcceptanceTests.cs).

**La version finale de votre code doit être disponible/pushed 5 heures minimum avant votre rencontre avec l'équipe.**

Vous serez invités à travailler avec des membres de l’équipe de développement et dans un environnement reflétant notre ambiance journalière. Nous travaillerons tous ensemble dans votre code, mais avec de nouvelles contraintes ajoutées à la problématique. Il faut donc essayer d’avoir une solution bien architecturée, compréhensible et qui permettra de facilement diviser les tâches.

## Problématique

Éric est chef de triage dans la gare de triage au Canadian National. Son travail quotidien consiste à organiser, planifier et déplacer des wagons
pour la mise en place de trains.

Une gare de triage consiste en plusieurs « lignes » de wagons et une ligne de train. Prenez ce modèle :

![Alt text](images/cours_de_triage.png?raw=true)

Cette gare de triage a six lignes de wagons. Chaque wagon est représenté par un rectangle et chaque lettre représente une destination
différente.

Le travail du chef de triage est de placer sur la ligne de train l’ensemble des wagons pour une destination sélectionnée, et ce de la manière la
plus efficace possible. Pour ce faire, il utilise une petite locomotive de triage, qui sert à tirer ou pousser des trains entre les différentes lignes.

Voici quelques règles qu’il doit suivre :

- La locomotive de triage peut tirer de 1 à 3 wagons continus.
  - Exemple : Tirer de la ligne #5 les deux wagons A sur la ligne 1
- Chaque ligne de triage peut accommoder un maximum de 10 wagons. Elle peut en avoir moins (espaces libres).
- Le chef de triage ne peut tirer les wagons que de la gauche vers la ligne de train.
- Les wagons mis sur la ligne de train doivent être exclusivement ceux de la destination sélectionnée, et une fois mis sur la ligne de train,
  ils ne peuvent être déplacés.
- Idéalement, les wagons des lignes les plus proches sont déplacés. Il est plus coûteux de déplacer des wagons de la ligne 1 à la ligne 6
  que de la ligne 1 à la ligne 2.
- Idéalement, les wagons sont déplacés en groupes (maximum de 3 par groupe). Il est moins coûteux de déplacer d’un coup 3 wagons
  continus de la ligne 1 à la ligne 2 que de faire trois voyages de 1 wagon entre ces deux lignes.

Éric a besoin d’un logiciel qui lui fournit la liste des mouvements à faire pour préparer sa ligne de train pour une destination particulière.
Idéalement, ce serait la liste de mouvements la plus optimisée, celle qui requiert le moins de mouvements possible.

## Exemple

Exemples de mouvements de wagons avec le modèle ci-dessus :
Destination sélectionnée : G

- Déplacer 1 wagon (D) de la ligne #1 sur la ligne #2
- Déplacer 1 wagon (G) de ligne #1 sur la ligne de train
- Déplacer 2 wagons (C et D) de la ligne #3 sur la ligne #1
- Déplacer 1 wagon (G) de la ligne #3 vers la ligne de train
- Déplacer 1 wagon (D) de la ligne #4 sur la ligne #3
- Déplacer 1 wagon (G) de la ligne #4 vers la ligne de train

Après ces déplacements, nous aurions une gare de triage dans cet état :

![Alt text](images/cours_de_triage_g.png?raw=true)

Évidemment, le travail n’est pas encore terminé (il reste encore des wagons G à mettre sur la ligne de train).
Mais ça vous donne une idée de ce qu’Éric attend comme liste de mouvements de wagons.

## Requis

- Le logiciel devra répondre à l'interface ITrainsStarter.cs et sa méthode Start()
- Le logiciel calculera la meilleure liste de mouvements à effectuer pour préparer le train selon une destination et retourner la série de mouvements que le chef de triage devra effectuer pour remplir la ligne de train.
- Tous les tests d'acceptations doivent réussir (AcceptanceTests.cs).

Sentez-vous libre d'ajouter plus de test qui serait pertinent ou de bons critères d'acceptation.

## Exemples d'entrées et de sorties de l'interface ITrainsStarter.cs

ITrainsStarter a pour but de permettre une interface entre votre code et nos tests unitaires. Voici un exemple simple d'entrées et de sorties pour cette classe. Nous avons utilisés seulement deux lignes de trains pour simplifier le résultat.

- Chaque caractère d'entré correspond à un wagon pour une destination particulière. Les zéros représentent un espace vide.
- Dans la sortie, la ligne 0 représente ici la "Ligne de train".
- Dans les deux exemples ci-dessous, la destination C a été choisie.

En entré (string array) :

```
00000ACDGC
00000000DG
```

En sortie (string) :

```
A,1,2;C,1,0;DG,1,2;C,1,0
```

Voici un exemple plus complexe.

En entré (string array) :

```
00000AGCAG
000DCACGDG
```

En sortie (string) :

```
AG,1,2;C,1,0;AGD,2,1;C,2,0;A,2,1;C,2,0
```

Si la solution est impossible, une string vide devrait être retourné.

## Critères d'évaluation
Le work sample inclura une revue de ton implémentation que tu présenteras à tes collègues au début de la session. Pendant la revue, nous évaluerons les critères suivants:
- Résolution de problèmes
- Compétence technique
- Communication
- Propreté du code
- Collaboration
