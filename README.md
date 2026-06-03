# MediaTek86 — Application de gestion des absences du personnel

Application de bureau développée en **C# / Windows Forms (.NET Framework)** dans le cadre de l'Atelier de Professionnalisation 2 du BTS SIO (option SLAM). Elle permet au responsable du personnel de MediaTek86 de gérer le personnel et leurs absences. L'application reprend la structure et la logique de l'application Habilitations (architecture MVC, classe BddManager, couche dal, modèle métier).

## Contexte et mission

MediaTek86 est un réseau de médiathèques. Le responsable du personnel a besoin d'une application lui permettant de gérer les membres du personnel (ajout, modification, suppression) et de suivre leurs absences. L'accès est protégé par une authentification (login + mot de passe hashé en SHA2 256).

## Fonctionnalités (8 cas d'utilisation)

1. Se connecter
2. Ajouter un personnel
3. Supprimer un personnel
4. Modifier un personnel
5. Afficher les absences d'un personnel
6. Ajouter une absence (avec contrôle du chevauchement)
7. Supprimer une absence
8. Modifier une absence (avec contrôle du chevauchement)

## Technologies

- Langage : C#
- Framework : .NET Framework (Windows Forms)
- IDE : Visual Studio
- SGBD : MySQL (WampServer)
- Connecteur : MySql.Data (NuGet)
- Modélisation : Looping
- Versioning : Git / GitHub

## Architecture MVC

L'application est organisée en packages :
- **bddmanager** : BddManager.cs (classe singleton de connexion)
- **dal** : Access.cs, PersonnelAccess.cs, AbsenceAccess.cs (couche d'accès aux données)
- **modele** : Service, Personnel, Motif, Absence (classes métier)
- **controleur** : FrmConnexionControleur, FrmAccueilControleur
- **vue** : FrmConnexion, FrmAccueil, FrmPersonnel, FrmAbsence

## Modèle de données (MCD)

5 tables : service, personnel, motif, absence (clé primaire composée idpersonnel + datedebut), responsable (login + pwd, une seule ligne).

*(MCD : captures/mcd.png)*

## Installation

1. Démarrer WampServer.
2. Dans phpMyAdmin, exécuter le script `mediatek86.sql` (création des tables, de l'utilisateur d'accès et des données de test).
3. Ouvrir la solution dans Visual Studio, restaurer le paquet NuGet MySql.Data.
4. Vérifier la chaîne de connexion dans App.config.
5. Compiler et lancer (F5). Un installateur (.msi) est disponible dans les Releases.

## Connexion

- Login : **admin**
- Mot de passe : **motdepasse**

## Interfaces
<img width="757" height="366" alt="2026-06-03 21 35 08" src="https://github.com/user-attachments/assets/8a81581a-8213-469b-bc2b-8ce97acce3de" />
<img width="755" height="360" alt="2026-06-03 21 35 03" src="https://github.com/user-attachments/assets/f77e2d7a-218f-4750-9b67-4b9d33afaee9" />
<img width="387" height="333" alt="2026-06-03 21 34 57" src="https://github.com/user-attachments/assets/923b5a35-9bc4-4587-aa12-3e3301b3f775" />
<img width="385" height="330" alt="2026-06-03 21 34 52" src="https://github.com/user-attachments/assets/38512f7c-8a98-403c-9798-cfa43bcc3710" />
<img width="215" height="137" alt="2026-06-03 21 34 40" src="https://github.com/user-attachments/assets/096c8238-69cb-413a-9466-9b936e772808" />
<img width="384" height="233" alt="2026-06-03 21 34 34" src="https://github.com/user-attachments/assets/9cad90bf-50dd-4033-8458-1bff4a6a1b3e" />
<img width="784" height="408" alt="2026-06-03 21 34 17" src="https://github.com/user-attachments/assets/1022a4c6-3b87-49e5-9642-2781dd867a84" />



## Diagramme de paquetages


## Étapes de construction (commits)

Le projet a été construit progressivement, chaque étape étant sauvegardée par un commit explicite sur le dépôt distant.

| Commit  | Description | Étape |
|---------|-------------|-------|
| da5a4a4 | Ajout des fichiers .gitattributes et .gitignore | Initialisation |
| 84a25a8 | Ajout des fichiers du projet (structure MVC) | Étape 2 |
| 0fc5924 | Modèle métier (classes Service, Personnel, Motif, Absence) | Étape 3 |
| b077140 | Ajout de la couche dal et du contrôleur | Étape 3 |
| bd3ddf4 | Ajout des fenêtres Windows Forms (partie Vue) | Étape 2 |
| 9762f56 | Ajout du fichier App.config (chaîne de connexion) | Étape 3 |
| 4f2c2f9 | Fonctionnalités opérationnelles (CU1 à CU8) et connexion BDD | Étape 4 |
| 811b253 | Création de l'installateur (Setup Project) | Étape 5 |
| c22f629 | Génération de la documentation technique | Étape 3 |
| 1d15735 | Création du script SQL complet (mediatek86.sql) | Étape 5 |
| f613616 | Ajout des fichiers (documentation, ressources) | Étape 5 |
| cd22d1a | Rédaction du README | Étape 5 |
| 9d29e1c | Mise à jour du README | Étape 5 |

> Le développement a suivi les étapes de l'atelier : préparation de la base de données, structuration MVC et codage de la Vue, codage du modèle et de la couche d'accès, implémentation des 8 cas d'utilisation, puis déploiement (installateur, documentation, README).

## Liens

- Compte rendu d'activité (PDF) : https://github.com/petrivskyyy/MediaTek86/blob/master/Compte_Rendu_MediaTek86.pdf)

## Auteur

Étudiant Maksym PETRIV BTS SIO (SLAM) — Atelier de Professionnalisation 2.
